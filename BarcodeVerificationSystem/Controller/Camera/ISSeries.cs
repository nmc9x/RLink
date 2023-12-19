using BarcodeVerificationSystem.Model;
using Cognex.InSight.Remoting.Serialization;
using Cognex.InSight.Web;
using Cognex.InSight.Web.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BarcodeVerificationSystem.Controller.Camera
{
    public class ISSeries : CamerasHandler
    {
        public static  CvsInSight _InSight = new CvsInSight() ;
        public static CvsDisplay _CvsDisplay = new CvsDisplay();

        public override string IPAddress { get; set; }
        public override string Port { get; set; }
        internal event EventHandler UpdateLabelStatusEvent;

        public ISSeries()
        {
            InitEvent();
        }

        private void InitEvent()
        {
            _InSight.ConnectedChanged += InSight_ConnectedChanged;
           // _InSight.ResultsChanged += _InSight_ResultsChangedAsync;
        }

        //private async void _InSight_ResultsChangedAsync(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("Vao su kien");
        //    await _CvsDisplay.UpdateResults();
        //}

        private List<ObjectResultModel> _ObjectResList = new List<ObjectResultModel>();
        List<(int, string)> _DesireDataList = new List<(int, string)>();
        private string CameraData = "";
        private async void InSight_ResultsChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    JToken tokenResults = _InSight.Results;
            //    JObject objectResult = tokenResults.ToObject<JObject>();
            //    JToken tokenObjectCells = objectResult["cells"];
            //    string jsonString = tokenObjectCells.ToString();

            //    if (objectResult != null && jsonString != "")
            //    {
            //        _ObjectResList = JsonConvert.DeserializeObject<List<ObjectResultModel>>(jsonString);
            //        _DesireDataList = GetDesireDataByObjectName().ToList();
            //        _DesireDataList.Sort();

            //        if (_DesireDataList != null)
            //        {
            //            CameraData = "";
            //            int i = 0;
            //            foreach (var item in _DesireDataList)
            //            {
            //                if (i < _DesireDataList.Count)
            //                    CameraData += item.Item2.ToString() + ";";
            //                if (i == _DesireDataList.Count - 1)
            //                {
            //                    CameraData = CameraData.Substring(0, CameraData.Length - 1);
            //                }
            //                i++;
            //            }
            //        }
            //    }
            Debug.WriteLine("Data OCR: " + CameraData);
            //   // await Task.CompletedTask;
            //    //await _CvsDisplay.UpdateResults();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

        }
        private bool[] _EnableObject = new bool[5] {true,false,false,false,false};
        private string[] _ObjectName = new string[5] {"Object_1", "Object_2" , "Object_3" , "Object_4" , "Object_5" };
        private ObjectResultModel[] _ObjectData = new ObjectResultModel[5];
        private List<(int, string)> GetDesireDataByObjectName()
        {
            // Find Data by Pattern Name (on camera)
            var desireData = new List<(int, string)>();
            var objectResultList = new List<ObjectResultModel>();
            for (int i = 0; i < 5; i++)
            {
                if (_EnableObject[i])
                {
                    _ObjectData[i] = _ObjectResList.FirstOrDefault(x => x.Name != null && x.Name.Contains(_ObjectName[i]));
                    if (_ObjectData[i] != null)
                    {
                        objectResultList.Add(_ObjectData[i]);
                        desireData.Add((i, _ObjectData[i].Data.ToString()));
                    }
                }
            }
            return desireData;
        }

    

        private void InSight_ConnectedChanged(object sender, EventArgs e)
        {
            try
            {
                CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(_InSight.RemoteIPAddress.Remove(_InSight.RemoteIPAddress.IndexOf(':')));
                if (cameraModel != null)
                {
                    if (_InSight.Connected)
                    {
                        cameraModel.IsConnected = true;
                        cameraModel.Name = _InSight.CameraInfo.ModelNumber;
                        cameraModel.SerialNumber = _InSight.CameraInfo.SerialNumber;
                    }
                    else
                    {
                        cameraModel.IsConnected = false;
                        cameraModel.Name = "";
                        cameraModel.SerialNumber = "";
                    }
                    UpdateLabelStatusEvent?.Invoke(this, EventArgs.Empty);
                    Shared.RaiseOnCameraStatusChangeEvent();
                }
            }
            catch (Exception){}
        }

        public override async void ConnectAsync(CvsDisplay cvsDisplay,string ipadd, string port = null)
        {
            _CvsDisplay = cvsDisplay;
            try
            {
                IPAddress = ipadd;
                Port = port;
                string iPAddressWithPort = string.Concat(ipadd, ":", port);
                cvsDisplay.SetInSight(_InSight);
                var sessionInfo = new HmiSessionInfo
                {
                    SheetName = "Inspection",
                    CellNames = new string[1] { "A0:Z599" },
                    EnableQueuedResults = true,
                    IncludeCustomView = true
                };
                await _InSight.Connect(iPAddressWithPort, "admin", "", sessionInfo);
                await cvsDisplay.OnConnected();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Connection Error: " + ex.Message);
#endif
            }
           
        }
    }
}
