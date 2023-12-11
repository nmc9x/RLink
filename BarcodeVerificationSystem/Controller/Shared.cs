using BarcodeVerificationSystem.Model;
using CommonVariable;
using EncrytionFile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Controller
{
    /// <summary>
    /// @Author: DungLe/ ThongThach
    /// @Email: dung.le@rynantech.com/ thong.thach@rynantech.com
    /// @Date created: October 14, 2022
    /// </summary>
    public class Shared
    {
        public static string Test = "";

        #region Variables
        public static OperationStatus OperStatus = OperationStatus.Stopped;
        public static UserDataModel LoggedInUser = null;
        public static List<PODController> PODControllerList = new List<PODController>();
        public static PODController PrinterPODController = null;
        public static SettingsModel Settings = new SettingsModel();
        public static JobModel Jobs = new JobModel();
        public static bool IsSensorControllerConnected = false;
        public static PODController SensorController = null;
        public static List<HardwareIDModel> listPCAllow = new List<HardwareIDModel>();

        public static string JobNameSelected = "";
        #endregion Variables
        #region Events
        public static event EventHandler OnLanguageChange;
        public static void RaiseLanguageChangeEvent(String languageCode)
        {
            //Set language
            UILanguage.Lang.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(languageCode);

            OnLanguageChange?.Invoke(languageCode, EventArgs.Empty);
        }
        public static event EventHandler OnRepeatTCPMessageChange;
        public static void RaiseOnRepeatTCPMessageChange(object tcpMessage)
        {
            OnRepeatTCPMessageChange?.Invoke(tcpMessage, EventArgs.Empty);
        }

        public static event EventHandler OnSensorControllerChangeEvent;
        public static void RaiseSensorControllerChangeEvent()
        {
            OnSensorControllerChangeEvent?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnPrinterStatusChange;
        public static void RaiseOnPrinterStatusChangeEvent()
        {
            OnPrinterStatusChange?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnPrintingStateChange;
        public static void RaiseOnPrintingStateChange()
        {
            OnPrintingStateChange?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnPrinterDataChange;
        public static void RaiseOnPrinterDataChangeEvent(PODDataModel data)
        {
            OnPrinterDataChange?.Invoke(data, EventArgs.Empty);
        }
        public static event EventHandler OnCameraStatusChange;
        public static void RaiseOnCameraStatusChangeEvent()
        {
            OnCameraStatusChange?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnCameraReadDataChange;
        public static void RaiseOnCameraReadDataChangeEvent(DetectModel detectModel)
        {
            OnCameraReadDataChange?.Invoke(detectModel, EventArgs.Empty);
        }
        public static event EventHandler OnOperationStatusChange;
        public static void RaiseOnOperationStatusChangeEvent(OperationStatus operationStatus)
        {
            OnOperationStatusChange?.Invoke(operationStatus, EventArgs.Empty);
        }
        public static event EventHandler OnCameraOutputSignalChange;
        public static void RaiseOnCameraOutputSignalChangeEvent()
        {
            OnCameraOutputSignalChange?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnCameraTriggerOnChange;
        public static void RaiseOnCameraTriggerOnChangeEvent()
        {
            OnCameraTriggerOnChange?.Invoke(null, EventArgs.Empty);
        }

        public static event EventHandler OnCameraTriggerOffChange;
        public static void RaiseOnCameraTriggerOffChangeEvent()
        {
            OnCameraTriggerOffChange?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler OnReceiveResponsePrinter;
        public static void RaiseOnReceiveResponsePrinter(object response)
        {
            OnReceiveResponsePrinter?.Invoke(response, EventArgs.Empty);
        }
        public static event EventHandler OnVerifyAndPrindSendDataMethod;
        public static void RaiseOnVerifyAndPrindSendDataMethod()
        {
            OnVerifyAndPrindSendDataMethod?.Invoke(true, EventArgs.Empty);
        }
        public static event EventHandler OnHanlderException;
        public static void RaiseOnOnHanlderException(Exception ex)
        {
            OnHanlderException?.Invoke(ex, UnhandledExceptionEventArgs.Empty);
        }
        // Catch Error - Add by ThongThach 05/12/2023
        public static event EventHandler OnLogError;
        public static void RaiseOnLogError(object sender)
        {
            OnLogError?.Invoke(sender, EventArgs.Empty);
        }
        #endregion Events
        #region Methods

        public static PrinterSettingsModel GetSettingsPrinter()
        {
            PrinterSettingsModel printerSettingsModel = new PrinterSettingsModel();
            try
            {
                string printerIPAddress = Shared.Settings.PrinterList.FirstOrDefault().IP;
                int printerPort = Shared.Settings.PrinterList.FirstOrDefault().NumPortRemote;

                string url = string.Format("http://{0}:{1}/api/request?act=get_system_setting", printerIPAddress, printerPort);

                // Create a request using a URL that can receive a post
                var request = (HttpWebRequest)WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "GET";
                // Set time out
                request.Timeout = 1000; //5000ms
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseFromServer = streamReader.ReadToEnd();
                    // Display the content.  
                    //Console.WriteLine(responseFromServer);
                    PrinterSettingsResponseModel printerSettingsResponse = JsonConvert.DeserializeObject<PrinterSettingsResponseModel>(responseFromServer);
                    if (printerSettingsResponse != null)
                    {
                        if (printerSettingsResponse.success)
                        {
                            printerSettingsModel = printerSettingsResponse.data;
                        }
                    }
                }
                printerSettingsModel.IsSupportHttpRequest = true;
                return printerSettingsModel;
            }
            catch (WebException)
            {
                printerSettingsModel.IsSupportHttpRequest = false;
                return printerSettingsModel;
            }
            catch (Exception)
            {
                return printerSettingsModel;
            }
            
        }

        // Login offline
        public static ActivationStatus LoginLocal(string username, string password)
        {
            LoggedInUser = UserController.Login(username, password);
            if (LoggedInUser == null)
            {
                return ActivationStatus.Failed;
            }
            else
            {
                return ActivationStatus.Successful;
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        // END Login offline
        #endregion
        #region Functions
        /// <summary>
        /// Load setting from program data directory function
        /// </summary>
        public static void LoadSettings()
        {
            try
            {
                string path = CommVariables.PathSettingsApp + "Settings.xml";
                Settings = SettingsModel.LoadSetting(path);
            }
            catch
            {
                Settings = new SettingsModel();
            }
            // Check settings unavailable camera and add default
            if (Settings.CameraList.Count <= 0)
            {
                Settings.CameraList.Add(new CameraModel { Index = 0,IP = "192.168.0.2",RoleOfCamera = RoleOfStation.ForProduct });
                //Settings.CameraList.Add(new CameraModel { Index = 1,IP = "192.168.0.3",RoleOfCamera = RoleOfStation.ForBox });
            }

            // Check settings unavailable printer and add default
            if (Settings.PrinterList.Count <= 0)
            {
                Settings.PrinterList.Add(new PrinterModel { Index = 0,IP = "192.168.1.2",RoleOfPrinter = RoleOfStation.ForProduct });
                //Settings.PrinterList.Add(new PrinterModel { Index = 1,IP = "192.168.1.3",RoleOfPrinter = RoleOfStation.ForBox });
            }
        }

        /// <summary>
        /// Save setting to program data directory function
        /// </summary>
        public static void SaveSettings()
        {
            try
            {
                string path = CommVariables.PathSettingsApp;
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                Settings.SaveSettings(path + "Settings.xml");
            }
            catch { }
        }

        public static CameraModel GetCameraModelBasedOnIPAddress(string ipAddress)
        {
            foreach (CameraModel cameraModel in Shared.Settings.CameraList)
            {
                if (cameraModel.IP.Equals(ipAddress) && cameraModel.IsEnable)
                {
                    return cameraModel;
                }
            }
            return null;
        }

        public static bool CheckJobHasExist(string templateNameWithoutExtension)
        {
            string filePath = CommVariables.PathJobsApp + templateNameWithoutExtension + Settings.JobFileExtension;
            // Check if file exists with its full path 
            return File.Exists(filePath);
        }

        public static bool DeleteJob(JobModel templatePath)
        {
            string filePath = CommVariables.PathJobsApp + templatePath.FileName + Settings.JobFileExtension;
            string filePathCheckResult = CommVariables.PathCheckedResult + templatePath.CheckedResultPath;
            string filePathPrintedResponse = CommVariables.PathPrintedResponse + templatePath.PrintedResponePath;
            // Check if file exists with its full path 
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (File.Exists(filePathCheckResult))
            {
                File.Delete(filePathCheckResult);
            }
            if (File.Exists(filePathPrintedResponse))
            {
                File.Delete(filePathPrintedResponse);
            }
            return true;
        }
        public static JobModel GetJob(string templateNameWithExtension)
        {
            string filePath = CommVariables.PathJobsApp + templateNameWithExtension;
            return JobModel.LoadFile(filePath);
        }
        public static List<string> GetJobNameList()
        {
            try
            {
                string folderPath = CommVariables.PathJobsApp;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //Get all file name with extension in folder
                //DirectoryInfo dir = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                string strFileNameExtension = String.Format("*{0}",Settings.JobFileExtension);
                FileInfo[] files = dir.GetFiles(strFileNameExtension).OrderByDescending(x => x.CreationTime).ToArray(); //Getting Text files
                List<string> result = new List<string>();
                foreach (FileInfo file in files)
                {
                    result.Add(file.Name);
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetReadStringFromResultXml(string resultXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(resultXml);

                XmlNode fullStringNode = doc.SelectSingleNode("result/general/full_string");

                if (fullStringNode != null)
                {
                    XmlAttribute encoding = fullStringNode.Attributes["encoding"];
                    if (encoding != null && encoding.InnerText == "base64")
                    {
                        if (!string.IsNullOrEmpty(fullStringNode.InnerText))
                        {
                            byte[] code = Convert.FromBase64String(fullStringNode.InnerText);
                            return Encoding.UTF8.GetString(code,0,code.Length);
                        }
                        else
                        {
                            return "";
                        }
                    }

                    return fullStringNode.InnerText;
                }
            }
            catch (Exception)
            {
            }

            return "";
        }
        public static bool GetCameraStatus()
        {
            foreach (CameraModel cameraModel in Shared.Settings.CameraList)
            {
                if (cameraModel.IsEnable && !cameraModel.IsConnected)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool GetPrinterStatus()
        {
            foreach (PrinterModel printerModel in Shared.Settings.PrinterList)
            {
                if (printerModel.IsEnable && !printerModel.IsConnected)
                {
                    return false;
                }
            }
            return true;
        }

        public static void SendSettingToSensorController()
        {
            try
            {
                if (SensorController != null && IsSensorControllerConnected)
                {
                    //(P03600D05000L00510H00250)
                    //int temp = 3;
                    //string strPulseEncoder = temp.ToString("D7");
                    string strPulseEncoder = Settings.SensorControllerPulseEncoder.ToString("D5");
                    float encoderDiameter = Settings.SensorControllerEncoderDiameter * 100.0f;
                    //float encoderDiameter = ((float)Settings.SensorControllerEncoderDiameter);
                    string strEnconderDiameter = ((int)encoderDiameter).ToString("D5");
                    string strSensorDisableLength = Settings.SensorControllerDelayBefore.ToString("D5");
                    string strSensorEnableLength = Settings.SensorControllerDelayAfter.ToString("D5");

                    string strCommand = string.Format("(P{0}D{1}L{2}H{3})", strPulseEncoder, strEnconderDiameter, strSensorDisableLength, strSensorEnableLength);
                    //Console.WriteLine("Send TCP: {0}", strCommand);
                    SensorController.Send(strCommand);
                }
            }
            catch { }
        }
        #endregion
    }
}
