using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeVerificationSystem.Controller;
using UILanguage;
using BarcodeVerificationSystem.Model;

namespace BarcodeVerificationSystem.View
{
    public partial class ucSystemSettings : UserControl
    {
        private bool _IsBinding = false;
        private String[] _ListLanguages = { "en-US", "vi-VN" };
        public ucSystemSettings()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitEvents();
            SetLanguage();
        }

        private void InitControls()
        {
            _IsBinding = true;
            //Checked result
            txtCheckedResultPath.Text = Shared.Settings.ExportCheckedResultPath;
            txtDataCheckedFileName.Text = Shared.Settings.DataCheckedFileName;
            //END Checked result

            //Image export
            radEnableImageExport.Checked = Shared.Settings.ExportImageEnable;
            radDisableImageExport.Checked = !Shared.Settings.ExportImageEnable;
            txtImageExportPath.Text = Shared.Settings.ExportImagePath;
            EnableExportImageUI(Shared.Settings.ExportImageEnable);
            //END Image export

            //Output
            radEnableOutput.Checked = Shared.Settings.OutputEnable;
            radDisableOutput.Checked = !Shared.Settings.OutputEnable;
            //END Output

            //Stop process condition
            radEnableTotalCode.Checked = Shared.Settings.TotalCheckEnable;
            radEnableTotalPassed.Checked = !Shared.Settings.TotalCheckEnable;
            //END

            //Verify and print sent method
            radBasic.Checked = Shared.Settings.VerifyAndPrintBasicSentMethod;
            radCompare.Checked = !Shared.Settings.VerifyAndPrintBasicSentMethod;
            txtFailedREdit.Text = Shared.Settings.FailedDataSentToPrinter;
            if (Shared.Settings.VerifyAndPrintBasicSentMethod)
            {
                txtPrintField.Enabled = false;
                btnSelectPrintField.Enabled = false;
                txtPrintField.Text = "";
            }
            else
            {
                UpdateTXTPrinField(Shared.Settings.PrintFieldForVerifyAndPrint);
                txtPrintField.Enabled = true;
                btnSelectPrintField.Enabled = true;
            }
            //END Verify and print

            // Language
            // List language http://cldr.unicode.org/index/downloads/cldr-1-8
            cboLanguages.Items.Add("English"); //Tiếng anh
            cboLanguages.Items.Add("Vietnamese [Tiếng Việt]"); //Tiếng việt
            //cboLanguage.Items.Add("German [Deutsch]"); //Tiếng đức
            //cboLanguage.Items.Add("Simplified Chinese [中文（简体）]");//Tiếng trung
            //cboLanguage.Items.Add("Korean [한국어]");//Tiếng hàn
            //cboLanguage.Items.Add("Russian [русский]");//Tiếng nga

            cboLanguages.SelectedIndex = 0;
            for (int i = 0; i < _ListLanguages.Length; i++)
            {
                if (_ListLanguages[i] == Shared.Settings.Language)
                {
                    cboLanguages.SelectedIndex = i;
                }
            }
            // END Language

            txtJobDateTimeFormat.Text = Shared.Settings.JobDateTimeFormat;
            txtJobFileName.Text = Shared.Settings.JobFileNameDefault;
            _IsBinding = false;
        }

        private void InitEvents()
        {
            txtCheckedResultPath.TextChanged += AdjustData;
            btnBrowserCheckedResultPath.Click += AdjustData;
            radEnableImageExport.CheckedChanged += AdjustData;
            radDisableImageExport.CheckedChanged += AdjustData;

            radEnableImageExport.CheckedChanged += frmJob.RadioButton_CheckedChanged; // Add by Thong Thach 23/03/23
            radDisableImageExport.CheckedChanged += frmJob.RadioButton_CheckedChanged; // Add by Thong Thach 23/03/23

            txtImageExportPath.TextChanged += AdjustData;
            radEnableOutput.CheckedChanged += AdjustData;
            radDisableOutput.CheckedChanged += AdjustData;
            radEnableTotalCode.CheckedChanged += AdjustData;
            radEnableTotalPassed.CheckedChanged += AdjustData;
            radBasic.CheckedChanged += AdjustData;
            radCompare.CheckedChanged += AdjustData;

            radEnableOutput.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radDisableOutput.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radEnableTotalCode.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radEnableTotalPassed.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radBasic.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radCompare.CheckedChanged += frmJob.RadioButton_CheckedChanged;

            cboLanguages.SelectedIndexChanged += AdjustData;
            txtJobDateTimeFormat.TextChanged += AdjustData;
            txtJobFileName.TextChanged += AdjustData;
            txtFailedREdit.TextChanged += AdjustData;
            btnDefaultDatimeJob.Click += AdjustData;
            btnDefaultFileNameJob.Click += AdjustData;
            btnDefaultFailedR.Click += AdjustData;
            btnSelectPrintField.Click += AdjustData;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            this.Load += UcSystemSettings_Load;

            btnBrowserImageExportPath.Click += AdjustData;

            cboLanguages.DrawMode = DrawMode.OwnerDrawVariable;
            cboLanguages.Height = 40;
            cboLanguages.DropDownHeight = 100;
            cboLanguages.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLanguages.DrawItem += ComboBoxCustom.myComboBox_DrawItem;
            cboLanguages.MeasureItem += ComboBoxCustom.cbo_MeasureItem;
        }

