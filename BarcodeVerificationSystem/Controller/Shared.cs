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
using System.Text;
using System.Xml;

namespace BarcodeVerificationSystem.Controller
{
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
                string printerIPAddress = Settings.PrinterList.FirstOrDefault().IP;
                int printerPort = Settings.PrinterList.FirstOrDefault().NumPortRemote;
                string url = string.Format("http://{0}:{1}/api/request?act=get_system_setting", printerIPAddress, printerPort);
                var request = (HttpWebRequest)WebRequest.Create(url);
               
                request.Method = "GET";
                request.Timeout = 1000; 
                request.ContentType = "application/json";
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseFromServer = streamReader.ReadToEnd();
                    var printerSettingsResponse = JsonConvert.DeserializeObject<PrinterSettingsResponseModel>(responseFromServer);
                    if (printerSettingsResponse != null)
                    {
                        if (printerSettingsResponse.Success)
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
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        #endregion
        #region Functions
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
            if (Settings.CameraList.Count <= 0)
            {
                Settings.CameraList.Add(new CameraModel { Index = 0,IP = "192.168.0.2",RoleOfCamera = RoleOfStation.ForProduct });
            }
            if (Settings.PrinterList.Count <= 0)
            {
                Settings.PrinterList.Add(new PrinterModel { Index = 0,IP = "192.168.1.2",RoleOfPrinter = RoleOfStation.ForProduct });
            }
        }


        public static void SaveSettings()
        {
            try
            {
                string path = CommVariables.PathSettingsApp;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Settings.SaveSettings(path + "Settings.xml");
            }
            catch { }
        }

        public static CameraModel GetCameraModelBasedOnIPAddress(string ipAddress)
        {
            foreach (CameraModel cameraModel in Settings.CameraList)
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
            return File.Exists(filePath);
        }

        public static bool DeleteJob(JobModel templatePath)
        {
            string filePath = CommVariables.PathJobsApp + templatePath.FileName + Settings.JobFileExtension;
            string filePathCheckResult = CommVariables.PathCheckedResult + templatePath.CheckedResultPath;
            string filePathPrintedResponse = CommVariables.PathPrintedResponse + templatePath.PrintedResponePath;
          
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

                var dir = new DirectoryInfo(folderPath);
                var strFileNameExtension = string.Format("*{0}",Settings.JobFileExtension);
                FileInfo[] files = dir.GetFiles(strFileNameExtension).OrderByDescending(x => x.CreationTime).ToArray(); 
                var result = new List<string>();
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
                var doc = new XmlDocument();
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
            foreach (CameraModel cameraModel in Settings.CameraList)
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
            foreach (PrinterModel printerModel in Settings.PrinterList)
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
                    string strPulseEncoder = Settings.SensorControllerPulseEncoder.ToString("D5");
                    float encoderDiameter = Settings.SensorControllerEncoderDiameter * 100.0f;
                    string strEnconderDiameter = ((int)encoderDiameter).ToString("D5");
                    string strSensorDisableLength = Settings.SensorControllerDelayBefore.ToString("D5");
                    string strSensorEnableLength = Settings.SensorControllerDelayAfter.ToString("D5");
                    string strCommand = string.Format("(P{0}D{1}L{2}H{3})", strPulseEncoder, strEnconderDiameter, strSensorDisableLength, strSensorEnableLength);
                    SensorController.Send(strCommand);
                }
            }
            catch { }
        }
        #endregion
    }
}
