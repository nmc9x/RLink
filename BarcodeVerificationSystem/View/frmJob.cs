using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Controller.Camera;
using BarcodeVerificationSystem.Model;
using Cognex.DataMan.SDK;
using Cognex.InSight.Remoting.Serialization;
using Cognex.InSight.Web;
using Cognex.InSight.Web.Controls;
using CommonVariable;
using DesignUI.CuzAlert;
using DesignUI.CuzMesageBox;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OperationLog.Controller;
using OperationLog.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILanguage;
using Timer = System.Windows.Forms.Timer;

namespace BarcodeVerificationSystem.View
{
    public partial class frmJob : Form
    {
        #region Variables Jobs
        private readonly DMSeries DMCamera = new DMSeries(); // MinhChau Add 11122023
        //private static  ISSeries ISCamera = new ISSeries(); // MinhChau Add 11122023
        private CvsInSight _InSight;
        public static CvsDisplay _CvsDisplay;

        #region MyRegion
        private readonly Timer _TimerDateTime = new Timer();
        private readonly string _DateTimeFormat = "yyyy/MM/dd hh:mm:ss tt";
        public double _NumberTotalsCode = 0;
        private bool _IsBinding = false;
        private bool _IsProcessing = false;
        private string _NameOfJobOld = "";
        private int countSkipFirstAlert = 0;
        private List<PODModel> _PODFormat = new List<PODModel>();
        private readonly List<PODModel> _PODList = new List<PODModel>();
        private List<string> _JobNameList = null;
        private readonly List<ToolStripLabel> _LabelStatusCameraList = new List<ToolStripLabel>();
        private readonly List<ToolStripLabel> _LabelStatusPrinterList = new List<ToolStripLabel>();
        private frmSettings _FormSettings;
        private JobModel _JobModel = null;
        private frmMain _FormMainPC = null;

        private Thread _ThreadMonitorPrinter;
        private readonly bool _IsObtainingPrintProductTemplateList = false;
        private string[] _PrintProductTemplateList = new string[] { };
        private Thread _ThreadMonitorCamera;
        private Thread _ThreadMonitorCameraIs;

        private static readonly CancellationTokenSource _ctsThreadConnectDM = new CancellationTokenSource();
        private readonly CancellationToken _ctsCamConnToken = _ctsThreadConnectDM.Token;

        private static readonly CancellationTokenSource _ctsThreadConnectIS = new CancellationTokenSource();
        private readonly CancellationToken _ctsIsToken = _ctsThreadConnectIS.Token;

        // private string[] _PrinterSeries = new string[] { "RYNAN R10", "RYNAN R20", "RYNAN B1040", "RYNAN R40", "RYNAN R60", "Standalone" };

        private readonly string SupportForPrinter = "Support for printer: RYNAN R10, RYNAN R20, RYNAN R40, RYNAN R60, RYNAN B1040.";
        private readonly string Standalone = "In this mode the software does not communicate and control the printer, the software only verifies the barcode through the camera.";
        private readonly List<string> CameraSupportNameList = new List<string>
        {
            "Camera Cognex DM series",
            "Camera Cognex IS2800 series"
        };
        private static readonly Color _Standalone = Color.DarkBlue;
        private static readonly Color _RLinkColor = Color.FromArgb(0, 171, 230);
        #endregion
        internal event EventHandler AutoAddSufixEvent;

        #endregion Variables Jobs

       
        public frmJob()
        {
            InitializeComponent();
            _InSight = new CvsInSight();
            _CvsDisplay = new CvsDisplay();
            // Reg Event InSight Camera
            _InSight.ConnectedChanged += InSight_ConnectedChanged;
            _InSight.ResultsChanged += InSight_ResultsChanged;
            _CvsDisplay.SetInSight(_InSight);
        }


        private List<ObjectResultModel> _ObjectResList = new List<ObjectResultModel>();
        private List<(int, string)> _DesireDataList = new List<(int, string)>();
        private string CameraData = "";
        private CameraModel cameraModel;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    _DesireDataList = GetDesireDataByObjectName(cameraModel).ToList();
                    _DesireDataList.Sort();

                    // Data String Concat
                    if (_DesireDataList != null && _DesireDataList.Count>0)
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
                if(urlImg != null)
                {
                    img = UtilityFunctions.GetImageFromUri(urlImg);
                }

