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
    /// <summary>
    /// @Author: Dung Le
    /// </summary>
    public partial class ucCameraSettings : UserControl
    {
        private CameraModel _CameraModel;
        private int _Index = 0;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                _CameraModel = Shared.Settings.CameraList.Count > _Index ?
                    Shared.Settings.CameraList[_Index] : new CameraModel { Index = _Index };
            }
        }
        private bool _IsBinding = false;
        public ucCameraSettings()
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
            txtIPAddress.Text = _CameraModel.IP;
            txtPassword.Text = _CameraModel.Password;
            textBoxPort.Text = _CameraModel.Port;
            comboBoxCamType.SelectedIndex = _CameraModel.CameraType == CameraType.DM ? 0 : 1;
            txtNoReadOutputString.Text = _CameraModel.NoReadOutputString;


            radAutoReconnectEnable.Checked = _CameraModel.AutoReconnect;
            radAutoReconnectDisable.Checked = !_CameraModel.AutoReconnect;
            radOutputEnable.Checked = _CameraModel.OutputEnable;
            radOutputDisable.Checked = !_CameraModel.OutputEnable;

            UpdateCameraInfo();
            _IsBinding = false;

            groupBoxOCR.Visible =
            labelPort.Visible =
            textBoxPort.Visible =
            _CameraModel.CameraType == CameraType.IS;

            labelNotesOCR.Text = Lang.NotesForSettingCam;

            bool[] listBoolCheckBox = UtilityFunctions.IntToBools(_CameraModel.ObjectSelectNum, 5);
            checkBoxObject1.Checked = listBoolCheckBox[0];
            checkBoxObject2.Checked = listBoolCheckBox[1];
            checkBoxObject3.Checked = listBoolCheckBox[2];
            checkBoxObject4.Checked = listBoolCheckBox[3];
            checkBoxObject5.Checked = listBoolCheckBox[4];
            InitControlsAndEvents_IS();
        }

        private void InitControlsAndEvents_IS()
        {
            radioButtonSym1.Checked = _CameraModel.IsSymbol[0];
            radioButtonSym2.Checked = _CameraModel.IsSymbol[1];
            radioButtonSym3.Checked = _CameraModel.IsSymbol[2];
            radioButtonSym4.Checked = _CameraModel.IsSymbol[3];
            radioButtonSym5.Checked = _CameraModel.IsSymbol[4];

            radioButtonText1.Checked = !_CameraModel.IsSymbol[0];
            radioButtonText2.Checked = !_CameraModel.IsSymbol[1];
            radioButtonText3.Checked = !_CameraModel.IsSymbol[2];
            radioButtonText4.Checked = !_CameraModel.IsSymbol[3];
            radioButtonText5.Checked = !_CameraModel.IsSymbol[4];
       
            radioButtonSym1.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonSym2.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonSym3.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonSym4.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonSym5.CheckedChanged += RadioButtonRead_CheckedChanged;

            radioButtonText1.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonText2.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonText3.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonText4.CheckedChanged += RadioButtonRead_CheckedChanged;
            radioButtonText5.CheckedChanged += RadioButtonRead_CheckedChanged;
        }
        private void Shared_OnCameraStatusChange(object sender, EventArgs e)
        {
            UpdateCameraInfo();
        }

        private void UpdateCameraInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCameraInfo()));
            }

            txtModel.Text = _CameraModel.Name;
            txtSerialNumber.Text = _CameraModel.SerialNumber;
        }
        private void InitEvents()
        {
            txtIPAddress.TextChanged += AdjustData;
            textBoxPort.TextChanged += AdjustData;
            txtPassword.TextChanged += AdjustData;
            txtNoReadOutputString.TextChanged += AdjustData;

            radAutoReconnectEnable.CheckedChanged += AdjustData;
            radAutoReconnectDisable.CheckedChanged += AdjustData;
            radOutputEnable.CheckedChanged += AdjustData;
            radOutputDisable.CheckedChanged += AdjustData;

            radAutoReconnectEnable.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radAutoReconnectDisable.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radOutputEnable.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radOutputDisable.CheckedChanged += FrmJob.RadioButton_CheckedChanged;

            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;

            Load += UcCameraSettings_Load;
            comboBoxCamType.SelectedIndexChanged += AdjustData;

            // Select Object for OCR IS2800
            checkBoxObject1.CheckedChanged += CheckBoxSelectObjectChange;
            checkBoxObject2.CheckedChanged += CheckBoxSelectObjectChange;
            checkBoxObject3.CheckedChanged += CheckBoxSelectObjectChange;
            checkBoxObject4.CheckedChanged += CheckBoxSelectObjectChange;
            checkBoxObject5.CheckedChanged += CheckBoxSelectObjectChange;
            

        }

       

        private void RadioButtonRead_CheckedChanged(object sender, EventArgs e)
        {
            _CameraModel.IsSymbol[0] = radioButtonSym1.Checked;
            _CameraModel.IsSymbol[1] = radioButtonSym2.Checked;
            _CameraModel.IsSymbol[2] = radioButtonSym3.Checked;
            _CameraModel.IsSymbol[3] = radioButtonSym4.Checked;
            _CameraModel.IsSymbol[4] = radioButtonSym5.Checked;
            Shared.SaveSettings();
        } 

        private void CheckBoxSelectObjectChange(object sender, EventArgs e)
        {
            _CameraModel.ObjectSelectNum = UtilityFunctions.BoolsToInt(new bool[] 
            { 
                checkBoxObject1.Checked, 
                checkBoxObject2.Checked, 
                checkBoxObject3.Checked, 
                checkBoxObject4.Checked, 
                checkBoxObject5.Checked});
            Shared.SaveSettings();
        }

        private void UcCameraSettings_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }

        private void AdjustData(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }
            if (sender == txtIPAddress)
            {
                _CameraModel.IsConnected = false;
                _CameraModel.IP = txtIPAddress.Text;
            }
            else if(sender == textBoxPort)
            {
                _CameraModel.IsConnected = false;
                _CameraModel.Port = textBoxPort.Text;
            }
            else if (sender == txtPassword)
            {
                _CameraModel.Password = txtPassword.Text;
            }
            else if (sender == txtNoReadOutputString)
            {
                _CameraModel.NoReadOutputString = txtNoReadOutputString.Text;
            }
            else if (sender == radAutoReconnectEnable)
            {
                if (radAutoReconnectEnable.Checked == true)
                {
                    _CameraModel.AutoReconnect = true;
                }
            }
            else if (sender == radAutoReconnectDisable)
            {
                if (radAutoReconnectDisable.Checked == true)
                {
                    _CameraModel.AutoReconnect = false;
                }
            }
            else if (sender == radOutputEnable)
            {
                if (radOutputEnable.Checked == true)
                {
                    _CameraModel.OutputEnable = true;
                }
            }
            else if (sender == radOutputDisable)
            {
                if (radOutputDisable.Checked == true)
                {
                    _CameraModel.OutputEnable = false;
                }
            }
            else if (sender == comboBoxCamType)
            {
                _CameraModel.IsConnected = false;
                switch (comboBoxCamType.SelectedIndex)
                {
                    case 0:
                        _CameraModel.CameraType = CameraType.DM;
                        labelPort.Visible = textBoxPort.Visible = false;
                        groupBoxOCR.Visible = false;
                        break;
                    case 1:
                        _CameraModel.CameraType = CameraType.IS;
                        labelPort.Visible = textBoxPort.Visible = true;
                        groupBoxOCR.Visible = true;
                        break;
                    default:
                        break;
                }
            }

            //Save to file
            Shared.SaveSettings();
        }
        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }
            radAutoReconnectEnable.Text =
            radOutputEnable.Text = Lang.Enable;
            radAutoReconnectDisable.Text =
            radOutputDisable.Text = Lang.Disable;

            lblModel.Text = Lang.Model;
            lblNoReadOuputString.Text = Lang.NoReadOuputString;
            lblAutoReconnect.Text = Lang.AutoReconnect;
            lblIPAddress.Text = Lang.IPAddress;
            lblSerialNumber.Text = Lang.SerialNumber;
            lblPassword.Text = Lang.Password;
            lblOutputSignal.Text = Lang.Output;
            grbCamera.Text = Lang.CameraTMP;
        }
    }
}
