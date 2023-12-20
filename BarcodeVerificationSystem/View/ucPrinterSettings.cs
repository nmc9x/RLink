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
    public partial class ucPrinterSettings : UserControl
    {
        private PrinterModel _PrinterModel;
        private int _Index = 0;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                _PrinterModel = Shared.Settings.PrinterList.Count > _Index ? Shared.Settings.PrinterList[_Index] : new PrinterModel { Index = _Index };
            }
        }
        private bool _IsBinding = false;

        public ucPrinterSettings()
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
            txtPrinterIP.Text = _PrinterModel.IP;
            numPrinterPort.Value = _PrinterModel.Port;
            lblPODProtocol.Visible = false;
            radNewPODProtocol.Checked = _PrinterModel.IsVersion;
            radOldPODProtocol.Checked = !_PrinterModel.IsVersion;

            radSupported.Checked = _PrinterModel.CheckAllPrinterSettings;
            radUnsuported.Checked = !_PrinterModel.CheckAllPrinterSettings;
            numPortRemote.Value = _PrinterModel.NumPortRemote;
            _IsBinding = false;
        }

        private void InitEvents()
        {
            txtPrinterIP.TextChanged += AdjustData;
            numPrinterPort.ValueChanged += AdjustData;
            numPortRemote.ValueChanged += AdjustData;
            radNewPODProtocol.CheckedChanged += AdjustData;
            radOldPODProtocol.CheckedChanged += AdjustData;
            radUnsuported.CheckedChanged += AdjustData;
            radSupported.CheckedChanged += AdjustData;
            btnSetupPrinter.Click += AdjustData;
            radNewPODProtocol.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radOldPODProtocol.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radUnsuported.CheckedChanged += FrmJob.RadioButton_CheckedChanged;
            radSupported.CheckedChanged += FrmJob.RadioButton_CheckedChanged;

            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnReceiveResponsePrinter += Shared_OnReceiveResponsePrinter;
            this.Load += UcPrinterSettings_Load;
        }

        private void UcPrinterSettings_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void Shared_OnReceiveResponsePrinter(object sender,EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Shared_OnReceiveResponsePrinter(sender,e)));
                return;
            }
            if (sender is string)
            {
                string message = sender as string;
                //listBoxContentResponsePrinter.Items.Add(DateTime.Now.ToString("yyyy/MM/dd_hh:mm:ss") + "_" + message);
                //listBoxContentResponsePrinter.SelectedIndex = listBoxContentResponsePrinter.Items.Count - 1;
            }
        }

        private void Shared_OnLanguageChange(object sender,EventArgs e)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }
            lblIPrinterIP.Text = Lang.IPAddress;
            lblPrinterPort.Text = Lang.Port;
            lblPODProtocol.Text = Lang.PODProtocol;
            radOldPODProtocol.Text = Lang.OldVersion;
            radNewPODProtocol.Text = Lang.NewVersion;
            grbPrinter.Text = Lang.Printer;
            //lblResponsePrinter.Text = Lang.ResponseContent;
            lblPortRemote.Text = Lang.PrinterRemotePort;
            //lblSetupPrinter.Text = Lang.AdvancedPrinterSettings;
            btnSetupPrinter.Text = Lang.AdvancedPrinterSettings;
            lblPODChangedWarning.Text = Lang.PODChangedWarning;
            lblPrinterOperSys.Text = Lang.CheckAllPrinterSettings;
            radSupported.Text = Lang.Enable;
            radUnsuported.Text = Lang.Disable;
        }

        private void AdjustData(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }
            if(sender == txtPrinterIP)
            {
                var checkExist = Shared.Settings.PrinterList.Find(x => x.IP == _PrinterModel.IP);
                if (checkExist != null)
                {
                    checkExist.IP = txtPrinterIP.Text;
                    var checkExistPOD = Shared.PODControllerList.Find(x => x.ServerIP == _PrinterModel.IP);
                    if (checkExistPOD != null)
                    {
                        checkExistPOD.Disconnect();
                    }
                }
                _PrinterModel.IP = txtPrinterIP.Text;
            }
            else if(sender == numPrinterPort)
            {
                var checkExist = Shared.Settings.PrinterList.Find(x => x.IP == _PrinterModel.IP);
                if (checkExist != null)
                {
                    checkExist.Port = (int)numPrinterPort.Value;
                    var checkExistPOD = Shared.PODControllerList.Find(x => x.ServerIP == _PrinterModel.IP);
                    if (checkExistPOD != null)
                    {
                        checkExistPOD.Disconnect();
                    }
                }
                _PrinterModel.Port = (int)numPrinterPort.Value;
            }
            else if(sender == radNewPODProtocol)
            {
                if (radNewPODProtocol.Checked)
                {
                    _PrinterModel.IsVersion = true;
                }
            }
            else if(sender == radOldPODProtocol)
            {
                if (radOldPODProtocol.Checked)
                {
                    _PrinterModel.IsVersion = false;
                }
            }
            else if(sender == btnSetupPrinter)
            {
                frmRemotePrinter remotePrinter = new frmRemotePrinter();

                remotePrinter.IPAddress = txtPrinterIP.Text;
                remotePrinter.Port = (int)numPortRemote.Value;
                remotePrinter.ShowDialog();
            }
            else if(sender == numPortRemote)
            {
                _PrinterModel.NumPortRemote = (int)numPortRemote.Value;
            }
            else if(sender == radSupported)
            {
                if (radSupported.Checked)
                {
                    _PrinterModel.CheckAllPrinterSettings = true;
                }
            }
            else if(sender == radUnsuported)
            {
                if (radUnsuported.Checked)
                {
                    _PrinterModel.CheckAllPrinterSettings = false;
                }
            }
            // Save to file
            Shared.SaveSettings();
        }
    }
}