                DetectModel detectModel = new DetectModel 
                {
                    Text = CameraData,
                    Image = img
                };
                Debug.WriteLine("Data OCR: " + CameraData);
                detectModel.RoleOfCamera = cameraModel?.RoleOfCamera ?? detectModel.RoleOfCamera;
                Shared.RaiseOnCameraReadDataChangeEvent(detectModel);

            }
            catch (Exception ex) {MessageBox.Show("Error: " + ex.Message);}
        }

        internal void Invoke_AutoAddSufixEvent()
        {
            AutoAddSufixEvent.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        private string[] _ObjectName_Temp = new string[5] { "Object_1", "Object_2", "Object_3", "Object_4", "Object_5" };
        public bool[] _IsSymbol = new bool[5];
        private readonly string suf_code = ".Result00.String";
        private readonly string suf_text = ".ReadText";
        private bool[] _EnableObject = new bool[5];
        private string[] _ObjectName = new string[5] { "Object_1", "Object_2", "Object_3", "Object_4", "Object_5" };
        private readonly ObjectResultModel[] _ObjectData = new ObjectResultModel[5];
        private const int numObject = 5;
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
            catch (Exception){}
        }
        private List<(int, string)> GetDesireDataByObjectName(CameraModel cameraModel)
        {
            if (cameraModel == null) cameraModel = Shared.GetCameraModelBasedOnIPAddress(_InSight.RemoteIPAddress);
            List<(int, string)> desireData = new List<(int, string)>();
            List<ObjectResultModel> objectResultList = new List<ObjectResultModel>();
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
            return desireData;
        }

        private async void InSight_ConnectedChanged(object sender, EventArgs e)
        {
            try
            {
                cameraModel = Shared.GetCameraModelBasedOnIPAddress(_InSight.RemoteIPAddress);
                await Task.Delay(500); // wait for preparing connection !
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
                        UnsubscribeEvents();

                    }
                    UpdateStatusLabelCamera();
                    Shared.RaiseOnCameraStatusChangeEvent();
                }
            }
            catch (Exception) { }
        }

        private void UnsubscribeEvents()
        {
            _InSight.ConnectedChanged -= InSight_ConnectedChanged;
            _InSight.ResultsChanged -= InSight_ResultsChanged;
        }

        public async Task FirstConnection()
        {
           
            var sessionInfo = new HmiSessionInfo();
            sessionInfo.SheetName = "Inspection";
            sessionInfo.CellNames = new string[1] { "A0:Z599" };
            sessionInfo.EnableQueuedResults = true; 
            sessionInfo.IncludeCustomView = true;
            await _InSight.Connect("192.168.1.42", "admin", "", sessionInfo);
            await _CvsDisplay.OnConnected();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitControls();
            DMCamera.InitCameraVariables();
            InitEvents();
            SetLanguage();
        }
      
        #region UI_Control_Event
        private void ActionResult(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }

            if (sender == tabControl1)
            {
                Shared.JobNameSelected = "";
                txtFileName.Text = "";
                if (tabControl1.SelectedIndex == 1)
                {
                    PrinterSupport(radRSeries.Checked, false);
                }
                LoadJobNameList();
            }
            else if (sender == radAfterProduction)
            {
                if (_JobModel != null)
                {
                    if (radAfterProduction.Checked && _JobModel.PrinterSeries)
                    {
                        _JobModel.JobType = JobType.AfterProduction;
                    }
                }
            }
            else if (sender == radOnProduction)
            {
                if (_JobModel != null)
                {
                    if (radOnProduction.Checked && _JobModel.PrinterSeries)
                    {
                        _JobModel.JobType = JobType.OnProduction;
                    }
                }
            }
            else if (sender == radVerifyAndPrint)
            {
                if (_JobModel != null)
                {
                    if (radVerifyAndPrint.Checked && _JobModel.PrinterSeries)
                    {
                        _JobModel.JobType = JobType.VerifyAndPrint;
                    }
                }
            }
            else if (sender == radRSeries)
            {
                if (_JobModel != null)
                {
                    if (radRSeries.Checked)
                    {
                        _JobModel.PrinterSeries = true;
                        PrinterSupport(true);
                    }
                }
            }
            else if (sender == radOther)
            {
                if (_JobModel != null)
                {
                    if (radOther.Checked)
                    {
                        _JobModel.PrinterSeries = false;
                        PrinterSupport(false);
                    }
                }
            }
            else if (sender == radCanRead)
            {
                if (_JobModel != null)
                {
                    if (radCanRead.Checked)
                    {
                        _JobModel.CompareType = CompareType.CanRead;
                    }
                    EnableForCompareType(CompareType.CanRead);
                }
            }
            else if (sender == radStaticText)
            {
                if (_JobModel != null)
                {
                    if (radStaticText.Checked)
                    {
                        _JobModel.CompareType = CompareType.StaticText;
                    }
                    EnableForCompareType(CompareType.StaticText);
                }

            }
            else if (sender == radDatabase)
            {
                if (_JobModel != null)
                {
                    if (radDatabase.Checked)
                    {
                        _JobModel.CompareType = CompareType.Database;
                    }
                    EnableForCompareType(CompareType.Database);
                    if (radAfterProduction.Checked)
                    {
                        _JobModel.JobType = JobType.AfterProduction;
                    }
                    else if (radOnProduction.Checked)
                    {
                        _JobModel.JobType = JobType.OnProduction;
                    }
                    else
                    {
                        _JobModel.JobType = JobType.VerifyAndPrint;
                    }
                }
            }
            else if (sender == btnPODFormat)
            {
                if (txtDirectoryDatabse.Text == "" || txtDirectoryDatabse.Text == null)
                {
                    CuzMessageBox.Show(Lang.PleaseSelectTheDatabaseFileFirst, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create and show dialog POD format form base on default POD format list
                using (frmPODFormat frmPODFormat = new frmPODFormat())
                {
                    frmPODFormat._DirectoryDatabase = txtDirectoryDatabse.Text;
                    txtPODFormat.Text = "";
                    frmPODFormat.ShowDialog();
                    if (frmPODFormat.DialogResult == DialogResult.OK)
                    {
                        // Get POD format from POD format form
                        _PODFormat = frmPODFormat._PODFormat;
                        if (_PODFormat.Count > 0)
                        {
                            foreach (PODModel item in _PODFormat)
                            {
                                txtPODFormat.Text += item.ToStringSample();
                            }
                        }

                        _NumberTotalsCode = frmPODFormat._NumberTotalsCode;
                        // END Get POD format from POD format form
                    }
                }
            }
            else if (sender == listBoxPrintProductTemplate)
            {
                if (_JobModel != null && radDatabase.Checked)
                {
                    _JobModel.TemplatePrint = GetSelectedPrintProductTemplate();
                }
            }
            else if (sender == listBoxJobList)
            {
                OpenJob();
            }
            else if (sender == btnSettings)
            {
                if (_FormSettings == null || _FormSettings.IsDisposed)
                {
                    _FormSettings = new frmSettings();
                    _FormSettings.Show();
                }
                else
                {
                    if (_FormSettings.WindowState == FormWindowState.Minimized)
                    {
                        _FormSettings.WindowState = FormWindowState.Normal;
                    }

                    _FormSettings.Focus();
                    _FormSettings.BringToFront();
                }
            }
            else if (sender == btnRefesh)
            {
                LoadJobNameList();
            }
            else if (sender == btnGennerate)
            {
                AutoGenerateFileName();
            }
            else if (sender == btnImportDatabase)
            {
                txtDirectoryDatabse.Text = _JobModel.DirectoryDatabase = OpenDirectoryFileDatabase();

                _PODFormat.Clear();
                txtPODFormat.Text = "";
            }
            else if (sender == btnNext)
            {
                if (Shared.JobNameSelected == "")
                {
                    JobModel jobModel = Shared.GetJob(txtFileName.Text + Shared.Settings.JobFileExtension);
                    if (jobModel == null && txtFileName.Text != "")
                    {
                        CuzMessageBox.Show(Lang.PleaseSaveTheWorkYouJustEntered, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    CuzMessageBox.Show(Lang.PleaseChooseAJobOrCreateANewOne, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (_JobModel != null && _JobModel.CompareType == CompareType.Database && !CheckExistTemplatePrint(_JobModel.TemplatePrint) && _JobModel.PrinterSeries)
                    {
                        CuzMessageBox.Show(Lang.CheckExistTemplatePrinter, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (Shared.Settings.PrinterList.FirstOrDefault().CheckAllPrinterSettings && _JobModel.CompareType == CompareType.Database && _JobModel.PrinterSeries)
                    {
                        PrinterSettingsModel printerSettingsModel = Shared.GetSettingsPrinter();
                        if (printerSettingsModel.IsSupportHttpRequest)
                        {
                            if (printerSettingsModel.PodDataType != 1)
                            {
                                CuzMessageBox.Show(Lang.DataTypeMustBeRAWData, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            CuzMessageBox.Show(Lang.PrinterNotSupportHttpRequest, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }

                    this.Hide();

                    _FormMainPC?.Dispose();
                    if (_FormMainPC == null || _FormMainPC.IsDisposed)
                    {
                        _FormMainPC = new frmMain(this);
                        _FormMainPC.Show();
                    }
                    else
                    {
                        if (_FormMainPC.WindowState == FormWindowState.Minimized)
                        {
                            _FormMainPC.WindowState = FormWindowState.Normal;
                        }

                        _FormMainPC.Focus();
                        _FormMainPC.BringToFront();
                    }

                }
               
            }
            else if (sender == btnSave)
            {
                if (_JobModel != null)
                {
                    _JobModel.TemplatePrint = GetSelectedPrintProductTemplate();
                    _JobModel.NumberTotalsCode = _NumberTotalsCode;
                    _JobModel.JobStatus = JobStatus.NewlyCreated;
                }

                SaveJob();
            }
            else if (sender == btnAbout)
            {
                frmAbout about = new frmAbout();
                about.ShowDialog();
            }
            else if (sender == btnDelete)
            {
                DeleteJob();
            }
            else if (sender == btnRefeshTemplate)
            {
                _PrintProductTemplateList = new string[] { };
                ObtainPrintProductTemplateList();
                UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);
            }
        }
        public static void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Color.FromArgb(210, 232, 255)
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Enabled)
                {
                    radioButton.BackColor = radioButton.Checked ? Color.FromArgb(0, 170, 230) : Color.White;
                }
            }
        }
        private void CboSupportForCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbbSupportCam = (ComboBox)sender;
            switch (cbbSupportCam.SelectedIndex)
            {
                case 0: // DM Series

                    break;
                case 1: // IS2800 Series

                    break;
                default:
                    break;
            }
        }
        private void BtnViewLog_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = CommVariables.PathProgramDataApp;
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|Job files (*.rvis)|*.rvis|Database files (*.db)|*.db|csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    //openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 5;
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog.FileName;
                        Process.Start("notepad.exe", selectedFile);
                    }
                }
            }
            catch (Exception) { }

        }
        private void ListBoxJobList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1 || (sender as ListBox).Items.Count == 0) return;
            try
            {
                var job = Shared.GetJob((sender as ListBox).Items[e.Index].ToString());
                Rectangle headItemRect = new Rectangle(0, e.Bounds.Y + 4, 8, e.Bounds.Height - 10);
                using (Brush brush = new SolidBrush(_Standalone))
                    if (!job.PrinterSeries)
                        e.Graphics.FillRectangle(brush, headItemRect);
            }
            catch
            {

            }
        }
        private void JobType_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (!radioButton.Enabled)
                {
                    radioButton.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    if (radioButton.Checked)
                    {
                        radioButton.BackColor = Color.FromArgb(0, 171, 230);
                    }
                    else
                    {
                        radioButton.BackColor = Color.White;
                    }
                }
            }
        }
        private void TxtSearchTemplate_TextChanged(object sender, EventArgs e)
        {
            string keyWord = txtSearchTemplate.Text.ToLower();
            if (_PrintProductTemplateList.Count() > 0)
            {
                UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList, keyWord);
            }
        }
        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            toolStripDateTime.Text = DateTime.Now.ToString(_DateTimeFormat);
        }
        private void TxtPODFormat_TextChanged(object sender, EventArgs e)
        {
            if (_JobModel != null && radDatabase.Checked)
            {
                _JobModel.PODFormat = _PODFormat;
            }
        }
        private void TxtDirectoryDatabse_TextChanged(object sender, EventArgs e)
        {
            if (_JobModel != null && radDatabase.Checked)
            {
                _JobModel.DirectoryDatabase = txtDirectoryDatabse.Text;
            }
        }
        private void TxtStaticText_TextChanged(object sender, EventArgs e)
        {
            if (_JobModel != null && radStaticText.Checked)
            {
                _JobModel.StaticText = txtStaticText.Text;
            }
        }
        private void TxtFileName_TextChanged(object sender, EventArgs e)
        {
            if (_JobModel != null)
            {
                _JobModel.FileName = txtFileName.Text;
            }
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyWord = txtSearch.Text.ToLower();
            if (_JobNameList != null)
            {
                listBoxJobList.Items.Clear();
                foreach (string templateName in _JobNameList)
                {
                    if (templateName.ToLower().Contains(keyWord))
                    {
                        var jobModel = Shared.GetJob(templateName);
                        if (jobModel != null && jobModel.JobStatus != JobStatus.Deleted)
                            listBoxJobList.Items.Add(templateName);
                    }
                }
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void FrmJob_Load(object sender, EventArgs e)
        {
            LoadJobNameList();

            radRSeries.Checked = _JobModel.PrinterSeries;
            radOther.Checked = !_JobModel.PrinterSeries;

            if (_JobModel.JobType == JobType.AfterProduction)
                radAfterProduction.Checked = true;
            else if (_JobModel.JobType == JobType.OnProduction)
                radOnProduction.Checked = true;
            else
                radVerifyAndPrint.Checked = true;

            EnableUIPrinting();

            _LabelStatusCameraList.Add(lblStatusCamera01);
            UpdateStatusLabelCamera();

            // Show icon printer status
            _LabelStatusPrinterList.Add(lblStatusPrinter01);
            UpdateStatusLabelPrinter();

            EnableUIPrinting();
            // Show icon sensor controller
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);

            tblJobType.Enabled = false;
            UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);

            pnlStandaloneColor.BackColor = _Standalone;
            pnlRLinkSeriesColor.BackColor = _RLinkColor;
        }
        #endregion UI_Control_Event

        #region Orther_Events
        private void Shared_OnPrintingStateChange(object sender, EventArgs e)
        {
            EnableUIPrinting();
        }
        private void Shared_OnPrinterStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelPrinter();
            ObtainPrintProductTemplateList();
        }
        private void Shared_OnSensorControllerChangeEvent(object sender, EventArgs e)
        {
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);
        }
        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }
        private void Shared_OnPrinterDataChange(object sender, EventArgs e)
        {
            if (sender is PODDataModel)
            {
                PODDataModel podDataModel = sender as PODDataModel;
                try
                {
                    string[] pODcommand = podDataModel.Text.Split(';');
                    PODResponseModel PODResponseModel = new PODResponseModel
                    {
                        Command = pODcommand[0]
                    };

                    if (PODResponseModel != null)
                    {
                        if (PODResponseModel.Command == "RSLI")
                        {
                            pODcommand = pODcommand.Skip(1).ToArray();
                            pODcommand = pODcommand.Take(pODcommand.Count() - 1).ToArray();
                            PODResponseModel.Template = pODcommand;
                            if (podDataModel.RoleOfPrinter == RoleOfStation.ForProduct)
                            {
                                // List print template
                                _PrintProductTemplateList = PODResponseModel.Template;
                                UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);
                            }
                            else { }
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        //--CAMEARA--//
        private void Shared_OnCameraStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelCamera();
        }
        //private void DMCamera_UpdateLabelStatusEvent(object sender, EventArgs e)
        //{
        //    UpdateStatusLabelCamera();
        //}
        private void Shared_OnCameraTriggerOnChange(object sender, EventArgs e)
        {
            switch (cameraModel.CameraType)
            {
                case CameraType.DM:
                    foreach (DataManSystem dataManSystem in DMCamera._DataManSystemList)
                    {
                        try
                        {
                            dataManSystem.SendCommand("TRIGGER ON");
                        }
                        catch (Exception) { }
                    }
                    break;
                case CameraType.IS:
                    _ = _InSight.ManualAcquire();
                    break;
                default:
                    break;
            }
           
        }
        private void Shared_OnCameraTriggerOffChange(object sender, EventArgs e)
        {
            foreach (DataManSystem dataManSystem in DMCamera._DataManSystemList)
            {
                try
                {
                    dataManSystem.SendCommand("TRIGGER OFF");
                }
                catch (Exception) { }
            }
        }
        private void Shared_OnCameraOutputSignalChange(object sender, EventArgs e)
        {
            foreach (DataManSystem dataManSystem in DMCamera._DataManSystemList)
            {
                try
                {
                    DmccResponse response = dataManSystem.SendCommand("OUTPUT.USER1");
                }
                catch (Exception) { }
            }
        }
        //--END CAMEARA--//

        private void PODController_OnPODReceiveDataEvent(object sender, EventArgs e)
        {
            if (sender is PODDataModel)
            {
                Shared.RaiseOnPrinterDataChangeEvent(sender as PODDataModel);
            }
        }
        private void SensorController_OnPODReceiveMessageEvent(object sender, EventArgs e)
        {
            Shared.RaiseOnRepeatTCPMessageChange(sender);
        }
        #endregion Orther_Events

        #region Utility_Function
        private void SetLanguage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetLanguage()));
                return;
            }

            btnSettings.Text = Lang.Settings;
            btnExit.Text = Lang.Exit;
            btnAbout.Text = Lang.About;

            pnlJobInfomation.Text = Lang.JobDetails;
            lblJobName.Text = Lang.FileName;
            lblCompareType.Text = Lang.CompareType;
            lblStaticText1.Text = Lang.StaticText;
            lblPODFormat.Text = Lang.PODFormat;
            lblTemplatePrint.Text = Lang.TemplateName;
            btnNext.Text = Lang.Next;
            lblPrinterSeries.Text = Lang.PrinterSeries;
            lblTemplate.Text = Lang.TemplateName;
            lblJobTypeInput.Text = Lang.JobType;
            radAfterProduction.Text = Lang.AfterProduction;
            radOnProduction.Text = Lang.OnProduction;
            radVerifyAndPrint.Text = Lang.VerifyAndPrint;

            lblJobType.Text = Lang.JobType;
            lblJobStatus.Text = Lang.JobStatus;
            //radRSeries.Text = Lang.Enable;
            //radOther.Text = Lang.Disable;
            //btnSave.Text = Lang.Save;
            btnSave.Text = Lang.Save;
            lblSupportForCamera.Text = Lang.SupportForCamera;
            //lblJobList.Text = Lang.JobList;
            //lblSearch.Text = Lang.Search;
            //lblJobDetails.Text = Lang.JobDetails;
            lblCompare.Text = Lang.CompareType;
            lblStaticText.Text = Lang.StaticText;
            radCanRead.Text = Lang.CanRead;
            radStaticText.Text = Lang.StaticText;

            radDatabase.Text = Lang.Database;
            lblImportDatabase.Text = Lang.ImportDatabase;
            lblPODFromat.Text = Lang.PODFormat;
            //lblJobSystem.Text = Lang.JobSystem;
            lblFileName.Text = Lang.JobList;

            lblStatusCamera01.Text = Lang.CameraTMP;
            lblStatusPrinter01.Text = Lang.Printer;
            lblSensorControllerStatus.Text = Lang.SensorController;

            lblToolStripVersion.Text = Lang.Version + ": " + Properties.Settings.Default.SoftwareVersion;
            btnDelete.Text = Lang.Delete;

            tabPage1.Text = Lang.SelectJob;
            tabPage2.Text = Lang.CreateANewJob;
        }
        private void InitControls()
        {
#if DEBUG
            DebugVirtual();
#endif
            _LabelStatusCameraList.Add(lblStatusCamera01);
            UpdateStatusLabelCamera();
            _LabelStatusPrinterList.Add(lblStatusPrinter01);
            UpdateStatusLabelPrinter();
            _NameOfJobOld = "";
            CreateJob();
            cboSupportForCamera.DataSource = CameraSupportNameList;
            cboSupportForCamera.SelectedIndex = 0;
            _TimerDateTime.Start();
            _NameOfJobOld = "";
            Shared.JobNameSelected = "";
            var podText = new PODModel(0, "", PODModel.TypePOD.TEXT, "");
            _PODList.Add(podText);

            for (int index = 1; index <= 20; index++)
            {
                var podVCD = new PODModel(index, "", PODModel.TypePOD.FIELD, "");
                _PODList.Add(podVCD);
            }

            MonitorCameraConnection();
            MonitorPrinterConnection();
            MonitorSensorControllerConnection();
            MonitorListenerServer();
        }

      

        private void InitEvents()
        {
            _TimerDateTime.Tick += TimerDateTime_Tick;
            btnGennerate.Click += ActionResult;
            radCanRead.CheckedChanged += ActionResult;
            radCanRead.EnabledChanged += JobType_EnabledChanged;
            radStaticText.CheckedChanged += ActionResult;
            radStaticText.EnabledChanged += JobType_EnabledChanged;
            radDatabase.CheckedChanged += ActionResult;
            radDatabase.EnabledChanged += JobType_EnabledChanged;

            radRSeries.CheckedChanged += ActionResult;
            radOther.CheckedChanged += ActionResult;
            radRSeries.CheckedChanged += RadioButton_CheckedChanged;
            radOther.CheckedChanged += RadioButton_CheckedChanged;

            radAfterProduction.CheckedChanged += RadioButton_CheckedChanged;
            radAfterProduction.CheckedChanged += ActionResult;
            radAfterProduction.EnabledChanged += JobType_EnabledChanged; ;
            radOnProduction.CheckedChanged += RadioButton_CheckedChanged;
            radOnProduction.CheckedChanged += ActionResult;
            radOnProduction.EnabledChanged += JobType_EnabledChanged;
            radVerifyAndPrint.CheckedChanged += RadioButton_CheckedChanged;
            radVerifyAndPrint.CheckedChanged += ActionResult;
            radVerifyAndPrint.EnabledChanged += JobType_EnabledChanged;
            txtStaticText.TextChanged += TxtStaticText_TextChanged; ;
            txtDirectoryDatabse.TextChanged += TxtDirectoryDatabse_TextChanged; ;
            txtFileName.TextChanged += TxtFileName_TextChanged; ;
            txtPODFormat.TextChanged += TxtPODFormat_TextChanged; ;

            txtSearch.TextChanged += TxtSearch_TextChanged; ;
            txtSearchTemplate.TextChanged += TxtSearchTemplate_TextChanged;

            btnPODFormat.Click += ActionResult;
     
            btnSettings.Click += ActionResult;
            listBoxJobList.SelectedIndexChanged += ActionResult;
            listBoxPrintProductTemplate.SelectedIndexChanged += ActionResult;
            btnRefesh.Click += ActionResult;
            btnImportDatabase.Click += ActionResult;
            Shared.OnLanguageChange += Shared_OnLanguageChange;

            Load += FrmJob_Load;
            tabControl1.SelectedIndexChanged += ActionResult;
            tabPage2.Click += ActionResult;

            btnExit.Click += BtnClose_Click;
            btnNext.Click += ActionResult;
            btnSave.Click += ActionResult;
            btnAbout.Click += ActionResult;
            btnDelete.Click += ActionResult;
            btnRefeshTemplate.Click += ActionResult;
            radCanRead.CheckedChanged += RadioButton_CheckedChanged;
            radDatabase.CheckedChanged += RadioButton_CheckedChanged;
            radStaticText.CheckedChanged += RadioButton_CheckedChanged;
            BtnViewLog.Click += BtnViewLog_Click;

            cboSupportForCamera.DrawMode = DrawMode.OwnerDrawVariable;
            cboSupportForCamera.Height = 40;
            cboSupportForCamera.DropDownHeight = 150;
            cboSupportForCamera.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSupportForCamera.DrawItem += ComboBoxCustom.myComboBox_DrawItem;
            cboSupportForCamera.MeasureItem += ComboBoxCustom.cbo_MeasureItem;
            cboSupportForCamera.SelectedIndexChanged += CboSupportForCamera_SelectedIndexChanged;
            listBoxJobList.DrawItem += ListBoxJobList_DrawItem;

            Shared.OnPrintingStateChange += Shared_OnPrintingStateChange;
            Shared.OnPrinterStatusChange += Shared_OnPrinterStatusChange;
            Shared.OnPrinterDataChange += Shared_OnPrinterDataChange;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnSensorControllerChangeEvent += Shared_OnSensorControllerChangeEvent;

            //Camera Event
            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;
            Shared.OnCameraTriggerOnChange += Shared_OnCameraTriggerOnChange;
            Shared.OnCameraTriggerOffChange += Shared_OnCameraTriggerOffChange;
            Shared.OnCameraOutputSignalChange += Shared_OnCameraOutputSignalChange;
            //DMCamera.UpdateLabelStatusEvent += DMCamera_UpdateLabelStatusEvent;

            AutoAddSufixEvent += FrmJob_AutoAddSufixEvent;

        }

        private void FrmJob_AutoAddSufixEvent(object sender, EventArgs e)
        {
            AutoAddSuffixes(cameraModel);
        }

        private void DebugVirtual()
        {
            BtnViewLog.Visible = true;
        }
        private async void MonitorListenerServer()
        {
            try
            {
                await StartListenerServer();
            }
            catch (Exception exx)
            {
                System.Windows.MessageBox.Show("ERROR: " + exx);
            }
        }
        private async Task StartListenerServer()
        {
            StringBuilder url = new StringBuilder("http://");
            url.Append(Shared.GetLocalIPAddress());
            url.Append("/");
            string[] prefixes = new string[] { url.ToString() };

            var server = new CameraListenerServer(prefixes);
            await server.StartAsync();
        }
        private void PrinterSupport(bool printerSub, bool isAlert = true)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PrinterSupport(printerSub)));
                return;
            }
            if (countSkipFirstAlert == 0)
            {
                countSkipFirstAlert++;
                return;
            }
            string content = printerSub ? SupportForPrinter : Standalone;

            if (isAlert) CuzAlert.Show(content, Alert.enmType.Info, new System.Drawing.Size(500, 90), new System.Drawing.Point(Location.X, Location.Y), this.Size);
            if (printerSub)
            {
                radDatabase.Checked = true;
                radCanRead.Enabled = false;
                radStaticText.Enabled = false;
                radDatabase.Enabled = true;
                tblJobType.Enabled = true;
                DatabaseChecked(true, true);
            }
            else
            {
                radCanRead.Enabled = true;
                radCanRead.Checked = true;
                if (_JobModel.CompareType == CompareType.Database) _JobModel.JobType = JobType.StandAlone;
                radStaticText.Enabled = true;
                radDatabase.Enabled = true;
                tblJobType.Enabled = false;
                DatabaseChecked(false, true);
            }

        }
        private void DatabaseChecked(bool isChecked, bool isTemplate)
        {
            if (isChecked)
            {
                txtDirectoryDatabse.Enabled = true;
                txtPODFormat.Enabled = true;

                if (isTemplate)
                {
                    txtSearchTemplate.Enabled = true;
                    btnRefeshTemplate.Enabled = true;
                    listBoxPrintProductTemplate.Enabled = true;
                    listBoxPrintProductTemplate.ClearSelected();
                    txtSearchTemplate.BackColor = Color.White;
                }

                btnImportDatabase.Enabled = true;
                btnPODFormat.Enabled = true;

                txtDirectoryDatabse.BackColor = Color.White;
                txtPODFormat.BackColor = Color.White;

                txtStaticText.Text = "";
                txtDirectoryDatabse.Text = "";
                txtPODFormat.Text = "";
            }
            else
            {
                txtDirectoryDatabse.Enabled = false;
                txtPODFormat.Enabled = false;

                if (isTemplate)
                {
                    txtSearchTemplate.Enabled = false;
                    btnRefeshTemplate.Enabled = false;
                    listBoxPrintProductTemplate.Enabled = false;
                    listBoxPrintProductTemplate.ClearSelected();
                    txtSearchTemplate.BackColor = Color.WhiteSmoke;
                }

                btnImportDatabase.Enabled = false;
                btnPODFormat.Enabled = false;

                txtDirectoryDatabse.BackColor = Color.WhiteSmoke;
                txtPODFormat.BackColor = Color.WhiteSmoke;

                txtStaticText.Text = "";
                txtDirectoryDatabse.Text = "";
                txtPODFormat.Text = "";
            }
        }
        public void ShowForm()
        {
            this.Show();
        }
        private bool CheckExistTemplatePrint(string tmp)
        {
            if (_PrintProductTemplateList.Count() <= 0)
            {
                return false;
            }
            foreach (var item in _PrintProductTemplateList)
            {
                if (item == tmp)
                {
                    return true;
                }
            }
            return false;
        }
        private string GetSelectedPrintProductTemplate()
        {
            string printTemplate = "";
            object selectedItem = null;
            Invoke(new Action(() =>
            {
                selectedItem = listBoxPrintProductTemplate.SelectedItem;
            }));

            if (selectedItem != null && selectedItem is ItemCustomModel)
            {
                ItemCustomModel itemCustomModel = selectedItem as ItemCustomModel;
                if (_PrintProductTemplateList != null && itemCustomModel.Value >= 0 && itemCustomModel.Value < _PrintProductTemplateList.Count())
                {
                    printTemplate = _PrintProductTemplateList[itemCustomModel.Value];
                }
            }
            return printTemplate;
        }
        private void AutoGenerateFileName()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AutoGenerateFileName()));
                return;
            }
            string defaultName = string.Format("{0}_{1}", DateTime.Now.ToString(Shared.Settings.JobDateTimeFormat), Shared.Settings.JobFileNameDefault);
            txtFileName.Text = defaultName;
        }
        private void UpdateUIClearJobInformation()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUIClearJobInformation()));
                return;
            }
            _JobModel = new JobModel
            {
                CompareType = CompareType.CanRead,
                StaticText = "",
                DirectoryDatabase = "",
                PODFormat = _PODFormat,
                FileName = "",
                UserCreate = Shared.LoggedInUser.FullName,
                AutoLoad = true
            };
            listBoxJobList.Enabled = true;
            UpdateUIClearTextBoxInfo(_JobModel);
        }
        private string OpenDirectoryFileDatabase()
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                string filePath = "";
                openFileDialog1.Filter = "Database files (*.csv, *.txt)|*.csv;*.txt";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;
                }
                return filePath;
            }
        }
        private void CreateJob()
        {
            _NameOfJobOld = "";
            _JobModel = new JobModel();
            _JobModel.CompareType = CompareType.CanRead;
            _JobModel.StaticText = "";
            _JobModel.DirectoryDatabase = "";
            _JobModel.PODFormat = _PODFormat;
            _JobModel.FileName = "";
            _JobModel.UserCreate = Shared.LoggedInUser.FullName;
            _JobModel.AutoLoad = true;
            _JobModel.PrinterSeries = _JobModel.PrinterSeries;
            _JobModel.TemplatePrint = "";
            _JobModel.JobStatus = JobStatus.NewlyCreated;
        }
        private void OpenJob()
        {
            // Check existing processing
            if (_IsProcessing || listBoxJobList.SelectedItem == null)
            {
                return;
            }
            _IsProcessing = true;

            // Get Job name with extension
            _NameOfJobOld = listBoxJobList.SelectedItem.ToString();

            // Open Job file
            Shared.JobNameSelected = _NameOfJobOld;
            _JobModel = Shared.GetJob(_NameOfJobOld);
            UpdateUIJobInformation(_JobModel);

            _IsProcessing = false;
        }
        private void DeleteJob()
        {
            try
            {
                if (_NameOfJobOld != "")
                {
                    var jobModel = Shared.GetJob(_NameOfJobOld);

                    var permission = !(Shared.LoggedInUser.Role == 1);
                    if (!permission)
                    {
                        var isNewCreate = jobModel.JobStatus == JobStatus.NewlyCreated;
                        if (!isNewCreate)
                        {
                            string warningMsg = Lang.YouDoNotHavePermission;
                            CuzMessageBox.Show(warningMsg, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string message = Lang.AreYouSureYouWantToDeleteFile + "\r\n" + _NameOfJobOld;
                    DialogResult result = CuzMessageBox.Show(message, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Reload Job name list
                        jobModel.JobStatus = JobStatus.Deleted;
                        string filePath = CommVariables.PathJobsApp + jobModel.FileName + Shared.Settings.JobFileExtension;
                        jobModel.SaveFile(filePath);
                    }

                    LoadJobNameList();
                }
            }
            catch
            {
                LoadJobNameList();
            }
        }
        private JobModel InitJobModel()
        {
            JobModel job = new JobModel();

            var isRSeries = radRSeries.Checked;
            job.PrinterSeries = isRSeries;
            job.FileName = txtFileName.Text;
            if (isRSeries)
            {
                job.CompareType = CompareType.Database;

                if (radAfterProduction.Checked)
                {
                    job.JobType = JobType.AfterProduction;
                }
                else if (radOnProduction.Checked)
                {
                    job.JobType = JobType.OnProduction;
                }
                else if (radVerifyAndPrint.Checked)
                {
                    job.JobType = JobType.VerifyAndPrint;
                }

                job.DirectoryDatabase = txtDirectoryDatabse.Text;
                job.PODFormat = _PODFormat;
                job.StaticText = "";
                job.TemplatePrint = GetSelectedPrintProductTemplate();
                job.NumberTotalsCode = _NumberTotalsCode;
                job.JobStatus = JobStatus.NewlyCreated;
            }
            else
            {
                job.JobType = JobType.StandAlone;
                job.TemplatePrint = "";
                job.StaticText = "";
                job.PODFormat = new List<PODModel>();
                job.DirectoryDatabase = "";
                if (radCanRead.Checked)
                {
                    job.CompareType = CompareType.CanRead;
                }
                else if (radStaticText.Checked)
                {
                    job.CompareType = CompareType.StaticText;
                    job.StaticText = txtStaticText.Text;
                }
                else if (radDatabase.Checked)
                {
                    job.CompareType = CompareType.Database;
                    job.DirectoryDatabase = txtDirectoryDatabse.Text;
                    job.PODFormat = _PODFormat;
                    job.NumberTotalsCode = _NumberTotalsCode;
                }
            }

            return job;
        }
        private void SaveJob()
        {
            try
            {
                _JobModel = InitJobModel();
                // Check current Job has null
                if (_JobModel != null)
                {
                    // Check Job name is empty
                    string JobName = _JobModel.FileName;
                    if (JobName == "")
                    {
                        CuzMessageBox.Show(Lang.PleaseInputJobName, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_JobModel.PrinterSeries)
                    {
                        if (_JobModel.CompareType == CompareType.Database)
                        {
                            // Check Database
                            string databasePath = _JobModel.DirectoryDatabase;
                            if (_JobModel.CompareType == CompareType.Database && databasePath == "")
                            {
                                CuzMessageBox.Show(Lang.PleaseSelectDatabasePath, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            // Check POD format
                            string podFormat = _JobModel.PODFormat.ToString();

                            if (_JobModel.CompareType == CompareType.Database && podFormat == "" || txtPODFormat.Text == "")
                            {
                                CuzMessageBox.Show(Lang.PleaseSelectPODFormat, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_JobModel != null && _JobModel.CompareType == CompareType.Database && !CheckExistTemplatePrint(_JobModel.TemplatePrint) && _JobModel.PrinterSeries)
                            {
                                CuzMessageBox.Show(Lang.CheckExistTemplatePrinter, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (_JobModel.CompareType == CompareType.Database)
                        {
                            // Check Database
                            string databasePath = _JobModel.DirectoryDatabase;
                            if (_JobModel.CompareType == CompareType.Database && databasePath == "")
                            {
                                CuzMessageBox.Show(Lang.PleaseSelectDatabasePath, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            // Check POD format
                            string podFormat = _JobModel.PODFormat.ToString();

                            if (_JobModel.CompareType == CompareType.Database && podFormat == "" || txtPODFormat.Text == "")
                            {
                                CuzMessageBox.Show(Lang.PleaseSelectPODFormat, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            if (_JobModel.CompareType == CompareType.StaticText)
                            {
                                if (_JobModel.StaticText == "")
                                {
                                    CuzMessageBox.Show(Lang.PleaseEnterTheStaticText, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                _JobModel.StaticText = "";
                            }
                        }
                    }

                    // Check Job name has exist and confirm replace
                    if (Shared.CheckJobHasExist(JobName))
                    {
                        var tmpJob = Shared.GetJob(JobName + Shared.Settings.JobFileExtension);
                        if (tmpJob != null)
                        {
                            if (tmpJob.JobStatus == JobStatus.Deleted)
                            {
                                string oldJobPath = CommVariables.PathJobsApp + JobName + Shared.Settings.JobFileExtension;
                                string newJobPath = CommVariables.PathJobsApp + JobName + "_Old_" +
                                    DateTime.Now.ToString("yyMMddHHmmss") + Shared.Settings.JobFileExtension;
                                try
                                {
                                    File.Move(oldJobPath, newJobPath);
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                string message = Lang.DoYouWantToReplaceExistingTemplate + "\r\n" + JobName + Shared.Settings.JobFileExtension;
                                DialogResult result = CuzMessageBox.Show(message, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    // Continue execute code below

                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }

                    if (JobName != "")
                    {
                        // Perform delete Job file
                        Shared.DeleteJob(_JobModel);
                    }
                    // Save Job
                    string filePath = CommVariables.PathJobsApp + _JobModel.FileName + Shared.Settings.JobFileExtension;
                    _JobModel.SaveFile(filePath);
                    // END Save Job
                    // Reload Job name list
                    Shared.JobNameSelected = JobName + Shared.Settings.JobFileExtension;
                    _PODFormat.Clear();
                    listBoxPrintProductTemplate.ClearSelected();
                }

                DialogResult dialogResult = CuzMessageBox.Show(Lang.SuccessfulNewJobCreationStartTheProcess, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (Shared.Settings.PrinterList.FirstOrDefault().CheckAllPrinterSettings && _JobModel.CompareType == CompareType.Database && _JobModel.PrinterSeries)
                    {
                        PrinterSettingsModel printerSettingsModel = Shared.GetSettingsPrinter();

                        if (printerSettingsModel.PodDataType != 1)
                        {
                            radOther.Checked = true;
                            txtFileName.Text = "";
                            UpdateUIClearJobInformation();
                            CuzMessageBox.Show(Lang.DataTypeMustBeRAWData, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    this.Hide();
                    _FormMainPC?.Dispose();
                    if (_FormMainPC == null || _FormMainPC.IsDisposed)
                    {
                        _FormMainPC = new frmMain(this);
                        _FormMainPC.Show();
                    }
                    else
                    {
                        if (_FormMainPC.WindowState == FormWindowState.Minimized)
                        {
                            _FormMainPC.WindowState = FormWindowState.Normal;
                        }

                        _FormMainPC.Focus();
                        _FormMainPC.BringToFront();
                    }

                    PrinterSupport(_JobModel.PrinterSeries, false);
                    txtFileName.Text = "";
                  
                }
                else
                {
                    UpdateUIClearJobInformation();
                }
                return;
            }
            catch
            {
                CuzMessageBox.Show(Lang.NewJobCreationFailed, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }
        }
        private void UpdateUIClearTextBoxInfo(JobModel jobModel)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUIClearTextBoxInfo(jobModel)));
                return;
            }


            Shared.JobNameSelected = "";
            lblJobNameInfo.Text = "";
            lblCompareTypeInfo.Text = "";
            lblStaticTextInfo.Text = "";
            lblPODFormatInfo.Text = "";
            lblTemplatePrintInfo.Text = "";
            txtJobType.Text = "";
            txtFileName.Text = jobModel.FileName;
            txtJobStatus.Text = "";

            txtStaticText.Text = jobModel.StaticText;

            txtDirectoryDatabse.Text = jobModel.DirectoryDatabase;

            txtPODFormat.Text = "";

            lblStaticTextInfo.BackColor = Color.White;
            lblPODFormatInfo.BackColor = Color.White;
            lblTemplatePrintInfo.BackColor = Color.White;
            txtJobType.BackColor = Color.White;

        }
        private void LoadJobNameList()
        {
            // Check existing processing
            if (_IsProcessing)
            {
                return;
            }
            Invoke(new Action(() =>
            {
                picLoading.Visible = false;
            }));
            Thread threadLoadJobNameList = new Thread(() =>
            {
                _IsProcessing = true;
                // Update user interface
                _NameOfJobOld = "";
                UpdateUIClearJobInformation();
                UpdateUILoadJobNameList(false);

                // Clear list box of Job name
                Invoke(new Action(() =>
                {
                    listBoxJobList.Items.Clear();
                }));

                // Get Job name list
                _JobNameList = null;
                _JobNameList = Shared.GetJobNameList();
                //Update Job name list to user interface
                Invoke(new Action(() =>
                {
                    if (_JobNameList != null)
                    {
                        foreach (string JobName in _JobNameList)
                        {
                            var jobModel = Shared.GetJob(JobName);
                            if (jobModel != null && jobModel.JobStatus != JobStatus.Deleted)
                                listBoxJobList.Items.Add(JobName);
                        }
                    }
                }));

                _IsProcessing = false;
                Thread.Sleep(5);
                Invoke(new Action(() =>
                {
                    picLoading.Visible = true;
                }));
                // Update user interface
                UpdateUILoadJobNameList(true);
            });
            threadLoadJobNameList.IsBackground = true;
            threadLoadJobNameList.Start();
        }
        private void UpdateUILoadJobNameList(bool isEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUILoadJobNameList(isEnable)));
                return;
            }

            picLoading.Visible = !isEnable;
            listBoxJobList.Enabled = isEnable;
        }
        private void UpdateUIJobInformation(JobModel jobModel)
        {

            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUIJobInformation(jobModel)));
                return;
            }

            if (jobModel != null)
            {
                _IsBinding = true;
                string pODFormat = "";

                if (jobModel.CompareType == CompareType.CanRead)
                {
                    lblCompareTypeInfo.Text = Lang.CanRead;
                    lblStaticText1.Text = Lang.StaticText;
                    lblStaticTextInfo.Text = jobModel.StaticText;
                    txtJobType.Text = jobModel.JobType.ToFriendlyString();
                }
                else if (jobModel.CompareType == CompareType.StaticText)
                {
                    lblCompareTypeInfo.Text = Lang.StaticText;
                    lblStaticText1.Text = Lang.StaticText;
                    lblStaticTextInfo.Text = jobModel.StaticText;
                    txtJobType.Text = jobModel.JobType.ToFriendlyString();
                }
                else
                {
                    lblCompareTypeInfo.Text = Lang.Database;
                    txtJobType.Text = jobModel.JobType.ToFriendlyString();
                    lblStaticText1.Text = Lang.Totals;
                    lblStaticTextInfo.Text = jobModel.NumberTotalsCode.ToString();
                }

                if (jobModel.JobType == JobType.StandAlone)
                {
                    if (jobModel.CompareType == CompareType.CanRead)
                        lblStaticTextInfo.BackColor = Color.WhiteSmoke;
                    else
                        lblStaticTextInfo.BackColor = Color.White;

                    if (jobModel.CompareType != CompareType.Database)
                        lblPODFormatInfo.BackColor = Color.WhiteSmoke;
                    else
                        lblPODFormatInfo.BackColor = Color.White;

                    lblTemplatePrintInfo.BackColor = Color.WhiteSmoke;
                    txtJobType.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    lblStaticTextInfo.BackColor = Color.White;
                    lblPODFormatInfo.BackColor = Color.White;
                    lblTemplatePrintInfo.BackColor = Color.White;
                    txtJobType.BackColor = Color.White;
                }

                //txtDirectoryDatabseInfo.Text = jobModel.DirectoryDatabase;
                foreach (var item in jobModel.PODFormat)
                {
                    if (item.Type == PODModel.TypePOD.FIELD)
                        pODFormat += item.ToString();
                    else if (item.Type == PODModel.TypePOD.TEXT)
                        pODFormat += item.ToStringSample();
                }
                txtJobStatus.Text = jobModel.JobStatus.ToFriendlyString();
                lblPODFormatInfo.Text = pODFormat;
                lblJobNameInfo.Text = jobModel.FileName;
                lblTemplatePrintInfo.Text = jobModel.TemplatePrint;
                _IsBinding = false;
            }
            else
            {
                UpdateUIClearJobInformation();
            }
        }
        private void EnableForCompareType(CompareType compareType)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableForCompareType(compareType)));
                return;
            }

            var isTemplate = radRSeries.Checked;
            if (compareType == CompareType.CanRead)
            {
                txtStaticText.ReadOnly = true;
                txtStaticText.Text = "";
                DatabaseChecked(false, isTemplate);
            }
            else if (compareType == CompareType.StaticText)
            {
                txtStaticText.ReadOnly = false;
                txtStaticText.Text = "";
                DatabaseChecked(false, isTemplate);
            }
            else if (compareType == CompareType.Database)
            {
                txtStaticText.ReadOnly = true;
                txtStaticText.Text = "";
                DatabaseChecked(true, isTemplate);
            }
        }
        public void Exit()
        {
            DialogResult dialogResult = CuzMessageBox.Show(Lang.DoYouWantExitApplication, Lang.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //Save history
                    LoggingController.SaveHistory(
                        Lang.Exit,
                        Lang.LogOut,
                        Lang.LogoutSuccessfully,
                        SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                        LoggingType.LogedOut);

                    this.Close();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        #endregion Utility_Function

        #region UpdateUI Printer 
        private void UpdateStatusLabelPrinter()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatusLabelPrinter()));
                return;
            }

            for (int i = 0; i < Shared.Settings.PrinterList.Count; i++)
            {
                if (i < _LabelStatusPrinterList.Count)
                {
                    PrinterModel printerModel = Shared.Settings.PrinterList[i];
                    ToolStripLabel labelStatusPrinter = _LabelStatusPrinterList[i];

                    if (printerModel.IsConnected)
                    {
                        ShowLabelIcon(labelStatusPrinter, Lang.Printer, Properties.Resources.icons8_printer_30px_connected);
                    }
                    else
                    {
                        ShowLabelIcon(labelStatusPrinter, Lang.Printer, Properties.Resources.icons8_printer_30px_disconnected);
                    }
                }
            }
        }
        private void UpdateUISensorControllerStatus(bool isConnect)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUISensorControllerStatus(isConnect)));
                return;
            }
            if (isConnect)
            {
                ShowLabelIcon(lblSensorControllerStatus, Lang.SensorController, Properties.Resources.icons8_sensor_30px_connected);
            }
            else
            {
                ShowLabelIcon(lblSensorControllerStatus, Lang.SensorController, Properties.Resources.icons8_sensor_30px_disconnected);
            }
        }
        private void ShowLabelIcon(ToolStripLabel label, String text, Image icon)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowLabelIcon(label, text, icon)));
                return;
            }

            if (label.Tag == icon)
            {
                return;
            }

            label.Tag = icon;
            label.ImageAlign = ContentAlignment.MiddleLeft;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Text = text;
            label.Image = icon;
        }
        #endregion UpdateUI Printer

        #region Monitor Printer
        private void MonitorPrinterConnection()
        {

            _ThreadMonitorPrinter = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        for (int i = 0; i < Shared.Settings.PrinterList.Count; i++)
                        {
                            PrinterModel printerModel = Shared.Settings.PrinterList[i];
                            if (printerModel.IsEnable)
                            {
                                // Get controller has exist if not exist then add new controller
                                PODController podController = printerModel.PODController;
                                if (podController == null)
                                {
                                    podController = new PODController(printerModel.IP, printerModel.Port, printerModel.RoleOfPrinter, 1000, 1000, printerModel.IsVersion);
                                    podController.Connect();
                                    podController.OnPODReceiveDataEvent -= PODController_OnPODReceiveDataEvent;
                                    podController.OnPODReceiveDataEvent += PODController_OnPODReceiveDataEvent;
                                    printerModel.PODController = podController;
                                }
                                else
                                {
                                    if (podController.Port != printerModel.Port)
                                    {
                                        podController.Port = printerModel.Port;
                                    }
                                    else if (podController.ServerIP != printerModel.IP)
                                    {
                                        podController.ServerIP = printerModel.IP;
                                    }
                                }

                                //Console.WriteLine("POD status: {0}", podController.IsConnected());
                                var isConnected = podController.IsConnected();
                                if (isConnected == false)
                                {   // Disconnect and connect again
                                    podController.Disconnect();
                                    podController.Connect();
                                }

                                if (isConnected != printerModel.IsConnected)
                                {
                                    printerModel.IsConnected = podController.IsConnected();
                                    UpdateStatusLabelPrinter();
                                    Shared.RaiseOnPrinterStatusChangeEvent();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

                    Thread.Sleep(2000);
                }
            })
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _ThreadMonitorPrinter.Start();
        }
        private void UpdateUIListBoxPrintProductTemplateList(string[] printTemplateNames, string keyWord = "")
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUIListBoxPrintProductTemplateList(printTemplateNames, keyWord)));
                return;
            }

            if (printTemplateNames == null)
            {
                listBoxPrintProductTemplate.Items.Clear();
            }
            else
            {
                listBoxPrintProductTemplate.Items.Clear();
                keyWord = keyWord.ToLower();
                int itemIndex = 0;
                foreach (string printTemplateName in printTemplateNames)
                {
                    if (printTemplateName.ToLower().Contains(keyWord))
                    {
                        ItemCustomModel obj = new ItemCustomModel(printTemplateName, itemIndex);
                        listBoxPrintProductTemplate.Items.Add(obj);
                    }
                    itemIndex++;
                }

            }
        }
        private void ObtainPrintProductTemplateList()
        {
            if (_IsObtainingPrintProductTemplateList)
            {
                return;
            }
            Invoke(new Action(() =>
            {
                listBoxPrintProductTemplate.Items.Clear();
            }));
            _PrintProductTemplateList = new string[] { };
            Task.Run(() =>
            {
                PODController podController = Shared.Settings.PrinterList.Where(p => p.RoleOfPrinter == RoleOfStation.ForProduct).FirstOrDefault().PODController;

                if (podController != null)
                {
                    // Send command request list print template
                    podController.Send("RQLI");
                    Task.Delay(5);
                    UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);
                }
            });
        }
        private void EnableUIPrinting(bool isActive = true, bool isObtain = true)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableUIPrinting(isActive, isObtain)));
                return;
            }
            bool isEnable = Shared.Settings.IsPrinting & isActive;
            listBoxPrintProductTemplate.Enabled = isEnable;
            if (isEnable && isObtain)
            {
                ObtainPrintProductTemplateList();
            }
        }
        #endregion Monitor Printer

        #region Camera Connection
        private void MonitorCameraConnection()
        {
            _ThreadMonitorCamera = new Thread(async () =>
            {
                while (!_ctsCamConnToken.IsCancellationRequested)
                {
                    foreach (CameraModel cameraModel in Shared.Settings.CameraList)
                    {
                        if (cameraModel.IsEnable && !cameraModel.IsConnected)
                        {
                            switch (cameraModel.CameraType)
                            {
                                case CameraType.DM: //DM Series Camera
                                    DMCamera.Connect(cameraModel.IP);
                                    cameraModel.CountTimeReconnect++;
                                    if (cameraModel.CountTimeReconnect >= 3)
                                    {
                                        cameraModel.CountTimeReconnect = 0;
                                        if (DMCamera._EthSystemDiscoverer != null && DMCamera._SerSystemDiscoverer != null)
                                        {
                                            DMCamera._EthSystemDiscoverer.Discover();
                                            DMCamera._SerSystemDiscoverer.Discover();
                                        }
                                    }
                                    break;

                                case CameraType.IS: //IS Series Camera
                                    //ISCamera.ConnectAsync(cvsDisplayImage,cameraModel.IP,cameraModel.Port);
                                    await FirstConnection();
                                    cameraModel.CountTimeReconnect++;
                                    if (cameraModel.CountTimeReconnect >= 3)
                                    {
                                        cameraModel.CountTimeReconnect = 0;
                                    }
                                    break;

                                case CameraType.UKN:

                                    break;
                            }
                        }
                        else
                        {
                            cameraModel.CountTimeReconnect = 0;
                        }
                    }
                    Thread.Sleep(2000);
                }
            })
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _ThreadMonitorCamera.Start();
        }

        private void UpdateStatusLabelCamera()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatusLabelCamera()));
                return;
            }

            for (int i = 0; i < Shared.Settings.CameraList.Count; i++)
            {
                if (i < _LabelStatusCameraList.Count)
                {
                    CameraModel cameraModel = Shared.Settings.CameraList[i];
                    ToolStripLabel labelStatusCamera = _LabelStatusCameraList[i];

                    if (cameraModel.IsConnected)
                    {
                        ShowLabelIcon(labelStatusCamera, Lang.CameraTMP, Properties.Resources.icons8_camera_30px_connected);
                    }
                    else
                    {
                        ShowLabelIcon(labelStatusCamera, Lang.CameraTMP, Properties.Resources.icons8_camera_30px_disconnected);
                    }
                }
            }
        }
        private void ShowLabelIcon(Label label, String text, Image icon)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowLabelIcon(label, text, icon)));
                return;
            }

            if (label.Tag == icon)
            {
                return;
            }

            label.Tag = icon;
            label.ImageAlign = ContentAlignment.MiddleLeft;
            label.TextAlign = ContentAlignment.MiddleRight;
            int gap = 0;
            label.Text = text;
            label.Image = icon;
            label.AutoSize = true;
            int autoWidth = label.Width;
            label.AutoSize = false;
            label.Width = autoWidth + gap + label.Image.Width;
        }
        #endregion Camera Connection

        #region Monitor_Sensor_Controller
        private Thread _ThreadMonitorSensorController;
        private void MonitorSensorControllerConnection()
        {
            _ThreadMonitorSensorController = new Thread(() =>
            {
                int counter = 0;
                while (true)
                {
                    try
                    {
                        if (Shared.Settings.SensorControllerEnable)
                        {
                            // Check controller has exist if not exist then add new controller
                            if (Shared.SensorController == null || counter >= 3)
                            {
                                Shared.SensorController = null;
                                Shared.SensorController = new PODController(Shared.Settings.SensorControllerIP, Shared.Settings.SensorControllerPort, 1000, 1000);
                                Shared.SensorController.Connect();

                                Shared.SensorController.OnPODReceiveMessageEvent -= SensorController_OnPODReceiveMessageEvent;
                                Shared.SensorController.OnPODReceiveMessageEvent += SensorController_OnPODReceiveMessageEvent;
                                // Reset counter
                                counter = 0;
                            }
                            else
                            {
                                var checkIP = Shared.SensorController.ServerIP == Shared.Settings.SensorControllerIP;
                                if (checkIP)
                                {
                                    var checkPort = Shared.SensorController.Port == Shared.Settings.SensorControllerPort;
                                    if (!checkPort)
                                    {
                                        Shared.SensorController.Disconnect();
                                        Shared.SensorController = null;
                                    }
                                }
                                else
                                {
                                    Shared.SensorController.Disconnect();
                                    Shared.SensorController = null;
                                }
                            }
                            if (Shared.SensorController.IsConnected() == false)
                            {
                                //Disconnect and connect again
                                Shared.SensorController.Disconnect();
                                Shared.SensorController.Connect();
                                counter++;
                            }
                            else
                            {
                                // Reset counter
                                counter = 0;
                            }

                            if (Shared.IsSensorControllerConnected != Shared.SensorController.IsConnected())
                            {
                                Shared.IsSensorControllerConnected = Shared.SensorController.IsConnected();
                                UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);

                                Shared.RaiseSensorControllerChangeEvent();

                                if (Shared.IsSensorControllerConnected)
                                {
                                    Shared.SendSettingToSensorController();
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                    Thread.Sleep(2000);
                }
            });
            _ThreadMonitorSensorController.IsBackground = true;
            _ThreadMonitorSensorController.Priority = ThreadPriority.Normal;
            _ThreadMonitorSensorController.Start();
        }
        #endregion Monitor_Sensor_Controller

        private void connectBtn_Click(object sender, EventArgs e)
        {
            FirstConnection();
        }
    }
}
