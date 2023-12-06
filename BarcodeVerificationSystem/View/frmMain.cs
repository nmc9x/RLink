using System;
using System.IO;
using UILanguage;
using System.Data;
using System.Text;
using System.Linq;
using System.Drawing;
using CommonVariable;
using System.Threading;
using OperationLog.Model;
using System.Diagnostics;
using System.Windows.Forms;
using DesignUI.CuzMesageBox;
using System.Threading.Tasks;
using OperationLog.Controller;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BarcodeVerificationSystem.Model;
using Timer = System.Windows.Forms.Timer;
using BarcodeVerificationSystem.Controller;
using DesignUI.CuzAlert;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace BarcodeVerificationSystem.View
{
    public partial class frmMain : Form
    {
        #region Variables
        private frmJob _ParentForm = null;
        private JobModel _SelectedJob = new JobModel();

        private bool _IsPrinterDisconnectedNot = false;

        //Datetime clock 
        private Timer _TimerDateTime = new Timer();
        private string _DateTimeFormatTicker = "yyyy/MM/dd hh:mm:ss tt";
        //END Datetime clock 

        // Values
        private int _TotalCode = 0;
        private int _TotalChecked = 0;
        private int _TotalMissed = 0;
        private int _ReceivedCode = 0;
        private int _NumberPrinted = 0;
        private int _NumberOfCheckPassed = 0;
        private int _NumberOfCheckFailed = 0;
        private int _NumberOfSentPrinter = 0;
        private int _NumberOfDuplicate = 0;
        private int _TotalColumns = 1;
        private int _StartIndex = 1;
        private int _EndIndex = 1;
        public int TotalChecked { get { return _TotalChecked; } set { _TotalChecked = value; Invoke(new Action(() => { lblTotalCheckedValue.Text = string.Format("{0:N0}", _TotalChecked); })); } }
        public int NumberOfCheckPassed { get { return _NumberOfCheckPassed; } set { _NumberOfCheckPassed = value; Invoke(new Action(() => { lblCheckResultPassedValue.Text = string.Format("{0:N0}", _NumberOfCheckPassed); })); } }
        public int NumberOfCheckFailed { get { return _NumberOfCheckFailed; } set { _NumberOfCheckFailed = value; Invoke(new Action(() => { lblCheckResultFailedValue.Text = string.Format("{0:N0}", _NumberOfCheckFailed); })); } }

        public int NumberPrinted { get { return _NumberPrinted; } set { _NumberPrinted = value; Invoke(new Action(() => { lblPrintedCodeValue.Text = string.Format("{0:N0}", _NumberPrinted); })); } }
        public int ReceivedCode { get { return _ReceivedCode; } set { _ReceivedCode = value; Invoke(new Action(() => { lblReceivedValue.Text = string.Format("{0:N0}", _ReceivedCode); })); } }
        public int NumberOfSentPrinter { get { return _NumberOfSentPrinter; } set { _NumberOfSentPrinter = value; Invoke(new Action(() => { lblSentDataValue.Text = string.Format("{0:N0}", _NumberOfSentPrinter); })); } }
        // End values

        // show max line on display dataGridViewDatabase

        private int _MaxDatabaseLine = 500;

        private List<ToolStripLabel> _LabelStatusCameraList = new List<ToolStripLabel>();
        private List<ToolStripLabel> _LabelStatusPrinterList = new List<ToolStripLabel>();

        #endregion Variables

        readonly static object _SyncObjBufferUpdateUIPrinter = new object();
        readonly static object _SyncObjCodeList = new object();
        readonly static object _SyncObjCheckedResultList = new object();
        readonly static object _SyncObjBufferDataObtained = new object();
        readonly static object _SyncObjPODCodeList = new object();
        readonly static object _SyncObjBufferDataObtainedResult = new object();
        readonly static object _SyncObjBufferExportImage = new object();
        readonly static object _SyncObjBufferExportResultFile = new object();
        readonly static object _SyncObjBufferExportPrintedResponeseFile = new object();
        private static object _SendLocker = new object();
        private static object _ProgressBarLocker = new object();

        private string _DateTimeFormat = "yyMMddHHmmss";
        private string[] _DatabaseColunms = new string[0];
        private string[] _ColumnNames = new string[] { "Index", "ResultData", "Result", "ProcessingTime", "DateTime" };
        private string[] defaultRecord = new string[] { "100000", "abcdefghijk123456789abcdefhgh", "Valid", "100", DateTime.Now.ToString() };

        #region On Production Variables

        private bool _IsAfterProductionMode = false;
        private bool _IsOnProductionMode = false;
        private bool _IsVerifyAndPrintMode = false;

        private bool _IsPrintedWait = false;
        private bool _IsSendWait = false;
        private bool _IsCheckedWait = true;
        private bool _IsDetectWait = true;
        private bool _IsPrintedResponse = false;
        
        private List<InitDataError> _InitDataErrorList = new List<InitDataError>();

        private ComparisonResult _CheckedResult = ComparisonResult.Valid;
        private ComparisonResult _PrintedResult = ComparisonResult.Valid;
        private PrinterStatus _PrinterStatus = PrinterStatus.Null;

        private object _PrintLocker = new object();
        private object _ReceiveLocker = new object();
        private object _CheckLocker = new object();
        private object _DetectLocker = new object();
        private object _PrintedResponseLocker = new object();
        private object _PrinterResponseRevceiveLocker = new object();

        #endregion

        #region Thread
        private Thread _ThreadPrinterResponseHandler = null;

        #endregion Thread

        #region Buffer
        private Queue<string> _QueueBufferPrintedResponse = new Queue<string>();
        private Queue<ExportImageModel> _QueueBufferExportImage = new Queue<ExportImageModel>();
        private Queue<ExportResultFileModel> _QueueBufferExportResultFile = new Queue<ExportResultFileModel>();
        private Queue<ExportResultFileModel> _QueueBufferExportPrintedResponseFile = new Queue<ExportResultFileModel>();

        private SynchronizedQueue<DetectModel> _QueueBufferDataObtained = new SynchronizedQueue<DetectModel>();
        private SynchronizedQueue<DetectModel> _QueueBufferDataObtainedResult = new SynchronizedQueue<DetectModel>();
        private SynchronizedQueue<string> _QueueBufferUpdateUIPrinter = new SynchronizedQueue<string>();

        private SynchronizedQueue<ExportImageModel> _QueueBufferBackupImage = new SynchronizedQueue<ExportImageModel>();
        private SynchronizedQueue<List<string[]>> _QueueBufferBackupPrintedCode = new SynchronizedQueue<List<string[]>>();
        private SynchronizedQueue<List<string[]>> _QueueBufferBackupCheckedResult = new SynchronizedQueue<List<string[]>>();
        private SynchronizedQueue<object> _QueueBufferPrinterResponseData = new SynchronizedQueue<object>();
        private SynchronizedQueue<string[]> _QueueBufferBackupSendLog = new SynchronizedQueue<string[]>();

        private List<string[]> _PrintedCodeObtainFromFile = new List<string[]>();
        private List<string[]> _CheckedResultCodeList = new List<string[]>();
        //private List<string> _CodeListPODFormat = new List<string>();
        private ConcurrentDictionary<string, CompareStatus> _CodeListPODFormat = new ConcurrentDictionary<string, CompareStatus>();
        private ConcurrentDictionary<string, int> _Emergency = new ConcurrentDictionary<string, int>();
        private List<PODModel> _PODFormat = new List<PODModel>();
        private List<PODModel> _PODList = new List<PODModel>();
        #endregion Buffer

        #region CancellationTokenSource

        private CancellationTokenSource _OperationCancelTokenSource;
        private CancellationTokenSource _UICheckedResultCancelTokenSource;
        private CancellationTokenSource _UIPrintedResponseCancelTokenSource;
        private CancellationTokenSource _BackupResultCancelTokenSource;
        private CancellationTokenSource _BackupResponseCancelTokenSource;
        private CancellationTokenSource _BackupImageCancelTokenSource;
        private CancellationTokenSource _SendDataToPrinterTokenCTS;
        private CancellationTokenSource _PrinterRespontCST;
        private CancellationTokenSource _VirtualCTS;
        private CancellationTokenSource _BackupSendLogCancelTokenSource;

        #endregion


        private frmSettings _FormSettings;
        private frmViewHistoryProgram _FormViewHistoryProgram;
        private frmPreviewDatabase _FormPreviewDatabase;
        private frmCheckedResult _FormCheckedResult;

        private PrinterSettingsModel _PrinterSettingsModel;
        //Export
        private string _ExportNamePrefix = "";
        //Prefix is datatime
        private string _ExportCheckedResultFileName = "";
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam); bool isFullHD = false;
        bool isDelProcessPnlMargin = false;
        public bool IsFullHD
        {
            get
            {
                return isFullHD;
            }

            set
            {
                isFullHD = value;
                if (!IsFullHD)
                {
                    tableLayoutPanelMain.ColumnStyles[0].Width = 0;
                    tableLayoutPanel1.Controls.Remove(pnlDatabase);
                    tableLayoutPanel1.Controls.Remove(pnlCheckedResult);
                    tableLayoutPanel1.RowCount = 1;
                    tableLayoutPanel1.ColumnCount = 2;
                    tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                    tableLayoutPanel1.ColumnStyles[0].Width = 50;
                    tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                    tableLayoutPanel1.ColumnStyles[1].Width = 50;
                    var pad = pnlVerificationProcess.Margin;
                    pad.Right += !isDelProcessPnlMargin ? 11 : 0;
                    pnlVerificationProcess.Margin = pad;
                    isDelProcessPnlMargin = true;
                    tableLayoutPanel1.Controls.Add(pnlVerificationProcess, 0, 0);
                    tableLayoutPanel1.Controls.Add(tableLayoutPanelCheckedResult, 2, 0);
                }
                else
                {
                    tableLayoutPanelMain.ColumnStyles[0].Width = 418;
                    tableLayoutPanel.RowCount = 2;
                    tableLayoutPanel.ColumnCount = 1;
                    var pad = pnlVerificationProcess.Margin;
                    pad.Right -= isDelProcessPnlMargin ? 11 : 0;
                    pnlVerificationProcess.Margin = pad;
                    isDelProcessPnlMargin = false;
                    tableLayoutPanel.Controls.Add(pnlVerificationProcess, 0, 1);
                    tableLayoutPanel.Controls.Add(tableLayoutPanelCheckedResult, 0, 0);

                    tableLayoutPanel1.RowCount = 2;
                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.RowStyles[0].Height = _SelectedJob.CompareType == CompareType.Database ?
                         tableLayoutPanel1.Width * 50 / 100 : 0;
                    tableLayoutPanel1.RowStyles[1].Height = tableLayoutPanel1.Width * 50 / 100;
                    if (_SelectedJob.CompareType != CompareType.Database)
                    {
                        tableLayoutPanel1.Controls.Remove(pnlDatabase);
                    }
                    else
                    {
                        tableLayoutPanel1.Controls.Add(pnlDatabase, 0, 0);
                    }
                    tableLayoutPanel1.Controls.Add(pnlCheckedResult, 0, 1);
                }
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(frmJob parentForm)
        {
            InitializeComponent();
            _ParentForm = parentForm;
            this.WindowState = FormWindowState.Maximized;
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_NCHITTEST = 0x0084;
            const int WM_SYSCOMMAND = 0x0112;
            const int WM_SIZE = 0x0005;

            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            const int resizeSize = 10;

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1 && this.FormBorderStyle == FormBorderStyle.Sizable)
            {
                m.Result = IntPtr.Zero;
                return;
            }
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);
                    if (clientPoint.Y <= resizeSize)
                    {
                        if (clientPoint.X <= resizeSize)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (clientPoint.X < (this.Size.Width - resizeSize))
                            m.Result = (IntPtr)HTTOP;
                        else
                            m.Result = (IntPtr)HTTOPRIGHT;
                    }
                    else if (clientPoint.Y <= (this.Size.Height - resizeSize))
                    {
                        if (clientPoint.X <= resizeSize)
                            m.Result = (IntPtr)HTLEFT;
                        else if (clientPoint.X > (this.Width - resizeSize))
                            m.Result = (IntPtr)HTRIGHT;
                    }
                    else
                    {
                        if (clientPoint.X <= resizeSize)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (clientPoint.X < (this.Size.Width - resizeSize))
                            m.Result = (IntPtr)HTBOTTOM;
                        else
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                    }
                }
                return;
            }
            else if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam == (IntPtr)0xF120)
                {
                    this.SuspendLayout();
                }
                else if (m.WParam == (IntPtr)0xF030)
                {
                    this.SuspendLayout();
                }
                else if (m.WParam == (IntPtr)0xF020)
                {

                }
            }
            else if (m.Msg == WM_SIZE)
            {
                Padding pad = this.WindowState == FormWindowState.Maximized ? new Padding(0) : new Padding(2, 2, 2, 2);
                this.Invoke(new Action(() =>
                {
                    this.Padding = pad;
                }));
            }
            base.WndProc(ref m);
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(RestoreBounds.Left, RestoreBounds.Top, RestoreBounds.Width, RestoreBounds.Height, BoundsSpecified.All);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitControls();
            InitEvents();
        }

        private void InitControls()
        {
            this.TransparencyKey = Color.DarkKhaki;
            SetLanguage();

            _TimerDateTime.Start();

            if (Shared.LoggedInUser.Role != 0)
            {
                mnManage.Visible = false;
                if (Shared.LoggedInUser.Role == 1000)
                {
                    mnManage.Visible = true;
                }
            }

            // Show icon camera status
            _LabelStatusCameraList.Add(lblStatusCamera01);
            UpdateStatusLabelCamera();

            // Show icon printer status
            _LabelStatusPrinterList.Add(lblStatusPrinter01);
            UpdateStatusLabelPrinter();

            // Show icon sensor controller status
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);
#if DEBUG
            DebugVirtual();
