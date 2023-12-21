using BarcodeVerificationSystem.Model;
using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace BarcodeVerificationSystem.Controller.Camera
{
    public class DMSeries : CamerasHandler
    {
        #region Variables
        private SynchronizationContext _SyncContext = null;
        internal EthSystemDiscoverer _EthSystemDiscoverer = null;
        internal SerSystemDiscoverer _SerSystemDiscoverer = null;
        internal List<object> _CameraSystemInfoList = new List<object>();
        internal List<DataManSystem> _DataManSystemList = new List<DataManSystem>();
        private readonly object _CurrentResultInfoSyncLock = new object();
        internal event EventHandler UpdateLabelStatusEvent;
        public override string IPAddress { get; set;}
        public override string Port { get; set; }
        #endregion Variables

        #region Cognex_Camera
        internal void InitCameraVariables()
        {

            _SyncContext = SynchronizationContext.Current;

            _EthSystemDiscoverer = new EthSystemDiscoverer();
            _SerSystemDiscoverer = new SerSystemDiscoverer();

            _EthSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
            _SerSystemDiscoverer.SystemDiscovered += new SerSystemDiscoverer.SystemDiscoveredHandler(OnSerSystemDiscovered);

            _EthSystemDiscoverer.Discover();
            _SerSystemDiscoverer.Discover();
        }
        public override void Connect(string ipadd, string port = null)
        {
            try
            {
                if(_CameraSystemInfoList.Count<= 0) 
                {
                    CameraModel cameraModel = Shared.Settings.CameraList.FirstOrDefault();
                    cameraModel.IsConnected = false;
                    Shared.RaiseOnCameraStatusChangeEvent();
                }
               
                foreach (var systemInfo in _CameraSystemInfoList)
                {
                    ISystemConnector iSysConnector = null;
                    if (systemInfo is EthSystemDiscoverer.SystemInfo)
                    {
                        EthSystemDiscoverer.SystemInfo ethSystemInfo = systemInfo as EthSystemDiscoverer.SystemInfo;
                        EthSystemConnector conn = new EthSystemConnector(ethSystemInfo.IPAddress);
                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(ethSystemInfo.IPAddress.ToString());
                        if (cameraModel != null && cameraModel.IP == ipadd)
                        {
                            conn.UserName = cameraModel.UserName;
                            conn.Password = cameraModel.Password;
                            cameraModel.Name = ethSystemInfo.Name;
                            cameraModel.SerialNumber = ethSystemInfo.SerialNumber;
                        }
                        else
                        {
                            UpdateLabelStatusEvent?.Invoke(this, EventArgs.Empty);
                            Shared.RaiseOnCameraStatusChangeEvent();
                            continue;
                        }

                        iSysConnector = conn;
                    }
                    else if (systemInfo is SerSystemDiscoverer.SystemInfo)
                    {
                        SerSystemDiscoverer.SystemInfo ser_system_info = systemInfo as SerSystemDiscoverer.SystemInfo;
                        SerSystemConnector conn = new SerSystemConnector(ser_system_info.PortName, ser_system_info.Baudrate);
                        iSysConnector = conn;
                    }

                    DataManSystem dataManSystem = new DataManSystem(iSysConnector);
                    dataManSystem.DefaultTimeout = 1000;
                    dataManSystem.SystemConnected += new SystemConnectedHandler(OnSystemConnected);
                    dataManSystem.SystemDisconnected += new SystemDisconnectedHandler(OnSystemDisconnected);

                    dataManSystem.SystemWentOnline += new SystemWentOnlineHandler(OnSystemWentOnline);
                    dataManSystem.SystemWentOffline += new SystemWentOfflineHandler(OnSystemWentOffline);
                    ResultTypes resultTypes = ResultTypes.ReadXml | ResultTypes.Image | ResultTypes.ImageGraphics;
                    ResultCollector resultCollector = new ResultCollector(dataManSystem, resultTypes);
                    resultCollector.ComplexResultCompleted += ResultCollector_ComplexResultCompleted;
                    dataManSystem.Connect();

                    try
                    {
                        dataManSystem.SetResultTypes(resultTypes);
                    }
                    catch (Exception) { }

                    _DataManSystemList.Add(dataManSystem);
                }
            }
            catch (Exception)
            {
                CleanupConnection();
            }
        }
        private void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo)
        {
            _SyncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        Console.WriteLine(string.Format("IP camera: {0}", systemInfo.IPAddress.ToString()));
                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(systemInfo.IPAddress.ToString());
                        bool hasExist = CheckCameraInfoHasExist(systemInfo, _CameraSystemInfoList);
                        if (cameraModel != null && hasExist == false)
                        {
                            _CameraSystemInfoList.Add(systemInfo);
                           
                        }
                    }),
                    null);
        }
        private void OnSerSystemDiscovered(SerSystemDiscoverer.SystemInfo systemInfo)
        {
            _SyncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                      
                    }),
                    null);
        }
        private bool CheckCameraInfoHasExist(EthSystemDiscoverer.SystemInfo cameraInfoNeedCheck, List<object> cameraSystemInfoList)
        {
            foreach (var systemInfo in cameraSystemInfoList)
            {
                if (systemInfo is EthSystemDiscoverer.SystemInfo)
                {
                    EthSystemDiscoverer.SystemInfo ethSystemInfo = systemInfo as EthSystemDiscoverer.SystemInfo;
                    if (ethSystemInfo.SerialNumber == cameraInfoNeedCheck.SerialNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void CleanupConnection()
        {
            foreach (DataManSystem dataManSystem in _DataManSystemList)
            {
                dataManSystem.Disconnect();
                dataManSystem.SystemConnected -= OnSystemConnected;
                dataManSystem.SystemDisconnected -= OnSystemDisconnected;
            }

            _DataManSystemList.Clear();
            _CameraSystemInfoList.Clear();
        }
        private void OnSystemConnected(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                    Console.WriteLine("OnSystemConnected");
                    if (sender is DataManSystem)
                    {
                        DataManSystem dataManSystem = sender as DataManSystem;
                        EthSystemConnector ethSystemConnector = dataManSystem.Connector as EthSystemConnector;
                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(ethSystemConnector.Address.ToString());
                        if (cameraModel != null)
                        {
                            cameraModel.IsConnected = true;
                        }
                    }
                    UpdateLabelStatusEvent?.Invoke(this, EventArgs.Empty);
                    Shared.RaiseOnCameraStatusChangeEvent();
                },
                null);
        }
        private void OnSystemDisconnected(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                    Console.WriteLine("OnSystemDisconnected");
                    if (sender is DataManSystem)
                    {
                        DataManSystem dataManSystem = sender as DataManSystem;
                        EthSystemConnector ethSystemConnector = dataManSystem.Connector as EthSystemConnector;
                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(ethSystemConnector.Address.ToString());
                        if (cameraModel != null)
                        {
                            cameraModel.IsConnected = false;
                            cameraModel.Name = "";
                            cameraModel.SerialNumber = "";
                        }
                    }
                    UpdateLabelStatusEvent?.Invoke(this, EventArgs.Empty);
                    Shared.RaiseOnCameraStatusChangeEvent();
                },
                null);
        }
        private void OnSystemWentOnline(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                },
                null);
        }
        private void OnSystemWentOffline(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                },
                null);
        }
        private void ResultCollector_ComplexResultCompleted(object sender, ComplexResult e)
        {
            _SyncContext.Post(
                delegate
                {
                    ProcessComplexResultCompleted(sender, e);
                },
                null);
        }
        private void ProcessComplexResultCompleted(object sender, ComplexResult complexResult)
        {
            CameraModel cameraModel = null;
            if (sender is ResultCollector)
            {
                ResultCollector resultCollector = sender as ResultCollector;
                var field = resultCollector.GetType().GetField("_dmSystem", BindingFlags.NonPublic | BindingFlags.Instance);
                DataManSystem dataManSystem = (DataManSystem)field.GetValue(resultCollector);
                if (dataManSystem != null)
                {
                    EthSystemConnector ethSystemConnector = dataManSystem.Connector as EthSystemConnector;
                    cameraModel = Shared.GetCameraModelBasedOnIPAddress(ethSystemConnector.Address.ToString());
                }
            }
            Image imageResult = null;
            string strResult = "";
            List<string> imageGraphics = new List<string>();
            lock (_CurrentResultInfoSyncLock)
            {
                foreach (SimpleResult simpleResult in complexResult.SimpleResults)
                {
                    switch (simpleResult.Id.Type)
                    {
                        case ResultTypes.Image:
                            imageResult = ImageArrivedEventArgs.GetImageFromImageBytes(simpleResult.Data);
                            break;

                        case ResultTypes.ImageGraphics:
                            imageGraphics.Add(simpleResult.GetDataAsString());
                            break;

                        case ResultTypes.ReadString:
                            strResult = Encoding.UTF8.GetString(simpleResult.Data);
                            break;

                        case ResultTypes.ReadXml:
                            strResult = Shared.GetReadStringFromResultXml(simpleResult.GetDataAsString());
                            break;

                        default:
                            break;
                    }
                }
            }
            Bitmap bitmap = new Bitmap(1024, 1024);
            if (imageResult != null)
            {
                bitmap = ((Bitmap)imageResult).Clone(new Rectangle(0, 0, imageResult.Width, imageResult.Height), PixelFormat.Format24bppRgb);
            }
            else
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                }
            }

            if (imageGraphics.Count > 0)
            {
                using (Graphics graphicsImage = Graphics.FromImage(bitmap))
                {
                    foreach (string graphics in imageGraphics)
                    {
                        ResultGraphics resultGraphics = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        ResultGraphicsRenderer.PaintResults(graphicsImage, resultGraphics);
                    }
                }
            }
            DetectModel detectModel = new DetectModel();
            detectModel.Image = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            detectModel.Text = Regex.Replace(strResult, @"\r\n", ";"); // split object by symbol ";"
            if (cameraModel != null)
            {
                detectModel.RoleOfCamera = cameraModel.RoleOfCamera;
            }
            Shared.RaiseOnCameraReadDataChangeEvent(detectModel);
        }
        #endregion Cognex_Camera
    }
}
