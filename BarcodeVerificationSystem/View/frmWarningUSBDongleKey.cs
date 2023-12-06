using BarcodeVerificationSystem.Controller;
using OperationLog.Controller;
using OperationLog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{
    //public partial class frmWarningUSBDongleKey : Form
    public partial class frmWarningUSBDongleKey : Form
    {
        private Timer _TimerCloseApp = new Timer();
        public frmWarningUSBDongleKey()
        {
            InitializeComponent();
            InitControl();
            InitEvents();
            SetLanguage();
            UpdateIcon();// Comment for edit user interface
        }

        #region Init

        private void InitControl()
        {
            _TimerCloseApp.Interval = 60 * 1000;//60 seconds
            _TimerCloseApp.Start();
            //Write log error 
            LoggingController.SaveHistory("USB key",
                    "USB key",
                    "USB key invalid or unplugged!",
                    UserController.LogedInUsername,
                    LoggingType.Error);
        }

        #endregion Init

        #region Event

        private void InitEvents()
        {
            btnClose.Click += btnClose_Click;
            _TimerCloseApp.Tick += TimerCloseApp_Tick;
            this.FormClosing += frmWarningUSBDongleKey_FormClosing;
            Shared.OnLanguageChange += ShareFunction_OnLanguageChange;
        }

        private void TimerCloseApp_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            CloseApplication();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            CloseApplication();
        }

        private void frmWarningUSBDongleKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            RemoveEventHandler();
            CloseApplication();
        }

        private void RemoveEventHandler()
        {
            Shared.OnLanguageChange -= ShareFunction_OnLanguageChange;
        }

        private void ShareFunction_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }

        #endregion Event

        #region Close application

        private void CloseApplication()
        {
            // The user wants to exit the application. Close everything down.
            //Application.Exit(); //Close app but will ask comfirm form user
            Environment.Exit(0); //Close app but dont ask comfirm form user
        }

        #endregion Close application

        #region Override
        public void SetLanguage()
        //public void SetLanguage() // For edit user interface
        {
            //base.SetLanguage();
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetLanguage()));
                return;
            }

            Text = Lang.Warning;
            btnClose.Text = Lang.Exit;
            lblCheckUSBKey.Text = Lang.PleaseCheckTheUSBKey;

        }
        public void UpdateIcon()
        {
            String path = Application.StartupPath + "\\Label\\icon.ico";
            if (File.Exists(path))
            {
                this.Icon = Icon.ExtractAssociatedIcon(path);
            }
            else
            {
                this.ShowIcon = false;
            }
        }
        #endregion Override
    }
}
