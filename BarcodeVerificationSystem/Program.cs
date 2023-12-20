using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using BarcodeVerificationSystem.View;
using CommonVariable;
using EncrytionFile.Model;
using OperationLog.Controller;
using OperationLog.Model;
using Security;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem
{
    static class Program
    {
        #region Variables
        private static USBKey _USBKey = new USBKey();
        private static uint _HardwareIDUsing = 0;
        private static frmWarningUSBDongleKey frmWarningKey = null;
        #endregion
        [STAThread]
        static void Main()
        {
            if (AnotherInstanceExists())//Check application already running
            {
                MessageBox.Show(Lang.ApplicationIsAlreadyRunning,Lang.Info,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            Shared.LoadSettings();
            Lang.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(Shared.Settings.Language);
            // Set language default
            //Lang.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");

            
            frmSplashScreen.ShowSplashScreen(Lang.Loading, Lang.PleaseWait); //Show splash screen
            string idOfPC = FingerPrint.Value(); //Get UUID of computer run this software
            bool isAllow = true;
#if DEBUG
             isAllow = true; // bypass usb key in debug mode
#endif
            string path1 = CommVariables.PathAllowPC + "RConfig.dat";
            if (!Directory.Exists(CommVariables.PathAllowPC))
            {
                Directory.CreateDirectory(CommVariables.PathAllowPC);
            }
            DecryptionHardwareID.DecryptFile(path1);
            foreach (HardwareIDModel id in Shared.listPCAllow)
            {
                string idOfList = SecurityController.Decrypt(id.HardwareID,DecryptionHardwareID._EncyptionPassword);
                if (idOfList.Equals(idOfPC))
                {
                    isAllow = true;
                    break;
                }
            }

            Shared.listPCAllow = null;  

            if (!isAllow)
            {
                InitVariableUSBDongle();
                if (CheckForValidUSBDongleKey() == false)
                {
                    ShowDialogUSBDongleKeyNotFound();
                    return;
                }
                Thread threadCheckUSBDongle = new Thread(() => CheckUSBDongleWhenRunning())
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Lowest
                };
                threadCheckUSBDongle.Start();
            }

            Application.EnableVisualStyles();
            LoggingController.LoginToAccess("_rynan_loggin_access_control_management_");
            string path = CommVariables.PathAccountsApp;
            if (!File.Exists(path + "AccountDB.db"))
            {
                UserController.CreateDefaultDatabase();
            }
            frmLoginNew loginform = new frmLoginNew
            {
                TopMost = true
            };
            frmSplashScreen.CloseSplash();
            DialogResult result = loginform.ShowDialog();
            if (result == DialogResult.OK)
            {
                UserController.LogedInUsername = "Administrator";
                AppDomain currentDomain = default;
                currentDomain = AppDomain.CurrentDomain;
                currentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
                Application.ThreadException += GlobalThreadExceptionHandler;
                FrmJob frmJob = new FrmJob();
                Application.Run(frmJob);
            }
        }

        public static bool AnotherInstanceExists()
        {
            Process currentRunningProcess = Process.GetCurrentProcess();
            Process[] listOfProcs = Process.GetProcessesByName(currentRunningProcess.ProcessName);
            foreach (Process proc in listOfProcs)
            {
                if ((proc.MainModule.FileName == currentRunningProcess.MainModule.FileName) && (proc.Id != currentRunningProcess.Id))
                {
                    return true;
                }
            }
            return false;
        }

        // Reffernce: https://stackoverflow.com/questions/10202987/in-c-sharp-how-to-collect-stack-trace-of-program-crash
        // Catching an exception
        private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            var result = "";
            if (ex.InnerException != null)
            {
                string innerExection = "InnerException: " + ex.InnerException.InnerException + " && ";
                result += innerExection;
            }
            if (ex.Message != null)
            {
                string errorMessage = ex.Message.Replace("\r", "").Replace("\n", "").Replace(',', '&');
                result += "Message: " + errorMessage;
            }
            if (ex.Source != null)
            {
                string errorSource = ex.Source;
                result += " && Source: " + errorSource;
            }
            if (ex.StackTrace != null)
            {
                StackTrace stackTrace = new StackTrace(ex, true);

                foreach (StackFrame stackFrame in stackTrace.GetFrames())
                {
                    string methodName = stackFrame.GetMethod().Name;
                    int lineNumber = stackFrame.GetFileLineNumber();
                    if (methodName != "" && lineNumber != 0)
                    {
                        result += " && Method: " + methodName + " line " + lineNumber;
                    }
                }
            }
            if (ex.TargetSite != null)
            {
                string targetSite = " && TargetSite: " + ex.TargetSite.ToString() + " - " + ex.TargetSite.DeclaringType.ToString();
                result += targetSite;
            }
            result = result.Replace("'", "");
            LoggingController.SaveHistory(
                string.Format("Unhandled Exception"),
                Lang.Error,
                string.Format(result),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Error);
        }

        private static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string result = "";
            if (ex.InnerException != null)
            {
                string innerExection = "InnerException: " + ex.InnerException.InnerException + " && ";
                result += innerExection;
            }
            if (ex.Message != null)
            {
                string errorMessage = ex.Message.Replace("\r", "").Replace("\n", "").Replace(',', '&');
                result += "Message: " + errorMessage;
            }
            if (ex.Source != null)
            {
                string errorSource = ex.Source;
                result += " && Source: " + errorSource;
            }
            if (ex.StackTrace != null)
            {
                StackTrace stackTrace = new StackTrace(ex, true);
                foreach (StackFrame stackFrame in stackTrace.GetFrames())
                {
                    string methodName = stackFrame.GetMethod().Name;
                    int lineNumber = stackFrame.GetFileLineNumber();
                    if (methodName != "" && lineNumber != 0)
                    {
                        result += " && Method: " + methodName + " line " + lineNumber;
                    }
                }
            }
            if (ex.TargetSite != null)
            {
                string targetSite = " && TargetSite: " + ex.TargetSite.ToString() + " - " + ex.TargetSite.DeclaringType.ToString();
                result += targetSite;
            }
            result = result.Replace("'", "");
            LoggingController.SaveHistory(
                string.Format("Thread Exception"),
                Lang.Error,
                string.Format(result),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Error);
        }

        #region USB Dongle key
        private static void InitVariableUSBDongle()
        {
            _USBKey = new USBKey();
            _USBKey.USBPassword = new ushort[] { 0xAB2A, 0x9718, 0xFF56, 0x2A25 };
            _USBKey.InputValue = new ushort[] { 0x06, 0x02, 0x08, 0x15 };
            _USBKey.ExpectedResult = CalculateValueWithFormulaDefined(_USBKey.InputValue[0], _USBKey.InputValue[1], _USBKey.InputValue[2], _USBKey.InputValue[3]);
        }

        private static ushort[] CalculateValueWithFormulaDefined(ushort ValueA, ushort ValueB, ushort ValueC, ushort ValueD)
        {
            ValueB = (ushort)(ValueB & ValueD);
            ValueA = (ushort)(ValueA + ValueB);
            ValueC = (ushort)(ValueC - ValueA);
            ValueD = (ushort)(ValueD | ValueC);
            return new ushort[] { ValueA, ValueB, ValueC, ValueD };
        }
        
        private static bool CheckForValidUSBDongleKey()
        {
            byte[] buffer = new byte[1024];
            uint hardwareID = 0;
            ushort handle = 0;
            uint lp1 = 0;
            uint lp2 = 0;
            ulong ret = 1;
            Securedongle.SecuredongleControl SD = new Securedongle.SecuredongleControl();
            ret = SD.SecureDongle((ushort)SDCmd.SD_FIND, ref handle, ref lp1, ref lp2,
                ref _USBKey.USBPassword[0], ref _USBKey.USBPassword[1], ref _USBKey.USBPassword[2], ref _USBKey.USBPassword[3], buffer);
            if (ret != 0)
            {
#if DEBUG
                Console.WriteLine("TrangNoi No SecureDongle found");
#endif
                return false;
            }
            hardwareID = lp1;
            if (_HardwareIDUsing == 0)
            {
                _HardwareIDUsing = hardwareID;
            }

            if (_HardwareIDUsing != hardwareID)
            {
#if DEBUG
                Console.WriteLine("_HardwareIDUsing changed");
#endif
                return false;
            }
           
            ret = SD.SecureDongle((ushort)SDCmd.SD_OPEN, ref handle, ref hardwareID, ref lp2,
                ref _USBKey.USBPassword[0], ref _USBKey.USBPassword[1], ref _USBKey.USBPassword[2], ref _USBKey.USBPassword[3], buffer);
            if (ret != 0)
            {
                return false;
            }   
            lp1 = 0; 
            lp2 = 0; 
            ushort[] inputValue = new ushort[] { _USBKey.InputValue[0], _USBKey.InputValue[1], _USBKey.InputValue[2], _USBKey.InputValue[3] };
            ret = SD.SecureDongle((ushort)SDCmd.SD_CALCULATE1, ref handle, ref lp1, ref lp2,
                ref inputValue[0], ref inputValue[1], ref inputValue[2], ref inputValue[3], buffer);
            if (ret != 0)
            {
#if DEBUG
                Console.WriteLine("SD_CALCULATE1 fail");
#endif
                return false;
            }
            for (int i = 0; i < inputValue.Length; i++)
            {
                if (inputValue[i] != _USBKey.ExpectedResult[i])
                {
                    return false;
                }
            }
            ushort p1 = 500;  //Offset of UDZ (UDZ memory position)
            //[500] Rynan 0x54
            //[501] 0x01 Basler camera, 0x02 Cognex camera
            //[502] support level
            //[503] 
            ushort p2 = 4;//length
            ushort p3 = 0;
            ushort p4 = 0;
            ret = SD.SecureDongle((ushort)SDCmd.SD_READ, ref handle, ref hardwareID, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
            if (ret != 0)
            {
                return false;
            }

            //Check OEM code
            if (buffer[0] != 0x54) //0x54 is label Rynan
            {
                return false;
            }
            
            // Check Cognex
            if(buffer[1] != 0x02)
            {
                return false;
            }

            //Close SecureDongle
            SD.SecureDongle((ushort)SDCmd.SD_CLOSE,
                ref handle, 
                ref lp1,
                ref lp2,
                ref _USBKey.USBPassword[0], 
                ref _USBKey.USBPassword[1], 
                ref _USBKey.USBPassword[2], 
                ref _USBKey.USBPassword[3], 
                buffer);
            return true;
        }

        private static void CheckUSBDongleWhenRunning()
        {
            while (true)
            {
#if DEBUG
                Console.WriteLine("CheckUSBDongleWhenRunning");
#endif
                Thread.Sleep(60000);
                if (CheckForValidUSBDongleKey() == false)
                {
                    ShowDialogUSBDongleKeyNotFound();
                    break;
                }
            }
        }

        private static void ShowDialogUSBDongleKeyNotFound()
        {
            if (frmWarningKey == null || frmWarningKey.IsDisposed)
            {
                frmWarningKey = new frmWarningUSBDongleKey();
                frmWarningKey.Focus();
                frmWarningKey.BringToFront();
                frmWarningKey.TopMost = true; 
                frmWarningKey.ShowDialog();
            }
            else
            {
                frmWarningKey.BringToFront();
            }
        }

        #endregion USB Dongle key
    }
}
