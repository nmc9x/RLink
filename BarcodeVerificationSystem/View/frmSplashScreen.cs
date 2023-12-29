using System.Threading;
using System.Windows.Forms;

namespace BarcodeVerificationSystem.View
{
    public partial class FrmSplashScreen : Form
    {
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
        public FrmSplashScreen()
        {
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
          
        }

        private delegate void CloseDelegate();
        private static FrmSplashScreen _splashForm;
        private static Thread _splashThread;
        private static bool IsWaitOne = false;

        public static void ShowSplashScreen()
        {
            IsWaitOne = true;
            if (_splashThread == null)
            {
                _splashThread = new Thread(new ThreadStart(DoShowSplash))
                {
                    IsBackground = true
                };
                _splashThread.Start();
            }
        }

        public static void ShowSplashScreen(string message, string comment)
        {
            IsWaitOne = true;
            if (_splashThread == null)
            {
                _splashThread = new Thread(new ThreadStart(() => DoShowSplash(message, comment)))
                {
                    IsBackground = true
                };
                _splashThread.Start();
            }
        }

        private static void DoShowSplash()
        {
            if (_splashForm == null)
            {
                _splashForm = new FrmSplashScreen
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
            }

            IsWaitOne = false;
            Application.Run(_splashForm);
        }

        private static void DoShowSplash(string message, string comment)
        {
            if (_splashForm == null)
            {
                _splashForm = new FrmSplashScreen();
                _splashForm.lblLoading.Text = message;
                _splashForm.lblComment.Text = comment;
                _splashForm.StartPosition = FormStartPosition.CenterScreen;
                _splashForm.TopMost = true;
            }
            IsWaitOne = false;      
            Application.Run(_splashForm);
        }
        
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
