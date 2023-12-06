using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using UILanguage;
using BarcodeVerificationSystem.Controller;
using System.Runtime.InteropServices;

namespace BarcodeVerificationSystem.View
{
    /// <summary>
    /// @Author: TrangDong
	/// @Email: trang.dong@rynantech.com
    /// </summary>
    public partial class frmAbout : Form
    //public partial class frmAbout:Form //For edit user interface
    {
        //private const int CS_DropShadow = 0x00020000;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams createParams = base.CreateParams;
        //        createParams.ClassStyle = CS_DropShadow;
        //        return createParams;
        //    }
        //}
        public frmAbout()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitControls();
            SetLanguage();
            Shared.OnLanguageChange += Shared_OnLanguageChange;
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
            lblFormName.Text = Lang.About;
            grbGeneralInfo.Text = Lang.GeneralInfo;
            grbReleaseNote.Text = Lang.ReleaseNote;
        }
        private void InitControls()
        {
            try
            {
                //load info
                if (File.Exists(Application.StartupPath + "\\Labels\\about.txt"))
                {
                    using (FileStream fs = File.Open(Application.StartupPath + "\\Labels\\about.txt",FileMode.Open,FileAccess.Read))
                    {
                        using (StreamReader rd = new StreamReader(fs))
                        {
                            String about = rd.ReadToEnd();
                            about = about.Replace("Soft_Version",Properties.Settings.Default.SoftwareVersion);


                            //about = about.Replace("Product",Lang.ProductName);
                            //about = about.Replace("Version",Lang.Version);
                            //about = about.Replace("Email",Lang.Email);
                            //about = about.Replace("Website",Lang.Website);
                            //about = about.Replace("Address",Lang.Address);
                            webBrowser1.DocumentText = about;
                        }
                    }
                }

                //load readme
                String text = File.ReadAllText(Application.StartupPath + "\\Readme.txt",Encoding.UTF8);
                rchReleaseNote.Text = text;

            }
            catch (Exception)
            {
                //Debug.WriteLine(ex.Message);
            }
        }

    }
}
