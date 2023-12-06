using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeVerificationSystem.View
{
    public partial class frmRemotePrinter : Form
    {
        public string IPAddress = "";
        public int Port = 1001;

        public frmRemotePrinter()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            string url = $"{IPAddress}:{Port}";
            //string url = $"chat.zalo.me";
            //webView21.Source = new Uri(queryURL);
            InitializeBrowser(url);

            reloadToolStripMenuItem.Click += (sender, eventArgs) =>
            {
                webView21.Reload();
            };

            exitToolStripMenuItem.Click += (sender, eventArgs) =>
            {
                webView21.Dispose();
                this.Close();
            };

            btnMenu.MouseDown += (sender, eventArgs) => {
                cuzDropdownMenu.PrimaryColor = Color.FromArgb(0, 171, 230);
                cuzDropdownMenu.MenuItemHeight = 40;
                cuzDropdownMenu.Font = new Font("Microsoft Sans Serif", 12);
                cuzDropdownMenu.ForeColor = Color.Black;
                cuzDropdownMenu.Show(btnMenu, btnMenu.Width+5, -17);
            };

            btnMenu.LostFocus += (s, ev) =>
            {
                cuzDropdownMenu.Close();
            };
        }


        private async void InitializeBrowser(string url = null)
        {
            var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BarcodeVerificationSystems";
            var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            await webView21.EnsureCoreWebView2Async(env);
            webView21.Source = new UriBuilder(url).Uri;
        }
    }
}
