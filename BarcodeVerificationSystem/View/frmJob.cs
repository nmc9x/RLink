using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using CommonVariable;
using DesignUI.CuzAlert;
using DesignUI.CuzMesageBox;
using DesignUI.CuzUI;
using OperationLog.Controller;
using OperationLog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
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
        //Datetime clock 
        private Timer _TimerDateTime = new Timer();
        private string _DateTimeFormat = "yyyy/MM/dd hh:mm:ss tt";
        //END Datetime clock 
        public double _NumberTotalsCode = 0;
        // Variable for loading data to user interface
        private bool _IsBinding = false;
        // END Variable for loading data to user interface
        // Variable for create, edit Job
        private bool _IsProcessing = false;
        private string _NameOfJobOld = "";
        private List<PODModel> _PODFormat = new List<PODModel>();
        private List<PODModel> _PODList = new List<PODModel>();
        private List<string> _JobNameList = null;
        private List<ToolStripLabel> _LabelStatusCameraList = new List<ToolStripLabel>();
        private List<ToolStripLabel> _LabelStatusPrinterList = new List<ToolStripLabel>();

        private frmSettings _FormSettings;
        
        private JobModel _JobModel = null;
        private frmMain _FormMainPC = null;

        private string[] _PrinterSeries = new string[] { "RYNAN R10", "RYNAN R20", "RYNAN B1040", "RYNAN R40", "RYNAN R60", "Standalone" };

        private string SupportForPrinter = "Support for printer: RYNAN R10, RYNAN R20, RYNAN R40, RYNAN R60, RYNAN B1040.";
        private string Standalone = "In this mode the software does not communicate and control the printer, the software only verifies the barcode through the camera.";
        private static Color _AferProductionColor = Color.FromArgb(0, 171, 230);
        private static Color _OnProductionColor = Color.FromArgb(62, 151, 149);
        private static Color _VerifyAndPrintColor = Color.DarkBlue;
        private static Color _CanreadColor = Color.IndianRed;
        private static Color _StaticTextColor = Color.LightGray;
        private static Color _Standalone = Color.DarkBlue;
        private static Color _RLinkColor = Color.FromArgb(0, 171, 230);
        #endregion Variables Jobs

        public frmJob()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            InitControls();
            InitCameraVariables();
            InitEvents();
            SetLanguage();
        }

        private void InitControls()
        {
            // Show icon camera status
            _LabelStatusCameraList.Add(lblStatusCamera01);
            UpdateStatusLabelCamera();

            // Show icon printer status
            _LabelStatusPrinterList.Add(lblStatusPrinter01);
            UpdateStatusLabelPrinter();

            _NameOfJobOld = "";

            CreateJob();

            cboSupportForCamera.Items.Add("Camera Cognex DM series");
            cboSupportForCamera.SelectedIndex = 0;

            _TimerDateTime.Start();
            // Initilize default item form POD format list
            _NameOfJobOld = "";
            Shared.JobNameSelected = "";

            PODModel podText = new PODModel(0, "", PODModel.TypePOD.TEXT, "");
            _PODList.Add(podText);
            for (int index = 1; index <= 20; index++)
            {
                PODModel podVCD = new PODModel(index, "", PODModel.TypePOD.FIELD, "");
                _PODList.Add(podVCD);
            }
            // Monitor camera connection
            MonitorCameraConnection();
            // Monitor printer connection
            MonitorPrinterConnection();
            // Monitor sensor connection
            MonitorSensorControllerConnection();
            // Camera sw Listener server
            MonitorListenerServer();
        }

        private async void MonitorListenerServer()
        {
            try
            {
                await StartListenerServer();
            }
            catch (Exception exx)
            {
                MessageBox.Show("ERROR: " + exx);
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
            radRSeries.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radOther.CheckedChanged += frmJob.RadioButton_CheckedChanged;

            radAfterProduction.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radAfterProduction.CheckedChanged += ActionResult;
            radAfterProduction.EnabledChanged += JobType_EnabledChanged; ;
            radOnProduction.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radOnProduction.CheckedChanged += ActionResult;
            radOnProduction.EnabledChanged += JobType_EnabledChanged;
            radVerifyAndPrint.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radVerifyAndPrint.CheckedChanged += ActionResult;
            radVerifyAndPrint.EnabledChanged += JobType_EnabledChanged;
            txtStaticText.TextChanged += TxtStaticText_TextChanged; ;
            txtDirectoryDatabse.TextChanged += TxtDirectoryDatabse_TextChanged; ;
            txtFileName.TextChanged += TxtFileName_TextChanged; ;
            txtPODFormat.TextChanged += TxtPODFormat_TextChanged; ;

            txtSearch.TextChanged += TxtSearch_TextChanged; ;
            txtSearchTemplate.TextChanged += TxtSearchTemplate_TextChanged;

            btnPODFormat.Click += ActionResult;
            //btnSave.Click += ActionResult;
            btnSettings.Click += ActionResult;
            listBoxJobList.SelectedIndexChanged += ActionResult;
            listBoxPrintProductTemplate.SelectedIndexChanged += ActionResult;
            btnRefesh.Click += ActionResult;
            btnImportDatabase.Click += ActionResult;
            Shared.OnLanguageChange += Shared_OnLanguageChange;

            this.Load += FrmJob_Load;
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

            cboSupportForCamera.DrawMode = DrawMode.OwnerDrawVariable;
            cboSupportForCamera.Height = 40;
            cboSupportForCamera.DropDownHeight = 150;
            cboSupportForCamera.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSupportForCamera.DrawItem += ComboBoxCustom.myComboBox_DrawItem;
            cboSupportForCamera.MeasureItem += ComboBoxCustom.cbo_MeasureItem;


            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;
            Shared.OnPrintingStateChange += Shared_OnPrintingStateChange;
            Shared.OnPrinterStatusChange += Shared_OnPrinterStatusChange;
            Shared.OnPrinterDataChange += Shared_OnPrinterDataChange;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnSensorControllerChangeEvent += Shared_OnSensorControllerChangeEvent;

            Shared.OnCameraTriggerOnChange += Shared_OnCameraTriggerOnChange;
            Shared.OnCameraTriggerOffChange += Shared_OnCameraTriggerOffChange;
            Shared.OnCameraOutputSignalChange += Shared_OnCameraOutputSignalChange;

            listBoxJobList.DrawItem += ListBoxJobList_DrawItem;
        }

        private void ListBoxJobList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1 || (sender as ListBox).Items.Count == 0) return;
            try
            {
                var job = Shared.GetJob((sender as ListBox).Items[e.Index].ToString());
                Rectangle headItemRect = new Rectangle(0, e.Bounds.Y + 4, 8, e.Bounds.Height - 10);
                using (Brush brush = new SolidBrush(_Standalone))
                    if(!job.PrinterSeries)
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

        // Counter skip the first alert when load form jobs.
        int countSkipFirstAlert = 0;
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

            if (isAlert) CuzAlert.Show(content, Alert.enmType.Info, new Size(500, 90), new Point(Location.X, Location.Y), this.Size);
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

            //if (radRSeries.Checked)
            //{
            //    radCanRead.Enabled = false;
            //    radStaticText.Enabled = false;
            //    radAfterProduction.Enabled = true;
            //    radAfterProduction.Checked = true;
            //    radOnProduction.Enabled = true;
            //    radVerifyAndPrint.Enabled = true;
            //    listBoxPrintProductTemplate.Enabled = true;
            //    txtSearchTemplate.BackColor = Color.White;
            //    txtSearchTemplate.Enabled = true;
            //    btnRefeshTemplate.Enabled = true;
            //    listBoxPrintProductTemplate.Enabled = true;
            //}
            //else
            //{
            //    btnImportDatabase.Enabled = false;
            //    btnPODFormat.Enabled = false;
            //    tblJobType.Enabled = false;

            //    txtDirectoryDatabse.BackColor = Color.WhiteSmoke;
            //    txtPODFormat.BackColor = Color.WhiteSmoke;
            //    txtSearchTemplate.BackColor = Color.WhiteSmoke;
            //    txtSearchTemplate.Enabled = false;
            //    btnRefeshTemplate.Enabled = false;
            //    listBoxPrintProductTemplate.Enabled = false;
            //    listBoxPrintProductTemplate.ClearSelected();
            //    txtDirectoryDatabse.Text = "";
            //    txtPODFormat.Text = "";
            //    txtStaticText.Text = "";
            //}
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

        private void Shared_OnPrinterDataChange(object sender, EventArgs e)
        {
            if (sender is PODDataModel)
            {
                PODDataModel podDataModel = sender as PODDataModel;
                try
                {
                    //Console.WriteLine("POD Message: {0}", message);
                    string[] pODcommand = podDataModel.Text.Split(';');
                    PODResponseModel PODResponseModel = new PODResponseModel
                    {
                        //PODResponseModel PODResponseModel = JsonConvert.DeserializeObject<PODResponseModel>(podDataModel.Text);
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
                                //_IsObtainingPrintProductTemplateList = false;
                                UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);
                            }
                            else { }
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            toolStripDateTime.Text = DateTime.Now.ToString(_DateTimeFormat);
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

        #region TextChanged
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
        #endregion TextChanged

        #region Events called
        private void Shared_OnPrinterStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelPrinter();
            ObtainPrintProductTemplateList();
        }

        private void Shared_OnCameraStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelCamera();
        }

        private void Shared_OnSensorControllerChangeEvent(object sender, EventArgs e)
        {
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Exit();
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

        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }

        #endregion Events called

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
                if(tabControl1.SelectedIndex == 1)
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

                        if (printerSettingsModel.podDataType != 1)
                        {
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
                //CreateJob();
            }
            else if (sender == btnRefeshTemplate)
            {
                _PrintProductTemplateList = new string[] { };
                ObtainPrintProductTemplateList();
                UpdateUIListBoxPrintProductTemplateList(_PrintProductTemplateList);
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

        /// <summary>
        /// Create job
        /// </summary>
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

        /// <summary>
        /// Open Job
        /// </summary>
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

        // Delete Job
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
                        // Perform delete Job file
                        //Shared.DeleteJob(_JobModel);
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
        // END Delete Job



        // Save Job
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
                                    System.IO.File.Move(oldJobPath, newJobPath);
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

                        if (printerSettingsModel.podDataType != 1)
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

        // END Save Job
        // Load list name of tempate
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
        // END Load list name of tempate

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
                    if(jobModel.CompareType == CompareType.CanRead)
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
                    //string printerName = string.Format("{0} {1}",Lang.Printer,i + 1);

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

        private Thread _ThreadObtainPrintProductTemplateList;
        private Thread _ThreadMonitorPrinter;
        private bool _IsObtainingPrintProductTemplateList = false;
        private string[] _PrintProductTemplateList = new string[] { };

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
                                    if(podController.Port != printerModel.Port)
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
            });
            _ThreadMonitorPrinter.IsBackground = true;
            _ThreadMonitorPrinter.Priority = ThreadPriority.Normal;
            _ThreadMonitorPrinter.Start();
        }

        private PODController GetPODController(string ipAddress, int port, List<PODController> podControllerList)
        {
            foreach (PODController podController in podControllerList)
            {
                if (podController.ServerIP == ipAddress)
                {
                    return podController;
                }
            }

            return null;
        }

        private void PODController_OnPODReceiveDataEvent(object sender, EventArgs e)
        {
            if (sender is PODDataModel)
            {
                Shared.RaiseOnPrinterDataChangeEvent(sender as PODDataModel);
            }
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

        private void Shared_OnPrintingStateChange(object sender, EventArgs e)
        {
            EnableUIPrinting();
        }

        private void EnableUIPrinting(bool isActive = true, bool isObtain = true)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableUIPrinting(isActive, isObtain)));
                return;
            }

            bool isEnable = Shared.Settings.IsPrinting & isActive;
            //grbPrint.Enabled = isEnable;
            listBoxPrintProductTemplate.Enabled = isEnable;

            if (isEnable && isObtain)
            {
                ObtainPrintProductTemplateList();
            }
        }

        private void KillThreadObtainPrintProductTemplateList()
        {
            if (_ThreadObtainPrintProductTemplateList != null && _ThreadObtainPrintProductTemplateList.IsAlive)
            {
                // Release thread
                _ThreadObtainPrintProductTemplateList.Abort();
                _ThreadObtainPrintProductTemplateList = null;
            }
            _IsObtainingPrintProductTemplateList = false;
        }