        private void UcSystemSettings_Load(object sender, EventArgs e)
        {
            InitControls();

            lblImageExportWarning.ForeColor = Color.FromArgb(255, 128, 0);
        }

        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }
            grbExportCheckedResult.Text = Lang.CheckedResult1;
            lblCheckedResultPath.Text = lblImageExportPath.Text = Lang.Path;
            grbImageExport.Text = Lang.ImageExport;
            radEnableImageExport.Text = radEnableOutput.Text = Lang.Enable;
            radDisableImageExport.Text = radDisableOutput.Text = Lang.Disable;
            radEnableTotalCode.Text = Lang.TotalChecked;
            radEnableTotalPassed.Text = Lang.TotalPassed;
            radBasic.Text = Lang.Basic;
            radCompare.Text = Lang.Compare;
            lblFailureEdit.Text = Lang.FailureDataSentToPrinter;
            lblImageExportWarning.ForeColor = Color.FromArgb(255, 128, 0);
            lblImageExportWarning.Text = Lang.TheImageExportFunction;
            lblDataCheckedFileName.Text = Lang.DataCheckedFileName;

            btnGenerateDataCheckedFileName.Text = Lang.Generate;
            btnDefaultFailedR.Text = Lang.Default;
            grbLanguage.Text = Lang.Language;
            grbOutput.Text = Lang.OutputSignal;
            //lblOutputSignal.Text = Lang.Output;

            grbJobFormat.Text = Lang.JobFormat;
            lblJobDatetimeFormat.Text = Lang.DateTimeFormat;
            lblJobFileName.Text = Lang.FileName;
            lblPrintField.Text = Lang.PrintField;
            btnDefaultDatimeJob.Text = btnDefaultFileNameJob.Text = Lang.Default;
            btnSelectPrintField.Text = Lang.Select;
            grbStopCondition.Text = Lang.StopCondition;
            grbSentDataMethod.Text = Lang.SendDataMethod;
        }

        private void AdjustData(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }
            if (sender == txtCheckedResultPath)
            {
                Shared.Settings.ExportCheckedResultPath = txtCheckedResultPath.Text;
            }
            else if (sender == txtJobDateTimeFormat)
            {
                Shared.Settings.JobDateTimeFormat = txtJobDateTimeFormat.Text;
            }
            else if (sender == txtJobFileName)
            {
                Shared.Settings.JobFileNameDefault = txtJobFileName.Text;
            }
            else if (sender == txtDataCheckedFileName)
            {
                Shared.Settings.DataCheckedFileName = txtDataCheckedFileName.Text;
            }
            else if (sender == radEnableImageExport)
            {
                if (radEnableImageExport.Checked)
                {
                    Shared.Settings.ExportImageEnable = true;
                    btnBrowserImageExportPath.Enabled = true;
                }
            }
            else if (sender == radDisableImageExport)
            {
                if (radDisableImageExport.Checked)
                {
                    Shared.Settings.ExportImageEnable = false;
                    btnBrowserImageExportPath.Enabled = false;
                }
            }
            else if (sender == txtImageExportPath)
            {
                Shared.Settings.ExportImagePath = txtImageExportPath.Text;
            }
            else if (sender == txtFailedREdit)
            {
                Shared.Settings.FailedDataSentToPrinter = txtFailedREdit.Text;
            }
            else if (sender == btnDefaultDatimeJob)
            {
                Shared.Settings.JobDateTimeFormat = txtJobDateTimeFormat.Text = "yyyyMMdd_HHmmss";
            }
            else if (sender == btnDefaultFileNameJob)
            {
                Shared.Settings.JobFileNameDefault = txtJobFileName.Text = "Template";
            }
            else if (sender == btnDefaultFailedR)
            {
                Shared.Settings.FailedDataSentToPrinter = txtFailedREdit.Text = "Failure";
            }
            else if (sender == btnSelectPrintField)
            {
                // Create and show dialog POD format form base on default POD format list
                frmPrintFieldForVerifyAndPrintCompareMode frmPODFormat = new frmPrintFieldForVerifyAndPrintCompareMode();
                //frmPrintFieldForVerifyAndPrintCompareMode._PODFormat = _PODList.Select(item => (PODModel)item.Clone()).ToList();
                frmPODFormat.ShowDialog();
                // END Create and show dialog POD format form base on default POD format list

                txtPrintField.Text = "";
                if (frmPODFormat.DialogResult == DialogResult.OK)
                {
                    //Get POD format from POD format form
                    var _PODFormat = frmPODFormat._PODFormat;
                    Shared.Settings.PrintFieldForVerifyAndPrint = _PODFormat;
                    UpdateTXTPrinField(_PODFormat);
                    //END Get POD format from POD format form
                }
                else
                {
                    Shared.Settings.PrintFieldForVerifyAndPrint.Clear();
                    txtPrintField.Text = "All Field";
                }
                Shared.RaiseOnVerifyAndPrindSendDataMethod();
            }
            else if (sender == radEnableOutput)
            {
                if (radEnableOutput.Checked)
                {
                    Shared.Settings.OutputEnable = true;
                }
            }
            else if (sender == radDisableOutput)
            {
                if (radDisableOutput.Checked)
                {
                    Shared.Settings.OutputEnable = false;
                }
            }
            else if (sender == radEnableTotalCode)
            {
                if (radEnableTotalCode.Checked)
                {
                    Shared.Settings.TotalCheckEnable = true;
                }
            }
            else if (sender == radEnableTotalPassed)
            {
                if (radEnableTotalPassed.Checked)
                {
                    Shared.Settings.TotalCheckEnable = false;
                }
            }
            else if (sender == radBasic)
            {
                if (radBasic.Checked)
                {
                    Shared.Settings.VerifyAndPrintBasicSentMethod = true;
                    btnSelectPrintField.Enabled = false;
                    txtPrintField.Enabled = false;
                    txtPrintField.Text = "";
                }
            }
            else if (sender == radCompare)
            {
                if (radCompare.Checked)
                {
                    Shared.Settings.VerifyAndPrintBasicSentMethod = false;
                    btnSelectPrintField.Enabled = true;
                    txtPrintField.Enabled = true;
                    var podFormat = Shared.Settings.PrintFieldForVerifyAndPrint;
                    UpdateTXTPrinField(podFormat);
                }
            }
            else if (sender == cboLanguages)
            {
                Shared.Settings.Language = _ListLanguages[cboLanguages.SelectedIndex];
                Shared.RaiseLanguageChangeEvent(Shared.Settings.Language);
            }
            else if (sender == btnBrowserImageExportPath)
            {
                var folderPath = Shared.Settings.ExportImagePath = OpenDirectoryFileDatabase();
                if (folderPath != null && folderPath != "")
                    txtImageExportPath.Text = folderPath;
            }
            // Save to file
            Shared.SaveSettings();
        }

        private void UpdateTXTPrinField(List<PODModel> list)
        {
            if (list.Count > 0)
            {
                foreach (PODModel item in list)
                {
                    txtPrintField.Text += item.ToString();
                }
            }
            else
            {
                txtPrintField.Text = "All Field";
            }
        }

        private string OpenDirectoryFileDatabase()
        {
            string selectedFolderPath = null;

            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    selectedFolderPath = dialog.SelectedPath;
                }
            }
            return selectedFolderPath;
        }

        private void EnableExportImageUI(bool isEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableExportImageUI(isEnable)));
                return;
            }

            lblImageExportPath.Enabled = isEnable;
            txtImageExportPath.Enabled = isEnable;
            btnBrowserImageExportPath.Enabled = isEnable;
            lblImageExportWarning.Enabled = isEnable;
        }
    }
}
