using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using System;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{

    public partial class UcPrinterSettings : UserControl
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

        public UcPrinterSettings()
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
            Load += UcPrinterSettings_Load;
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
                var message = sender as string;
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
                Invoke(new Action(() => SetLanguage()));
                return;
            }
            lblIPrinterIP.Text = Lang.IPAddress;
            lblPrinterPort.Text = Lang.Port;
            lblPODProtocol.Text = Lang.PODProtocol;
            radOldPODProtocol.Text = Lang.OldVersion;
            radNewPODProtocol.Text = Lang.NewVersion;
            grbPrinter.Text = Lang.Printer;
            lblPortRemote.Text = Lang.PrinterRemotePort;
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
                PrinterModel checkExist = Shared.Settings.PrinterList.Find(x => x.IP == _PrinterModel.IP);
                if (checkExist != null)
                {
                    checkExist.IP = txtPrinterIP.Text;
                    PODController checkExistPOD = Shared.PODControllerList.Find(x => x.ServerIP == _PrinterModel.IP);
                    checkExistPOD?.Disconnect();
                }
                _PrinterModel.IP = txtPrinterIP.Text;
            }
            else if(sender == numPrinterPort)
            {
                PrinterModel checkExist = Shared.Settings.PrinterList.Find(x => x.IP == _PrinterModel.IP);
                if (checkExist != null)
                {
                    checkExist.Port = (int)numPrinterPort.Value;
                    var checkExistPOD = Shared.PODControllerList.Find(x => x.ServerIP == _PrinterModel.IP);
                    checkExistPOD?.Disconnect();
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
                var remotePrinter = new FrmRemotePrinter
                {
                    IPAddress = txtPrinterIP.Text,
                    Port = (int)numPortRemote.Value
                };
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
            Shared.SaveSettings();
        }
    }
}