#endregion Monitor Printer

#region Camera Connection
        private SynchronizationContext _SyncContext = null;
        private EthSystemDiscoverer _EthSystemDiscoverer = null;
        private SerSystemDiscoverer _SerSystemDiscoverer = null;

        private List<object> _CameraSystemInfoList = new List<object>();
        private List<DataManSystem> _DataManSystemList = new List<DataManSystem>();
        private object _CurrentResultInfoSyncLock = new object();
        //END Cognex camera

        private Thread _ThreadMonitorCamera;
        private void InitCameraVariables()
        {
            // The SDK may fire events from arbitrary thread context. Therefore if you want to change
            // the state of controls or windows from any of the SDK' events, you have to use this
            // synchronization context to execute the event handler code on the main GUI thread.
            _SyncContext = WindowsFormsSynchronizationContext.Current;

            // Create discoverers to discover ethernet and serial port systems.
            _EthSystemDiscoverer = new EthSystemDiscoverer();
            _SerSystemDiscoverer = new SerSystemDiscoverer();

            // Subscribe to the system discoved event.
            _EthSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
            _SerSystemDiscoverer.SystemDiscovered += new SerSystemDiscoverer.SystemDiscoveredHandler(OnSerSystemDiscovered);

            // Ask the discoverers to start discovering systems.
            _EthSystemDiscoverer.Discover();
            _SerSystemDiscoverer.Discover();
        }

        private void MonitorCameraConnection()
        {
            _ThreadMonitorCamera = new Thread(() =>
            {
                while (true)
                {
                    foreach (CameraModel cameraModel in Shared.Settings.CameraList)
                    {
                        if (cameraModel.IsEnable)
                        {
                            if (!cameraModel.IsConnected)
                            {
                                // Try to connect the camera
                                DeviceConnect(cameraModel.IP);
                                // Reset counter variable
                                cameraModel.CountTimeReconnect++;
                                // Discover camera again after time reconnect
                                if (cameraModel.CountTimeReconnect >= 3)
                                {
                                    cameraModel.CountTimeReconnect = 0;
                                    //_CameraSystemInfoList.Clear();
                                    if (_EthSystemDiscoverer != null && _SerSystemDiscoverer != null)
                                    {
                                        _EthSystemDiscoverer.Discover();
                                        _SerSystemDiscoverer.Discover();
                                    }
                                }
                            }
                            else
                            {
                                // Reset counter variable
                                cameraModel.CountTimeReconnect = 0;
                            }
                        }
                    }

                    // Wait for camera connecting
                    Thread.Sleep(2000);
                }
            });
            _ThreadMonitorCamera.IsBackground = true;
            _ThreadMonitorCamera.Priority = ThreadPriority.Normal;
            _ThreadMonitorCamera.Start();

        }

        private void Shared_OnCameraTriggerOnChange(object sender, EventArgs e)
        {
            foreach (DataManSystem dataManSystem in _DataManSystemList)
            {
                try
                {
                    dataManSystem.SendCommand("TRIGGER ON");
                }
                catch (Exception)
                {
                    //MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString());
                }
            }
        }

        private void Shared_OnCameraTriggerOffChange(object sender, EventArgs e)
        {
            foreach (DataManSystem dataManSystem in _DataManSystemList)
            {
                try
                {
                    dataManSystem.SendCommand("TRIGGER OFF");
                }
                catch (Exception)
                {
                    //MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString());
                }
            }
            Thread.Sleep(500);
        }

        private void Shared_OnCameraOutputSignalChange(object sender, EventArgs e)
        {
            foreach (DataManSystem dataManSystem in _DataManSystemList)
            {
                try
                {
                    DmccResponse response = dataManSystem.SendCommand("OUTPUT.USER1");
                }
                catch (Exception)
                {
                    //MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString());
                }
            }
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
                    //string cameraName = string.Format("{0} {1}",Lang.Camera,i + 1);
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
            //label.BackColor = Color.Cyan;
            //int gap = 5;
            int gap = 0;
            label.Text = text;
            label.Image = icon;
            label.AutoSize = true;
            int autoWidth = label.Width;
            label.AutoSize = false;
            label.Width = autoWidth + gap + label.Image.Width;
        }

