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
        private static USBKey _USBKey = new USBKey();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Check application already running
            if (AnotherInstanceExists())
            {
                MessageBox.Show(Lang.ApplicationIsAlreadyRunning,Lang.Info,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            //END Check application already running

            //Load settings
            Shared.LoadSettings();

            // Load language
            Lang.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(Shared.Settings.Language);
            // Set language default
            //Lang.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");

            //Show splash screen
            frmSplashScreen.ShowSplashScreen(Lang.Loading, Lang.PleaseWait);

            //Get UUID of computer run this software
            string idOfPC = FingerPrint.Value();

            bool isAllow = false;
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
            //Change when release
            if (!isAllow)
            {
                //MessageBox.Show("You do not have the license for this software, please contact the vendor!");
                //return;
                //TrangDong - Check USB Dongle key here - On May 28, 2020
                //Initial list key
                InitVariableUSBDongle();

                //Check USB Dongle key when software statup
                if (CheckForValidUSBDongleKey() == false)
                {
                    ShowDialogUSBDongleKeyNotFound();
                    return;
                }

                //Check USB Dongle key when software running
                Thread threadCheckUSBDongle = new Thread(() => CheckUSBDongleWhenRunning());
                threadCheckUSBDongle.IsBackground = true;
                threadCheckUSBDongle.Priority = ThreadPriority.Lowest;
                threadCheckUSBDongle.Start();

                //END TrangDong - Check USB Dongle key here - On May 28, 2020
            }

//#endif


            //END Get UUID of computer run this software
            //END Only allow this software to run on PCs with UUIDs in the list

            Application.EnableVisualStyles();
            LoggingController.LoginToAccess("_rynan_loggin_access_control_management_");
            
            String path = CommVariables.PathAccountsApp;
            if (!File.Exists(path + "AccountDB.db"))
            {
                UserController.CreateDefaultDatabase();
            }

            //run login form
            //frmLogin loginform = new frmLogin();
            frmLoginNew loginform = new frmLoginNew();
            loginform.TopMost = true;

            //Close show splash screen
            frmSplashScreen.CloseSplash();
            DialogResult result = loginform.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                UserController.LogedInUsername = "Administrator";

                //frmMain frmMain = new frmMain();
                //frmMainAnime frmMain = new frmMainAnime();
                //frmMainNew frmMain = new frmMainNew();
                AppDomain currentDomain = default(AppDomain);
                currentDomain = AppDomain.CurrentDomain;
                // Handler for unhandled exceptions.
                currentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
                // Handler for exceptions in threads behind forms.
                System.Windows.Forms.Application.ThreadException += GlobalThreadExceptionHandler;
                frmJob frmJob = new frmJob();
                Application.Run(frmJob);
                //currentDomain.ProcessExit += (obj, e) =>
                //{
                //    LoggingController.SaveHistory(
                //        Lang.Exit,
                //        Lang.LogOut,
                //        Lang.LogoutSuccessfully,
                //        SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                //        LoggingType.LogedOut);
                //};
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
            Exception ex = default(Exception);
            ex = (Exception)e.ExceptionObject;
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
                String.Format("Unhandled Exception"),
                Lang.Error,
                String.Format(result),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Error);
        }

        private static void GlobalThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = default(Exception);
            ex = (Exception)e.Exception;
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
                String.Format("Thread Exception"),
                Lang.Error,
                String.Format(result),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Error);
        }
        // END Reference

        #region USB Dongle key

        private static void InitVariableUSBDongle()
        {
            //SecureDongle password of software
            _USBKey = new USBKey();
            _USBKey.USBPassword = new ushort[] { 0xAB2A, 0x9718, 0xFF56, 0x2A25 };
            _USBKey.InputValue = new ushort[] { 0x06, 0x02, 0x08, 0x15 };//Value form 0 - 63
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

        private static uint _HardwareIDUsing = 0;
        private static bool CheckForValidUSBDongleKey()
        {
            //Declare variable
            byte[] buffer = new byte[1024];
            //uint[] TempHID = new uint[16];
            uint hardwareID = 0;
            ushort handle = 0;
            uint lp1 = 0;
            uint lp2 = 0;
            ulong ret = 1;
            //SystemTime st;

            Securedongle.SecuredongleControl SD = new Securedongle.SecuredongleControl();
            //Find USB Dongle

            ret = SD.SecureDongle((ushort)SDCmd.SD_FIND, ref handle, ref lp1, ref lp2,
                ref _USBKey.USBPassword[0], ref _USBKey.USBPassword[1], ref _USBKey.USBPassword[2], ref _USBKey.USBPassword[3], buffer);
            if (ret != 0)//No SecureDongle found
            {
#if DEBUG
                Console.WriteLine("TrangNoi No SecureDongle found");
#endif
                return false;
            }

            //Avoid user unplug USB Dongle when software running
            hardwareID = lp1;
            if (_HardwareIDUsing == 0)//Assign hardware ID for the first use avoid user change another USB Dongle
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
            //END Avoid user unplug USB Dongle when software running

            //Open USB Dongle, lp1 is Hardware ID read when found USB Dongle
            ret = SD.SecureDongle((ushort)SDCmd.SD_OPEN, ref handle, ref hardwareID, ref lp2,
                ref _USBKey.USBPassword[0], ref _USBKey.USBPassword[1], ref _USBKey.USBPassword[2], ref _USBKey.USBPassword[3], buffer);
            if (ret != 0)//Open SecureDongle failed
            {
                return false;
            }

            //Check Hardware algorithm of USB Dongle Calculation 1      
            lp1 = 0; //UAZ start position
            lp2 = 0; //which module?
            ushort[] inputValue = new ushort[] { _USBKey.InputValue[0], _USBKey.InputValue[1], _USBKey.InputValue[2], _USBKey.InputValue[3] };
            ret = SD.SecureDongle((ushort)SDCmd.SD_CALCULATE1, ref handle, ref lp1, ref lp2,
                ref inputValue[0], ref inputValue[1], ref inputValue[2], ref inputValue[3], buffer);
            if (ret != 0)
            {
#if DEBUG
                Console.WriteLine("TrangNoi SD_CALCULATE1 fail");
#endif
                return false;
            }

            //Check value after calculate formula by Hardware algorithm of USD Dongle key. The value input will change if Calculation 1 execute success
            for (int i = 0; i < inputValue.Length; i++)
            {
                if (inputValue[i] != _USBKey.ExpectedResult[i])
                {
                    return false;
                }
            }

            //Read User Data Zone(UDZ)
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
            //END Check OEM code
            
            // Check Cognex
            if(buffer[1] != 0x02)
            {
                return false;
            }
            //END Check Cognex

            //Close SecureDongle
            SD.SecureDongle((ushort)SDCmd.SD_CLOSE, ref handle, ref lp1, ref lp2,
                ref _USBKey.USBPassword[0], ref _USBKey.USBPassword[1], ref _USBKey.USBPassword[2], ref _USBKey.USBPassword[3], buffer);

            return true;
        }

        private static void CheckUSBDongleWhenRunning()
        {
            while (true)
            {
#if DEBUG
                Console.WriteLine("TrangNoi CheckUSBDongleWhenRunning");
#endif
                Thread.Sleep(60000);//Check again in 60 seconds. Sleep before because, checked when startup
                if (CheckForValidUSBDongleKey() == false)
                {
                    ShowDialogUSBDongleKeyNotFound();
                    break;
                }

            }
        }

        private static frmWarningUSBDongleKey frmWarningKey = null;
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
