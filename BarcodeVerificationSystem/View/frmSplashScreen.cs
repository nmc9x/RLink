using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeVerificationSystem.View
{
    /// <summary>
    /// @Author: TrangDong
    /// </summary>
    public partial class frmSplashScreen : Form
    {
        //private GifImage gifImage;
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
        public frmSplashScreen()
        {
            InitializeComponent();

            InitControl();
        }

        private void InitControl()
        {
            //gifImage = new GifImage(Properties.Resource.Loading);
            //gifImage.ReverseAtEnd = false; //dont reverse at end

            //timer1.Interval = 100;
            //timer1.Tick += new EventHandler(timer1_Tick);
            //timer1.Start();
        }

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmSplashScreen _splashForm;
        private static Thread _splashThread;

        private static bool IsWaitOne = false;

        public static void ShowSplashScreen()
        {
            IsWaitOne = true;
            if (_splashThread == null)
            {
                // show the form in a new thread            
                _splashThread = new Thread(new ThreadStart(DoShowSplash));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        public static void ShowSplashScreen(String message, String comment)
        {
            IsWaitOne = true;
            if (_splashThread == null)
            {
                // show the form in a new thread            
                _splashThread = new Thread(new ThreadStart(() => DoShowSplash(message, comment)));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        // Called by the thread    
        private static void DoShowSplash()
        {
            if (_splashForm == null)
            {
                _splashForm = new frmSplashScreen();
                //_splashForm.lblComment.Text = "";
                _splashForm.StartPosition = FormStartPosition.CenterScreen;
                _splashForm.TopMost = true;
            }

            IsWaitOne = false;
            Application.Run(_splashForm);
        }

        // Called by the thread    
        private static void DoShowSplash(String message, String comment)
        {

            if (_splashForm == null)
            {
                _splashForm = new frmSplashScreen();
                _splashForm.lblLoading.Text = message;
                _splashForm.lblComment.Text = comment;
                _splashForm.StartPosition = FormStartPosition.CenterScreen;
                _splashForm.TopMost = true;
            }
            IsWaitOne = false;
            // create a new message pump on this thread (started from ShowSplash)        
            Application.Run(_splashForm);
        }

        // Close the splash (Loading...) screen    
        public static void CloseSplash()
        {
            int i = 0;
            while (IsWaitOne && i < 20)
            {
                Thread.Sleep(100);
                i++;
            }

            if (_splashForm == null)
            {
                return;
            }
            // Need to call on the thread that launched this splash        
            if (_splashForm.InvokeRequired)
            {
                _splashForm.Invoke(new MethodInvoker(CloseSplash));
            }
            else
            {
                _splashThread = null;
                _splashForm = null;
                Application.ExitThread();
            }
        }
    }
}