#endregion End Camera Connection

#region Cognex camera
        private void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo)
        {
            _SyncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        Console.WriteLine(string.Format("IP camera: {0}", systemInfo.IPAddress.ToString()));
                        // Check camera has exist in list
                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(systemInfo.IPAddress.ToString());
                        bool hasExist = CheckCameraInfoHasExist(systemInfo, _CameraSystemInfoList);
                        // Check camera has added
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
                        //_CameraSystemInfoList.Add(systemInfo);
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

        private void DeviceConnect(string ipAddress)
        {
            try
            {
                foreach (var systemInfo in _CameraSystemInfoList)
                {
                    ISystemConnector iSysConnector = null;
                    if (systemInfo is EthSystemDiscoverer.SystemInfo)
                    {
                        EthSystemDiscoverer.SystemInfo ethSystemInfo = systemInfo as EthSystemDiscoverer.SystemInfo;
                        EthSystemConnector conn = new EthSystemConnector(ethSystemInfo.IPAddress);

                        CameraModel cameraModel = Shared.GetCameraModelBasedOnIPAddress(ethSystemInfo.IPAddress.ToString());
                        if (cameraModel != null && cameraModel.IP == ipAddress)
                        {
                            conn.UserName = cameraModel.UserName;
                            conn.Password = cameraModel.Password;
                            cameraModel.Name = ethSystemInfo.Name;
                            cameraModel.SerialNumber = ethSystemInfo.SerialNumber;
                        }
                        else
                        {
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

                    //_SysConnector.Logger = new GuiLogger(tbLog, cbLoggingEnabled.Checked, ref _Closing);

                    DataManSystem dataManSystem = new DataManSystem(iSysConnector);
                    //dataManSystem.DefaultTimeout = 5000;
                    dataManSystem.DefaultTimeout = 1000;

                    // Subscribe to events that are signalled when the system is connected / disconnected.
                    dataManSystem.SystemConnected += new SystemConnectedHandler(OnSystemConnected);
                    dataManSystem.SystemDisconnected += new SystemDisconnectedHandler(OnSystemDisconnected);

                    dataManSystem.SystemWentOnline += new SystemWentOnlineHandler(OnSystemWentOnline);
                    dataManSystem.SystemWentOffline += new SystemWentOfflineHandler(OnSystemWentOffline);
                    //END Subscribe to events that are signalled when the system is connected / disconnected.

                    // Subscribe to events that are signalled when the deveice sends auto-responses.
                    ResultTypes resultTypes = ResultTypes.ReadXml | ResultTypes.Image | ResultTypes.ImageGraphics;
                    // Current error missing image in mode Multi-Read Sync when Master no read and Slave read
                    //ResultTypes resultTypes = ResultTypes.ReadXml;

                    // ResultCollector
                    // The order of result components may not always be the same.For example sometimes the XML result arrives first, sometimes the image. This issue can be overcome by using the ResultCollector.
                    // The user needs to specify what makes a result complete(e.g.it consists of an image, an SVG graphic and an xml read result) and subscribe to ResultCollector’s ComplexResultArrived event.

                    // The ResultCollector waits for the result components. If a result is complete, a ComplexResultArrived event is fired. If a result is not complete but it times out (time out value can be set via the ResultTimeOut property) 
                    // or the ResultCollector’s buffer is full(buffer length can be set via the ResultCacheLength property), then a PartialResultDropped event is fired. Both events provide the available result components in their event argument, 
                    // which can be used to process the complex result (e.g.maintain result history, show the image, graphic and result string, and so on.)

                    ResultCollector resultCollector = new ResultCollector(dataManSystem, resultTypes);
                    resultCollector.ComplexResultCompleted += ResultCollector_ComplexResultCompleted;
                    // Event for get result timeout, current event not work well (Timeout not exactly)
                    //resultCollector.SimpleResultDropped += _ResultCollector_SimpleResultDropped;
                    //END Subscribe to events that are signalled when the deveice sends auto-responses.

                    //dataManSystem.SetKeepAliveOptions(true, 3000, 1000);

                    dataManSystem.Connect();

                    try
                    {
                        //
                        // Summary:
                        //     Sets which result types the SDK should handle.
                        //
                        // Parameters:
                        //   resultTypes:
                        //     The result types that the user is interested in.
                        dataManSystem.SetResultTypes(resultTypes);
                    }
                    catch (Exception)
                    {

                    }

                    _DataManSystemList.Add(dataManSystem);
                }
            }
            catch (Exception)
            {
                CleanupConnection();
            }
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
                    UpdateStatusLabelCamera();
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
                    UpdateStatusLabelCamera();
                    Shared.RaiseOnCameraStatusChangeEvent();
                },
                null);
        }

        private void OnSystemWentOnline(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                    //AddListItem("System went online");
                },
                null);
        }

        private void OnSystemWentOffline(object sender, EventArgs args)
        {
            _SyncContext.Post(
                delegate
                {
                    //AddListItem("System went offline");
                },
                null);
        }

        private void _ResultCollector_SimpleResultDropped(object sender, SimpleResult e)
        {
            //throw new NotImplementedException();
            //Console.WriteLine("Nhan duoc du lieu ne!!! ResultCollector_SimpleResultDropped");
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
                // Get value of a non-public member of sender object https://stackoverflow.com/questions/48328141/get-value-of-a-non-public-member-of-sender-object
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
            // Take a reference or copy values from the locked result info object. This is done
            // so that the lock is used only for a short period of time.
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

            //Draw image graphic result

            Bitmap bitmap = new Bitmap(1024, 1024);
            if (imageResult != null)
            {
                // The original bitmap with the wrong pixel format. 
                // You can check the pixel format with originalBmp.PixelFormat

                //Convert bitmap to corect pixel format
                bitmap = ((Bitmap)imageResult).Clone(new Rectangle(0, 0, imageResult.Width, imageResult.Height), PixelFormat.Format24bppRgb);
            }
            else
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                }
            }

            //Draw ROI
            if (imageGraphics.Count > 0)
            {
                // From this bitmap, the graphics can be obtained, because it has the right PixelFormat
                using (Graphics graphicsImage = Graphics.FromImage(bitmap))
                {
                    foreach (var graphics in imageGraphics)
                    {
                        ResultGraphics resultGraphics = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        ResultGraphicsRenderer.PaintResults(graphicsImage, resultGraphics);
                    }
                }
            }
            //END Draw ROI
            //END Draw image graphic result

            //Add data obtained to queue
            DetectModel detectModel = new DetectModel();
            detectModel.Image = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            detectModel.Text = strResult;
            if (cameraModel != null)
            {
                detectModel.RoleOfCamera = cameraModel.RoleOfCamera;
            }

            // Raise event camera read data
            Shared.RaiseOnCameraReadDataChangeEvent(detectModel);
        }
#endregion Cognex camera

#region Monitor Sensor controller
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

        private void SensorController_OnPODReceiveMessageEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Shared.RaiseOnRepeatTCPMessageChange(sender);
        }

#endregion Monitor Sensor controller

        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
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
    }
}
