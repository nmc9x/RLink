using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{
    public partial class frmSettings : Form
    {
        #region Properties
        private List<ToolStripLabel> _LabelStatusCameraList = new List<ToolStripLabel>();
        private List<ToolStripLabel> _LabelStatusPrinterList = new List<ToolStripLabel>();
        private Timer _DateTimeTicker = new Timer();
        private string _DateTimeFormat = "yyyy/MM/dd hh:mm:ss tt";
        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle = CS_DropShadow;
                return createParams;
            }
        }

        #endregion Properties

        public frmSettings()
        {
            InitializeComponent();
            InitControls();
            SetLanguage();

        }
        protected override void OnHandleCreated(EventArgs e)
        {
            InitEvents();
        }
        private void InitControls()
        {
            _DateTimeTicker.Start();
            // Show icon camera status
            _LabelStatusCameraList.Add(lblStatusCamera01);
            UpdateStatusLabelCamera();

            // Show icon printer status
            _LabelStatusPrinterList.Add(lblStatusPrinter01);
            UpdateStatusLabelPrinter();

            // Show icon sensor controller status
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);

            //Initial tab settings
            tabPageSystemSettings.Controls.Clear();
            ucSystemSettings ucSystemSettings = new ucSystemSettings();
            ucSystemSettings.Dock = DockStyle.Fill;
            tabPageSystemSettings.Controls.Add(ucSystemSettings);
            //END Initial tab settings

            // Initial tab camera settings
            tabPageCameraSettings.Controls.Clear();
            for (int i = 0; i < Shared.Settings.CameraList.Count; i++)
            {
                ucCameraSettings ucCameraSettings = new ucCameraSettings
                {
                    Index = Shared.Settings.CameraList[i].Index,
                    Dock = DockStyle.Top
                };
                tabPageCameraSettings.Controls.Add(ucCameraSettings);
                ucCameraSettings.BringToFront();
            }
            // END Intial tab camera settings

            // Initial tab camera settings
            tabPagePrinterSettings.Controls.Clear();
            for (int i = 0; i < Shared.Settings.PrinterList.Count; i++)
            {
                ucPrinterSettings ucPrinterSettings = new ucPrinterSettings
                {
                    Index = Shared.Settings.PrinterList[i].Index,
                    Dock = DockStyle.Top
                };
                tabPagePrinterSettings.Controls.Add(ucPrinterSettings);
                ucPrinterSettings.BringToFront();
            }
            // END Intial tab camera settings

            //Initial tab camera settings
            tabPageSensorController.Controls.Clear();
            ucSensorSettings ucSensorSettings = new ucSensorSettings();
            ucSensorSettings.Dock = DockStyle.Fill;
            tabPageSensorController.Controls.Add(ucSensorSettings);
            //END Initial tab camera settings
        }

        private void InitEvents()
        {
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;
            Shared.OnPrinterStatusChange += Shared_OnPrinterStatusChange;
            Shared.OnSensorControllerChangeEvent += Shared_OnSensorControllerChangeEvent;
            _DateTimeTicker.Tick += TimerDateTime_Tick;
        }

        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            toolStripDateTime.Text = DateTime.Now.ToString(_DateTimeFormat);
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
            tabPageCameraSettings.Text = Lang.CameraSettings;
            tabPageSystemSettings.Text = Lang.SystemSettings;
            tabPagePrinterSettings.Text = Lang.PrinterSettings;
            tabPageSensorController.Text = Lang.SensorController;
            lblFormName.Text = Lang.Settings;
            toolStripVersion.Text = Lang.Version + ": " + Properties.Settings.Default.SoftwareVersion;
            lblSensorControllerStatus.Text = Lang.SensorController;
            lblStatusCamera01.Text = Lang.CameraTMP;
            lblStatusPrinter01.Text = Lang.Printer;
        }
        private void Shared_OnPrinterStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelPrinter();
        }
        private void Shared_OnCameraStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelCamera();
        }
        private void Shared_OnSensorControllerChangeEvent(object sender, EventArgs e)
        {
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);
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
    }
}
