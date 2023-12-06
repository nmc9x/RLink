using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using CommonVariable;
using OperationLog.Controller;
using OperationLog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{
    public partial class frmLoginNew : Form
    {
        private bool _IsBinding = false;
        private bool _IsProcessing = false;
        private string _RememberPath = "";
        private Thread _ThreadLogin;

        public frmLoginNew()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            InitControl();
            InitEvent();
            SetLanguage();
        }

        private void InitControl()
        {
            _IsBinding = true;
            KillThreadLogin();
            lblMessage.Visible = false;
            _RememberPath = CommVariables.PathSettingsApp;
            if (!Directory.Exists(_RememberPath))
            {
                Directory.CreateDirectory(_RememberPath);
            }
            _RememberPath += "remember.dat";

            // Load remember file
            try
            {
                if (File.Exists(_RememberPath))
                {
                    String[] texts = File.ReadAllLines(_RememberPath);
                    txtUsername.Text = SecurityController.Decrypt(texts[0], "rynan_encrypt_remember");
                    txtPassword.Text = SecurityController.Decrypt(texts[1], "rynan_encrypt_remember");
                    chbRememberPassword.Checked = true;
                }
            }
            catch
            { }
            _IsBinding = false;
        }

        private void InitEvent()
        {
            btnLogin.Click += ActionChanged;
            //btnCancel.Click += ActionChanged;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
        }
        
        private void Shared_OnLanguageChange(object sender,EventArgs e)
        {
            SetLanguage();
        }

        private void ActionChanged(object sender, EventArgs e)
        {
            if (_IsBinding)
            {
                return;
            }
            if(sender == btnLogin)
            {
                if(txtUsername.Text == "" || txtPassword.Text == "")
                {
                    UpdateMessageLabel(true,false,Lang.UsernameOrPasswordCannotBeLeftBlank);
                }
                else
                {
                    Login(txtUsername.Text, txtPassword.Text, chbRememberPassword.Checked);
                }
            }
            //else if(sender == btnCancel)
            //{
            //    this.Close();
            //}
        }
        private void Login(string username, string password, bool isRemenber)
        {
            if (_IsProcessing)
            {
                return;
            }

            KillThreadLogin();

            _ThreadLogin = new Thread(() =>
            {
                _IsProcessing = true;
                UpdateMessageLabel(false, true, "");

                if (username.Trim() == "" || password == "")
                {
                    UpdateMessageLabel(true, false, Lang.UsernameOrPasswordCannotBeLeftBlank);
                }
                else
                {
                    if (isRemenber)
                    {
                        // Do remember
                        try
                        {
                            if (File.Exists(_RememberPath))
                            {
                                File.Delete(_RememberPath);
                            }
                            String ecryptUsername = SecurityController.Encrypt(txtUsername.Text, "rynan_encrypt_remember");
                            String ecryptPassword = SecurityController.Encrypt(txtPassword.Text, "rynan_encrypt_remember");
                            String[] content = { ecryptUsername, ecryptPassword };
                            File.WriteAllLines(_RememberPath, content);
                        }
                        catch
                        { }
                    }
                    else
                    {
                        // Remove remember
                        if (File.Exists(_RememberPath))
                        {
                            File.Delete(_RememberPath);
                        }
                    }

                    ActivationStatus activationStatus = Shared.LoginLocal(username, password);
                    if (activationStatus == ActivationStatus.Successful)
                    {
                        // Save history
                        LoggingController.SaveHistory("Login success",
                            "Login",
                            "Login success: " + username,
                            username,
                            LoggingType.LogedIn);

                        Invoke(new Action(() =>
                        {
                            // Close login form
                            DialogResult = DialogResult.OK;
                        }));
                    }
                    else
                    {
                        UpdateMessageLabel(true, false, Lang.UsernameOrPasswordIsIncorrect);

                        //save history
                        LoggingController.SaveHistory("Login error",
                            "Login",
                            "Password incorrect!",
                            username,
                            LoggingType.LogedIn);
                    }
                }

                _IsProcessing = false;
            });
            _ThreadLogin.IsBackground = true;
            _ThreadLogin.Priority = ThreadPriority.Normal;
            _ThreadLogin.Start();
        }

        private void KillThreadLogin()
        {
            if (_ThreadLogin != null && _ThreadLogin.IsAlive)
            {
                // Release thread
                _ThreadLogin.Abort();
                _ThreadLogin = null;
            }
            _IsProcessing = false;
        }
        private void UpdateMessageLabel(bool isVisible, bool isNormalState, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateMessageLabel(isVisible, isNormalState, message)));
                return;
            }
            picLoading.Visible = !isVisible;
            lblMessage.Visible = isVisible;
            if (isNormalState)
            {
                lblMessage.ForeColor = Color.Black;
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Text = message;

            txtUsername.Enabled = isVisible;
            txtPassword.Enabled = isVisible;
            chbRememberPassword.Enabled = isVisible;
            btnLogin.Enabled = isVisible;
        }
        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }
            lblLogIn.Text = Lang.Login.ToUpper();
            lblUsername.Text = Lang.Username;
            lblPassword.Text = Lang.Password;
            chbRememberPassword.Text = Lang.RememberPassword;
            lblMessage.Text = Lang.UsernameOrPasswordIsIncorrect;
            btnLogin.Text = Lang.Login.ToUpper();
            //btnCancel.Text = Lang.Exit.ToUpper();
        }
    }
}
