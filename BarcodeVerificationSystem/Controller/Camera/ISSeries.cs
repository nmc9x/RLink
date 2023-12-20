using BarcodeVerificationSystem.Model;
using Cognex.InSight.Remoting.Serialization;
using Cognex.InSight.Web;
using Cognex.InSight.Web.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BarcodeVerificationSystem.Controller.Camera
{
    public class ISSeries : CamerasHandler
    {
        #region Variables Definition
        public override string IPAddress { get; set; }
        public override string Port { get; set; }

        internal readonly CvsInSight _InSight;
        internal CvsDisplay _CvsDisplay;

        internal event EventHandler AutoAddSufixEvent;
        private List<ObjectResultModel> _ObjectResList = new List<ObjectResultModel>();
        internal readonly CameraModel _CameraModel = Shared.Settings.CameraList.FirstOrDefault();
        private readonly ObjectResultModel[] _ObjectData = new ObjectResultModel[5];
        internal event EventHandler UpdateLabelStatusEvent;
        private List<(int, string)> _DesireDataList = new List<(int, string)>();

        private string CameraData = "";
        private readonly string[] _ObjectName_Temp = new string[5] { "Object_1", "Object_2", "Object_3", "Object_4", "Object_5" };
        public bool[] _IsSymbol = new bool[5];
        private readonly string suf_code = ".Result00.String";
        private readonly string suf_text = ".ReadText";
        private bool[] _EnableObject = new bool[5];
        private readonly string[] _ObjectName = new string[5] { "Object_1", "Object_2", "Object_3", "Object_4", "Object_5" };
        private const int numObject = 5;
        #endregion

        public ISSeries()
        {
            _InSight = new CvsInSight();
            _CvsDisplay = new CvsDisplay();
            _InSight.ConnectedChanged += InSight_ConnectedChanged;
            _InSight.ResultsChanged += InSight_ResultsChanged;
            _CvsDisplay.SetInSight(_InSight);
        }

        #region Functions
        public async Task FirstConnection()
        {
            try
            {
                HmiSessionInfo sessionInfo = new HmiSessionInfo
                {
                    SheetName = "Inspection",
                    CellNames = new string[1] { "A0:Z599" },
                    EnableQueuedResults = true,
                    IncludeCustomView = true
                };
                await _InSight.Connect(_CameraModel.IP + ":" + _CameraModel.Port, "admin", "", sessionInfo);
                await _CvsDisplay.OnConnected();
                Shared.RaiseOnCameraStatusChangeEvent();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
            }
        }

        internal void AutoAddSuffixes(CameraModel cameraModel)
        {
            try
            {
                if (cameraModel == null) return;
                _IsSymbol = cameraModel.IsSymbol;
                for (int i = 0; i < 5; i++)
                {
                    _ObjectName_Temp[i] = _ObjectName[i].Replace(suf_code, "");
                    _ObjectName_Temp[i] = _ObjectName[i].Replace(suf_text, "");
                    if (_ObjectName_Temp[i] != null)
                    {
                        if (_IsSymbol[i])
                        {
                            if (_ObjectName_Temp[i].EndsWith(suf_code))
                            {
                                int suffixPosition = _ObjectName_Temp[i].LastIndexOf(suf_code);  // If already exists, update only the non-suffix part
                                _ObjectName_Temp[i] = _ObjectName_Temp[i].Substring(0, suffixPosition);
                            }
                            _ObjectName_Temp[i] += suf_code;
                        }
                        else
                        {
                            if (_ObjectName_Temp[i].EndsWith(suf_text))
                            {
                                int suffixPosition = _ObjectName_Temp[i].LastIndexOf(suf_text);  // If already exists, update only the non-suffix part
                                _ObjectName_Temp[i] = _ObjectName_Temp[i].Substring(0, suffixPosition);
                            }
                            _ObjectName_Temp[i] += suf_text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
            }
        }

        private List<(int, string)> GetDesireDataByObjectName(CameraModel cameraModel)
        {
            try
            {
                List<(int, string)> desireData = new List<(int, string)>();
                List<ObjectResultModel> objectResultList = new List<ObjectResultModel>();
                if (cameraModel != null)
                {
                    _EnableObject = UtilityFunctions.IntToBools(cameraModel.ObjectSelectNum, 5);
                    for (int i = 0; i < numObject; i++)
                    {
                        if (_EnableObject[i])
                        {
                            _ObjectData[i] = _ObjectResList.FirstOrDefault(x => x.Name != null && x.Name.Contains(_ObjectName_Temp[i]));
                            if (_ObjectData[i] != null)
                            {
                                objectResultList.Add(_ObjectData[i]);
                                desireData.Add((i, _ObjectData[i].Data.ToString()));
                            }
                        }
                    }
                }
                return desireData;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
                return null;
            }
        }
        #endregion

        #region Events
        private async void InSight_ConnectedChanged(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(500); // wait for preparing connection !
                if (_CameraModel != null)
                {
                    if (_InSight.Connected)
                    {
                        _CameraModel.IsConnected = true;
                        _CameraModel.Name = _InSight.CameraInfo.ModelNumber;
                        _CameraModel.SerialNumber = _InSight.CameraInfo.SerialNumber;
                    }
                    else
                    {
                        _CameraModel.IsConnected = false;
                        _CameraModel.Name = "";
                        _CameraModel.SerialNumber = "";
                    }
                    UpdateLabelStatusEvent?.Invoke(this, EventArgs.Empty);
                    Shared.RaiseOnCameraStatusChangeEvent();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
            }
        }

        private async void InSight_ResultsChanged(object sender, EventArgs e)
        {
            try
            {
                JToken tokenResults = _InSight.Results;
                JObject objectResult = tokenResults.ToObject<JObject>();
                JToken tokenObjectCells = objectResult["cells"];
                string jsonString = tokenObjectCells.ToString();

                if (objectResult != null && jsonString != "")
                {
                    _ObjectResList = JsonConvert.DeserializeObject<List<ObjectResultModel>>(jsonString);
                    _DesireDataList = GetDesireDataByObjectName(_CameraModel).ToList();
                    _DesireDataList.Sort();

                    // Data String Concat
                    if (_DesireDataList != null && _DesireDataList.Count > 0)
                    {
                        CameraData = "";
                        int i = 0;
                        foreach ((int, string) item in _DesireDataList)
                        {
                            if (i < _DesireDataList.Count)
                            {
                                CameraData += item.Item2.ToString() + ";";
                            }
                            if (i == _DesireDataList.Count - 1)
                            {
                                CameraData = CameraData.Substring(0, CameraData.Length - 1);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        CameraData = "";
                    }
                }

                // Data Transmition via Event
                Bitmap img = null;
                string urlImg = await _CvsDisplay.UpdateResults();
                if (urlImg != null)
                {
                    img = UtilityFunctions.GetImageFromUri(urlImg);
                }

                DetectModel detectModel = new DetectModel
                {
                    Text = CameraData,
                    Image = img
                };
                Debug.WriteLine("Data OCR: " + CameraData);
                detectModel.RoleOfCamera = _CameraModel?.RoleOfCamera ?? detectModel.RoleOfCamera;
                Shared.RaiseOnCameraReadDataChangeEvent(detectModel);

            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error: " + ex.Message);
#endif
            }
        }
        #endregion
    }
}
