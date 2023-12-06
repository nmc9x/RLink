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
using System.Threading;

namespace BarcodeVerificationSystem.View
{
    public partial class ucSensorSettings : UserControl
    {
        private bool _IsBinding = false;
        public ucSensorSettings()
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
            radSensorControllerEnable.Checked = Shared.Settings.SensorControllerEnable;
            radSensorControllerDisable.Checked = !Shared.Settings.SensorControllerEnable;
            txtSensorControllerIP.Text = Shared.Settings.SensorControllerIP;
            numSensorControllerPort.Value = Shared.Settings.SensorControllerPort;
            numSensorControllerPulseEncoder.Value = Shared.Settings.SensorControllerPulseEncoder;
            numSensorControllerEncoderDiameter.Value = (decimal)Shared.Settings.SensorControllerEncoderDiameter;
            numSensorControllerDelayBefore.Value = Shared.Settings.SensorControllerDelayBefore;
            numSensorControllerDelayAfter.Value = Shared.Settings.SensorControllerDelayAfter;
            EnableSensorController(Shared.Settings.SensorControllerEnable);
            _IsBinding = false;
        }
        
        private void InitEvents()
        {
            radSensorControllerEnable.CheckedChanged += AdjustData;
            radSensorControllerDisable.CheckedChanged += AdjustData;

            radSensorControllerEnable.CheckedChanged += frmJob.RadioButton_CheckedChanged;
            radSensorControllerDisable.CheckedChanged += frmJob.RadioButton_CheckedChanged;

            txtSensorControllerIP.TextChanged += AdjustData;
            numSensorControllerPort.ValueChanged += AdjustData;
            numSensorControllerPulseEncoder.ValueChanged += AdjustData;
            numSensorControllerEncoderDiameter.ValueChanged += AdjustData;
            numSensorControllerDelayBefore.ValueChanged += AdjustData;
            numSensorControllerDelayAfter.ValueChanged += AdjustData;
            btnContenResponseClear.Click += AdjustData;

            numSensorControllerPort.KeyUp += AdjustData;
            numSensorControllerPulseEncoder.KeyUp += AdjustData;
            numSensorControllerEncoderDiameter.KeyUp += AdjustData;
            numSensorControllerDelayBefore.KeyUp += AdjustData;
            numSensorControllerDelayAfter.KeyUp += AdjustData;
            btnContenResponseClear.Click += AdjustData;

            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnRepeatTCPMessageChange += Shared_OnRepeatTCPMessageChange;
            this.Load += UcSensorSettings_Load;
        }

        private void UcSensorSettings_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void Shared_OnLanguageChange(object sender,EventArgs e)
        {
            SetLanguage();
        }
        private void Shared_OnRepeatTCPMessageChange(object sender,EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Shared_OnRepeatTCPMessageChange(sender,e)));
                return;
            }
            //throw new NotImplementedException();
            if (sender is string)
            {
                string message = sender as string;
                richTXTContentResponse.Text = richTXTContentResponse.Text.Insert(0,DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ": " + message + "\n");
                try
                {
                    richTXTContentResponse.SelectedText = message;
                    richTXTContentResponse.ScrollToCaret();
                }
                catch
                {

                }
            }
        }
        private void SetLanguage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetLanguage()));
                return;
            }
            grbSensorController.Text = Lang.SensorController;
            //lblOperation.Text = Lang.;
            lblSensorControllerIP.Text = Lang.IPAddress;
            lblSensorControllerPort.Text = Lang.Port;
            radSensorControllerEnable.Text = Lang.Enable;
            radSensorControllerDisable.Text = Lang.Disable;
            lblContentResponse.Text = Lang.ResponseContent;
            btnContenResponseClear.Text = Lang.ClearResponse;
        }

        private void AdjustData(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }

            if (sender == radSensorControllerEnable)
            {
                if (radSensorControllerEnable.Checked == true)
                {
                    Shared.Settings.SensorControllerEnable = true;
                    EnableSensorController(Shared.Settings.SensorControllerEnable);
                }
            }
            else if (sender == radSensorControllerDisable)
            {
                if (radSensorControllerDisable.Checked == true)
                {
                    Shared.Settings.SensorControllerEnable = false;
                    EnableSensorController(Shared.Settings.SensorControllerEnable);
                }
            }
            else if (sender == txtSensorControllerIP)
            {
                Shared.Settings.SensorControllerIP = txtSensorControllerIP.Text;
            }
            else if (sender == numSensorControllerPort)
            {
                Shared.Settings.SensorControllerPort = (int)numSensorControllerPort.Value;
            }
            else if (sender == numSensorControllerPulseEncoder)
            {
                Shared.Settings.SensorControllerPulseEncoder = (int)numSensorControllerPulseEncoder.Value;
                Shared.SendSettingToSensorController();
            }
            else if (sender == numSensorControllerEncoderDiameter)
            {
                Shared.Settings.SensorControllerEncoderDiameter = (float)numSensorControllerEncoderDiameter.Value;
                Shared.SendSettingToSensorController();
            }
            else if (sender == numSensorControllerDelayBefore)
            {
                Shared.Settings.SensorControllerDelayBefore = (int)numSensorControllerDelayBefore.Value;
                Shared.SendSettingToSensorController();
            }
            else if (sender == numSensorControllerDelayAfter)
            {
                Shared.Settings.SensorControllerDelayAfter = (int)numSensorControllerDelayAfter.Value;
                Shared.SendSettingToSensorController();
            }
            else if (sender == btnContenResponseClear)
            {
                richTXTContentResponse.Text = "";
                richTXTContentResponse.Clear();
                Shared.SendSettingToSensorController();
            }

            // Save to file
            Shared.SaveSettings();
        }
        private void EnableSensorController(bool isEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableSensorController(isEnable)));
                return;
            }

            txtSensorControllerIP.Enabled = isEnable;
            numSensorControllerPort.Enabled = isEnable;
            numSensorControllerPulseEncoder.Enabled = isEnable;
            numSensorControllerEncoderDiameter.Enabled = isEnable;
            numSensorControllerDelayBefore.Enabled = isEnable;
            numSensorControllerDelayAfter.Enabled = isEnable;
        }
    }
}
