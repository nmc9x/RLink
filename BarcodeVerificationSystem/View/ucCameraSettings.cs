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
            labelPort.Visible = textBoxPort.Visible = _CameraModel.CameraType == CameraType.IS;
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
            txtPassword.TextChanged += AdjustData;
            txtNoReadOutputString.TextChanged += AdjustData;
           
            radAutoReconnectEnable.CheckedChanged += AdjustData;
            radAutoReconnectDisable.CheckedChanged += AdjustData;
            radOutputEnable.CheckedChanged += AdjustData;
            radOutputDisable.CheckedChanged += AdjustData;

            radAutoReconnectEnable.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radAutoReconnectDisable.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radOutputEnable.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radOutputDisable.CheckedChanged += frmJob.RadioButton_CheckedChanged;

            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;

            this.Load += UcCameraSettings_Load;
            comboBoxCamType.SelectedIndexChanged += AdjustData;
        }

       


        private void UcCameraSettings_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void Shared_OnLanguageChange(object sender,EventArgs e)
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
            else if(sender == comboBoxCamType)
            {
                _CameraModel.IsConnected = false;
                switch (comboBoxCamType.SelectedIndex)
                {
                    case 0:
                        _CameraModel.CameraType = CameraType.DM;
                        labelPort.Visible = textBoxPort.Visible = false;
                        break;
                    case 1:
                        _CameraModel.CameraType = CameraType.IS;
                        labelPort.Visible = textBoxPort.Visible = true;
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