#endif
            UpdateJobInfomationInterface();

            if (_SelectedJob.CompareType == CompareType.Database)
            {
                tableLayoutPanelPrintedState.Visible = true;
                btnDatabase.Visible = true;
                btnExport.Visible = true;
                if (_SelectedJob.PrinterSeries)
                {
                    _IsPrinterDisconnectedNot = true;
                }
            }
            else
            {
                _IsPrinterDisconnectedNot = false;
                tableLayoutPanelPrintedState.Visible = false;
                btnDatabase.Visible = false;
                btnExport.Visible = false;
            }
            IsFullHD = true;

        }

        private void DebugVirtual()
        {
            btnVirtualStart.Visible = true;
            btnVirtualStop.Visible = true;
            btnValid.Visible = true;
            btnInvalid.Visible = true;
            btnDuplicate.Visible = true;
            btnNull.Visible = true;

            btnVirtualStart.Click += (sender, eventArgs) =>
            {
                StartAllThreadForTesting();
            };

            btnVirtualStop.Click += (sender, eventArgs) =>
            {
                StopAllThreadForTesting();
            };

            btnValid.Click += async (sender, eventArgs) =>
            {
                await Task.Run(() => { AddValidInput(); });
            };

            btnInvalid.Click += async (sender, eventArgs) =>
            {
                await Task.Run(() => { AddInvalidInput(0); });
            };

            btnDuplicate.Click += async (sender, eventArgs) =>
            {
                await Task.Run(() => { AddInvalidInput(1); });
            };

            btnNull.Click += async (sender, eventArgs) =>
            {
                await Task.Run(() => { AddInvalidInput(2); });
            };
        }

        private string printedResponseValue = "";
        public void AddValidInput()
        {
            DetectModel dtm = new DetectModel();
            dtm.Text = "OKDC_SSTYOGHS115220";
            string[] data = new string[0];

            if (!_IsVerifyAndPrintMode)
            {
                if (_CodeListPODFormat.TryGetValue(printedResponseValue, out CompareStatus compareStatus))
                {
                    if (!compareStatus.Status)
                    {
                        data = _PrintedCodeObtainFromFile[compareStatus.Index];
                    }
                }

                printedResponseValue = "";
            }
            else
            {
                for (int i = 0; i < _TotalCode; i++)
                {
                    string tmp = GetCompareDataByPODFormat(_PrintedCodeObtainFromFile[i], _SelectedJob.PODFormat);
                    if (_CodeListPODFormat.TryGetValue(tmp, out CompareStatus compareStatus))
                    {
                        if (!compareStatus.Status)
                        {
                            data = _PrintedCodeObtainFromFile[i];
                            break;
                        }
                    }
                }
            }

            if (_SelectedJob.CompareType == CompareType.Database)
            {
                dtm.Text = GetCompareDataByPODFormat(data, _SelectedJob.PODFormat);
            }

            PODDataModel pod2 = new PODDataModel();
            pod2.Text = "RSFP;1/101;DATA";
            if (data != null)
            {
                for (int i = 1; i < data.Count() - 1; i++)
                {
                    pod2.Text += ";" + data[i];
                }
            }

            //    Shared.RaiseOnPrinterDataChangeEvent(pod2);

            if (_SelectedJob.CompareType != CompareType.Database)
            {
                if (_SelectedJob.CompareType == CompareType.StaticText)
                {
                    dtm.Text = _SelectedJob.StaticText;
                }
            }

            Shared.RaiseOnCameraReadDataChangeEvent(dtm);
        }

        public void AddInvalidInput(int num = 0)
        {
            DetectModel dtm = new DetectModel();
            if (num == 0)
            {
                dtm.Text = "Trigger";

                var data = _PrintedCodeObtainFromFile.Find(x => x[x.Length - 1] == "Waiting");
                PODDataModel pod2 = new PODDataModel();
                pod2.Text = "RSFP;1/101;DATA";
                if (data != null)
                {
                    for (int i = 1; i < data.Count() - 1; i++)
                    {
                        pod2.Text += ";" + data[i];
                    }
                }

                if (_SelectedJob.CompareType != CompareType.Database)
                {
                    if (_SelectedJob.CompareType == CompareType.CanRead)
                    {
                        dtm.Text = "";
                    }
                }
            }
            else if (num == 1)
            {
                dtm.Text = _CheckedResultCodeList.Find(x => x[2] == "Valid")[1];
            }
            else
            {
                dtm.Text = "";
            }
            Shared.RaiseOnCameraReadDataChangeEvent(dtm);
        }

        private void InitEvents()
        {
            _TimerDateTime.Tick += TimerDateTime_Tick;
            btnStart.Click += ActionChanged;
            btnStop.Click += ActionChanged;
            btnTrigger.MouseUp += BtnTrigger_MouseUp;
            btnTrigger.MouseDown += BtnTrigger_MouseDown;
            pnlSentData.MouseDown += (obj, e) =>
            {
                ReleaseCapture();
                Message m = Message.Create(this.Handle, 0x00A1, (IntPtr)0x0002, IntPtr.Zero);
                base.WndProc(ref m);
            };
            pnlJobInformation.MouseDown += (obj, e) =>
            {
                if (e.Y <= pnlJobInformation.TitleHeight)
                {
                    ReleaseCapture();
                    Message m = Message.Create(this.Handle, 0x00A1, (IntPtr)0x0002, IntPtr.Zero);
                    base.WndProc(ref m);
                }
            };

            pnlMenu.DoubleClick += PnlMenu_DoubleClick;
            btnJob.Click += ActionChanged;
            btnExit.Click += ActionChanged;
            btnDatabase.Click += ActionChanged;
            btnAccount.Click += ActionChanged;
            btnHistory.Click += ActionChanged;
            btnSettings.Click += ActionChanged;
            pnlPrintedCode.Click += ActionChanged;
            pnlCheckFailed.Click += ActionChanged;
            pnlCheckPassed.Click += ActionChanged;
            pnlTotalChecked.Click += ActionChanged;
            btnExport.Click += ActionChanged;
            FormClosing += FrmMainNew_FormClosing;

            //menu
            mnManage.Click += ActionChanged;
            mnChangePassword.Click += ActionChanged;
            mnLogOut.Click += ActionChanged;
            //menu
            Shared.OnCameraStatusChange += Shared_OnCameraStatusChange;
            Shared.OnCameraReadDataChange += Shared_OnCameraReadDataChange;
            Shared.OnPrinterDataChange += Shared_OnPrinterDataChange;
            Shared.OnPrintingStateChange += Shared_OnPrintingStateChange;
            Shared.OnPrinterStatusChange += Shared_OnPrinterStatusChange;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            Shared.OnSensorControllerChangeEvent += Shared_OnSensorControllerChangeEvent;
            Shared.OnVerifyAndPrindSendDataMethod += Shared_OnVerifyAndPrindSendDataMethod;
            this.OnReceiveVerifyDataEvent += SendVerifiedDataToPrinter;
            Shared.OnLogError += Shared_OnLogError;

            this.Resize += (obj, e) =>
            {
                if (this.Size.Width < 1800 && this.Size.Height < 900)
                {
                    if (IsFullHD)
                    {
                        IsFullHD = false;
                    }
                }
                else if (this.Size.Width >= 1800 && this.Size.Height >= 900)
                {
                    if (!IsFullHD)
                    {
                        IsFullHD = true;
                    }
                }
                prBarCheckPassed.Height = prBarCheckPassed.Width = (int)(tableLayoutPanelCheckedResult.RowStyles[0].Height * tableLayoutPanelCheckedResult.Height / 100) * 955 / 1000;
            };

            _QueueBufferPrinterResponseData.Clear();
            ReceiveResponseFromPrinterHandlerAsync();
        }

        private async void Shared_OnVerifyAndPrindSendDataMethod(object sender, EventArgs e)
        {
            if (Shared.Settings.VerifyAndPrintBasicSentMethod) return;

            EnableUIComponentWhenLoadData(false);
            _Emergency.Clear();
            _Emergency = await InitVNPUpdatePrintedStatusConditionBuffer();

            EnableUIComponentWhenLoadData(true);
        }
        
        private async Task<ConcurrentDictionary<string, int>> InitVNPUpdatePrintedStatusConditionBuffer()
        {
            ConcurrentDictionary<string, int> result = new ConcurrentDictionary<string, int>();
            // Use a HashSet instead of a List
            HashSet<string> _CheckedResultCodeSet = new HashSet<string>();

            // Populate the HashSet with the second element of each array
            var validCond = ComparisonResult.Valid.ToString();
            var columnCount = _ColumnNames.Length;
            foreach (var array in _CheckedResultCodeList)
            {
                if (columnCount == array.Length && array[2] == validCond)
                {
                    _CheckedResultCodeSet.Add(array[1]);
                }
            }

            if (_PrintedCodeObtainFromFile.Count > 0)
            {
                int codeLenght = _PrintedCodeObtainFromFile[0].Count() - 1;
                for (int index = 0; index < _PrintedCodeObtainFromFile.Count; index++)
                {
                    string[] row = _PrintedCodeObtainFromFile[index].ToArray();
                    string data = "";
                    foreach (var item in _SelectedJob.PODFormat)
                    {
                        if (item.Type == PODModel.TypePOD.DATETIME)
                        {
                            data += DateTime.Now;
                        }
                        else if (item.Type == PODModel.TypePOD.FIELD)
                        {
                            data += row[item.Index];
                        }
                        else
                        {
                            data += item.Value;
                        }
                    }

                    // Use Contains instead of FindIndex
                    if (!_CheckedResultCodeSet.Contains(data))
                    {
                        if (_IsVerifyAndPrintMode)
                        {
                            string tmp = "";
                            for (int i = 1; i < row.Length - 1; i++)
                            {
                                var tmpPOD = Shared.Settings.PrintFieldForVerifyAndPrint.Find(x => x.Index == i);
                                if (tmpPOD != null)
                                {
                                    tmp += row[tmpPOD.Index];
                                }
                            }

                            result.TryAdd(tmp, index);
                        }
                    }
                }
            }
            await Task.Delay(10);
            _CheckedResultCodeSet.Clear();
            return result;
        }

        public event EventHandler OnReceiveVerifyDataEvent;
        public void RaiseOnReceiveVerifyDataEvent(object sender)
        {
            OnReceiveVerifyDataEvent?.Invoke(sender, EventArgs.Empty);
        }

        // Send date to printer if code is passed - Add by Thong Thach 10/05/23
        PODController podController = Shared.Settings.PrinterList.Where(p => p.RoleOfPrinter == RoleOfStation.ForProduct).FirstOrDefault().PODController;
        private void SendVerifiedDataToPrinter(object sender, EventArgs e)
        {
            string command = "DATA;";
            string[] arr = sender as string[];
            // Basic
            if (Shared.Settings.VerifyAndPrintBasicSentMethod)
                command += arr[1] == null ? Shared.Settings.FailedDataSentToPrinter : arr[1];
            // Field
            else
            {
                // All field
                if (Shared.Settings.PrintFieldForVerifyAndPrint.Count() == 0)
                {
                    command += string.Join(";", arr.Take(arr.Length - 1).Skip(1)
                        .Select(x => x == null ? Shared.Settings.FailedDataSentToPrinter : x));
                }
                // Specific field
                else
                {
                    command += string.Join(";", Shared.Settings.PrintFieldForVerifyAndPrint
                        .Where(x => x.Index < arr.Length - 1)
                        .Select(x => arr[x.Index] == null ? Shared.Settings.FailedDataSentToPrinter : arr[x.Index])
                        );
                }
            }

            if (podController != null)
            {
                podController.Send(command);
                NumberOfSentPrinter++;
            }
            else
            {
                podController = Shared.Settings.PrinterList.Where(p => p.RoleOfPrinter == RoleOfStation.ForProduct).FirstOrDefault().PODController;
            }
        }
        // END

        private void PnlMenu_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                return;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private int _CurrentPage = 0;
        public void InitDataGridView(DataGridView dgv, string[] columns, int imgIndex = -1, bool isPOD = false)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => InitDataGridView(dgv, columns, imgIndex, isPOD)));
                return;
            }

            if (columns.Length == 0) return;

            dgv.Columns.Clear();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            int tableWidth = dgv.Width;
            float percentWidth = (float)1 / columns.Length;
            int tableCodeProductListWidth = dgv.Width - 39;
            for (int index = 0; index < columns.Length; index++)
            {
                if (index == imgIndex && imgIndex != -1)
                {
                    DataGridViewImageColumn col = new DataGridViewImageColumn();
                    col.HeaderText = columns[index];
                    col.Name = columns[index].Trim();
                    col.DefaultCellStyle.NullValue = null;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    Size textSize = TextRenderer.MeasureText(col.HeaderText, dgv.Font);
                    col.Width = textSize.Width + 40;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns.Add(col);
                }
                else
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = columns[index];
                    col.Name = columns[index].Trim();
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    Size textSize = TextRenderer.MeasureText(col.HeaderText, dgv.Font);
                    col.Width = textSize.Width + 25;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns.Add(col);
                }
            }
            
            if (isPOD)
            {
                _DatabaseImageIndex = imgIndex;
                dgv.CellValueNeeded += Database_CellValueNeeded;
            }
            else
            {
                _CheckedResulImageIndex = imgIndex;
                dgv.CellValueNeeded += CheckedResult_CellValueNeeded;
            }
            dgv.VirtualMode = true;
            dgv.RowCount = _MaxDatabaseLine;
        }
        private int _DatabaseImageIndex = -1;
        private void Database_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                if (e.RowIndex > _PrintedCodeObtainFromFile.Count - 1) return;
                int correspondingIndex = e.RowIndex + _MaxDatabaseLine * _CurrentPage;
                if (correspondingIndex > _PrintedCodeObtainFromFile.Count - 1) return;

                if (e.ColumnIndex != _DatabaseImageIndex)
                    e.Value = _PrintedCodeObtainFromFile[correspondingIndex][e.ColumnIndex];
                else
                {
                    switch (_PrintedCodeObtainFromFile[correspondingIndex][e.ColumnIndex])
                    {
                        case "Printed":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_done_24px_result;
                            break;
                        case "Waiting":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_in_progress_20px_4;
                            break;
                        case "Sent":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_in_progress_20px_4;
                            break;
                        case "Reprint":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_done_24px_result;
                            break;
                        case "Duplicate":
                            (sender as DataGridView).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icon_check_241;
                            break;
                    }
                }
            }
            catch
            {

            }
        }

        private int _CheckedResulImageIndex = -1;
        private void CheckedResult_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                if (e.RowIndex > _CheckedResultCodeList.Count - 1) return;
                int correspondingIndex = _CheckedResultCodeList.Count < 500 ? e.RowIndex : _CheckedResultCodeList.Count - (_MaxDatabaseLine - e.RowIndex);
                if (correspondingIndex >= _CheckedResultCodeList.Count) return;

                if (e.ColumnIndex != _CheckedResulImageIndex)
                {
                    string value = _CheckedResultCodeList[correspondingIndex][e.ColumnIndex];
                    e.Value = value == "" ? Lang.CannotDetect : value;
                }
                else
                {
                    switch (_CheckedResultCodeList[correspondingIndex][e.ColumnIndex])
                    {
                        case "Valid":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_done_24px_result;
                            break;
                        case "Duplicated":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icon_Duplicated_Barcode;
                            break;
                        case "Missed":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icon_Missed_Barcode;
                            break;
                        case "Null":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icon_CantDetect_Barcode;
                            break;
                        case "Invalided":
                            e.Value = BarcodeVerificationSystem.Properties.Resources.icons8_multiply_20px;
                            break;
                    }
                }
            }
            catch
            {

            }
        }

        public static void AutoResizeColumnWith(DataGridView dgv, string[] value, int imgIndex = 0)
        {
            try
            {
                var firstRowWith = value;
                int totalColumnsWidth = TextRenderer.MeasureText(firstRowWith[0], dgv.Font).Width;
                int[] thickestRowIndex = { 0, TextRenderer.MeasureText(firstRowWith[0], dgv.Font).Width };
                for (int i = 1; i < firstRowWith.Length; i++)
                {
                    if (i == imgIndex)
                    {
                        totalColumnsWidth += dgv.Columns[i].Width;
                        continue;
                    }
                    Size colTextSize = TextRenderer.MeasureText(dgv.Columns[i].HeaderText, dgv.Font);
                    Size rowTextSize = TextRenderer.MeasureText(firstRowWith[i], dgv.Font);
                    if (rowTextSize.Width > thickestRowIndex[1])
                    {
                        thickestRowIndex[0] = i;
                        thickestRowIndex[1] = rowTextSize.Width;
                    }

                    if (colTextSize.Width < rowTextSize.Width)
                    {
                        dgv.Columns[i].Width = rowTextSize.Width + 40;
                    }
                    else if (colTextSize.Width > rowTextSize.Width)
                    {
                        dgv.Columns[i].Width = colTextSize.Width + 40;
                    }
                    totalColumnsWidth += dgv.Columns[i].Width;
                }
                if (totalColumnsWidth < dgv.Width)
                {
                    dgv.Columns[thickestRowIndex[0]].Width += dgv.Width - totalColumnsWidth - 35;
                }
            }
            catch
            {

            }
        }

        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            toolStripDateTime.Text = DateTime.Now.ToString(_DateTimeFormatTicker);
        }

        private void BtnTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            Shared.RaiseOnCameraTriggerOnChangeEvent();
        }

        private void BtnTrigger_MouseUp(object sender, MouseEventArgs e)
        {
            Shared.RaiseOnCameraTriggerOffChangeEvent();
        }

        #region Operation
        private void StopAllThreadForTesting()
        {
            // Save history
            var fileName = "";
            if (_SelectedJob.CheckedResultPath != "")
            {
                fileName = _SelectedJob.CheckedResultPath;
            }

            LoggingController.SaveHistory(
                String.Format("{0}: {1}", Lang.TotalChecked, TotalChecked),
                "Stop testing",
                String.Format("{0}: {1}", Lang.ResultFile, fileName),
                UserController.LogedInUsername,
                LoggingType.Stopped);

            // END Stop print


            // Stop print
            if (Shared.Settings.IsPrinting)
            {
                PODController podController = Shared.Settings.PrinterList.Where(p => p.RoleOfPrinter == RoleOfStation.ForProduct).FirstOrDefault().PODController;
                if (podController != null)
                {
                    podController.Send("CLPB");
                    Thread.Sleep(5);
                    // Send command to stop printer
                    podController.Send("STOP");
                }
            }

            if (_VirtualCTS != null)
                _VirtualCTS.Cancel();
            if (_SendDataToPrinterTokenCTS != null)
                _SendDataToPrinterTokenCTS.Cancel();

            _TotalMissed = 0;

            Thread.Sleep(50);
            // Stop thread
            _UIPrintedResponseCancelTokenSource?.Cancel();
            _OperationCancelTokenSource?.Cancel();
            Thread.Sleep(50);
            _QueueBufferDataObtained.Enqueue(null);
            _QueueBufferUpdateUIPrinter.Enqueue(null);

            Shared.OperStatus = OperationStatus.Stopped;
            Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);

            //END  Kill all thread
            Console.WriteLine("Stop all thread!");
        }

        #region Testing
        private void StartAllThreadForTesting()
        {
            Shared.OperStatus = OperationStatus.Processing;
            Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);
            EnableUIComponent(Shared.OperStatus);

            //Save history
            string fileName = DateTime.Now.ToString(_DateTimeFormat) + "_" + _SelectedJob.FileName + ".txt";
            LoggingController.SaveHistory(
                String.Format("{0}: {1}; {2}: {3}", Lang.StartIndex, _PrintedCodeObtainFromFile.FindIndex(x => x.Last() == "Waiting"), Lang.EndIndex, _TotalCode),
                "Start testing",
                String.Format("{0}: {1}", Lang.ResultFile, fileName),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Started);

            // Init new token to able cancel all operatons
            _OperationCancelTokenSource = new CancellationTokenSource();
            _UICheckedResultCancelTokenSource = new CancellationTokenSource();
            _UIPrintedResponseCancelTokenSource = new CancellationTokenSource();
            _BackupImageCancelTokenSource = new CancellationTokenSource();
            _BackupResponseCancelTokenSource = new CancellationTokenSource();
            _BackupResultCancelTokenSource = new CancellationTokenSource();

            var operationToken = _OperationCancelTokenSource.Token;
            var uiCheckedResultToken = _UICheckedResultCancelTokenSource.Token;
            var uiPrintedResponseToken = _UIPrintedResponseCancelTokenSource.Token;
            var backupImageToken = _BackupImageCancelTokenSource.Token;
            var backupResultToken = _BackupResultCancelTokenSource.Token;
            var backupResponseToken = _BackupResponseCancelTokenSource.Token;

            if (!Shared.GetCameraStatus())
            {
                VirtualTestAsync();
            }

            CompareAsync(operationToken);

            if (Shared.Settings.ExportImageEnable)
                ExportImageToFileAsync(backupImageToken);

            ExportCheckedResultToFileAsync(backupResultToken);
            UpdateUICheckedResultAsync(uiCheckedResultToken);

            if (_SelectedJob.CompareType == CompareType.Database)
            {
                ExportPrintedResponseToFileAsync(backupResponseToken);
                UpdateUIPrintedResponseAsync(uiPrintedResponseToken);
            }
            Thread.Sleep(200);
            Console.WriteLine("Run all thread!");
        }
        
        public async void VirtualTestAsync()
        {
            _VirtualCTS = new CancellationTokenSource();
            var token = _VirtualCTS.Token;

            await Task.Run(() => { VirtualTest(token); });
        }

        private void VirtualTest(CancellationToken token)
        {
            var codes = new List<string[]>();
            lock (_SyncObjCodeList)
            {
                codes = _PrintedCodeObtainFromFile.Where(x => x.Last() == "Waiting").ToList();
            }
            if (_SelectedJob.JobType == JobType.VerifyAndPrint)
            {
                _IsDetectWait = false;
            }

            try
            {
                if (_SelectedJob.CompareType == CompareType.Database)
                {
                    if (Shared.Settings.IsPrinting && _SelectedJob.CompareType == CompareType.Database && _SelectedJob.PrinterSeries)
                    {
                        foreach (PODController podController in Shared.Settings.PrinterList.Select(x => x.PODController))
                        {
                            ////Stop print
                            //podController.Send("STOP");
                            //Thread.Sleep(5);
                            //podController.Send("CLPB");

                            //string templateName = "";
                            //if (podController.RoleOfPrinter == RoleOfStation.ForProduct)
                            //{
                            //    // Get template name
                            //    templateName = _SelectedJob.TemplatePrint;
                            //}
                            //Thread.Sleep(50);
                            //// Start print
                            //string startPrintCommand = string.Format("STAR;{0};1;1;true", templateName);
                            //podController.Send(startPrintCommand);
                        }
                    }
                    else
                    {
                        Shared.OperStatus = OperationStatus.Running;
                    }

                    if (_IsVerifyAndPrintMode)
                    {
                        Thread.Sleep(3000);
                    }

                    for(int i = 0; i < codes.Count(); i++)
                    {
                        // fake
                        token.ThrowIfCancellationRequested();
                        string[] codeModel = codes[i];
                        if (codeModel.Last() == "Printed") continue;

                        PODDataModel podDataModel = new PODDataModel();
                        podDataModel.Text = "RSFP;1/101;DATA;";
                        podDataModel.Text += string.Join(";", codeModel.Take(codeModel.Length - 1).Skip(1));

                        DetectModel detectModel = new DetectModel();
                        Bitmap bmp = new Bitmap(1024, 1024);
                        detectModel = new DetectModel();
                        detectModel.Text = GetCompareDataByPODFormat(codeModel, _SelectedJob.PODFormat);
                        detectModel.RoleOfCamera = RoleOfStation.ForProduct;

                        if(_SelectedJob.PrinterSeries)
                            _QueueBufferPrinterResponseData.Enqueue(podDataModel);
                        _QueueBufferDataObtained.Enqueue(detectModel);
                        Thread.Sleep(25);
                    }
                }
                else if (_SelectedJob.CompareType == CompareType.CanRead)
                {
                    while (Shared.OperStatus != OperationStatus.Stopped)
                    {
                        DetectModel detectModel = new DetectModel();
                        detectModel.RoleOfCamera = RoleOfStation.ForProduct;
                        detectModel.Image = new Bitmap(1024, 1024);
                        detectModel.Text = "Hello Worlds";

                        _QueueBufferDataObtained.Enqueue(detectModel);
                        Thread.Sleep(40);
                    }
                }
                else if (_SelectedJob.CompareType == CompareType.StaticText)
                {
                    while (Shared.OperStatus != OperationStatus.Stopped)
                    {
                        DetectModel detectModel = new DetectModel();
                        detectModel.RoleOfCamera = RoleOfStation.ForProduct;
                        detectModel.Image = new Bitmap(1024, 1024);
                        detectModel.Text = _SelectedJob.StaticText;
                        _QueueBufferDataObtained.Enqueue(detectModel);
                        Thread.Sleep(40);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread virual data was stopped!");
            }
            catch
            {
                Console.WriteLine("Thread virual data was stopped!");
                _VirtualCTS?.Cancel();
            }
        }

        #endregion Testing
        
        private async void CompareAsync(CancellationToken token)
        {
            await Task.Run(() => { Compare(token); });
        }

        private void Compare(CancellationToken token)
        {
            Debug.WriteLine("Compare thread working on thread " + Environment.CurrentManagedThreadId);
            int currentCheckedIndex = -1; // Be use if verify and print function is enable
            string staticText = "";
            if (_SelectedJob.CompareType == CompareType.StaticText)
            {
                Invoke(new Action(() =>
                {
                    staticText = txtStaticText.Text;
                }));
            }
            var isAutoComplete = _SelectedJob.CompareType == CompareType.Database; // Need to auto stop proccess when compare type is database
            var isReprint = _SelectedJob.CompareType == CompareType.Database && _SelectedJob.JobType == JobType.AfterProduction && _TotalMissed > 0 && Shared.Settings.TotalCheckEnable;
            var isDBStandalone = _SelectedJob.CompareType == CompareType.Database && _SelectedJob.JobType == JobType.StandAlone;
            var reprintStopCond = TotalChecked + _TotalMissed - _NumberOfDuplicate;
            var stopCond = _TotalCode - _NumberOfDuplicate;
            var isOneMore = false;
            var isComplete = false;
            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        if (_QueueBufferDataObtained.Count() == 0)
                            token.ThrowIfCancellationRequested();
                    }
                    else
                    {
                        if (isComplete)
                        {
                            continue;
                        }

                        if (isAutoComplete) // check if auto complete
                        {
                            bool completeCondition = false;
                            var stopNumber = Shared.Settings.TotalCheckEnable ? TotalChecked : NumberOfCheckPassed;

                            if (_SelectedJob.JobType == JobType.VerifyAndPrint)
                            {
                                if (!Shared.Settings.TotalCheckEnable)
                                {
                                    if (isOneMore) stopNumber++;
                                    if (NumberOfCheckPassed >= _TotalCode - _NumberOfDuplicate)
                                    {
                                        isOneMore = true;
                                    }
                                }
                                stopNumber--;
                            }

                            if (!isReprint)
                            {
                                completeCondition = Shared.OperStatus != OperationStatus.Stopped && stopNumber >= stopCond;
                            }
                            else
                            {
                                completeCondition = Shared.OperStatus != OperationStatus.Stopped && stopNumber >= reprintStopCond;
                            }

                            if (completeCondition)
                            {
                                Invoke(new Action(() => { StopProcess(false, Lang.CompleteTheBarcodeVerificationProcess, true); }));
                                isComplete = true;
                                continue;
                            }
                        }
                    }

                    DetectModel detectModel = _QueueBufferDataObtained.Dequeue(); // Waiting until have data to dequeue
                    int compareIndex = _StartIndex + TotalChecked;
                    if (detectModel != null)
                    {
                        Stopwatch measureTime = Stopwatch.StartNew();
                        // Check compareType: Can read, Static text, database
                        if (_SelectedJob.CompareType == CompareType.CanRead)
                        {
                            detectModel.CompareResult = CanreadCompare(detectModel.Text);
                        }
                        else if (_SelectedJob.CompareType == CompareType.StaticText)
                        {
                            detectModel.CompareResult = StaticTextCompare(detectModel.Text, staticText);
                        }
                        else if (_SelectedJob.CompareType == CompareType.Database)
                        {
                            // Need to check printed response from printer job type is on production
                            var isNeedToCheckPrintedResponse = true;
                            if (_IsOnProductionMode)
                            {
                                lock (_PrintedResponseLocker)
                                {
                                    isNeedToCheckPrintedResponse = _IsPrintedResponse;
                                    _IsPrintedResponse = false; // Notify that have a printed response
                                }
                            }

                            if (!isNeedToCheckPrintedResponse || _CodeListPODFormat == null)
                            {
                                detectModel.CompareResult = ComparisonResult.Invalided;
                            }
                            else
                            {
                                // Veridy barcode
                                detectModel.CompareResult = DatabaseCompare(detectModel.Text, ref currentCheckedIndex);

                                if (_IsOnProductionMode)
                                {
                                    lock (_CheckLocker) // Notify that code is check
                                    {
                                        _CheckedResult = detectModel.CompareResult; // Assign result of current check
                                        _IsCheckedWait = false;
                                        Monitor.PulseAll(_CheckLocker);
                                    }
                                }
                            }

                            if (_IsVerifyAndPrintMode) // Verify and print function: send data to printer if detectModel is passed - Add by Thong Thach 23/03/23
                            {
                                var verifyAndPrintCondition = Shared.GetPrinterStatus();
                                if (verifyAndPrintCondition)
                                {
                                    string[] arr = new string[0];
                                    if (detectModel.CompareResult == ComparisonResult.Valid)
                                    {
                                        if (!Shared.Settings.VerifyAndPrintBasicSentMethod)
                                        {
                                            lock (_SyncObjCodeList)
                                            {
                                                arr = _PrintedCodeObtainFromFile[currentCheckedIndex];
                                            }
                                        }
                                        else
                                        {
                                            arr = new string[3];
                                            arr[1] = detectModel.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (!Shared.Settings.VerifyAndPrintBasicSentMethod)
                                        {
                                            lock (_SyncObjCodeList)
                                            {
                                                arr = new string[_PrintedCodeObtainFromFile[0].Length];
                                            }
                                        }
                                        else
                                        {
                                            arr = new string[3];
                                        }
                                    }

                                    RaiseOnReceiveVerifyDataEvent(arr);
                                    currentCheckedIndex = -1;
                                }
                            }
                        }

                        //Output signal - Time consuming about 11ms AMD Ryzen 3600X and Cognex DM60 connect via adapter Ugreen
                        if (Shared.Settings.OutputEnable)
                        {
                            var outputCondition = Shared.GetCameraStatus() && detectModel.CompareResult != ComparisonResult.Valid;
                            if (outputCondition)
                            {
                                Shared.RaiseOnCameraOutputSignalChangeEvent();
                            }
                        }
                        //END Output signal

                        measureTime.Stop();

                        TotalChecked++;
                        if (detectModel.CompareResult == ComparisonResult.Valid)
                        {
                            NumberOfCheckPassed++;
                        }
                        else
                        {
                            NumberOfCheckFailed++;
                        }

                        detectModel.Index = compareIndex;
                        detectModel.CompareTime = measureTime.ElapsedMilliseconds;
                        detectModel.ProcessingDateTime = DateTime.Now.ToString(Shared.Settings.DateTimeFormatOfResult);

                        // Send printed response manual for Database - Standalone mode
                        if (isDBStandalone)
                        {
                            if (detectModel.CompareResult == ComparisonResult.Valid)
                                _QueueBufferUpdateUIPrinter.Enqueue(detectModel.Text);
                        }

                        _QueueBufferDataObtainedResult.Enqueue(detectModel);
                    }

                    //Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread compare was stopped!");
                // Stop anorther operation thread after compare thread stop
                _UICheckedResultCancelTokenSource?.Cancel();
                //_UIPrintedResponseCancelTokenSource?.Cancel();
                _QueueBufferDataObtainedResult.Enqueue(null);
                //_QueueBufferUpdateUIPrinter.Enqueue(null);
                Thread.Sleep(200);
                UpdateStopUI();
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread compare was error!");
                Thread.Sleep(200);
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private ComparisonResult CanreadCompare(string txt)
        {
            // Verify data canread job
            if (txt == Shared.Settings.CameraList[0].NoReadOutputString || txt == "")
            {
                return ComparisonResult.Invalided;
            }
            else
            {
                return ComparisonResult.Valid;
            }
        }

        private ComparisonResult StaticTextCompare(string txt, string staticText)
        {
            // Verify data static text
            if (staticText != "" && txt == staticText)
            {
                return ComparisonResult.Valid;
            }
            else
            {
                return ComparisonResult.Invalided;
            }
        }

        private ComparisonResult DatabaseCompare(string txt, ref int currentValidIndex)
        {
            if (_CodeListPODFormat == null)
            {
                return ComparisonResult.Invalided;
            }
            else
            {
                var checkNull = txt == "";
                if (!checkNull) // Check null
                {
                    if (_CodeListPODFormat.TryGetValue(txt, out CompareStatus compareStatus))
                    {
                        if (compareStatus.Index == -1)
                        {
                            compareStatus.Index = _PrintedCodeObtainFromFile.FindIndex(x => GetCompareDataByPODFormat(x, _SelectedJob.PODFormat) == txt);
                        }

                        if (!compareStatus.Status) // Check duplicate
                        {
                            _CodeListPODFormat[txt].Status = true;
                            currentValidIndex = compareStatus.Index;
                            return ComparisonResult.Valid;
                        }
                        else
                        {
                            return ComparisonResult.Duplicated;
                        }
                    }
                    else
                    {
                        return ComparisonResult.Invalided;
                    }
                }
                else
                {
                    return ComparisonResult.Null;
                }
            }
        }

        private void StartProcess(bool interactOnUI = true)
        {
            // Avoid start more 1 time
            if (Shared.OperStatus == OperationStatus.Running || Shared.OperStatus == OperationStatus.Processing)
            {
                return;
            }


            string checkInitDataMessage = "";
            checkInitDataMessage = CheckInitDataErrorAndGenerateMessage();
            if (checkInitDataMessage != "")
            {
                DialogResult dialogResult = CuzMessageBox.Show(checkInitDataMessage, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var isDatabaseDeny = _SelectedJob.CompareType == CompareType.Database && _TotalCode == 0;
            if (isDatabaseDeny)
            {
                CuzMessageBox.Show(Lang.DatabaseDoesNotExist, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check all condition
            CheckCondition checkCondition = CheckAllTheConditions();
            if (checkCondition != CheckCondition.Success)
            {
                if (interactOnUI)
                {
                    //Show dialog waring
                    if (checkCondition == CheckCondition.NoJobsSelected)
                    {
                        CuzMessageBox.Show(Lang.PleaseSeletedJobForTheSystem, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (checkCondition == CheckCondition.NotLoadDatabase)
                    {
                        CuzMessageBox.Show(Lang.PleaseCheckTheDatabaseConnection, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.NotLoadTemplate)
                    {
                        CuzMessageBox.Show(Lang.PleaseCheckTheTemplate, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.NotConnectCamera)
                    {
                        CuzMessageBox.Show(Lang.PleaseCheckTheCameraConnection, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.MissingParameter)
                    {
                        CuzMessageBox.Show(Lang.SomeParametersAreMissing, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.NotConnectPrinter)
                    {
                        CuzMessageBox.Show(Lang.PleaseCheckThePrinterConnection, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.LeastOneAction)
                    {
                        CuzMessageBox.Show(Lang.ThereMustBeAtLeastOneActionSelected, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.MissingParameterActivation)
                    {
                        CuzMessageBox.Show(Lang.SomeActivationParametersAreMissing, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkCondition == CheckCondition.MissingParameterPrinting)
                    {
                        CuzMessageBox.Show(Lang.SomePrintParametersAreMissing, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //END Show dialog waring
                }
                return;
            }
            // END check all condition

            var isNeedToCheckPrinter = _SelectedJob.PrinterSeries && _SelectedJob.CompareType == CompareType.Database;
            CheckPrinterSettings checkPrinterSettings = CheckAllSettingsPrinter();
            if (checkPrinterSettings != CheckPrinterSettings.Success && isNeedToCheckPrinter)
            {
                if (interactOnUI)
                {
                    if (checkPrinterSettings == CheckPrinterSettings.NotRawData)
                    {
                        CuzMessageBox.Show(Lang.DataTypeMustBeRAWData, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkPrinterSettings == CheckPrinterSettings.MonitorNotEnable)
                    {
                        CuzMessageBox.Show(Lang.MonitorFeatureIsNotEnabled, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkPrinterSettings == CheckPrinterSettings.PODNotEnabled)
                    {
                        CuzMessageBox.Show(Lang.PODFeatureIsNotEnabled, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkPrinterSettings == CheckPrinterSettings.ResponsePODCommandNotEnable)
                    {
                        CuzMessageBox.Show(Lang.ResponsePODCommandFeatureIsNotEnable, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkPrinterSettings == CheckPrinterSettings.ResponsePODDataNotEnable)
                    {
                        CuzMessageBox.Show(Lang.ResponsePODDataFeatureIsNotEnable, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (checkPrinterSettings == CheckPrinterSettings.PODMode)
                    {
                        var mes = _SelectedJob.JobType == JobType.AfterProduction ? Lang.PODModeMustBePrintAll : Lang.PODModeMustBePrintLast;
                        CuzMessageBox.Show(mes, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return;
            }

            if (_PrinterStatus != PrinterStatus.Stop && Shared.Settings.PrinterList.FirstOrDefault().CheckAllPrinterSettings && isNeedToCheckPrinter)
            {
                //The printer is in an abnormal state, please check again!
                CuzMessageBox.Show(Lang.ThePrinterIsInAnAbnormalStatePleaseCheckAgain + $" ({_PrinterStatus.ToString().ToUpper()})", Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ExportNamePrefix = DateTime.Now.ToString(Shared.Settings.ExportNamePrefixFormat);
            _ExportCheckedResultFileName = String.Format("{0}_BarcodeCheckedResult.txt", _ExportNamePrefix);

            //Save history
            string fileName = DateTime.Now.ToString(_DateTimeFormat) + "_" + _SelectedJob.FileName + ".txt";
            var startIndex = _PrintedCodeObtainFromFile.FindIndex(x => x[x.Length - 1] == "Waiting") + 1;

            LoggingController.SaveHistory(
                String.Format("{0}: {1}; {2}: {3} - Job: {4}", Lang.StartIndex, startIndex, Lang.EndIndex, _TotalCode, _SelectedJob.FileName),
                Lang.Start,
                String.Format("{0}: {1}", Lang.ResultFile, fileName),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Started);

            _QueueBufferPrinterResponseData.Clear();

            if (Shared.Settings.IsPrinting && _SelectedJob.CompareType == CompareType.Database && _SelectedJob.PrinterSeries)
            {
                // If has printing then wait printer running
                Shared.OperStatus = OperationStatus.Processing;

                foreach (PODController podController in Shared.Settings.PrinterList.Select(x => x.PODController))
                {
                    // Stop print
                    podController.Send("STOP");
                    Thread.Sleep(5);
                    podController.Send("CLPB");

                    string templateName = "";
                    if (podController.RoleOfPrinter == RoleOfStation.ForProduct)
                    {
                        // Get template name
                        templateName = _SelectedJob.TemplatePrint;
                    }
                    Thread.Sleep(50);
                    // Start print
                    string startPrintCommand = string.Format("STAR;{0};1;1;true", templateName);
                    podController.Send(startPrintCommand);
                }
            }
            else
            {
                Shared.OperStatus = OperationStatus.Running;
            }

            var isNonePrinted = _SelectedJob.CompareType == CompareType.CanRead || _SelectedJob.CompareType == CompareType.StaticText;

            _SelectedJob.JobStatus = JobStatus.Unfinished;
            string filePath = CommVariables.PathJobsApp + _SelectedJob.FileName + Shared.Settings.JobFileExtension;
            _SelectedJob.SaveFile(filePath);

            // Init new token to able cancel all operatons
            _OperationCancelTokenSource = new CancellationTokenSource();
            _UICheckedResultCancelTokenSource = new CancellationTokenSource();
            _UIPrintedResponseCancelTokenSource = new CancellationTokenSource();
            _BackupImageCancelTokenSource = new CancellationTokenSource();
            _BackupResponseCancelTokenSource = new CancellationTokenSource();
            _BackupResultCancelTokenSource = new CancellationTokenSource();
            _BackupSendLogCancelTokenSource = new CancellationTokenSource();

            var operationToken = _OperationCancelTokenSource.Token;
            var uiCheckedResultToken = _UICheckedResultCancelTokenSource.Token;
            var uiPrintedResponseToken = _UIPrintedResponseCancelTokenSource.Token;
            var backupImageToken = _BackupImageCancelTokenSource.Token;
            var backupResultToken = _BackupResultCancelTokenSource.Token;
            var backupResponseToken = _BackupResponseCancelTokenSource.Token;
            var backupSendLogToken = _BackupSendLogCancelTokenSource.Token;

            BackupSendLogAsync(backupSendLogToken);
            CompareAsync(operationToken);

            if (Shared.Settings.ExportImageEnable)
                ExportImageToFileAsync(backupImageToken);

            ExportCheckedResultToFileAsync(backupResultToken);
            UpdateUICheckedResultAsync(uiCheckedResultToken);

            if (_SelectedJob.CompareType == CompareType.Database)
            {
                ExportPrintedResponseToFileAsync(backupResponseToken);
                UpdateUIPrintedResponseAsync(uiPrintedResponseToken);
            }

            Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);
            EnableUIComponent(Shared.OperStatus);
        }

        private void UpdateStopUI()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStopUI()));
                return;
            }

            EnableUIComponent(Shared.OperStatus);
            NumberOfSentPrinter = 0;
            ReceivedCode = 0;
        }

        private void StopProcess(bool interactOnUI = true, string messages = "", bool isClosed = false, bool isManualClose = false)
        {
            if (interactOnUI)
            {
                DialogResult dialogResult = CuzMessageBox.Show(Lang.DoYouWantToStopTheSystem, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
                messages = Lang.UserStoppedTheSystem;
            }

            // Stop send data to printer thread
            _VirtualCTS?.Cancel();
            KillTThreadSendPODDataToPrinter();
            _QueueBufferPrinterResponseData.Clear();

            // Stop print
            if (Shared.Settings.IsPrinting)
            {
                PODController podController = Shared.Settings.PrinterList.Where(p => p.RoleOfPrinter == RoleOfStation.ForProduct).FirstOrDefault().PODController;
                if (podController != null)
                {
                    podController.Send("CLPB");
                    Thread.Sleep(5);
                    // Send command to stop printer
                    podController.Send("STOP");
                }
            }

            _TotalMissed = 0;

            // Stop thread
            _UIPrintedResponseCancelTokenSource?.Cancel();
            _OperationCancelTokenSource?.Cancel();
            _QueueBufferDataObtained.Enqueue(null);
            _QueueBufferUpdateUIPrinter.Enqueue(null);
            Shared.OperStatus = OperationStatus.Stopped;
            Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);

            // Save history
            var fileName = "";
            if (_SelectedJob.CheckedResultPath != "")
            {
                fileName = _SelectedJob.CheckedResultPath;
            }

            // Save history
            LoggingController.SaveHistory(
                String.Format("{0}: {1}", Lang.TotalChecked, TotalChecked),
                messages,
                String.Format("{0}: {1}", Lang.ResultFile, fileName),
                SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                LoggingType.Stopped);
            
            // Update job status
            if (_SelectedJob.CompareType == CompareType.Database)
            {
                var completeNum = 0;
                completeNum = Shared.Settings.TotalCheckEnable ? TotalChecked : NumberOfCheckPassed;
                if (completeNum >= _TotalCode - _NumberOfDuplicate)
                {
                    _SelectedJob.JobStatus = JobStatus.Accomplished;
                    string filePath = CommVariables.PathJobsApp + _SelectedJob.FileName + Shared.Settings.JobFileExtension;
                    _SelectedJob.SaveFile(filePath);
                }
            }

            if (!interactOnUI)
            {
                DialogResult dialogResult = CuzMessageBox.Show(messages, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK && isClosed)
                {
                    //Close();
                }

                NumberOfSentPrinter = 0;
                ReceivedCode = 0;
            }
        }

        private void KillThread(ref Thread thread)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
                thread = null;
            }
        }

        private void KillAllProccessThread()
        {
            _VirtualCTS?.Cancel();
            _UICheckedResultCancelTokenSource?.Cancel();
            _UIPrintedResponseCancelTokenSource?.Cancel();
            _BackupImageCancelTokenSource?.Cancel();
            _BackupResultCancelTokenSource?.Cancel();
            _BackupResponseCancelTokenSource?.Cancel();
            _BackupSendLogCancelTokenSource?.Cancel();

            _QueueBufferDataObtainedResult.Enqueue(null);
            _QueueBufferUpdateUIPrinter.Enqueue(null);
            _QueueBufferBackupImage.Enqueue(null);
            _QueueBufferBackupPrintedCode.Enqueue(null);
            _QueueBufferBackupCheckedResult.Enqueue(null);
            _QueueBufferBackupSendLog.Enqueue(null);
        }

        private bool CheckIfProccessDataIsClear()
        {
            var checkResult = true;

            checkResult = _QueueBufferDataObtained.Count() > 0 ? false : checkResult;
            checkResult = _QueueBufferDataObtainedResult.Count() > 0 ? false : checkResult;
            checkResult = _QueueBufferPrintedResponse.Count() > 0 ? false : checkResult;
            checkResult = _QueueBufferBackupImage.Count() > 0 ? false : checkResult;
            checkResult = _QueueBufferBackupPrintedCode.Count() > 0 ? false : checkResult;
            checkResult = _QueueBufferBackupCheckedResult.Count() > 0 ? false : checkResult;

            return checkResult;
        }

        private async void UpdateUIPrintedResponseAsync(CancellationToken token)
        {
            await Task.Run(() => { UpdateUIPrintedResponse(token); });
        }

        private async void UpdateUICheckedResultAsync(CancellationToken token)
        {
            await Task.Run(() => { UpdateUICheckedResult(token); });
        }

        private void UpdateUIPrintedResponse(CancellationToken token)
        {
            Debug.WriteLine("UI 1 thread working on thread " + Environment.CurrentManagedThreadId);
            List<string[]> strPrintedResponseList = new List<string[]>();
            var isAutoComplete = _SelectedJob.CompareType == CompareType.Database; // Check if need to auto stop procces when compare type is verify and print
            int numOfResponse = NumberPrinted;
            int currentIndex = 0;
            var currentPage = 0;
            try
            {
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferUpdateUIPrinter.Count() == 0)
                            token.ThrowIfCancellationRequested(); // Stop thread

                    // Waiting until have data
                    string podCommand = _QueueBufferUpdateUIPrinter.Dequeue();
                    if ( podCommand != null)
                    {
                        NumberPrinted++;
                        if (_IsOnProductionMode) // Check if need to wait check result
                        {
                            ComparisonResult checkedResult = ComparisonResult.Null;
                            lock (_CheckLocker)
                            {
                                while (_IsCheckedWait) Monitor.Wait(_CheckLocker); // Waiting until detect data was verify
                                checkedResult = _CheckedResult;
                                _IsCheckedWait = true;
                            }

                            lock (_PrintLocker) // Notify that code is printed
                            {
                                _IsPrintedWait = false;
                                _PrintedResult = checkedResult;
                                Monitor.PulseAll(_PrintLocker);
                            }

                            if (checkedResult != ComparisonResult.Valid) continue;
                        }

                        // Update printed status
                        if (_CodeListPODFormat.TryGetValue(podCommand, out CompareStatus compareStatus))
                        {
                            // Update current code position
                            currentIndex = compareStatus.Index % _MaxDatabaseLine;
                            currentPage = compareStatus.Index / _MaxDatabaseLine;

                            if (currentPage != _CurrentPage)
                            {
                                _CurrentPage = currentPage;
                                Invoke(new Action(() => { dgvDatabase.Invalidate(); }));
                            }
                            else
                            {
                                Invoke(new Action(() =>
                                {
                                    dgvDatabase.Invalidate();
                                    dgvDatabase.Rows[currentIndex].Cells[0].Selected = true;
                                }));
                            }

                            // Printed response backup data
                            if (_PrintedCodeObtainFromFile[compareStatus.Index].Last() == "Waiting")
                            {
                                lock (_SyncObjCodeList)
                                {
                                    (_PrintedCodeObtainFromFile[compareStatus.Index])[_PrintedCodeObtainFromFile[compareStatus.Index].Length - 1] = "Printed";
                                    strPrintedResponseList.Add(_PrintedCodeObtainFromFile[compareStatus.Index]);
                                }
                            }

                            // Send backup data to backup thread
                            _QueueBufferBackupPrintedCode.Enqueue(new List<string[]>(strPrintedResponseList)); // Enqueue backup data
                                                                                                               //Clear list
                            strPrintedResponseList.Clear();
                        }
                    }

                    //Time delay avoid frezee user interface
                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread update printed status was stoppped!");
                _BackupResponseCancelTokenSource?.Cancel();
                _QueueBufferBackupPrintedCode.Enqueue(null);
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread update printed status was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private void UpdateUICheckedResult(CancellationToken token)
        {
            Debug.WriteLine("UI 2 thread working on thread " + Environment.CurrentManagedThreadId);
            List<string[]> strResultCheckList = new List<string[]>();
            string[] strResult = new string[0];
            int lineCounter = TotalChecked;
            try
            {
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferDataObtainedResult.Count() == 0)
                            token.ThrowIfCancellationRequested();

                    // Waiting until have data
                    DetectModel detectModel = _QueueBufferDataObtainedResult.Dequeue();

                    if (detectModel == null) continue;

                    //Draw text result to image
                    //Get image
                    Image image = detectModel.Image == null ? new Bitmap(100, 100) : detectModel.Image;
                    //END Draw text result to image

                    //Save image result(optional) - Update later
                    //END Save image result (optional)
                    if (Shared.Settings.ExportImageEnable && image != null)
                    {
                        if (detectModel.CompareResult != ComparisonResult.Valid)
                        {
                            _QueueBufferBackupImage.Enqueue(new ExportImageModel(image.Clone() as Image, detectModel.Index));
                        }
                    }

                    Invoke(new Action(() =>
                    {
                        //Image preview
                        var oldImage = pictureBoxPreview.Image;
                        pictureBoxPreview.Image = image;
                        oldImage?.Dispose();
                        //END Image preview
                    }));

                    strResult = new string[] { detectModel.Index + "", detectModel.Text,
                            detectModel.CompareResult.ToString(), (detectModel.CompareTime) + " ms", detectModel.ProcessingDateTime};
                    strResultCheckList.Add(strResult);

                    lock (_SyncObjCheckedResultList)
                    {
                        _CheckedResultCodeList.Add(strResult);
                    }

                    // Update checked results
                    Invoke(new Action(() =>
                    {
                        lineCounter = _CheckedResultCodeList.Count() - 1;
                        if (lineCounter < 500)
                        {
                            dgvCheckedResult.Invalidate();
                            dgvCheckedResult.Rows[lineCounter].Cells[0].Selected = true;
                        }
                        else
                        {
                            dgvCheckedResult.Invalidate();
                            dgvCheckedResult.Rows[499].Cells[0].Selected = true;
                        }
                    }));

                    //END Add row

                    //Update value to user interface
                    Invoke(new Action(() =>
                    {
                        txtCodeResult.Text = detectModel.Text;
                        txtProcessingTimeResult.Text = (detectModel.CompareTime) + " ms";
                        txtStatusResult.Text = detectModel.CompareResult.ToFriendlyString();
                        txtStatusResult.ForeColor = detectModel.CompareResult == ComparisonResult.Valid ? Color.FromArgb(0, 199, 82) : Color.Red;
                    }));

                    //END Add result need save to queue
                    _QueueBufferBackupCheckedResult.Enqueue(new List<string[]>(strResultCheckList));
                    //Clear list
                    strResult.DefaultIfEmpty();
                    strResultCheckList.Clear();

                    ProgressBarCheckedUpdate();

                    //END Update value to user interface
                    //Time delay avoid frezee user interface
                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread update checked result was stopped!");
                Thread.Sleep(20);
                _BackupImageCancelTokenSource?.Cancel();
                _BackupResultCancelTokenSource?.Cancel();
                _QueueBufferBackupImage.Enqueue(null);
                _QueueBufferBackupCheckedResult.Enqueue(null);
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread update checked result was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private CheckCondition CheckAllTheConditions()
        {
            // Check camera connection - Uncomment when release - Update later
#if !DEBUG
            if (Shared.GetCameraStatus() == false)
            {
                return CheckCondition.NotConnectCamera;
            }
#endif
            //END Check camera connection

            if (_SelectedJob.CompareType == CompareType.Database && _SelectedJob.PrinterSeries)
            {
                // Check printer connection
                if (Shared.GetPrinterStatus() == false && Shared.Settings.IsPrinting)
                {
                    return CheckCondition.NotConnectPrinter;
                }
                // END Check printer connection

                // Check print template 
                if (_SelectedJob.CompareType == CompareType.Database && (_SelectedJob == null || _SelectedJob.TemplatePrint == ""))
                {
                    return CheckCondition.MissingParameterActivation;
                }
                // END Check print template
            }

            // Check list code for print and check
            if (_CodeListPODFormat == null || _CodeListPODFormat == null)
            {
                return CheckCondition.MissingParameterActivation;
            }
            // END Check list code for print and check

            return CheckCondition.Success;
        }

        #endregion Operation

        #region ExportFile

        private void SaveResultToFile(List<string[]> list, string path)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, true, new UTF8Encoding(true)))
                {
                    //Add row result
                    foreach (string[] strArr in list)
                    {
                        streamWriter.WriteLine(String.Join(",", strArr.Select(x => Csv.Escape(x))));
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.RaiseOnLogError(ex);
            }
        }
        private void SaveSendLogToFile(string[] list, string path)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, true, new UTF8Encoding(true)))
                {
                    //Add row result

                    streamWriter.WriteLine(String.Join(",", list.Select(x => Csv.Escape(x))));
                }
            }
            catch (Exception ex)
            {
                Shared.RaiseOnLogError(ex);
            }
        }

        private async void ExportPrintedResponseToFileAsync(CancellationToken token)
        {
            await Task.Run(() => { NewExportPrintedResponseToFile(token); });
        }

        private void NewExportPrintedResponseToFile(CancellationToken token)
        {
            if (_SelectedJob.PrintedResponePath == "")
            {
                string fileName = DateTime.Now.ToString(_DateTimeFormat) + "_Printed_" + _SelectedJob.FileName;
                string filePath = CommVariables.PathJobsApp + _SelectedJob.FileName + Shared.Settings.JobFileExtension;
                string path = CommVariables.PathPrintedResponse + fileName + ".csv";

                // Determine whether the directory exists.
                if (!Directory.Exists(CommVariables.PathPrintedResponse))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(CommVariables.PathPrintedResponse);
                }

                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter streamWriter = new StreamWriter(path, true, new UTF8Encoding(true)))
                    {
                        //Add header
                        streamWriter.WriteLine(String.Join(",", _DatabaseColunms));
                    }
                }

                _SelectedJob.PrintedResponePath = fileName + ".csv";
                _SelectedJob.SaveFile(filePath);
            }

            try
            {
                string path = CommVariables.PathPrintedResponse + _SelectedJob.PrintedResponePath;
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferBackupPrintedCode.Count() == 0)
                            token.ThrowIfCancellationRequested();

                    List<string[]> valueArr = _QueueBufferBackupPrintedCode.Dequeue();
                    if (valueArr == null) continue;
                    if (valueArr.Count() > 0)
                    {
                        SaveResultToFile(valueArr, path);
                    }
                    valueArr.Clear();
                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread backup printed response was stopped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread backup printed response was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private async void BackupSendLogAsync(CancellationToken token)
        {
            await Task.Run(() => { BackupSendLog(token); });
        }

        private void BackupSendLog(CancellationToken token)
        {
            string path = CommVariables.PathPrintedResponse + DateTime.Now.ToString(_DateTimeFormat) + "_SendLog_" + _SelectedJob.FileName + ".csv";

            try
            {
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferBackupSendLog.Count() == 0)
                            token.ThrowIfCancellationRequested();

                    string[] valueArr = (string[])_QueueBufferBackupSendLog.Dequeue();
                    if (valueArr == null) continue;
                    if (valueArr.Count() > 0)
                    {
                        // This text is added only once to the file.
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter streamWriter = new StreamWriter(path, true, new UTF8Encoding(true)))
                            {
                                //Add header
                                streamWriter.WriteLine(String.Join(",", _DatabaseColunms.Take(_DatabaseColunms.Length - 1).Skip(1)));
                            }
                        }
                        SaveSendLogToFile(valueArr, path);
                    }
                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread backup printed response was stopped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread backup printed response was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private async void ExportCheckedResultToFileAsync(CancellationToken token)
        {
            await Task.Run(() => { NewExportCheckedResultToFile(token); });
        }

        private void NewExportCheckedResultToFile(CancellationToken token)
        {
            if (_SelectedJob.CheckedResultPath == "")
            {
                string fileName = DateTime.Now.ToString(_DateTimeFormat) + "_" + _SelectedJob.FileName;
                string filePath = CommVariables.PathJobsApp + _SelectedJob.FileName + Shared.Settings.JobFileExtension;
                string path = CommVariables.PathCheckedResult + fileName + ".csv";

                // Determine whether the directory exists.
                if (!Directory.Exists(CommVariables.PathCheckedResult))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(CommVariables.PathCheckedResult);
                }

                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter streamWriter = new StreamWriter(path, true, new UTF8Encoding(true)))
                    {
                        //Add header
                        streamWriter.WriteLine(String.Join(",", _ColumnNames));
                    }
                }

                _SelectedJob.CheckedResultPath = fileName + ".csv";
                _SelectedJob.SaveFile(filePath);
            }

            try
            {
                string path = CommVariables.PathCheckedResult + _SelectedJob.CheckedResultPath;
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferBackupCheckedResult.Count() == 0)
                            token.ThrowIfCancellationRequested();

                    List<string[]> valueArr = _QueueBufferBackupCheckedResult.Dequeue();
                    if (valueArr == null) continue;
                    if (valueArr.Count() > 0)
                    {
                        SaveResultToFile(valueArr, path);
                    }
                    valueArr.Clear();
                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread backup checked result was stopped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread backup checked result was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private async void ExportImageToFileAsync(CancellationToken token)
        {
            await Task.Run(() => { NewExportImageToFile(token); });
        }

        private void NewExportImageToFile(CancellationToken token)
        {
            // Determine whether the directory exists.
            if (!Directory.Exists(Shared.Settings.ExportImagePath + "\\" + _SelectedJob.FileName))
            {
                // Try to create the directory.
                Directory.CreateDirectory(Shared.Settings.ExportImagePath + "\\" + _SelectedJob.FileName);
            }
            string fileName = "";
            string path = "";
            try
            {
                while (true)
                {
                    // Only stop if handled all data
                    if (token.IsCancellationRequested)
                        if (_QueueBufferBackupImage.Count() == 0)
                            token.ThrowIfCancellationRequested();

                    ExportImageModel exportImageModel = null;

                    exportImageModel = _QueueBufferBackupImage.Dequeue();

                    if (exportImageModel != null)
                    {
                        fileName = String.Format("\\{0}_Job_{1}_Image_{2:D7}.bmp", _ExportNamePrefix, _SelectedJob.FileName, exportImageModel.Index); //Use bmp because it is the least time consuming
                        path = Shared.Settings.ExportImagePath + "\\" + _SelectedJob.FileName + fileName;


                        if (exportImageModel.Image != null)
                        {
                            //Save image
                            exportImageModel.Image.Save(path);

                            //Release memory
                            exportImageModel.Image.Dispose();
                            //END Release memory
                        }
                    }

                    Thread.Sleep(5);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread backup image was stoppped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread backup image was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        #endregion ExportFile

        #region Jobs
        private void UpdateJobInfomationInterface()
        {
            if (Shared.JobNameSelected == null || Shared.JobNameSelected == "")
            {
                return;
            }

            _SelectedJob = Shared.GetJob(Shared.JobNameSelected);
            lblStatusPrinter01.Visible = _SelectedJob.PrinterSeries;

            if (_SelectedJob != null)
            {
                _IsAfterProductionMode = _SelectedJob.JobType == JobType.AfterProduction ? true : false;
                _IsOnProductionMode = _SelectedJob.JobType == JobType.OnProduction ? true : false;
                _IsVerifyAndPrintMode = _SelectedJob.JobType == JobType.VerifyAndPrint ? true : false;

                _TotalCode = 0;
                NumberOfSentPrinter = 0;
                ReceivedCode = 0;
                NumberPrinted = 0;

                TotalChecked = 0;
                NumberOfCheckPassed = 0;
                NumberOfCheckFailed = 0;
                ProgressBarInitialize();
                ProgressBarCheckedUpdate();
                UpdateJobInfo(_SelectedJob);
                EnableUIComponentWhenLoadData(false);
                btnStop.Enabled = false;
                btnTrigger.Enabled = false;
                pnlMenu.Enabled = false;

                _CheckedResultCodeList.Clear();
                _PrintedCodeObtainFromFile.Clear();
                _CodeListPODFormat.Clear();

                InitDataAsync(_SelectedJob);
            }
        }

        Stopwatch bigSTW = new Stopwatch();

        private async void InitDataAsync(JobModel jobModel)
        {
            bigSTW.Start();
            Stopwatch stw = Stopwatch.StartNew();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " Start init data on thread " + Environment.CurrentManagedThreadId);

            if (jobModel.CompareType == CompareType.Database)
            {
                // Load database and update printed status
                var databaseTsk = InitDatabaseAndPrintedStatusAsync(jobModel);
                // Load checked result
                var checkedResultTsk = InitCheckedResultDataAsync(jobModel);
                // Waiting until database and checked result completed load
                await Task.WhenAll(databaseTsk, checkedResultTsk);

                string checkInitDataMessage = "";
                checkInitDataMessage = CheckInitDataErrorAndGenerateMessage();
                if (checkInitDataMessage != "")
                {
                    foreach (string value in checkInitDataMessage.Split('\n'))
                    {
                        if(value != "")
                            CuzAlert.Show(value, Alert.enmType.Error, new Size(500, 120), new Point(Location.X, Location.Y), this.Size, true);
                    }
                }
                else
                {
                    _PrintedCodeObtainFromFile = databaseTsk.Result;
                    _CheckedResultCodeList = checkedResultTsk.Result;

                    // Inititalize database information
                    if (_PrintedCodeObtainFromFile.Count() > 1)
                    {
                        _DatabaseColunms = _PrintedCodeObtainFromFile[0];
                        _PrintedCodeObtainFromFile.RemoveAt(0);

                        // Initialize compare data
                        if (_SelectedJob.CompareType == CompareType.Database)
                        {
                            // Waiting until initialize compare data completed
                            await InitCompareDataAsync(_PrintedCodeObtainFromFile, _CheckedResultCodeList);
                        }

                        _TotalCode = _PrintedCodeObtainFromFile.Count();
                        NumberPrinted = _PrintedCodeObtainFromFile.Where(x => x[x.Length - 1] == "Printed").Count();

                        // Identify datas need to display by first waiting code
                        int firstWaiting = _PrintedCodeObtainFromFile.IndexOf(_PrintedCodeObtainFromFile.Find(x => x[x.Length - 1] == "Waiting"));
                        _CurrentPage = _TotalCode > _MaxDatabaseLine ? (firstWaiting > 0 ? firstWaiting / _MaxDatabaseLine : (firstWaiting == 0 ? 0 : _TotalCode / _MaxDatabaseLine - 1)) : 0;
                        // Implement virtual mode for DataGridView display database
                        InitDataGridView(dgvDatabase, _DatabaseColunms, _DatabaseColunms.Count() - 1, true);
                        // Adjust width of columns
                        var lastCode = _PrintedCodeObtainFromFile[_PrintedCodeObtainFromFile.Count() - 1];
                        AutoResizeColumnWith(dgvDatabase, lastCode, _DatabaseColunms.Length - 1);

                        // Define number of DataGridView row
                        dgvDatabase.RowCount = _TotalCode > _MaxDatabaseLine ? _MaxDatabaseLine : _TotalCode;
                        // Update both of DataGridView
                        dgvDatabase.Invalidate();

                        if (_NumberOfDuplicate > 0)
                        {
                            txtStaticText.Text = $"{_TotalCode} ({_NumberOfDuplicate} Duplicate)";
                            CuzAlert.Show(Lang.DuplicateDataMessage.Replace("NN", "" + _NumberOfDuplicate) + Lang.PODFormat, Alert.enmType.Warning, new Size(500, 120), new Point(Location.X, Location.Y), this.Size, true);
                        }
                    }
                }
            }
            else
            {
                // Load checked result
                _CheckedResultCodeList = await InitCheckedResultDataAsync(jobModel);
            }

            // Initialize checked result information
            TotalChecked = _CheckedResultCodeList.Count();
            NumberOfCheckPassed = _CheckedResultCodeList.Where(x => x[2] == "Valid").Count();
            NumberOfCheckFailed = TotalChecked - NumberOfCheckPassed;
            // Implement virtual mode for DataGridView display checked results
            InitDataGridView(dgvCheckedResult, _ColumnNames, 2);
            // Adjust width of columns
            await Task.Delay(50);
            AutoResizeColumnWith(dgvCheckedResult, defaultRecord, 2);

            // Update progress bar
            ProgressBarInitialize();
            ProgressBarCheckedUpdate();
            prBarCheckPassed.Invalidate();

            dgvCheckedResult.Invalidate();

            // Enable control after completed initialize data
            pnlMenu.Enabled = true;
            EnableUIComponentWhenLoadData(true);
            stw.Stop();
            Debug.WriteLine("Init completed, it took " + stw.ElapsedMilliseconds);
        }

        private string CheckInitDataErrorAndGenerateMessage()
        {
            if (_InitDataErrorList.Count() > 0)
            {
                string tmp = "";
                foreach (var value in _InitDataErrorList)
                {
                    if (value == InitDataError.DatabaseUnknownError)
                        tmp += Lang.DetectError.Replace("NN", Lang.Database.ToLower()) + "\n";
                    else if (value == InitDataError.CheckedResultUnknownError)
                        tmp += Lang.DetectError.Replace("NN", Lang.CheckedResult.ToLower()) + "\n";
                    else if (value == InitDataError.PrintedStatusUnknownError)
                        tmp += Lang.DetectError.Replace("NN", Lang.PrintedResponse.ToLower()) + "\n";
                    else if (value == InitDataError.CannotAccessDatabase)
                        tmp += Lang.UnableToAccess.Replace("NN", Lang.Database.ToLower()) + "\n";
                    else if (value == InitDataError.CannotAccessCheckedResult)
                        tmp += Lang.UnableToAccess.Replace("NN", Lang.CheckedResult.ToLower()) + "\n";
                    else if (value == InitDataError.CannotAccessPrintedResponse)
                        tmp += Lang.UnableToAccess.Replace("NN", Lang.PrintedResponse.ToLower()) + "\n";
                    else if (value == InitDataError.DatabaseDoNotExist)
                        tmp += Lang.CanNotFindDatabase + "\n";
                    else if (value == InitDataError.CheckedResultDoNotExist)
                        tmp += Lang.CanNotFindCheckedResult + "\n";
                    else if (value == InitDataError.PrintedResponseDoNotExist)
                        tmp += Lang.CanNotFindPrintedResponse + "\n";
                    else
                        tmp += Lang.Unknown + "\n";
                }

                return tmp;
            }

            return "";
        }

        private async Task<List<string[]>> InitDatabaseAndPrintedStatusAsync(JobModel jobModel)
        {
            var pathDatabase = jobModel.DirectoryDatabase;
            var pathBackupPrintedResponse = CommVariables.PathPrintedResponse + jobModel.PrintedResponePath;

            // Initialize barcode data
            var tmp = await Task.Run(() => { return InitDatabase(pathDatabase); });
            if (jobModel.PrintedResponePath != "" && File.Exists(jobModel.DirectoryDatabase) && tmp.Count() > 1)
                await Task.Run(() => { InitPrintedStatus(pathBackupPrintedResponse, tmp); });
            return tmp;
        }

        private async Task<List<string[]>> InitCheckedResultDataAsync(JobModel jobModel)
        {
            // Loading checked result async implement
            var path = CommVariables.PathCheckedResult + jobModel.CheckedResultPath;
            if (jobModel.CheckedResultPath != "")
            {
                var tsk = Task.Run(() => { return InitCheckedResultData(path); });
                return await tsk;
            }
            return new List<string[]>();
        }

        private async Task InitCompareDataAsync(List<string[]> datas, List<string[]> result)
        {
            // Initialize compare data async implement
            await Task.Run(() => { InitCompareData(datas, result); });
        }

        private List<string[]> InitDatabase(string path)
        {
            Stopwatch stw = Stopwatch.StartNew();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitDatabase work on thread " + Environment.CurrentManagedThreadId);
            List<string[]> result = new List<string[]>(); 

            if (!File.Exists(path))
            {
                _InitDataErrorList.Add(InitDataError.DatabaseDoNotExist);
                DialogResult dialogResult = CuzMessageBox.Show("'" + path + "' " + Lang.CanNotFindDatabase, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return result;
            }

            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8, true))
                {
                    var rexCsvSplitter = path.EndsWith(".csv") ? new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))") : new Regex(@"[\t]");
                    int lineCounter = -1;
                    int columnCount = 0;
                    while (!reader.EndOfStream)
                    {
                        string[] line = rexCsvSplitter.Split(reader.ReadLine()).Select(x => Csv.Unescape(x)).ToArray();
                        lineCounter++;
                        if (lineCounter == 0)
                        {
                            // Create database columns
                            var tmp = new string[line.Length + 2];
                            tmp[0] = "Index";
                            tmp[tmp.Length - 1] = "Status";
                            for (int i = 1; i < tmp.Length - 1; i++)
                            {
                                tmp[i] = line[i - 1] + $" - Field{i}";
                            }
                            columnCount = tmp.Count();
                            result.Add(tmp);
                        }
                        else
                        {
                            // ignore empty line 16/11/2023 by Thong Thach
                            //if (line.Length == 1 && line[0] == "") continue;
                            // handle database row before adding
                            var tmp = new string[columnCount];
                            tmp[0] = "" + lineCounter;
                            tmp[columnCount - 1] = "Waiting";
                            for (int i = 1; i < tmp.Length - 1; i++)
                            {
                                if (i - 1 < line.Length)
                                {
                                    tmp[i] = line[i - 1];
                                }
                                else
                                {
                                    tmp[i] = "";
                                }
                            }
                            result.Add(tmp);
                        }
                    }
                }
            }
            catch (IOException)
            {
                _InitDataErrorList.Add(InitDataError.CannotAccessDatabase);
            }
            catch (Exception)
            {
                _InitDataErrorList.Add(InitDataError.DatabaseUnknownError);
            }

            stw.Stop();
            Debug.WriteLine(bigSTW .ElapsedMilliseconds + " InitDatabase completed, it took " + stw.ElapsedMilliseconds);
            return result;
        }

        private void InitPrintedStatus(string path, List<string[]> list)
        {
            Stopwatch stw = Stopwatch.StartNew();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitPrintedStatus work on thread " + Environment.CurrentManagedThreadId);
            if (!File.Exists(path))
            {
                _InitDataErrorList.Add(InitDataError.CheckedResultDoNotExist);
                DialogResult dialogResult = CuzMessageBox.Show(Lang.CanNotFindPrintedResponse, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8, true))
                {
                    int i = -1;
                    var rexCsvSplitter = path.EndsWith(".csv") ? new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))") : new Regex(@"[\t]");

                    while (!reader.EndOfStream)
                    {
                        i++;
                        if (i == 0)
                        {
                            reader.ReadLine();
                        }
                        else
                        {
                            // using only index value to update printed status
                            var line = Csv.Unescape(rexCsvSplitter.Split(reader.ReadLine())[0]);
                            if (int.TryParse(line, out int index))
                            {
                                list[index][list[index].Length - 1] = "Printed";
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                _InitDataErrorList.Add(InitDataError.CannotAccessPrintedResponse);
            }
            catch (Exception)
            {
                _InitDataErrorList.Add(InitDataError.PrintedStatusUnknownError);
            }

            stw.Stop();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitPrintedStaus complete, it took " + stw.ElapsedMilliseconds);
        }

        private List<string[]> InitCheckedResultData(string path)
        {
            Stopwatch stw = Stopwatch.StartNew();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitCheckedResult work on thread " + Environment.CurrentManagedThreadId);
            List<string[]> result = new List<string[]>();

            if (!File.Exists(path))
            {
                _InitDataErrorList.Add(InitDataError.CheckedResultDoNotExist);
                DialogResult dialogResult = CuzMessageBox.Show(Lang.CanNotFindCheckedResult, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return result;
            }

            try
            {
                var rexCsvSplitter = path.EndsWith(".csv") ? new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))") : new Regex(@"[\t]");
                using (var reader = new StreamReader(path, Encoding.UTF8, true))
                {
                    bool isFirstline = false;
                    while (!reader.EndOfStream)
                    {
                        if (!isFirstline)
                        {
                            reader.ReadLine();
                            isFirstline = true;
                        }
                        else
                        {
                            // handle checked result row before adding
                            string[] line = rexCsvSplitter.Split(reader.ReadLine()).Select(x => Csv.Unescape(x)).ToArray();
                            // ignore empty line 16/11/2023 by Thong Thach
                            if (line.Length == 1 && line[0] == "") continue;
                            if (line.Length < _ColumnNames.Length)
                            {
                                var checkedResult = GetTheRightString(line);
                                result.Add(checkedResult);
                            }
                            else
                            {
                                result.Add(line);
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                _InitDataErrorList.Add(InitDataError.CannotAccessCheckedResult);
            }
            catch (Exception)
            {
                _InitDataErrorList.Add(InitDataError.CheckedResultUnknownError);
            }
            stw.Stop();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitCheckedResult completed, it took " + stw.ElapsedMilliseconds);
            return result;
        }

        private void InitCompareData(List<string[]> datas, List<string[]> result)
        {
            Stopwatch stw = Stopwatch.StartNew();
            Debug.WriteLine( bigSTW.ElapsedMilliseconds + " InitCompareData work on thread " + Environment.CurrentManagedThreadId);

            // Use a HashSet instead of a List
            HashSet<string> _CheckedResultCodeSet = new HashSet<string>();

            // Populate the HashSet with the second element of each array
            var validCond = ComparisonResult.Valid.ToString();
            var columnCount = 5;
            try
            {
                foreach (var array in result)
                {
                    if (columnCount == array.Length && array[2] == validCond)
                    {
                        _CheckedResultCodeSet.Add(array[1]);
                    }
                }
                if (datas.Count > 0)
                {
                    int codeLenght = datas[0].Count() - 1;
                    for (int index = 0; index < datas.Count; index++)
                    {
                        string[] row = datas[index].ToArray();
                        string data = "";
                        data = GetCompareDataByPODFormat(row, _SelectedJob.PODFormat);

                        // Use Contains instead of FindIndex
                        if (_CheckedResultCodeSet.Contains(data))
                        {
                            bool tryAdd = _CodeListPODFormat.TryAdd(data, new CompareStatus(index, true));
                            if (!tryAdd)
                            {
                                _PrintedCodeObtainFromFile[index][_DatabaseColunms.Length - 1] = "Duplicate";
                                _NumberOfDuplicate++;
                            }
                        }
                        else
                        {
                            bool tryAdd = _CodeListPODFormat.TryAdd(data, new CompareStatus(index, false));
                            if (!tryAdd)
                            {
                                _PrintedCodeObtainFromFile[index][_DatabaseColunms.Length - 1] = "Duplicate";
                                _NumberOfDuplicate++;
                            }

                            // Data use to update printed status for Verify and print - Compare mode
                            if (_IsVerifyAndPrintMode)
                            {
                                string tmp = "";
                                for (int i = 1; i < row.Length - 1; i++)
                                {
                                    var tmpPOD = Shared.Settings.PrintFieldForVerifyAndPrint.Find(x => x.Index == i);
                                    if (tmpPOD != null)
                                    {
                                        tmp += row[tmpPOD.Index];
                                    }
                                }

                                var tryAdd2 = _Emergency.TryAdd(tmp, index);

                                //if (!tryAdd2)
                                //{
                                //    _PrintedCodeObtainFromFile[index][_DatabaseColunms.Length - 1] = "Duplicate";
                                //}
                            }
                        }
                    }
                }

                _CheckedResultCodeSet.Clear();
            }
            catch
            {
                _InitDataErrorList.Add(InitDataError.Unknown);
            }
            stw.Stop();
            Debug.WriteLine(bigSTW.ElapsedMilliseconds + " InitCompareData completed, it took " + stw.ElapsedMilliseconds);
        }

        // Get the right valua base on checked result column name - Create by Thong Thach 2023/08/31
        private string[] GetTheRightString(string[] line)
        {
            var code = new string[_ColumnNames.Length];
            for (int i = 0; i < code.Length; i++)
            {
                if (i < line.Length)
                    code[i] = line[i];
                else
                    code[i] = "";
            }
            return code;
        }

        // Get compare string value by POD format list aka compare format - Create bt Thong Thach 2023/09/06
        public string GetCompareDataByPODFormat(string[] values, List<PODModel> podFormat, int addingIndex = 0)
        {
            if (values.Length == 0) return "";
            var compareString = "";
            foreach (var item in podFormat)
            {
                if (item.Type == PODModel.TypePOD.FIELD)
                {
                    compareString += values[item.Index + addingIndex];
                }
                else if (item.Type == PODModel.TypePOD.TEXT)
                {
                    compareString += item.Value;
                }
            }
            return compareString;
        }

        #endregion Jobs

        #region UpdateUI

        private void UpdateJobInfo(JobModel jobModel)
        {
            txtJobName.Text = jobModel.FileName;
            txtTemplatePrint.Text = jobModel.TemplatePrint;
            txtPODFormat.Text = "";

            foreach (PODModel item in jobModel.PODFormat)
            {
                txtPODFormat.Text += item.ToStringSample();
            }
            if (jobModel.CompareType == CompareType.CanRead)
            {
                lblStaticText.Text = Lang.StaticText;
                txtStaticText.Text = jobModel.StaticText;
                txtCompareType.Text = Lang.CanRead;
                txtJobType.Text = "";
                txtPODFormat.BackColor = Color.WhiteSmoke;
                txtStaticText.BackColor = Color.WhiteSmoke;
            }
            else if (jobModel.CompareType == CompareType.StaticText)
            {
                lblStaticText.Text = Lang.StaticText;
                txtStaticText.Text = jobModel.StaticText;
                txtCompareType.Text = Lang.StaticText;
                txtJobType.Text = "";
                txtPODFormat.BackColor = Color.WhiteSmoke;
                txtStaticText.BackColor = Color.White;
            }
            else
            {
                lblStaticText.Text = Lang.Totals;
                txtCompareType.Text = Lang.Database;
                txtStaticText.Text = jobModel.NumberTotalsCode.ToString();
                txtJobType.Text = jobModel.JobType.ToFriendlyString();
                txtPODFormat.BackColor = Color.White;
                txtStaticText.BackColor = Color.White;
            }

            if (_SelectedJob.JobType == JobType.StandAlone)
            {
                txtTemplatePrint.BackColor = Color.WhiteSmoke;
                txtJobType.BackColor = Color.WhiteSmoke;
            }
            else
            {
                txtStaticText.BackColor = Color.White;
                txtJobType.BackColor = Color.White;
            }
        }

        private void UpdateCheckTotalAndCheckFailedLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCheckTotalAndCheckFailedLabel()));
                return;
            }

            lblCheckResultPassedValue.Text = string.Format("{0:N0}", NumberOfCheckPassed);//{0:N3} 0.000 decimal
            lblCheckResultFailedValue.Text = string.Format("{0:N0}", (TotalChecked - NumberOfCheckPassed));
            lblTotalCheckedValue.Text = string.Format("{0:N0}", TotalChecked);
            ProgressBarCheckedUpdate();
        }

        private void UpdateCheckTotalAndPrintedDatabase()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCheckTotalAndPrintedDatabase()));
                return;
            }

            lblReceivedValue.Text = string.Format("{0:N0}", ReceivedCode);//{0:N3} 0.000 decimal
            lblPrintedCodeValue.Text = string.Format("{0:N0}", NumberPrinted);//{0:N3} 0.000 decimal
            lblSentDataValue.Text = string.Format("{0:N0}", NumberOfSentPrinter);
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
                        if (!cameraModel.IsConnected)
                        {
                            CuzAlert.Show(Lang.CameraDisconnected, Alert.enmType.Warning, new Size(500, 120), new Point(Location.X, Location.Y), this.Size, true);
                        }
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
                        if (!printerModel.IsConnected && _IsPrinterDisconnectedNot)
                        {
                            CuzAlert.Show(Lang.PrinterDisconnected, Alert.enmType.Warning, new Size(500, 120), new Point(Location.X, Location.Y), this.Size, true);
                        }
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

        private void EnableUIComponent(OperationStatus operationStatus, bool isNonStart = false)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableUIComponent(operationStatus)));
                return;
            }
            bool isEnable = false;
            //if (operationStatus == OperationStatus.Running)
            if (operationStatus != OperationStatus.Stopped)
            {
                isEnable = false;
            }
            else
            {
                isEnable = true;
            }

            btnStart.Enabled = isEnable;
            btnStop.Enabled = !isEnable;
            btnTrigger.Enabled = !isEnable;
            btnJob.Enabled = isEnable;
            btnAccount.Enabled = isEnable;
            btnHistory.Enabled = isEnable;
            btnSettings.Enabled = isEnable;
            btnExport.Enabled = isEnable;
            btnExit.Enabled = isEnable;

            // END menu script
            if (isEnable)
            {
                ProcessUserAccess();
            }

            if (!isNonStart)
            {
                toolStripOperationStatus.Text = operationStatus.ToFriendlyString();
                toolStripOperationStatus.ForeColor = operationStatus.GetForegroundColor();
                Console.WriteLine(toolStripOperationStatus.Text);
            }
        }

        void EnableUIComponentWhenLoadData(bool isEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EnableUIComponentWhenLoadData(isEnable)));
                return;
            }

            btnStart.Enabled = isEnable;
            btnStop.Enabled = !isEnable;
            btnTrigger.Enabled = !isEnable;
            btnJob.Enabled = isEnable;
            btnAccount.Enabled = isEnable;
            btnHistory.Enabled = isEnable;
            btnSettings.Enabled = isEnable;
            btnExport.Enabled = isEnable;
            btnExit.Enabled = isEnable;

            btnDatabase.Enabled = isEnable;
            pnlPrintedCode.Enabled = isEnable;
            pnlTotalChecked.Enabled = isEnable;
            pnlCheckPassed.Enabled = isEnable;
            pnlCheckFailed.Enabled = isEnable;

            dgvDatabase.Enabled = isEnable;
            dgvCheckedResult.Enabled = isEnable;
            picDatabaseLoading.Visible = !isEnable;
            picCheckedResultLoading.Visible = !isEnable;
        }

        private void ProcessUserAccess()
        {
            if (Shared.LoggedInUser == null)
            {
                //process logout on ui
                //hide menu change password and logout
            }
            else if (Shared.LoggedInUser.Role == 0)
            {

            }
            else if (Shared.LoggedInUser.Role == 1)
            {

            }
        }

        #endregion Update UI 

        #region Progress bar

        private void ProgressBarInitialize()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProgressBarInitialize()));
                return;
            }
            if (_SelectedJob != null)
            {
                if (_SelectedJob.CompareType == CompareType.Database)
                {
                    prBarCheckPassed.Maximum = 100;
                    prBarCheckPassed.Minimum = 0;
                    prBarCheckPassed.Update();
                }
                else
                {
                    prBarCheckPassed.Maximum = 100;
                    prBarCheckPassed.Minimum = 0;
                    prBarCheckPassed.Update();
                }
            }
        }

        private void ProgressBarCheckedUpdate()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProgressBarCheckedUpdate()));
                return;
            }

            try
            {
                var progress = 0;
                if (_SelectedJob.CompareType == CompareType.Database)
                {
                    //Check passed
                    progress = _TotalCode > 0 ? NumberOfCheckPassed * 100 / (_TotalCode - _NumberOfDuplicate) : 0;
                }
                else
                {
                    //Check passed
                    progress = TotalChecked > 0 ? NumberOfCheckPassed * 100 / TotalChecked : 0;
                }

                if (progress < 100)
                {
                    prBarCheckPassed.Text = string.Format("{0:N0}%", progress);//{0:N3} 0.000 decimal
                    prBarCheckPassed.Value = progress;
                }
                else
                {
                    prBarCheckPassed.Value = 100;
                    prBarCheckPassed.Text = string.Format("100%");//{0:N3} 0.000 decimal
                }

                prBarCheckPassed.Invalidate();
            }
            catch (Exception)
            {

            }
        }

        #endregion Progress bar

        private void ActionChanged(object sender, EventArgs e)
        {
            if (sender == btnJob)
            {
                this.Close();
            }
            else if (sender == btnStart)
            {
                StartProcess();
            }
            else if (sender == btnStop)
            {
                StopProcess(true, "", false, true);
            }
            else if (sender == btnDatabase || sender == pnlPrintedCode)
            {
                var isDatabaseDeny = _SelectedJob.CompareType == CompareType.Database && _TotalCode == 0;
                if (isDatabaseDeny)
                {
                    CuzMessageBox.Show(Lang.DatabaseDoesNotExist, Lang.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_FormPreviewDatabase == null || _FormPreviewDatabase.IsDisposed)
                {
                    _FormPreviewDatabase = new frmPreviewDatabase();
                    _FormPreviewDatabase._DatabaseColunms = new List<string>(_DatabaseColunms);
                    _FormPreviewDatabase._ObtainCodeList = _PrintedCodeObtainFromFile.ToList();
                    _FormPreviewDatabase._TotalColumns = _TotalColumns;
                    _FormPreviewDatabase._Totals = _TotalCode;
                    _FormPreviewDatabase._NumberPrinted = NumberPrinted;
                    _FormPreviewDatabase.Show();
                }
                else
                {
                    if (_FormPreviewDatabase.WindowState == FormWindowState.Minimized)
                    {
                        _FormPreviewDatabase.WindowState = FormWindowState.Normal;
                    }
                    _FormPreviewDatabase.Focus();
                    _FormPreviewDatabase.BringToFront();
                }
            }
            else if (sender == pnlCheckFailed || sender == pnlCheckPassed || sender == pnlTotalChecked)
            {
                if (_FormCheckedResult == null || _FormCheckedResult.IsDisposed)
                {
                    _FormCheckedResult = new frmCheckedResult();
                    _FormCheckedResult._IsAfterProduction = _IsAfterProductionMode;
                    _FormCheckedResult._IsRSeries = _SelectedJob.PrinterSeries;
                    _FormCheckedResult._ColumnNames = _ColumnNames.ToList();
                    _FormCheckedResult._CheckedResult = _CheckedResultCodeList.ToList();
                    _FormCheckedResult._CheckedData = _CodeListPODFormat;
                    _FormCheckedResult._CodeData = _PrintedCodeObtainFromFile.ToList();
                    _FormCheckedResult._TotalColumns = _ColumnNames.Count();
                    _FormCheckedResult._TotalCode = _TotalCode;
                    _FormCheckedResult._NumberOfPrinted = NumberPrinted;
                    _FormCheckedResult._TotalChecked = TotalChecked;
                    _FormCheckedResult._NumberOfCheckedPassed = NumberOfCheckPassed;
                    _FormCheckedResult._NumberOfCheckedFailed = (TotalChecked - NumberOfCheckPassed);
                    _FormCheckedResult._JobName = _SelectedJob.FileName;
                    _FormCheckedResult._PODFormat = _SelectedJob.PODFormat;
                    _FormCheckedResult._frmParent = this;
                    // fillValue = 0: Load all
                    // fillValue = 1: Load passed result
                    // fillValue > 1: Load failed
                    if (sender == pnlCheckFailed)
                    {
                        _FormCheckedResult._FillValue = "Failed";
                    }
                    else if (sender == pnlCheckPassed)
                    {
                        _FormCheckedResult._FillValue = ComparisonResult.Valid.ToString();
                    }
                    else if (sender == pnlTotalChecked)
                    {
                        _FormCheckedResult._FillValue = "All";
                    }
                    _FormCheckedResult.Show();
                }
                else
                {
                    if (_FormCheckedResult != null)
                    {
                        if (sender == pnlCheckFailed)
                        {
                            _FormCheckedResult._FillValue = "Failed";
                        }
                        else if (sender == pnlCheckPassed)
                        {
                            _FormCheckedResult._FillValue = ComparisonResult.Valid.ToString();
                        }
                        else if (sender == pnlTotalChecked)
                        {
                            _FormCheckedResult._FillValue = "All";
                        }

                        _FormCheckedResult.Reload();
                        _FormCheckedResult.BringToFront();
                        _FormCheckedResult.Focus();
                        _FormCheckedResult.TopMost = true;
                    }
                }
            }
            else if (sender == btnHistory)
            {
                if (_FormViewHistoryProgram == null || _FormViewHistoryProgram.IsDisposed)
                {
                    _FormViewHistoryProgram = new frmViewHistoryProgram("_rynan_loggin_access_control_management_");
                    _FormViewHistoryProgram.Show();
                }
                else
                {
                    if (_FormViewHistoryProgram.WindowState == FormWindowState.Minimized)
                    {
                        _FormViewHistoryProgram.WindowState = FormWindowState.Normal;
                    }

                    _FormViewHistoryProgram.Focus();
                    _FormViewHistoryProgram.BringToFront();
                }
            }
            else if (sender == btnSettings)
            {
                if (_FormSettings == null || _FormSettings.IsDisposed)
                {
                    _FormSettings = new frmSettings();
                    _FormSettings.Show();
                }
                else
                {
                    if (_FormSettings.WindowState == FormWindowState.Minimized)
                    {
                        _FormSettings.WindowState = FormWindowState.Normal;
                    }

                    _FormSettings.Focus();
                    _FormSettings.BringToFront();
                }
            }
            else if (sender == btnAccount)
            {
                cuzDropdownManageAccount.PrimaryColor = Color.FromArgb(0, 171, 230);
                cuzDropdownManageAccount.MenuItemHeight = 40;
                cuzDropdownManageAccount.Font = new Font("Microsoft Sans Serif", 12);
                cuzDropdownManageAccount.ForeColor = Color.Black;
                cuzDropdownManageAccount.Show(btnAccount, btnAccount.Width, 0);
            }
            else if (sender == mnManage)
            {
                frmManageAccount form = new frmManageAccount();
                DialogResult result = form.ShowDialog();
            }
            else if (sender == mnChangePassword)
            {
                frmChangePassword frmChangePassword = new frmChangePassword();
                frmChangePassword.ShowDialog();
            }
            else if (sender == mnLogOut)
            {
                _ParentForm.Exit();
            }
            else if (sender == btnExit)
            {
                _ParentForm.Exit();
            }
            else if (sender == btnExport)
            {
                ExportLogAsynce();
            }
        }

        private CheckPrinterSettings CheckAllSettingsPrinter()
        {
            if (Shared.Settings.PrinterList.FirstOrDefault().CheckAllPrinterSettings)
            {
                _PrinterSettingsModel = Shared.GetSettingsPrinter();
                if (!_PrinterSettingsModel.enablePOD)
                {
                    return CheckPrinterSettings.PODNotEnabled;
                }

                if (!_PrinterSettingsModel.responsePODCommand)
                {
                    return CheckPrinterSettings.ResponsePODCommandNotEnable;
                }

                if (!_PrinterSettingsModel.responsePODData)
                {
                    return CheckPrinterSettings.ResponsePODDataNotEnable;
                }
                // 0: json, 1: Raw data, 2: Customise
                if (_PrinterSettingsModel.podDataType != 1)
                {
                    return CheckPrinterSettings.NotRawData;
                }
                // 0:print all, 1:print last, 2 print last and repeat
                var podMode = _SelectedJob.JobType == JobType.AfterProduction ? 0 : 1;
                if (_PrinterSettingsModel.podMode != podMode)
                {
                    return CheckPrinterSettings.PODMode;
                }

                if (!_PrinterSettingsModel.enableMonitor)
                {
                    return CheckPrinterSettings.MonitorNotEnable;
                }
            }

            return CheckPrinterSettings.Success;
        }
        #region Events Called
        private void Shared_OnCameraReadDataChange(object sender, EventArgs e)
        {
            if (Shared.OperStatus != OperationStatus.Running && Shared.OperStatus != OperationStatus.Processing)
            {
                return;
            }

            if (sender is DetectModel)
            {
                DetectModel detectModel = sender as DetectModel;
                // Check code read form camera
                if (detectModel.RoleOfCamera == RoleOfStation.ForProduct)
                {
                    //Add data obtained to queue
                    _QueueBufferDataObtained.Enqueue(detectModel);
                    //END Add data obtained to queue
                }
                else
                {

                }
            }
        }

        private void Shared_OnCameraStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelCamera();
        }

        private void Shared_OnPrintingStateChange(object sender, EventArgs e)
        {

        }


        private async void ReceiveResponseFromPrinterHandlerAsync()
        {
            _PrinterRespontCST = new CancellationTokenSource();
            var token = _PrinterRespontCST.Token;
            try
            {
                await Task.Run(() => { ReceiveResponseFromPrinterHandler(token); });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread handle printed response was stopped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread handle printed response was error!");
                KillAllProccessThread();
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private void ReceiveResponseFromPrinterHandler(CancellationToken token)
        {
            Debug.WriteLine("Task handle printer response work on thread " + Environment.CurrentManagedThreadId);
            while (true)
            {
                token.ThrowIfCancellationRequested();
                var sender = _QueueBufferPrinterResponseData.Dequeue();
                if (sender == null) continue;
                try
                {
                    if (sender is PODDataModel)
                    {
                        PODDataModel podDataModel = sender as PODDataModel;
                        //Shared.RaiseOnReceiveResponsePrinter(podDataModel.Text);
                        string[] pODcommand = podDataModel.Text.Split(';');
                        PODResponseModel PODResponseModel = new PODResponseModel();
                        PODResponseModel.Command = pODcommand[0];

                        if (PODResponseModel != null)
                        {
                            if (PODResponseModel.Command == "DATA")
                            {
                                PODResponseModel.Status = pODcommand[1];
                                if (PODResponseModel.Status != null && PODResponseModel.Status == "RYES")
                                {
                                    if (ReceivedCode < 100)
                                    {
                                        lock (_ReceiveLocker)
                                        {
                                            _IsSendWait = false;
                                            Monitor.Pulse(_ReceiveLocker); // Notify that printer was received data
                                        }
                                    }

                                    if (Shared.OperStatus != OperationStatus.Stopped) ReceivedCode++;

                                    if (Shared.OperStatus == OperationStatus.Processing)
                                    {
                                        if (ReceivedCode >= 1 && _IsVerifyAndPrintMode)
                                        {
                                            Shared.OperStatus = OperationStatus.Running;
                                            Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);
                                            EnableUIComponent(Shared.OperStatus);
                                        }
                                    }
                                }
                            }
                            else if (PODResponseModel.Command == "RSFP")
                            {
                                if (_IsOnProductionMode)
                                {
                                    lock (_PrintedResponseLocker)
                                    {
                                        _IsPrintedResponse = true; // Notify that have a printed response
                                    }
                                }

                                if (_IsAfterProductionMode)
                                {
                                    lock (_PrintLocker)
                                    {
                                        _IsPrintedWait = false;
                                        Monitor.Pulse(_PrintLocker);
                                    }
                                }

                                //Receive data: RSFP;1/101;DATA; check.pvcfc.com.vn/?id=L927GCCR72;L927GCCR72;0;0;1
                                pODcommand = pODcommand.Skip(3).ToArray();
                                //221031
                                string printedResult = "";
                                if (_SelectedJob.JobType == JobType.VerifyAndPrint)
                                {
                                    if (Shared.Settings.VerifyAndPrintBasicSentMethod)
                                    {
                                        printedResult = pODcommand[0];
                                    }
                                    else
                                    {
                                        if (Shared.Settings.PrintFieldForVerifyAndPrint.Count() > 0)
                                        {
                                            if (_Emergency.TryGetValue(string.Join("", pODcommand), out int codeIndex))
                                            {
                                                lock (_SyncObjCodeList)
                                                {
                                                    printedResult = GetCompareDataByPODFormat(_PrintedCodeObtainFromFile[codeIndex], _SelectedJob.PODFormat);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in _SelectedJob.PODFormat)
                                            {
                                                if (item.Type == PODModel.TypePOD.FIELD)
                                                {
                                                    int indexItem = item.Index - 1;
                                                    if (indexItem < pODcommand.Length)
                                                        printedResult += pODcommand[item.Index - 1];
                                                }
                                                else if (item.Type == PODModel.TypePOD.TEXT)
                                                {
                                                    printedResult += item.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var item in _SelectedJob.PODFormat)
                                    {
                                        if (item.Type == PODModel.TypePOD.FIELD)
                                        {
                                            int indexItem = item.Index - 1;
                                            if (indexItem < pODcommand.Length)
                                                printedResult += pODcommand[item.Index - 1];
                                        }
                                        else if (item.Type == PODModel.TypePOD.TEXT)
                                        {
                                            printedResult += item.Value;
                                        }
                                    }
                                }
#if DEBUG
                                printedResponseValue = printedResult;
#endif
                                _QueueBufferUpdateUIPrinter.Enqueue(printedResult);
                            }
                            else if (PODResponseModel.Command == "STAR")
                            {
                                PODResponseModel.Command = pODcommand[0];
                                PODResponseModel.Status = pODcommand[1];
                                if (PODResponseModel.Status != null && (PODResponseModel.Status == "OK" || PODResponseModel.Status == "READY"))
                                {
                                    if (podDataModel.RoleOfPrinter == RoleOfStation.ForProduct && !_IsVerifyAndPrintMode)
                                    {
                                        // Send POD data to printer when printer ready receive data
                                        SendDataToPrinterAsync();
                                        // END Send POD data to printer when printer ready receive data
                                    }
                                    else { }
                                }
                                else
                                {
                                    PODResponseModel.Error = pODcommand[2];
                                    var message = "Unknown";
                                    if (PODResponseModel.Error == "001")
                                    {
                                        message = "Open templates failed (dose not exist, others templates being opening,...)";
                                    }
                                    else if (PODResponseModel.Error == "002")
                                    {
                                        message = "Start pages, End pages is invalid";
                                    }
                                    else if (PODResponseModel.Error == "003")
                                    {
                                        message = "No printhead is selected";
                                    }
                                    else if (PODResponseModel.Error == "004")
                                    {
                                        message = "Speed limit";
                                    }
                                    else if (PODResponseModel.Error == "005")
                                    {
                                        message = "Printhead disconnected";
                                    }
                                    else if (PODResponseModel.Error == "006")
                                    {
                                        message = "Unknown printhead";
                                    }
                                    else if (PODResponseModel.Error == "007")
                                    {
                                        message = "No cartridges";
                                    }
                                    else if (PODResponseModel.Error == "008")
                                    {
                                        message = "Invalid cartridges";
                                    }
                                    else if (PODResponseModel.Error == "009")
                                    {
                                        message = "Out of ink";
                                    }
                                    else if (PODResponseModel.Error == "010")
                                    {
                                        message = "Cartridges is locked";
                                    }
                                    else if (PODResponseModel.Error == "011")
                                    {
                                        message = "Invalid version";
                                    }
                                    else if (PODResponseModel.Error == "012")
                                    {
                                        message = "Incorrect printhead";
                                    }

                                    Invoke(new Action(() =>
                                    {
                                        StopProcess(false, Lang.SomePrintParametersAreMissing + ": " + message, false, true);
                                    }));
                                }
                            }
                            else if (PODResponseModel.Command == "STOP") { }
                            else if (PODResponseModel.Command == "MON")
                            {
                                PODResponseModel.Status = pODcommand[3];

                                if (PODResponseModel.Status == "Stop" && Shared.OperStatus == OperationStatus.Running && _SelectedJob.CompareType == CompareType.Database && _SelectedJob.JobType != JobType.StandAlone)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        StopProcess(false, "Printer stops suddenly!", false, true);
                                    }));
                                }
                                //Stop, Processing, Ready, Printing, Connected, Disconnected, Error, Disable
                                if (PODResponseModel.Status == "Stop")
                                {
                                    _PrinterStatus = PrinterStatus.Stop;
                                }
                                else if (PODResponseModel.Status == "Processing")
                                {
                                    _PrinterStatus = PrinterStatus.Processing;
                                }
                                else if (PODResponseModel.Status == "Ready" || PODResponseModel.Status == "Start")
                                {
                                    _PrinterStatus = PrinterStatus.Ready;
                                }
                                else if (PODResponseModel.Status == "Printing")
                                {
                                    _PrinterStatus = PrinterStatus.Printing;
                                }
                                else if (PODResponseModel.Status == "Connected")
                                {
                                    _PrinterStatus = PrinterStatus.Connected;
                                }
                                else if (PODResponseModel.Status == "Disconnected")
                                {
                                    _PrinterStatus = PrinterStatus.Disconnected;
                                }
                                else if (PODResponseModel.Status == "Error")
                                {
                                    _PrinterStatus = PrinterStatus.Error;
                                }
                                else if (PODResponseModel.Status == "Disable")
                                {
                                    _PrinterStatus = PrinterStatus.Disable;
                                }
                                else if (PODResponseModel.Status == "Start")
                                {
                                    _PrinterStatus = PrinterStatus.Start;
                                }
                                else if (PODResponseModel.Status == "")
                                {
                                    _PrinterStatus = PrinterStatus.Null;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    LoggingController.SaveHistory(
                        String.Format("Thread Exception"),
                        Lang.Error,
                        String.Format("Printed response handler"),
                        SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                        LoggingType.Error);
                }
            }
        }

        private void Shared_OnPrinterDataChange(object sender, EventArgs e)
        {
            _QueueBufferPrinterResponseData.Enqueue(sender);
        }

        private async void SendDataToPrinterAsync()
        {
            _SendDataToPrinterTokenCTS = new CancellationTokenSource();
            var token = _SendDataToPrinterTokenCTS.Token;
            await Task.Run(() => { SendPODDataProductToPrinter(token); });
        }

        private void SendPODDataProductToPrinter(CancellationToken token)
        {
            // Wait printer ready
            Thread.Sleep(500);
            int counter = 0;
            List<string[]> codeList = null;
            List<string> tmpListLog = new List<string>();
            lock (_SyncObjCodeList)
            {
                //Clone list
                codeList = new List<string[]>(_PrintedCodeObtainFromFile);
            }
            lock (_PrintLocker)
            {
                _IsPrintedWait = false;
                _PrintedResult = ComparisonResult.Valid;
            }
            try
            {
                // Get the first not-printed code in database 
                int startIndex = codeList.FindIndex(x => x.Last() != "Printed");
                if (startIndex == -1) return;
                for (int codeIndex = startIndex; codeIndex < codeList.Count(); codeIndex++)
                {
                    token.ThrowIfCancellationRequested();
                    string[] codeModel = codeList[codeIndex];
                    // Last index of valid code
                    int index = codeModel.Length - 1;
                    // Check if current code is printed or duplicate
                    if (codeModel[index] != "Printed" && codeModel[index] != "Duplicate")
                    {
                        string data = "";

                        token.ThrowIfCancellationRequested();

                        // Init send data
                        data = string.Join(";", codeModel.Take(index).Skip(1));

                        // Init send command
                        string command = string.Format("DATA;{0}", data);

                        if (podController != null)
                        {
                            podController.Send(command);
                            NumberOfSentPrinter++;

                            tmpListLog = command.Split(';').Skip(1).ToList();
                            _QueueBufferBackupSendLog.Enqueue(tmpListLog.ToArray());
                            tmpListLog.Clear();
                        }

                        counter++;

                        // Change operation status
                        if (Shared.OperStatus == OperationStatus.Processing)
                        {
                            // Check allow system runing, not waiting util send data complete
                            if (counter >= 100 && _IsAfterProductionMode)
                            {
                                // Update user interface the system is ready
                                Shared.OperStatus = OperationStatus.Running;
                                Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);
                                EnableUIComponent(Shared.OperStatus);
                            }
                            else if (counter >= 1 && _IsOnProductionMode)
                            {
                                Shared.OperStatus = OperationStatus.Running;
                                Shared.RaiseOnOperationStatusChangeEvent(Shared.OperStatus);
                                EnableUIComponent(Shared.OperStatus);
                            }
                        }

                        if (_IsOnProductionMode)
                        {
                            lock (_PrintLocker)
                            {
                                _IsPrintedWait = true;
                                while (_IsPrintedWait) Monitor.Wait(_PrintLocker); // Waiting until code is print
                                if (_PrintedResult != ComparisonResult.Valid && _PrintedResult != ComparisonResult.Duplicated) // Check checked result to know if need to re sent code
                                {
                                    //codeIndex = codeIndex <= 0 ? 0 : codeIndex - 1;
                                    //codeModel = codeList[codeIndex];
                                    codeIndex--;
                                }
                            }
                        }
                        else if (_IsAfterProductionMode)
                        {
                            if (counter < 100)
                            {
                                Thread.Sleep(50);
                            }
                            else
                            {
                                lock (_PrintLocker)
                                {
                                    _IsPrintedWait = true;
                                    while (_IsPrintedWait) Monitor.Wait(_PrintLocker); // Waiting until receive DATA:RYES from printer
                                }
                            }
                        }
                    }
                }

                if (Shared.OperStatus == OperationStatus.Processing)
                {
                    // Update user interface the system is ready
                    Shared.OperStatus = OperationStatus.Running;
                    EnableUIComponent(Shared.OperStatus);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread send data to printer was stopped!");
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                Console.WriteLine("Thread send data to printer was error!");
                StopProcess(false, Lang.HandleError, false, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private void ReleaseLocker()
        {
            lock (_PrintLocker)
            {
                _IsPrintedWait = false;
                Monitor.Pulse(_PrintLocker);
            }
            lock (_ReceiveLocker)
            {
                _IsSendWait = false;
                Monitor.Pulse(_ReceiveLocker);
            }
            lock (_CheckLocker)
            {
                _IsCheckedWait = true;
                Monitor.Pulse(_CheckLocker);
            }
            lock (_ReceiveLocker)
            {
                _IsSendWait = false;
                Monitor.Pulse(_ReceiveLocker);
            }

            lock (_PrintLocker)
                _IsPrintedWait = false;
            lock (_ReceiveLocker)
                _IsSendWait = false;
            lock (_CheckLocker)
                _IsCheckedWait = true;
            lock (_ReceiveLocker)
                _IsSendWait = false;
        }

        private void KillTThreadSendPODDataToPrinter()
        {
            ReleaseLocker();
            _SendDataToPrinterTokenCTS?.Cancel();
        }

        public async void ReprintAsync()
        {
            await Task.Run(() => { Reprint(); });
        }

        private void Reprint()
        {
            try
            {
                if (!_IsAfterProductionMode)
                    return;
                lock (_SyncObjCodeList)
                {
                    if (_PrintedCodeObtainFromFile.Count() > 0 && _CodeListPODFormat.Count() > 0)
                    {
                        _TotalMissed = 0;
                        int codeDataLenght = _PrintedCodeObtainFromFile[0].Length - 1;
                        foreach (var item in _CodeListPODFormat)
                        {
                            if (!item.Value.Status)
                            {
                                if (_PrintedCodeObtainFromFile[item.Value.Index][codeDataLenght] == "Printed")
                                {
                                    _PrintedCodeObtainFromFile[item.Value.Index][codeDataLenght] = "Reprint";
                                }
                            }
                            else
                            {
                                if (_PrintedCodeObtainFromFile[item.Value.Index][codeDataLenght] != "Printed")
                                {
                                    _PrintedCodeObtainFromFile[item.Value.Index][codeDataLenght] = "Printed";
                                }
                            }
                        }
                        //_TotalMissed = _TotalCode - NumberOfCheckPassed;
                        _TotalMissed = _TotalCode - NumberOfCheckPassed;
                    }
                }

                if (_FormCheckedResult != null)
                {
                    Invoke(new Action(() => { _FormCheckedResult.Close(); }));
                }

                if (NumberOfCheckPassed < _TotalCode)
                {
                    DialogResult dialogResult = CuzMessageBox.Show(Lang.ReprintConfirm, Lang.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                        StartProcess();
                    else
                        return;
                }
            }
            catch
            {
                CuzMessageBox.Show(Lang.ReprintError, Lang.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
        }

        private async void ExportLogAsynce()
        {
            if (Shared.OperStatus != OperationStatus.Stopped)
            {
                return;
            }

            string checkInitDataMessage = "";
            checkInitDataMessage = CheckInitDataErrorAndGenerateMessage();
            if (checkInitDataMessage != "")
            {
                DialogResult dialogResult = CuzMessageBox.Show(checkInitDataMessage, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv";
            sfd.FileName = _SelectedJob.FileName;
            sfd.ShowDialog();
            if (sfd.FileName == "")
            {
                return;
            }

            EnableUIComponentWhenLoadData(false);

            await Task.Run(() => { Export(sfd.FileName); });

            EnableUIComponentWhenLoadData(true);
        }

        public void Export(string fileName)
        {
            try
            {
                //var checkedResultDict = _CheckedResultCodeList.Where(x => x[2] == "Valid").Select(x => new string[] { x[1], x[x.Length - 1] }).ToDictionary(x => x[0], x => x[1]);
                Dictionary<string, string> checkedResultDict = new Dictionary<string, string>();
                int endIndex = _CheckedResultCodeList.Count();
                int lastIndexValue = _ColumnNames.Count() - 1;
                for (int i = 0; i < endIndex; i++)
                {
                    bool isValid = _CheckedResultCodeList[i][2] == "Valid";
                    if (isValid)
                    {
                        var tKey = _CheckedResultCodeList[i][1];
                        var tValue = _CheckedResultCodeList[i][lastIndexValue];
                        if (!checkedResultDict.TryGetValue(tKey, out string tmp))
                        {
                            checkedResultDict.Add(tKey, tValue);
                        }
                    }
                }

                if (File.Exists(fileName)) File.Delete(fileName);
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    string header = "";
                    foreach (var col in _DatabaseColunms)
                    {
                        header += Csv.Escape(col) + ",";
                    }
                    header += "VerifyDate";
                    writer.WriteLine(header);

                    for (int i = 0; i < _TotalCode; i++)
                    {
                        var record = _PrintedCodeObtainFromFile[i];
                        var compareString = GetCompareDataByPODFormat(record, _SelectedJob.PODFormat);
                        var writeValue = string.Join(",", record.Take(record.Length - 1).Select(x => Csv.Escape(x))) + ",";
                        if (checkedResultDict.TryGetValue(compareString, out string dateVerify))
                        {
                            writeValue += "Verified";
                            writeValue += "," + Csv.Escape(dateVerify);
                            checkedResultDict.Remove(compareString);
                        }
                        else
                        {
                            string tmpValue = record[record.Length - 1];
                            writeValue += tmpValue == "Printed" ? "Unknown" : tmpValue;
                            writeValue += "," + "";
                        }
                        writer.WriteLine(writeValue);
                    }
                }
                Process.Start(fileName);
                checkedResultDict.Clear();
            }
            catch (Exception ex)
            {
                // Catch Error - Add by ThongThach 05/12/2023
                // Detected some unusual errors - Phát hiện một số lỗi bất thường
                CuzAlert.Show(Lang.DetectError, Alert.enmType.Warning, new Size(500, 120), new Point(Location.X, Location.Y), this.Size, true);
                Shared.RaiseOnLogError(ex);
                EnableUIComponent(OperationStatus.Stopped);
            }
        }

        private void Shared_OnPrinterStatusChange(object sender, EventArgs e)
        {
            UpdateStatusLabelPrinter();
        }

        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLanguage();
        }

        private void Shared_OnSensorControllerChangeEvent(object sender, EventArgs e)
        {
            UpdateUISensorControllerStatus(Shared.IsSensorControllerConnected);
        }

        #endregion Events Called
        private void ReleaseResource()
        {
            try
            {
                _VirtualCTS?.Cancel();
                _SendDataToPrinterTokenCTS?.Cancel();
                _PrinterRespontCST?.Cancel();
                _QueueBufferPrinterResponseData.Enqueue(null);

                _VirtualCTS?.Dispose();
                _SendDataToPrinterTokenCTS?.Dispose();
                _PrinterRespontCST?.Dispose();

                _OperationCancelTokenSource?.Cancel();
                _UICheckedResultCancelTokenSource?.Cancel(); 
                _UIPrintedResponseCancelTokenSource?.Cancel();
                _BackupImageCancelTokenSource?.Cancel(); 
                _BackupResponseCancelTokenSource?.Cancel(); 
                _BackupResultCancelTokenSource?.Cancel();
                _BackupSendLogCancelTokenSource?.Cancel();

                _OperationCancelTokenSource?.Dispose();
                _UICheckedResultCancelTokenSource?.Dispose();
                _UIPrintedResponseCancelTokenSource?.Dispose();
                _BackupImageCancelTokenSource?.Dispose();
                _BackupResponseCancelTokenSource?.Dispose();
                _BackupResultCancelTokenSource?.Dispose();
                _BackupSendLogCancelTokenSource?.Dispose();

                _CheckedResultCodeList.Clear();
                _CodeListPODFormat.Clear();
                _PrintedCodeObtainFromFile.Clear();

                _QueueBufferDataObtained.Clear();
                _QueueBufferDataObtainedResult.Clear();
                _QueueBufferUpdateUIPrinter.Clear();

                _QueueBufferBackupImage.Clear();
                _QueueBufferBackupPrintedCode.Clear();
                _QueueBufferBackupCheckedResult.Clear();
                _QueueBufferBackupSendLog.Clear();

                Shared.OnCameraStatusChange -= Shared_OnCameraStatusChange;
                Shared.OnCameraReadDataChange -= Shared_OnCameraReadDataChange;
                Shared.OnPrinterDataChange -= Shared_OnPrinterDataChange;
                Shared.OnPrintingStateChange -= Shared_OnPrintingStateChange;
                Shared.OnPrinterStatusChange -= Shared_OnPrinterStatusChange;
                Shared.OnLanguageChange -= Shared_OnLanguageChange;
                Shared.OnSensorControllerChangeEvent -= Shared_OnSensorControllerChangeEvent;
                Shared.OnVerifyAndPrindSendDataMethod -= Shared_OnVerifyAndPrindSendDataMethod;
                Shared.OnLogError -= Shared_OnLogError;
                this.OnReceiveVerifyDataEvent -= SendVerifiedDataToPrinter;
                KillThread(ref _ThreadPrinterResponseHandler);

                _FormCheckedResult?.Close();
                _FormCheckedResult?.Dispose();
                _FormPreviewDatabase?.Close();
                _FormPreviewDatabase?.Dispose();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        private void Shared_OnLogError(object sender, EventArgs e)
        {
            try
            {
                Exception ex = default(Exception);
                ex = (Exception)sender;
                var result = "";
                if (ex.InnerException != null)
                {
                    string innerExection = "InnerException: " + ex.InnerException.InnerException + " && ";
                    result += innerExection;
                }
                if (ex.Message != null)
                {
                    string errorMessage = ex.Message.Replace("\r", "").Replace("\n", "").Replace(',', '&');
                    result += "Message: " + errorMessage;
                }
                if (ex.Source != null)
                {
                    string errorSource = ex.Source;
                    result += " && Source: " + errorSource;
                }
                if (ex.StackTrace != null)
                {
                    StackTrace stackTrace = new StackTrace(ex, true);

                    foreach (StackFrame stackFrame in stackTrace.GetFrames())
                    {
                        string methodName = stackFrame.GetMethod().Name;
                        int lineNumber = stackFrame.GetFileLineNumber();
                        if (methodName != "" && lineNumber != 0)
                        {
                            result += " && Method: " + methodName + " line " + lineNumber;
                        }
                    }
                }
                if (ex.TargetSite != null)
                {
                    string targetSite = " && TargetSite: " + ex.TargetSite.ToString() + " - " + ex.TargetSite.DeclaringType.ToString();
                    result += targetSite;
                }
                result = result.Replace("'", "");
                LoggingController.SaveHistory(
                    String.Format("Error catch"),
                    Lang.Error,
                    String.Format(result),
                    SecurityController.Decrypt(Shared.LoggedInUser.UserName, "rynan_encrypt_remember"),
                    LoggingType.Error);
            }
            catch
            {

            }
        }

        private void FrmMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseResource();

            if (_ParentForm != null)
            {
                _ParentForm.ShowForm();
            }
        }

        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }

            lblJobType.Text = Lang.JobType;
            btnJob.Text = Lang.Operation;
            btnDatabase.Text = Lang.DatabaseFrmMain;
            btnAccount.Text = Lang.Account;
            btnHistory.Text = Lang.ProgramHistoryFrmMain;
            btnSettings.Text = Lang.Settings;
            btnExit.Text = Lang.Exit;
            btnExport.Text = Lang.ExportLog;

            pnlJobInformation.Text = Lang.JobDetails;
            lblJobName.Text = Lang.FileName;
            lblCompareType.Text = Lang.CompareType;
            lblPODFormat.Text = Lang.PODFormat;
            lblTemplatePrint.Text = Lang.TemplateName;

            lblReceived.Text = Lang.Received;
            lblSentData.Text = Lang.SentData;
            lblPrintedCode.Text = Lang.PrintedCode;

            pnlVerificationProcess.Text = Lang.VerifyProgress;
            lblTotalChecked.Text = Lang.TotalChecked;
            lblPassed.Text = Lang.CheckedPassed;
            lblFailed.Text = Lang.CheckedFailed;

            pnlCurrentCheck.Text = Lang.CheckedResult;
            lblCodeResult.Text = Lang.Code;
            lblProcessingTime.Text = Lang.ProcessingTime;
            lblStatusResult.Text = Lang.StatusCode;

            lblStatusCamera01.Text = Lang.CameraTMP;
            lblStatusPrinter01.Text = Lang.Printer;
            lblSensorControllerStatus.Text = Lang.SensorController;
            toolStripOperationStatus.Text = Lang.Stopped;

            mnChangePassword.Text = Lang.ChangePassword;
            mnManage.Text = Lang.Manage;
            mnLogOut.Text = Lang.LogOut;

            lblDatabase.Text = Lang.Database;
            lblCheckedResult.Text = Lang.CheckedResult1;

            toolStripVersion.Text = Lang.Version + ": " + Properties.Settings.Default.SoftwareVersion;
        }
    }
}

