namespace BarcodeVerificationSystem.View
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusCamera01 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusPrinter01 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSensorControllerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripOperationStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlControllButton = new DesignUI.CuzUI.CuzPanel();
            this.tableLayoutControl = new System.Windows.Forms.TableLayoutPanel();
            this.btnTrigger = new DesignUI.CuzUI.CuzButton();
            this.btnStop = new DesignUI.CuzUI.CuzButton();
            this.btnStart = new DesignUI.CuzUI.CuzButton();
            this.pnlJobInformation = new DesignUI.CuzUI.RoundPanel();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.txtJobType = new DesignUI.CuzUI.CuzTextBox();
            this.lblJobType = new System.Windows.Forms.Label();
            this.txtTemplatePrint = new DesignUI.CuzUI.CuzTextBox();
            this.txtPODFormat = new DesignUI.CuzUI.CuzTextBox();
            this.txtStaticText = new DesignUI.CuzUI.CuzTextBox();
            this.txtCompareType = new DesignUI.CuzUI.CuzTextBox();
            this.txtJobName = new DesignUI.CuzUI.CuzTextBox();
            this.lblTemplatePrint = new System.Windows.Forms.Label();
            this.lblPODFormat = new System.Windows.Forms.Label();
            this.lblStaticText = new System.Windows.Forms.Label();
            this.lblCompareType = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblJobName = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCheckedResult = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPictureBox = new DesignUI.CuzUI.CuzPanel();
            this.pnlPicture = new DesignUI.CuzUI.CuzPanel();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.pnlCurrentCheck = new DesignUI.CuzUI.RoundPanel();
            this.lblStatusResult = new System.Windows.Forms.Label();
            this.lblProcessingTime = new System.Windows.Forms.Label();
            this.txtStatusResult = new DesignUI.CuzUI.CuzTextBox();
            this.txtProcessingTimeResult = new DesignUI.CuzUI.CuzTextBox();
            this.txtCodeResult = new DesignUI.CuzUI.CuzTextBox();
            this.lblCodeResult = new System.Windows.Forms.Label();
            this.pnlVerificationProcess = new DesignUI.CuzUI.RoundPanel();
            this.tableLayoutPanelProcess = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalChecked = new DesignUI.CuzUI.CuzPanel();
            this.lblTotalCheckedValue = new System.Windows.Forms.Label();
            this.lblTotalChecked = new System.Windows.Forms.Label();
            this.prBarCheckPassed = new CircularProgressBar.CircularProgressBar();
            this.pnlCheckFailed = new DesignUI.CuzUI.CuzPanel();
            this.lblFailed = new System.Windows.Forms.Label();
            this.lblCheckResultFailedValue = new System.Windows.Forms.Label();
            this.pnlCheckPassed = new DesignUI.CuzUI.CuzPanel();
            this.lblCheckResultPassedValue = new System.Windows.Forms.Label();
            this.lblPassed = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCheckedResult = new DesignUI.CuzUI.CuzPanel();
            this.picCheckedResultLoading = new System.Windows.Forms.PictureBox();
            this.btnNull = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnInvalid = new System.Windows.Forms.Button();
            this.btnValid = new System.Windows.Forms.Button();
            this.dgvCheckedResult = new DesignUI.CuzUI.CuzDataGridView();
            this.lblCheckedResult = new System.Windows.Forms.Label();
            this.pnlDatabase = new DesignUI.CuzUI.CuzPanel();
            this.picDatabaseLoading = new System.Windows.Forms.PictureBox();
            this.dgvDatabase = new DesignUI.CuzUI.CuzDataGridView();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.tableLayoutPanelPrintedState = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPrintedState3 = new System.Windows.Forms.Panel();
            this.pnlPrintedCode = new DesignUI.CuzUI.CuzPanel();
            this.lblPrintedCodeValue = new System.Windows.Forms.Label();
            this.lblPrintedCode = new System.Windows.Forms.Label();
            this.cuzPanel4 = new DesignUI.CuzUI.CuzPanel();
            this.pnlPrintedState2 = new System.Windows.Forms.Panel();
            this.pnlReveied = new DesignUI.CuzUI.CuzPanel();
            this.lblReceived = new System.Windows.Forms.Label();
            this.lblReceivedValue = new System.Windows.Forms.Label();
            this.cuzPanel2 = new DesignUI.CuzUI.CuzPanel();
            this.pnlPrintedState1 = new System.Windows.Forms.Panel();
            this.pnlSentData = new DesignUI.CuzUI.CuzPanel();
            this.lblSentDataValue = new System.Windows.Forms.Label();
            this.lblSentData = new System.Windows.Forms.Label();
            this.cuzPanel10 = new DesignUI.CuzUI.CuzPanel();
            this.pnlMenu = new DesignUI.CuzUI.CuzPanel();
            this.btnVirtualStop = new FontAwesome.Sharp.IconButton();
            this.btnVirtualStart = new FontAwesome.Sharp.IconButton();
            this.btnExport = new FontAwesome.Sharp.IconButton();
            this.btnSettings = new FontAwesome.Sharp.IconButton();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.btnHistory = new FontAwesome.Sharp.IconButton();
            this.btnAccount = new FontAwesome.Sharp.IconButton();
            this.btnDatabase = new FontAwesome.Sharp.IconButton();
            this.btnJob = new FontAwesome.Sharp.IconButton();
            this.cuzDropdownManageAccount = new DesignUI.CuzUI.CuzDropdownMenu(this.components);
            this.mnManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.cvsDisplayImage = new Cognex.InSight.Web.Controls.CvsDisplay();
            this.cvsDisplayImg = new Cognex.InSight.Web.Controls.CvsDisplay();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlControllButton.SuspendLayout();
            this.tableLayoutControl.SuspendLayout();
            this.pnlJobInformation.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelCheckedResult.SuspendLayout();
            this.pnlPictureBox.SuspendLayout();
            this.pnlPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.pnlCurrentCheck.SuspendLayout();
            this.pnlVerificationProcess.SuspendLayout();
            this.tableLayoutPanelProcess.SuspendLayout();
            this.pnlTotalChecked.SuspendLayout();
            this.pnlCheckFailed.SuspendLayout();
            this.pnlCheckPassed.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlCheckedResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckedResultLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckedResult)).BeginInit();
            this.pnlDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabase)).BeginInit();
            this.tableLayoutPanelPrintedState.SuspendLayout();
            this.pnlPrintedState3.SuspendLayout();
            this.pnlPrintedCode.SuspendLayout();
            this.pnlPrintedState2.SuspendLayout();
            this.pnlReveied.SuspendLayout();
            this.pnlPrintedState1.SuspendLayout();
            this.pnlSentData.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.cuzDropdownManageAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusCamera01,
            this.lblStatusPrinter01,
            this.lblSensorControllerStatus,
            this.toolStripOperationStatus,
            this.toolStripVersion,
            this.toolStripDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(82, 867);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1718, 33);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStripMain";
            // 
            // lblStatusCamera01
            // 
            this.lblStatusCamera01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusCamera01.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblStatusCamera01.Image = global::BarcodeVerificationSystem.Properties.Resources.icons8_camera_30px_connected;
            this.lblStatusCamera01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatusCamera01.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblStatusCamera01.Margin = new System.Windows.Forms.Padding(15, 3, 0, 5);
            this.lblStatusCamera01.Name = "lblStatusCamera01";
            this.lblStatusCamera01.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblStatusCamera01.Size = new System.Drawing.Size(95, 25);
            this.lblStatusCamera01.Text = "Camera";
            // 
            // lblStatusPrinter01
            // 
            this.lblStatusPrinter01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusPrinter01.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblStatusPrinter01.Image = global::BarcodeVerificationSystem.Properties.Resources.icons8_printer_30px_connected;
            this.lblStatusPrinter01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatusPrinter01.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblStatusPrinter01.Margin = new System.Windows.Forms.Padding(10, 3, 0, 5);
            this.lblStatusPrinter01.Name = "lblStatusPrinter01";
            this.lblStatusPrinter01.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblStatusPrinter01.Size = new System.Drawing.Size(85, 25);
            this.lblStatusPrinter01.Text = "Printer";
            // 
            // lblSensorControllerStatus
            // 
            this.lblSensorControllerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerStatus.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblSensorControllerStatus.Image = global::BarcodeVerificationSystem.Properties.Resources.icons8_sensor_30px_disconnected;
            this.lblSensorControllerStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSensorControllerStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblSensorControllerStatus.Margin = new System.Windows.Forms.Padding(10, 3, 0, 5);
            this.lblSensorControllerStatus.Name = "lblSensorControllerStatus";
            this.lblSensorControllerStatus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblSensorControllerStatus.Size = new System.Drawing.Size(1115, 25);
            this.lblSensorControllerStatus.Spring = true;
            this.lblSensorControllerStatus.Text = "Sensor controller";
            this.lblSensorControllerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripOperationStatus
            // 
            this.toolStripOperationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripOperationStatus.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStripOperationStatus.Image = global::BarcodeVerificationSystem.Properties.Resources.icon_status_181;
            this.toolStripOperationStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripOperationStatus.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripOperationStatus.Name = "toolStripOperationStatus";
            this.toolStripOperationStatus.Size = new System.Drawing.Size(145, 28);
            this.toolStripOperationStatus.Text = "Operation status";
            // 
            // toolStripVersion
            // 
            this.toolStripVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripVersion.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStripVersion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripVersion.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripVersion.Name = "toolStripVersion";
            this.toolStripVersion.Size = new System.Drawing.Size(67, 28);
            this.toolStripVersion.Text = "Version";
            // 
            // toolStripDateTime
            // 
            this.toolStripDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDateTime.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStripDateTime.Name = "toolStripDateTime";
            this.toolStripDateTime.Size = new System.Drawing.Size(155, 28);
            this.toolStripDateTime.Text = "2022/10/17 1:35 PM";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pnlControllButton);
            this.panel1.Controls.Add(this.pnlJobInformation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 867);
            this.panel1.TabIndex = 48;
            // 
            // pnlControllButton
            // 
            this.pnlControllButton._BorderColor = System.Drawing.Color.Silver;
            this.pnlControllButton._BorderRadius = 0;
            this.pnlControllButton._BorderSize = 0;
            this.pnlControllButton._Corner = 0F;
            this.pnlControllButton._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlControllButton._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlControllButton._GradientPanel = false;
            this.pnlControllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.pnlControllButton.Controls.Add(this.tableLayoutControl);
            this.pnlControllButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControllButton.Location = new System.Drawing.Point(0, 783);
            this.pnlControllButton.Name = "pnlControllButton";
            this.pnlControllButton.Padding = new System.Windows.Forms.Padding(7, 5, 1, 6);
            this.pnlControllButton.Size = new System.Drawing.Size(305, 84);
            this.pnlControllButton.TabIndex = 107;
            // 
            // tableLayoutControl
            // 
            this.tableLayoutControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.tableLayoutControl.ColumnCount = 3;
            this.tableLayoutControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutControl.Controls.Add(this.btnTrigger, 2, 0);
            this.tableLayoutControl.Controls.Add(this.btnStop, 1, 0);
            this.tableLayoutControl.Controls.Add(this.btnStart, 0, 0);
            this.tableLayoutControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutControl.Location = new System.Drawing.Point(7, 13);
            this.tableLayoutControl.Name = "tableLayoutControl";
            this.tableLayoutControl.RowCount = 1;
            this.tableLayoutControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutControl.Size = new System.Drawing.Size(297, 65);
            this.tableLayoutControl.TabIndex = 10;
            // 
            // btnTrigger
            // 
            this.btnTrigger._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnTrigger._BorderRadius = 10;
            this.btnTrigger._BorderSize = 1;
            this.btnTrigger._GradientsButton = false;
            this.btnTrigger._Text = "";
            this.btnTrigger.BackColor = System.Drawing.Color.White;
            this.btnTrigger.BackgroundColor = System.Drawing.Color.White;
            this.btnTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTrigger.FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTrigger.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnTrigger.FlatAppearance.BorderSize = 0;
            this.btnTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrigger.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrigger.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTrigger.Image = global::BarcodeVerificationSystem.Properties.Resources.icon_trigger_321;
            this.btnTrigger.Location = new System.Drawing.Point(200, 3);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(94, 59);
            this.btnTrigger.TabIndex = 96;
            this.btnTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTrigger.TextColor = System.Drawing.SystemColors.ControlText;
            this.btnTrigger.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            this.btnStop._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnStop._BorderRadius = 10;
            this.btnStop._BorderSize = 1;
            this.btnStop._GradientsButton = false;
            this.btnStop._Text = "";
            this.btnStop.BackColor = System.Drawing.Color.White;
            this.btnStop.BackgroundColor = System.Drawing.Color.White;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnStop.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop.Image = global::BarcodeVerificationSystem.Properties.Resources.Stop321;
            this.btnStop.Location = new System.Drawing.Point(101, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(93, 59);
            this.btnStop.TabIndex = 96;
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.TextColor = System.Drawing.SystemColors.ControlText;
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnStart._BorderRadius = 10;
            this.btnStart._BorderSize = 1;
            this.btnStart._GradientsButton = false;
            this.btnStart._Text = "";
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.BackgroundColor = System.Drawing.Color.White;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnStart.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Image = global::BarcodeVerificationSystem.Properties.Resources.Play321;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(92, 59);
            this.btnStart.TabIndex = 96;
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStart.TextColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.UseVisualStyleBackColor = false;
            // 
            // pnlJobInformation
            // 
            this.pnlJobInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlJobInformation.BackColor = System.Drawing.Color.White;
            this.pnlJobInformation.Controls.Add(this.btnViewLog);
            this.pnlJobInformation.Controls.Add(this.txtJobType);
            this.pnlJobInformation.Controls.Add(this.lblJobType);
            this.pnlJobInformation.Controls.Add(this.txtTemplatePrint);
            this.pnlJobInformation.Controls.Add(this.txtPODFormat);
            this.pnlJobInformation.Controls.Add(this.txtStaticText);
            this.pnlJobInformation.Controls.Add(this.txtCompareType);
            this.pnlJobInformation.Controls.Add(this.txtJobName);
            this.pnlJobInformation.Controls.Add(this.lblTemplatePrint);
            this.pnlJobInformation.Controls.Add(this.lblPODFormat);
            this.pnlJobInformation.Controls.Add(this.lblStaticText);
            this.pnlJobInformation.Controls.Add(this.lblCompareType);
            this.pnlJobInformation.Controls.Add(this.label14);
            this.pnlJobInformation.Controls.Add(this.lblJobName);
            this.pnlJobInformation.IsTitleHatchStyle = false;
            this.pnlJobInformation.Location = new System.Drawing.Point(10, 14);
            this.pnlJobInformation.Name = "pnlJobInformation";
            this.pnlJobInformation.Radious = 15;
            this.pnlJobInformation.Size = new System.Drawing.Size(292, 769);
            this.pnlJobInformation.TabIndex = 51;
            this.pnlJobInformation.TabStop = false;
            this.pnlJobInformation.Text = "Job information";
            this.pnlJobInformation.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(230)))));
            this.pnlJobInformation.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlJobInformation.TitleForeColor = System.Drawing.Color.White;
            this.pnlJobInformation.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            this.pnlJobInformation.TitleHeight = 30;
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(14, 431);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(78, 31);
            this.btnViewLog.TabIndex = 123;
            this.btnViewLog.Text = "Log View";
            this.btnViewLog.UseVisualStyleBackColor = true;
            this.btnViewLog.Visible = false;
            // 
            // txtJobType
            // 
            this.txtJobType._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtJobType._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtJobType.BackColor = System.Drawing.Color.White;
            this.txtJobType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtJobType.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtJobType.BorderRadius = 8;
            this.txtJobType.BorderSize = 1;
            this.txtJobType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobType.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtJobType.Location = new System.Drawing.Point(147, 149);
            this.txtJobType.Margin = new System.Windows.Forms.Padding(4);
            this.txtJobType.Multiline = false;
            this.txtJobType.Name = "txtJobType";
            this.txtJobType.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtJobType.PasswordChar = false;
            this.txtJobType.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtJobType.PlaceholderText = "";
            this.txtJobType.ReadOnly = true;
            this.txtJobType.Size = new System.Drawing.Size(128, 35);
            this.txtJobType.TabIndex = 122;
            this.txtJobType.TabStop = false;
            this.txtJobType.UnderlinedStyle = false;
            // 
            // lblJobType
            // 
            this.lblJobType.AutoSize = true;
            this.lblJobType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobType.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblJobType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblJobType.Location = new System.Drawing.Point(143, 124);
            this.lblJobType.Name = "lblJobType";
            this.lblJobType.Size = new System.Drawing.Size(69, 20);
            this.lblJobType.TabIndex = 121;
            this.lblJobType.Text = "Job type";
            // 
            // txtTemplatePrint
            // 
            this.txtTemplatePrint._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtTemplatePrint._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtTemplatePrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplatePrint.BackColor = System.Drawing.Color.White;
            this.txtTemplatePrint.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTemplatePrint.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtTemplatePrint.BorderRadius = 8;
            this.txtTemplatePrint.BorderSize = 1;
            this.txtTemplatePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplatePrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTemplatePrint.Location = new System.Drawing.Point(14, 382);
            this.txtTemplatePrint.Margin = new System.Windows.Forms.Padding(4);
            this.txtTemplatePrint.Multiline = false;
            this.txtTemplatePrint.Name = "txtTemplatePrint";
            this.txtTemplatePrint.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtTemplatePrint.PasswordChar = false;
            this.txtTemplatePrint.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtTemplatePrint.PlaceholderText = "";
            this.txtTemplatePrint.ReadOnly = true;
            this.txtTemplatePrint.Size = new System.Drawing.Size(261, 35);
            this.txtTemplatePrint.TabIndex = 116;
            this.txtTemplatePrint.TabStop = false;
            this.txtTemplatePrint.UnderlinedStyle = false;
            // 
            // txtPODFormat
            // 
            this.txtPODFormat._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtPODFormat._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtPODFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPODFormat.BackColor = System.Drawing.Color.White;
            this.txtPODFormat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPODFormat.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtPODFormat.BorderRadius = 8;
            this.txtPODFormat.BorderSize = 1;
            this.txtPODFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPODFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPODFormat.Location = new System.Drawing.Point(14, 304);
            this.txtPODFormat.Margin = new System.Windows.Forms.Padding(4);
            this.txtPODFormat.Multiline = false;
            this.txtPODFormat.Name = "txtPODFormat";
            this.txtPODFormat.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPODFormat.PasswordChar = false;
            this.txtPODFormat.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPODFormat.PlaceholderText = "";
            this.txtPODFormat.ReadOnly = true;
            this.txtPODFormat.Size = new System.Drawing.Size(261, 35);
            this.txtPODFormat.TabIndex = 117;
            this.txtPODFormat.TabStop = false;
            this.txtPODFormat.UnderlinedStyle = false;
            // 
            // txtStaticText
            // 
            this.txtStaticText._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtStaticText._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtStaticText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStaticText.BackColor = System.Drawing.Color.White;
            this.txtStaticText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtStaticText.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtStaticText.BorderRadius = 8;
            this.txtStaticText.BorderSize = 1;
            this.txtStaticText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStaticText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtStaticText.Location = new System.Drawing.Point(14, 226);
            this.txtStaticText.Margin = new System.Windows.Forms.Padding(4);
            this.txtStaticText.Multiline = false;
            this.txtStaticText.Name = "txtStaticText";
            this.txtStaticText.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtStaticText.PasswordChar = false;
            this.txtStaticText.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtStaticText.PlaceholderText = "";
            this.txtStaticText.ReadOnly = true;
            this.txtStaticText.Size = new System.Drawing.Size(261, 35);
            this.txtStaticText.TabIndex = 118;
            this.txtStaticText.TabStop = false;
            this.txtStaticText.UnderlinedStyle = false;
            // 
            // txtCompareType
            // 
            this.txtCompareType._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtCompareType._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtCompareType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompareType.BackColor = System.Drawing.Color.White;
            this.txtCompareType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCompareType.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtCompareType.BorderRadius = 8;
            this.txtCompareType.BorderSize = 1;
            this.txtCompareType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompareType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCompareType.Location = new System.Drawing.Point(14, 149);
            this.txtCompareType.Margin = new System.Windows.Forms.Padding(4);
            this.txtCompareType.Multiline = false;
            this.txtCompareType.Name = "txtCompareType";
            this.txtCompareType.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCompareType.PasswordChar = false;
            this.txtCompareType.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtCompareType.PlaceholderText = "";
            this.txtCompareType.ReadOnly = true;
            this.txtCompareType.Size = new System.Drawing.Size(112, 35);
            this.txtCompareType.TabIndex = 119;
            this.txtCompareType.TabStop = false;
            this.txtCompareType.UnderlinedStyle = false;
            // 
            // txtJobName
            // 
            this.txtJobName._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtJobName._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJobName.BackColor = System.Drawing.Color.White;
            this.txtJobName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtJobName.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtJobName.BorderRadius = 8;
            this.txtJobName.BorderSize = 1;
            this.txtJobName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtJobName.Location = new System.Drawing.Point(14, 72);
            this.txtJobName.Margin = new System.Windows.Forms.Padding(4);
            this.txtJobName.Multiline = false;
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtJobName.PasswordChar = false;
            this.txtJobName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtJobName.PlaceholderText = "";
            this.txtJobName.ReadOnly = true;
            this.txtJobName.Size = new System.Drawing.Size(261, 35);
            this.txtJobName.TabIndex = 120;
            this.txtJobName.TabStop = false;
            this.txtJobName.UnderlinedStyle = false;
            // 
            // lblTemplatePrint
            // 
            this.lblTemplatePrint.AutoSize = true;
            this.lblTemplatePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplatePrint.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblTemplatePrint.Location = new System.Drawing.Point(13, 355);
            this.lblTemplatePrint.Name = "lblTemplatePrint";
            this.lblTemplatePrint.Size = new System.Drawing.Size(110, 20);
            this.lblTemplatePrint.TabIndex = 110;
            this.lblTemplatePrint.Text = "Template print";
            // 
            // lblPODFormat
            // 
            this.lblPODFormat.AutoSize = true;
            this.lblPODFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPODFormat.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblPODFormat.Location = new System.Drawing.Point(13, 278);
            this.lblPODFormat.Name = "lblPODFormat";
            this.lblPODFormat.Size = new System.Drawing.Size(93, 20);
            this.lblPODFormat.TabIndex = 111;
            this.lblPODFormat.Text = "POD format";
            // 
            // lblStaticText
            // 
            this.lblStaticText.AutoSize = true;
            this.lblStaticText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticText.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblStaticText.Location = new System.Drawing.Point(13, 200);
            this.lblStaticText.Name = "lblStaticText";
            this.lblStaticText.Size = new System.Drawing.Size(80, 20);
            this.lblStaticText.TabIndex = 112;
            this.lblStaticText.Text = "Static text";
            // 
            // lblCompareType
            // 
            this.lblCompareType.AutoSize = true;
            this.lblCompareType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompareType.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblCompareType.Location = new System.Drawing.Point(13, 124);
            this.lblCompareType.Name = "lblCompareType";
            this.lblCompareType.Size = new System.Drawing.Size(108, 20);
            this.lblCompareType.TabIndex = 113;
            this.lblCompareType.Text = "Compare type";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(42, 208);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 20);
            this.label14.TabIndex = 114;
            // 
            // lblJobName
            // 
            this.lblJobName.AutoSize = true;
            this.lblJobName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblJobName.Location = new System.Drawing.Point(13, 46);
            this.lblJobName.Name = "lblJobName";
            this.lblJobName.Size = new System.Drawing.Size(79, 20);
            this.lblJobName.TabIndex = 115;
            this.lblJobName.Text = "Job name";
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.tableLayoutPanelMain);
            this.pnlContainer.Controls.Add(this.panel1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(82, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1718, 867);
            this.pnlContainer.TabIndex = 26;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 418F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(305, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1413, 867);
            this.tableLayoutPanelMain.TabIndex = 54;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelCheckedResult, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.pnlVerificationProcess, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(412, 861);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutPanelCheckedResult
            // 
            this.tableLayoutPanelCheckedResult.ColumnCount = 1;
            this.tableLayoutPanelCheckedResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCheckedResult.Controls.Add(this.pnlPictureBox, 0, 0);
            this.tableLayoutPanelCheckedResult.Controls.Add(this.pnlCurrentCheck, 0, 1);
            this.tableLayoutPanelCheckedResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelCheckedResult.Location = new System.Drawing.Point(11, 9);
            this.tableLayoutPanelCheckedResult.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.tableLayoutPanelCheckedResult.Name = "tableLayoutPanelCheckedResult";
            this.tableLayoutPanelCheckedResult.RowCount = 2;
            this.tableLayoutPanelCheckedResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.33333F));
            this.tableLayoutPanelCheckedResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.66667F));
            this.tableLayoutPanelCheckedResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCheckedResult.Size = new System.Drawing.Size(398, 418);
            this.tableLayoutPanelCheckedResult.TabIndex = 113;
            // 
            // pnlPictureBox
            // 
            this.pnlPictureBox._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlPictureBox._BorderRadius = 10;
            this.pnlPictureBox._BorderSize = 1;
            this.pnlPictureBox._Corner = 0F;
            this.pnlPictureBox._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlPictureBox._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlPictureBox._GradientPanel = false;
            this.pnlPictureBox.Controls.Add(this.pnlPicture);
            this.pnlPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPictureBox.Location = new System.Drawing.Point(3, 3);
            this.pnlPictureBox.Name = "pnlPictureBox";
            this.pnlPictureBox.Padding = new System.Windows.Forms.Padding(4);
            this.pnlPictureBox.Size = new System.Drawing.Size(392, 150);
            this.pnlPictureBox.TabIndex = 50;
            // 
            // pnlPicture
            // 
            this.pnlPicture._BorderColor = System.Drawing.Color.Silver;
            this.pnlPicture._BorderRadius = 10;
            this.pnlPicture._BorderSize = 0;
            this.pnlPicture._Corner = 0F;
            this.pnlPicture._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlPicture._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlPicture._GradientPanel = false;
            this.pnlPicture.Controls.Add(this.pictureBoxPreview);
            this.pnlPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPicture.Location = new System.Drawing.Point(4, 4);
            this.pnlPicture.Name = "pnlPicture";
            this.pnlPicture.Size = new System.Drawing.Size(384, 142);
            this.pnlPicture.TabIndex = 0;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(384, 142);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 3;
            this.pictureBoxPreview.TabStop = false;
            // 
            // pnlCurrentCheck
            // 
            this.pnlCurrentCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCurrentCheck.BackColor = System.Drawing.Color.White;
            this.pnlCurrentCheck.Controls.Add(this.lblStatusResult);
            this.pnlCurrentCheck.Controls.Add(this.lblProcessingTime);
            this.pnlCurrentCheck.Controls.Add(this.txtStatusResult);
            this.pnlCurrentCheck.Controls.Add(this.txtProcessingTimeResult);
            this.pnlCurrentCheck.Controls.Add(this.txtCodeResult);
            this.pnlCurrentCheck.Controls.Add(this.lblCodeResult);
            this.pnlCurrentCheck.IsTitleHatchStyle = false;
            this.pnlCurrentCheck.Location = new System.Drawing.Point(3, 159);
            this.pnlCurrentCheck.Name = "pnlCurrentCheck";
            this.pnlCurrentCheck.Radious = 15;
            this.pnlCurrentCheck.Size = new System.Drawing.Size(392, 256);
            this.pnlCurrentCheck.TabIndex = 51;
            this.pnlCurrentCheck.TabStop = false;
            this.pnlCurrentCheck.Text = "Check result";
            this.pnlCurrentCheck.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(230)))));
            this.pnlCurrentCheck.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCurrentCheck.TitleForeColor = System.Drawing.Color.White;
            this.pnlCurrentCheck.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            this.pnlCurrentCheck.TitleHeight = 30;
            // 
            // lblStatusResult
            // 
            this.lblStatusResult.AutoSize = true;
            this.lblStatusResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblStatusResult.Location = new System.Drawing.Point(11, 183);
            this.lblStatusResult.Name = "lblStatusResult";
            this.lblStatusResult.Size = new System.Drawing.Size(56, 20);
            this.lblStatusResult.TabIndex = 113;
            this.lblStatusResult.Text = "Status";
            // 
            // lblProcessingTime
            // 
            this.lblProcessingTime.AutoSize = true;
            this.lblProcessingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessingTime.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblProcessingTime.Location = new System.Drawing.Point(11, 115);
            this.lblProcessingTime.Name = "lblProcessingTime";
            this.lblProcessingTime.Size = new System.Drawing.Size(121, 20);
            this.lblProcessingTime.TabIndex = 117;
            this.lblProcessingTime.Text = "Processing time";
            // 
            // txtStatusResult
            // 
            this.txtStatusResult._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtStatusResult._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtStatusResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatusResult.BackColor = System.Drawing.Color.White;
            this.txtStatusResult.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtStatusResult.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtStatusResult.BorderRadius = 8;
            this.txtStatusResult.BorderSize = 1;
            this.txtStatusResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtStatusResult.Location = new System.Drawing.Point(15, 206);
            this.txtStatusResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatusResult.Multiline = false;
            this.txtStatusResult.Name = "txtStatusResult";
            this.txtStatusResult.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtStatusResult.PasswordChar = false;
            this.txtStatusResult.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtStatusResult.PlaceholderText = "";
            this.txtStatusResult.ReadOnly = true;
            this.txtStatusResult.Size = new System.Drawing.Size(363, 36);
            this.txtStatusResult.TabIndex = 114;
            this.txtStatusResult.UnderlinedStyle = false;
            // 
            // txtProcessingTimeResult
            // 
            this.txtProcessingTimeResult._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtProcessingTimeResult._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtProcessingTimeResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcessingTimeResult.BackColor = System.Drawing.Color.White;
            this.txtProcessingTimeResult.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtProcessingTimeResult.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtProcessingTimeResult.BorderRadius = 8;
            this.txtProcessingTimeResult.BorderSize = 1;
            this.txtProcessingTimeResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcessingTimeResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtProcessingTimeResult.Location = new System.Drawing.Point(15, 138);
            this.txtProcessingTimeResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtProcessingTimeResult.Multiline = false;
            this.txtProcessingTimeResult.Name = "txtProcessingTimeResult";
            this.txtProcessingTimeResult.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtProcessingTimeResult.PasswordChar = false;
            this.txtProcessingTimeResult.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtProcessingTimeResult.PlaceholderText = "";
            this.txtProcessingTimeResult.ReadOnly = true;
            this.txtProcessingTimeResult.Size = new System.Drawing.Size(363, 36);
            this.txtProcessingTimeResult.TabIndex = 115;
            this.txtProcessingTimeResult.UnderlinedStyle = false;
            // 
            // txtCodeResult
            // 
            this.txtCodeResult._ReadOnlyBackColor = System.Drawing.Color.White;
            this.txtCodeResult._ReadOnlyBorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtCodeResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodeResult.BackColor = System.Drawing.Color.White;
            this.txtCodeResult.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodeResult.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtCodeResult.BorderRadius = 8;
            this.txtCodeResult.BorderSize = 1;
            this.txtCodeResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodeResult.Location = new System.Drawing.Point(15, 69);
            this.txtCodeResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodeResult.Multiline = false;
            this.txtCodeResult.Name = "txtCodeResult";
            this.txtCodeResult.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCodeResult.PasswordChar = false;
            this.txtCodeResult.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtCodeResult.PlaceholderText = "";
            this.txtCodeResult.ReadOnly = true;
            this.txtCodeResult.Size = new System.Drawing.Size(363, 36);
            this.txtCodeResult.TabIndex = 116;
            this.txtCodeResult.UnderlinedStyle = false;
            // 
            // lblCodeResult
            // 
            this.lblCodeResult.AutoSize = true;
            this.lblCodeResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblCodeResult.Location = new System.Drawing.Point(11, 46);
            this.lblCodeResult.Name = "lblCodeResult";
            this.lblCodeResult.Size = new System.Drawing.Size(47, 20);
            this.lblCodeResult.TabIndex = 118;
            this.lblCodeResult.Text = "Code";
            // 
            // pnlVerificationProcess
            // 
            this.pnlVerificationProcess.BackColor = System.Drawing.Color.White;
            this.pnlVerificationProcess.Controls.Add(this.tableLayoutPanelProcess);
            this.pnlVerificationProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVerificationProcess.IsTitleHatchStyle = false;
            this.pnlVerificationProcess.Location = new System.Drawing.Point(11, 442);
            this.pnlVerificationProcess.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.pnlVerificationProcess.Name = "pnlVerificationProcess";
            this.pnlVerificationProcess.Radious = 15;
            this.pnlVerificationProcess.Size = new System.Drawing.Size(398, 416);
            this.pnlVerificationProcess.TabIndex = 51;
            this.pnlVerificationProcess.TabStop = false;
            this.pnlVerificationProcess.Text = "Verification progress";
            this.pnlVerificationProcess.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(230)))));
            this.pnlVerificationProcess.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlVerificationProcess.TitleForeColor = System.Drawing.Color.White;
            this.pnlVerificationProcess.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            this.pnlVerificationProcess.TitleHeight = 30;
            // 
            // tableLayoutPanelProcess
            // 
            this.tableLayoutPanelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelProcess.ColumnCount = 1;
            this.tableLayoutPanelProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelProcess.Controls.Add(this.pnlTotalChecked, 0, 1);
            this.tableLayoutPanelProcess.Controls.Add(this.prBarCheckPassed, 0, 0);
            this.tableLayoutPanelProcess.Controls.Add(this.pnlCheckFailed, 0, 3);
            this.tableLayoutPanelProcess.Controls.Add(this.pnlCheckPassed, 0, 2);
            this.tableLayoutPanelProcess.Location = new System.Drawing.Point(14, 41);
            this.tableLayoutPanelProcess.Name = "tableLayoutPanelProcess";
            this.tableLayoutPanelProcess.RowCount = 4;
            this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelProcess.Size = new System.Drawing.Size(370, 369);
            this.tableLayoutPanelProcess.TabIndex = 108;
            // 
            // pnlTotalChecked
            // 
            this.pnlTotalChecked._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlTotalChecked._BorderRadius = 10;
            this.pnlTotalChecked._BorderSize = 1;
            this.pnlTotalChecked._Corner = 0F;
            this.pnlTotalChecked._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlTotalChecked._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlTotalChecked._GradientPanel = false;
            this.pnlTotalChecked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlTotalChecked.Controls.Add(this.lblTotalCheckedValue);
            this.pnlTotalChecked.Controls.Add(this.lblTotalChecked);
            this.pnlTotalChecked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalChecked.Location = new System.Drawing.Point(3, 150);
            this.pnlTotalChecked.Name = "pnlTotalChecked";
            this.pnlTotalChecked.Size = new System.Drawing.Size(364, 67);
            this.pnlTotalChecked.TabIndex = 107;
            // 
            // lblTotalCheckedValue
            // 
            this.lblTotalCheckedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalCheckedValue.AutoSize = true;
            this.lblTotalCheckedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCheckedValue.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalCheckedValue.Location = new System.Drawing.Point(13, 28);
            this.lblTotalCheckedValue.Name = "lblTotalCheckedValue";
            this.lblTotalCheckedValue.Size = new System.Drawing.Size(30, 31);
            this.lblTotalCheckedValue.TabIndex = 31;
            this.lblTotalCheckedValue.Text = "0";
            this.lblTotalCheckedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalChecked
            // 
            this.lblTotalChecked.AutoSize = true;
            this.lblTotalChecked.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalChecked.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalChecked.Location = new System.Drawing.Point(13, 6);
            this.lblTotalChecked.Name = "lblTotalChecked";
            this.lblTotalChecked.Size = new System.Drawing.Size(108, 20);
            this.lblTotalChecked.TabIndex = 30;
            this.lblTotalChecked.Text = "Total checked";
            // 
            // prBarCheckPassed
            // 
            this.prBarCheckPassed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.prBarCheckPassed.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.prBarCheckPassed.AnimationSpeed = 500;
            this.prBarCheckPassed.BackColor = System.Drawing.Color.White;
            this.prBarCheckPassed.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prBarCheckPassed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.prBarCheckPassed.InnerColor = System.Drawing.Color.White;
            this.prBarCheckPassed.InnerMargin = -5;
            this.prBarCheckPassed.InnerWidth = -1;
            this.prBarCheckPassed.Location = new System.Drawing.Point(117, 6);
            this.prBarCheckPassed.MarqueeAnimationSpeed = 2000;
            this.prBarCheckPassed.Maximum = 50000000;
            this.prBarCheckPassed.Name = "prBarCheckPassed";
            this.prBarCheckPassed.OuterColor = System.Drawing.Color.Gray;
            this.prBarCheckPassed.OuterMargin = -10;
            this.prBarCheckPassed.OuterWidth = 10;
            this.prBarCheckPassed.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(199)))), ((int)(((byte)(82)))));
            this.prBarCheckPassed.ProgressWidth = 15;
            this.prBarCheckPassed.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prBarCheckPassed.Size = new System.Drawing.Size(135, 135);
            this.prBarCheckPassed.StartAngle = 270;
            this.prBarCheckPassed.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.prBarCheckPassed.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.prBarCheckPassed.SubscriptText = "";
            this.prBarCheckPassed.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.prBarCheckPassed.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.prBarCheckPassed.SuperscriptText = "";
            this.prBarCheckPassed.TabIndex = 54;
            this.prBarCheckPassed.Text = "0%";
            this.prBarCheckPassed.TextMargin = new System.Windows.Forms.Padding(0);
            this.prBarCheckPassed.Value = 25000000;
            // 
            // pnlCheckFailed
            // 
            this.pnlCheckFailed._BorderColor = System.Drawing.Color.Red;
            this.pnlCheckFailed._BorderRadius = 10;
            this.pnlCheckFailed._BorderSize = 1;
            this.pnlCheckFailed._Corner = 0F;
            this.pnlCheckFailed._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlCheckFailed._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlCheckFailed._GradientPanel = false;
            this.pnlCheckFailed.BackColor = System.Drawing.Color.Red;
            this.pnlCheckFailed.Controls.Add(this.lblFailed);
            this.pnlCheckFailed.Controls.Add(this.lblCheckResultFailedValue);
            this.pnlCheckFailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCheckFailed.Location = new System.Drawing.Point(3, 296);
            this.pnlCheckFailed.Name = "pnlCheckFailed";
            this.pnlCheckFailed.Size = new System.Drawing.Size(364, 70);
            this.pnlCheckFailed.TabIndex = 107;
            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailed.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblFailed.Location = new System.Drawing.Point(13, 6);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(52, 20);
            this.lblFailed.TabIndex = 30;
            this.lblFailed.Text = "Failed";
            // 
            // lblCheckResultFailedValue
            // 
            this.lblCheckResultFailedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckResultFailedValue.AutoSize = true;
            this.lblCheckResultFailedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckResultFailedValue.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCheckResultFailedValue.Location = new System.Drawing.Point(13, 28);
            this.lblCheckResultFailedValue.Name = "lblCheckResultFailedValue";
            this.lblCheckResultFailedValue.Size = new System.Drawing.Size(30, 31);
            this.lblCheckResultFailedValue.TabIndex = 29;
            this.lblCheckResultFailedValue.Text = "0";
            this.lblCheckResultFailedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCheckPassed
            // 
            this.pnlCheckPassed._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(199)))), ((int)(((byte)(82)))));
            this.pnlCheckPassed._BorderRadius = 10;
            this.pnlCheckPassed._BorderSize = 1;
            this.pnlCheckPassed._Corner = 0F;
            this.pnlCheckPassed._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlCheckPassed._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlCheckPassed._GradientPanel = false;
            this.pnlCheckPassed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(199)))), ((int)(((byte)(82)))));
            this.pnlCheckPassed.Controls.Add(this.lblCheckResultPassedValue);
            this.pnlCheckPassed.Controls.Add(this.lblPassed);
            this.pnlCheckPassed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCheckPassed.Location = new System.Drawing.Point(3, 223);
            this.pnlCheckPassed.Name = "pnlCheckPassed";
            this.pnlCheckPassed.Size = new System.Drawing.Size(364, 67);
            this.pnlCheckPassed.TabIndex = 107;
            // 
            // lblCheckResultPassedValue
            // 
            this.lblCheckResultPassedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckResultPassedValue.AutoSize = true;
            this.lblCheckResultPassedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckResultPassedValue.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCheckResultPassedValue.Location = new System.Drawing.Point(13, 28);
            this.lblCheckResultPassedValue.Name = "lblCheckResultPassedValue";
            this.lblCheckResultPassedValue.Size = new System.Drawing.Size(30, 31);
            this.lblCheckResultPassedValue.TabIndex = 31;
            this.lblCheckResultPassedValue.Text = "0";
            this.lblCheckResultPassedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPassed
            // 
            this.lblPassed.AutoSize = true;
            this.lblPassed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassed.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblPassed.Location = new System.Drawing.Point(13, 6);
            this.lblPassed.Name = "lblPassed";
            this.lblPassed.Size = new System.Drawing.Size(62, 20);
            this.lblPassed.TabIndex = 32;
            this.lblPassed.Text = "Passed";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Controls.Add(this.tableLayoutPanelPrintedState);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(421, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(989, 861);
            this.panel2.TabIndex = 55;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnlCheckedResult, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlDatabase, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 117);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6, 0, 10, 2);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(989, 744);
            this.tableLayoutPanel1.TabIndex = 111;
            // 
            // pnlCheckedResult
            // 
            this.pnlCheckedResult._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlCheckedResult._BorderRadius = 10;
            this.pnlCheckedResult._BorderSize = 1;
            this.pnlCheckedResult._Corner = 0F;
            this.pnlCheckedResult._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlCheckedResult._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlCheckedResult._GradientPanel = false;
            this.pnlCheckedResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheckedResult.BackColor = System.Drawing.Color.White;
            this.pnlCheckedResult.Controls.Add(this.picCheckedResultLoading);
            this.pnlCheckedResult.Controls.Add(this.btnNull);
            this.pnlCheckedResult.Controls.Add(this.btnDuplicate);
            this.pnlCheckedResult.Controls.Add(this.btnInvalid);
            this.pnlCheckedResult.Controls.Add(this.btnValid);
            this.pnlCheckedResult.Controls.Add(this.dgvCheckedResult);
            this.pnlCheckedResult.Controls.Add(this.lblCheckedResult);
            this.pnlCheckedResult.Location = new System.Drawing.Point(9, 381);
            this.pnlCheckedResult.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnlCheckedResult.Name = "pnlCheckedResult";
            this.pnlCheckedResult.Size = new System.Drawing.Size(967, 358);
            this.pnlCheckedResult.TabIndex = 51;
            // 
            // picCheckedResultLoading
            // 
            this.picCheckedResultLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picCheckedResultLoading.Image = global::BarcodeVerificationSystem.Properties.Resources.icon_loading_2681;
            this.picCheckedResultLoading.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picCheckedResultLoading.Location = new System.Drawing.Point(437, 148);
            this.picCheckedResultLoading.Name = "picCheckedResultLoading";
            this.picCheckedResultLoading.Size = new System.Drawing.Size(92, 62);
            this.picCheckedResultLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCheckedResultLoading.TabIndex = 124;
            this.picCheckedResultLoading.TabStop = false;
            this.picCheckedResultLoading.Visible = false;
            // 
            // btnNull
            // 
            this.btnNull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNull.BackColor = System.Drawing.Color.PowderBlue;
            this.btnNull.FlatAppearance.BorderSize = 0;
            this.btnNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNull.Location = new System.Drawing.Point(924, 12);
            this.btnNull.Name = "btnNull";
            this.btnNull.Size = new System.Drawing.Size(20, 20);
            this.btnNull.TabIndex = 123;
            this.btnNull.UseVisualStyleBackColor = false;
            this.btnNull.Visible = false;
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuplicate.BackColor = System.Drawing.Color.Gold;
            this.btnDuplicate.FlatAppearance.BorderSize = 0;
            this.btnDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuplicate.Location = new System.Drawing.Point(889, 12);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(20, 20);
            this.btnDuplicate.TabIndex = 123;
            this.btnDuplicate.UseVisualStyleBackColor = false;
            this.btnDuplicate.Visible = false;
            // 
            // btnInvalid
            // 
            this.btnInvalid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInvalid.BackColor = System.Drawing.Color.Red;
            this.btnInvalid.FlatAppearance.BorderSize = 0;
            this.btnInvalid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvalid.Location = new System.Drawing.Point(854, 12);
            this.btnInvalid.Name = "btnInvalid";
            this.btnInvalid.Size = new System.Drawing.Size(20, 20);
            this.btnInvalid.TabIndex = 123;
            this.btnInvalid.UseVisualStyleBackColor = false;
            this.btnInvalid.Visible = false;
            // 
            // btnValid
            // 
            this.btnValid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValid.BackColor = System.Drawing.Color.Green;
            this.btnValid.FlatAppearance.BorderSize = 0;
            this.btnValid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValid.Location = new System.Drawing.Point(818, 12);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(20, 20);
            this.btnValid.TabIndex = 123;
            this.btnValid.UseVisualStyleBackColor = false;
            this.btnValid.Visible = false;
            // 
            // dgvCheckedResult
            // 
            this.dgvCheckedResult.AllowUserToAddRows = false;
            this.dgvCheckedResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCheckedResult.AlterRowBackColor = System.Drawing.Color.White;
            this.dgvCheckedResult.AlterRowForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCheckedResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCheckedResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvCheckedResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCheckedResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCheckedResult.ColumnHeaderBackColor = System.Drawing.Color.White;
            this.dgvCheckedResult.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgvCheckedResult.ColumnHeaderForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.ColumnHeaderHeight = 4;
            this.dgvCheckedResult.ColumnHeaderPadding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.dgvCheckedResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCheckedResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCheckedResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCheckedResult.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCheckedResult.EnableHeadersVisualStyles = false;
            this.dgvCheckedResult.GridLineColor = System.Drawing.Color.LightBlue;
            this.dgvCheckedResult.HeaderBorder = true;
            this.dgvCheckedResult.HeaderBorderStyle = DesignUI.CuzUI.CuzDataGridView.CustomBorderStyle.SingleHorizontal;
            this.dgvCheckedResult.Location = new System.Drawing.Point(24, 41);
            this.dgvCheckedResult.MultiSelect = false;
            this.dgvCheckedResult.Name = "dgvCheckedResult";
            this.dgvCheckedResult.ReadOnly = true;
            this.dgvCheckedResult.RowBackColor = System.Drawing.Color.White;
            this.dgvCheckedResult.RowBorder = true;
            this.dgvCheckedResult.RowBorderStyle = DesignUI.CuzUI.CuzDataGridView.CustomBorderStyle.SingleHorizontal;
            this.dgvCheckedResult.RowFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgvCheckedResult.RowForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCheckedResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCheckedResult.RowHeadersVisible = false;
            this.dgvCheckedResult.RowHeight = 35;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCheckedResult.RowSelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvCheckedResult.RowSelectionForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvCheckedResult.RowTemplate.Height = 35;
            this.dgvCheckedResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheckedResult.Size = new System.Drawing.Size(929, 296);
            this.dgvCheckedResult.TabIndex = 112;
            // 
            // lblCheckedResult
            // 
            this.lblCheckedResult.AutoSize = true;
            this.lblCheckedResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckedResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblCheckedResult.Location = new System.Drawing.Point(21, 12);
            this.lblCheckedResult.Name = "lblCheckedResult";
            this.lblCheckedResult.Size = new System.Drawing.Size(129, 20);
            this.lblCheckedResult.TabIndex = 109;
            this.lblCheckedResult.Text = "Checked result";
            // 
            // pnlDatabase
            // 
            this.pnlDatabase._BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlDatabase._BorderRadius = 10;
            this.pnlDatabase._BorderSize = 1;
            this.pnlDatabase._Corner = 0F;
            this.pnlDatabase._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlDatabase._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlDatabase._GradientPanel = false;
            this.pnlDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDatabase.BackColor = System.Drawing.Color.White;
            this.pnlDatabase.Controls.Add(this.picDatabaseLoading);
            this.pnlDatabase.Controls.Add(this.dgvDatabase);
            this.pnlDatabase.Controls.Add(this.lblDatabase);
            this.pnlDatabase.Location = new System.Drawing.Point(9, 10);
            this.pnlDatabase.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnlDatabase.Name = "pnlDatabase";
            this.pnlDatabase.Size = new System.Drawing.Size(967, 358);
            this.pnlDatabase.TabIndex = 50;
            // 
            // picDatabaseLoading
            // 
            this.picDatabaseLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picDatabaseLoading.Image = global::BarcodeVerificationSystem.Properties.Resources.icon_loading_2681;
            this.picDatabaseLoading.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picDatabaseLoading.Location = new System.Drawing.Point(437, 148);
            this.picDatabaseLoading.Name = "picDatabaseLoading";
            this.picDatabaseLoading.Size = new System.Drawing.Size(92, 62);
            this.picDatabaseLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDatabaseLoading.TabIndex = 112;
            this.picDatabaseLoading.TabStop = false;
            this.picDatabaseLoading.Visible = false;
            // 
            // dgvDatabase
            // 
            this.dgvDatabase.AllowUserToAddRows = false;
            this.dgvDatabase.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDatabase.AlterRowBackColor = System.Drawing.Color.White;
            this.dgvDatabase.AlterRowForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatabase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatabase.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDatabase.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDatabase.ColumnHeaderBackColor = System.Drawing.Color.White;
            this.dgvDatabase.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgvDatabase.ColumnHeaderForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.ColumnHeaderHeight = 4;
            this.dgvDatabase.ColumnHeaderPadding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.dgvDatabase.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatabase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatabase.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDatabase.EnableHeadersVisualStyles = false;
            this.dgvDatabase.GridLineColor = System.Drawing.Color.LightBlue;
            this.dgvDatabase.HeaderBorder = true;
            this.dgvDatabase.HeaderBorderStyle = DesignUI.CuzUI.CuzDataGridView.CustomBorderStyle.SingleHorizontal;
            this.dgvDatabase.Location = new System.Drawing.Point(24, 41);
            this.dgvDatabase.MultiSelect = false;
            this.dgvDatabase.Name = "dgvDatabase";
            this.dgvDatabase.ReadOnly = true;
            this.dgvDatabase.RowBackColor = System.Drawing.Color.White;
            this.dgvDatabase.RowBorder = true;
            this.dgvDatabase.RowBorderStyle = DesignUI.CuzUI.CuzDataGridView.CustomBorderStyle.SingleHorizontal;
            this.dgvDatabase.RowFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgvDatabase.RowForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatabase.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDatabase.RowHeadersVisible = false;
            this.dgvDatabase.RowHeight = 35;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDatabase.RowSelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvDatabase.RowSelectionForeColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvDatabase.RowTemplate.Height = 35;
            this.dgvDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatabase.Size = new System.Drawing.Size(929, 296);
            this.dgvDatabase.TabIndex = 111;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblDatabase.Location = new System.Drawing.Point(21, 11);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(87, 20);
            this.lblDatabase.TabIndex = 108;
            this.lblDatabase.Text = "Database";
            // 
            // tableLayoutPanelPrintedState
            // 
            this.tableLayoutPanelPrintedState.ColumnCount = 3;
            this.tableLayoutPanelPrintedState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPrintedState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPrintedState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPrintedState.Controls.Add(this.pnlPrintedState3, 2, 0);
            this.tableLayoutPanelPrintedState.Controls.Add(this.pnlPrintedState2, 1, 0);
            this.tableLayoutPanelPrintedState.Controls.Add(this.pnlPrintedState1, 0, 0);
            this.tableLayoutPanelPrintedState.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelPrintedState.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelPrintedState.Name = "tableLayoutPanelPrintedState";
            this.tableLayoutPanelPrintedState.RowCount = 1;
            this.tableLayoutPanelPrintedState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPrintedState.Size = new System.Drawing.Size(989, 117);
            this.tableLayoutPanelPrintedState.TabIndex = 110;
            // 
            // pnlPrintedState3
            // 
            this.pnlPrintedState3.Controls.Add(this.pnlPrintedCode);
            this.pnlPrintedState3.Controls.Add(this.cuzPanel4);
            this.pnlPrintedState3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrintedState3.Location = new System.Drawing.Point(661, 3);
            this.pnlPrintedState3.Name = "pnlPrintedState3";
            this.pnlPrintedState3.Size = new System.Drawing.Size(325, 111);
            this.pnlPrintedState3.TabIndex = 2;
            // 
            // pnlPrintedCode
            // 
            this.pnlPrintedCode._BorderColor = System.Drawing.Color.Silver;
            this.pnlPrintedCode._BorderRadius = 10;
            this.pnlPrintedCode._BorderSize = 1;
            this.pnlPrintedCode._Corner = 0F;
            this.pnlPrintedCode._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlPrintedCode._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlPrintedCode._GradientPanel = false;
            this.pnlPrintedCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPrintedCode.BackColor = System.Drawing.Color.White;
            this.pnlPrintedCode.Controls.Add(this.lblPrintedCodeValue);
            this.pnlPrintedCode.Controls.Add(this.lblPrintedCode);
            this.pnlPrintedCode.Location = new System.Drawing.Point(12, 11);
            this.pnlPrintedCode.Name = "pnlPrintedCode";
            this.pnlPrintedCode.Size = new System.Drawing.Size(303, 90);
            this.pnlPrintedCode.TabIndex = 0;
            // 
            // lblPrintedCodeValue
            // 
            this.lblPrintedCodeValue.AutoSize = true;
            this.lblPrintedCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintedCodeValue.ForeColor = System.Drawing.Color.Black;
            this.lblPrintedCodeValue.Location = new System.Drawing.Point(17, 39);
            this.lblPrintedCodeValue.Name = "lblPrintedCodeValue";
            this.lblPrintedCodeValue.Size = new System.Drawing.Size(35, 37);
            this.lblPrintedCodeValue.TabIndex = 2;
            this.lblPrintedCodeValue.Text = "0";
            // 
            // lblPrintedCode
            // 
            this.lblPrintedCode.AutoSize = true;
            this.lblPrintedCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintedCode.ForeColor = System.Drawing.Color.Gray;
            this.lblPrintedCode.Location = new System.Drawing.Point(11, 10);
            this.lblPrintedCode.Name = "lblPrintedCode";
            this.lblPrintedCode.Size = new System.Drawing.Size(98, 20);
            this.lblPrintedCode.TabIndex = 2;
            this.lblPrintedCode.Text = "Printed code";
            // 
            // cuzPanel4
            // 
            this.cuzPanel4._BorderColor = System.Drawing.Color.Silver;
            this.cuzPanel4._BorderRadius = 10;
            this.cuzPanel4._BorderSize = 0;
            this.cuzPanel4._Corner = 0F;
            this.cuzPanel4._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cuzPanel4._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.cuzPanel4._GradientPanel = false;
            this.cuzPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cuzPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cuzPanel4.Location = new System.Drawing.Point(5, 17);
            this.cuzPanel4.Name = "cuzPanel4";
            this.cuzPanel4.Size = new System.Drawing.Size(303, 90);
            this.cuzPanel4.TabIndex = 0;
            // 
            // pnlPrintedState2
            // 
            this.pnlPrintedState2.Controls.Add(this.pnlReveied);
            this.pnlPrintedState2.Controls.Add(this.cuzPanel2);
            this.pnlPrintedState2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrintedState2.Location = new System.Drawing.Point(332, 3);
            this.pnlPrintedState2.Name = "pnlPrintedState2";
            this.pnlPrintedState2.Size = new System.Drawing.Size(323, 111);
            this.pnlPrintedState2.TabIndex = 1;
            // 
            // pnlReveied
            // 
            this.pnlReveied._BorderColor = System.Drawing.Color.Silver;
            this.pnlReveied._BorderRadius = 10;
            this.pnlReveied._BorderSize = 1;
            this.pnlReveied._Corner = 0F;
            this.pnlReveied._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlReveied._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlReveied._GradientPanel = false;
            this.pnlReveied.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReveied.BackColor = System.Drawing.Color.White;
            this.pnlReveied.Controls.Add(this.lblReceived);
            this.pnlReveied.Controls.Add(this.lblReceivedValue);
            this.pnlReveied.Location = new System.Drawing.Point(15, 11);
            this.pnlReveied.Name = "pnlReveied";
            this.pnlReveied.Size = new System.Drawing.Size(302, 90);
            this.pnlReveied.TabIndex = 0;
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceived.ForeColor = System.Drawing.Color.Gray;
            this.lblReceived.Location = new System.Drawing.Point(29, 12);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(75, 20);
            this.lblReceived.TabIndex = 2;
            this.lblReceived.Text = "Received";
            // 
            // lblReceivedValue
            // 
            this.lblReceivedValue.AutoSize = true;
            this.lblReceivedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivedValue.ForeColor = System.Drawing.Color.Black;
            this.lblReceivedValue.Location = new System.Drawing.Point(34, 41);
            this.lblReceivedValue.Name = "lblReceivedValue";
            this.lblReceivedValue.Size = new System.Drawing.Size(35, 37);
            this.lblReceivedValue.TabIndex = 2;
            this.lblReceivedValue.Text = "0";
            // 
            // cuzPanel2
            // 
            this.cuzPanel2._BorderColor = System.Drawing.Color.Silver;
            this.cuzPanel2._BorderRadius = 10;
            this.cuzPanel2._BorderSize = 0;
            this.cuzPanel2._Corner = 0F;
            this.cuzPanel2._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cuzPanel2._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.cuzPanel2._GradientPanel = false;
            this.cuzPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cuzPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cuzPanel2.Location = new System.Drawing.Point(8, 17);
            this.cuzPanel2.Name = "cuzPanel2";
            this.cuzPanel2.Size = new System.Drawing.Size(302, 90);
            this.cuzPanel2.TabIndex = 0;
            // 
            // pnlPrintedState1
            // 
            this.pnlPrintedState1.Controls.Add(this.pnlSentData);
            this.pnlPrintedState1.Controls.Add(this.cuzPanel10);
            this.pnlPrintedState1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrintedState1.Location = new System.Drawing.Point(3, 3);
            this.pnlPrintedState1.Name = "pnlPrintedState1";
            this.pnlPrintedState1.Size = new System.Drawing.Size(323, 111);
            this.pnlPrintedState1.TabIndex = 0;
            // 
            // pnlSentData
            // 
            this.pnlSentData._BorderColor = System.Drawing.Color.Silver;
            this.pnlSentData._BorderRadius = 10;
            this.pnlSentData._BorderSize = 1;
            this.pnlSentData._Corner = 0F;
            this.pnlSentData._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlSentData._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.pnlSentData._GradientPanel = false;
            this.pnlSentData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSentData.BackColor = System.Drawing.Color.White;
            this.pnlSentData.Controls.Add(this.lblSentDataValue);
            this.pnlSentData.Controls.Add(this.lblSentData);
            this.pnlSentData.Location = new System.Drawing.Point(15, 11);
            this.pnlSentData.Name = "pnlSentData";
            this.pnlSentData.Size = new System.Drawing.Size(302, 90);
            this.pnlSentData.TabIndex = 0;
            // 
            // lblSentDataValue
            // 
            this.lblSentDataValue.AutoSize = true;
            this.lblSentDataValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSentDataValue.ForeColor = System.Drawing.Color.Black;
            this.lblSentDataValue.Location = new System.Drawing.Point(27, 39);
            this.lblSentDataValue.Name = "lblSentDataValue";
            this.lblSentDataValue.Size = new System.Drawing.Size(35, 37);
            this.lblSentDataValue.TabIndex = 3;
            this.lblSentDataValue.Text = "0";
            // 
            // lblSentData
            // 
            this.lblSentData.AutoSize = true;
            this.lblSentData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSentData.ForeColor = System.Drawing.Color.Gray;
            this.lblSentData.Location = new System.Drawing.Point(21, 10);
            this.lblSentData.Name = "lblSentData";
            this.lblSentData.Size = new System.Drawing.Size(79, 20);
            this.lblSentData.TabIndex = 4;
            this.lblSentData.Text = "Sent data";
            // 
            // cuzPanel10
            // 
            this.cuzPanel10._BorderColor = System.Drawing.Color.Silver;
            this.cuzPanel10._BorderRadius = 10;
            this.cuzPanel10._BorderSize = 0;
            this.cuzPanel10._Corner = 0F;
            this.cuzPanel10._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cuzPanel10._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.cuzPanel10._GradientPanel = false;
            this.cuzPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cuzPanel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cuzPanel10.Location = new System.Drawing.Point(9, 17);
            this.cuzPanel10.Name = "cuzPanel10";
            this.cuzPanel10.Size = new System.Drawing.Size(302, 90);
            this.cuzPanel10.TabIndex = 0;
            // 
            // pnlMenu
            // 
            this.pnlMenu._BorderColor = System.Drawing.Color.Silver;
            this.pnlMenu._BorderRadius = 0;
            this.pnlMenu._BorderSize = 0;
            this.pnlMenu._Corner = 90F;
            this.pnlMenu._FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlMenu._FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlMenu._GradientPanel = false;
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.pnlMenu.Controls.Add(this.btnVirtualStop);
            this.pnlMenu.Controls.Add(this.btnVirtualStart);
            this.pnlMenu.Controls.Add(this.btnExport);
            this.pnlMenu.Controls.Add(this.btnSettings);
            this.pnlMenu.Controls.Add(this.btnExit);
            this.pnlMenu.Controls.Add(this.btnHistory);
            this.pnlMenu.Controls.Add(this.btnAccount);
            this.pnlMenu.Controls.Add(this.btnDatabase);
            this.pnlMenu.Controls.Add(this.btnJob);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(82, 900);
            this.pnlMenu.TabIndex = 24;
            // 
            // btnVirtualStop
            // 
            this.btnVirtualStop.BackColor = System.Drawing.Color.Transparent;
            this.btnVirtualStop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVirtualStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStop.FlatAppearance.BorderSize = 0;
            this.btnVirtualStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVirtualStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVirtualStop.ForeColor = System.Drawing.Color.White;
            this.btnVirtualStop.IconChar = FontAwesome.Sharp.IconChar.Stop;
            this.btnVirtualStop.IconColor = System.Drawing.Color.White;
            this.btnVirtualStop.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVirtualStop.IconSize = 30;
            this.btnVirtualStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVirtualStop.Location = new System.Drawing.Point(0, 432);
            this.btnVirtualStop.Name = "btnVirtualStop";
            this.btnVirtualStop.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnVirtualStop.Size = new System.Drawing.Size(82, 62);
            this.btnVirtualStop.TabIndex = 64;
            this.btnVirtualStop.Text = "Stop";
            this.btnVirtualStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVirtualStop.UseVisualStyleBackColor = false;
            this.btnVirtualStop.Visible = false;
            // 
            // btnVirtualStart
            // 
            this.btnVirtualStart.BackColor = System.Drawing.Color.Transparent;
            this.btnVirtualStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVirtualStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStart.FlatAppearance.BorderSize = 0;
            this.btnVirtualStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnVirtualStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVirtualStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVirtualStart.ForeColor = System.Drawing.Color.White;
            this.btnVirtualStart.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.btnVirtualStart.IconColor = System.Drawing.Color.White;
            this.btnVirtualStart.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVirtualStart.IconSize = 30;
            this.btnVirtualStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVirtualStart.Location = new System.Drawing.Point(0, 370);
            this.btnVirtualStart.Name = "btnVirtualStart";
            this.btnVirtualStart.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnVirtualStart.Size = new System.Drawing.Size(82, 62);
            this.btnVirtualStart.TabIndex = 63;
            this.btnVirtualStart.Text = "Start";
            this.btnVirtualStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVirtualStart.UseVisualStyleBackColor = false;
            this.btnVirtualStart.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.IconChar = FontAwesome.Sharp.IconChar.ExternalLink;
            this.btnExport.IconColor = System.Drawing.Color.White;
            this.btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExport.IconSize = 30;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExport.Location = new System.Drawing.Point(0, 300);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnExport.Size = new System.Drawing.Size(82, 70);
            this.btnExport.TabIndex = 62;
            this.btnExport.Text = "Xuất log";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.btnSettings.IconColor = System.Drawing.Color.White;
            this.btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSettings.IconSize = 30;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSettings.Location = new System.Drawing.Point(0, 760);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnSettings.Size = new System.Drawing.Size(82, 70);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.IconSize = 30;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(0, 830);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnExit.Size = new System.Drawing.Size(82, 70);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.IconChar = FontAwesome.Sharp.IconChar.ListSquares;
            this.btnHistory.IconColor = System.Drawing.Color.White;
            this.btnHistory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHistory.IconSize = 30;
            this.btnHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHistory.Location = new System.Drawing.Point(0, 230);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnHistory.Size = new System.Drawing.Size(82, 70);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "History";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistory.UseVisualStyleBackColor = false;
            // 
            // btnAccount
            // 
            this.btnAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnAccount.FlatAppearance.BorderSize = 0;
            this.btnAccount.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccount.ForeColor = System.Drawing.Color.White;
            this.btnAccount.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.btnAccount.IconColor = System.Drawing.Color.White;
            this.btnAccount.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAccount.IconSize = 30;
            this.btnAccount.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAccount.Location = new System.Drawing.Point(0, 160);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnAccount.Size = new System.Drawing.Size(82, 70);
            this.btnAccount.TabIndex = 2;
            this.btnAccount.Text = "Account";
            this.btnAccount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAccount.UseVisualStyleBackColor = false;
            // 
            // btnDatabase
            // 
            this.btnDatabase.BackColor = System.Drawing.Color.Transparent;
            this.btnDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDatabase.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnDatabase.FlatAppearance.BorderSize = 0;
            this.btnDatabase.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnDatabase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnDatabase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatabase.ForeColor = System.Drawing.Color.White;
            this.btnDatabase.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.btnDatabase.IconColor = System.Drawing.Color.White;
            this.btnDatabase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDatabase.IconSize = 30;
            this.btnDatabase.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDatabase.Location = new System.Drawing.Point(0, 90);
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnDatabase.Size = new System.Drawing.Size(82, 70);
            this.btnDatabase.TabIndex = 1;
            this.btnDatabase.Text = "Database";
            this.btnDatabase.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDatabase.UseVisualStyleBackColor = false;
            // 
            // btnJob
            // 
            this.btnJob.BackColor = System.Drawing.Color.Transparent;
            this.btnJob.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnJob.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnJob.FlatAppearance.BorderSize = 0;
            this.btnJob.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnJob.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnJob.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(230)))));
            this.btnJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJob.ForeColor = System.Drawing.Color.White;
            this.btnJob.IconChar = FontAwesome.Sharp.IconChar.Briefcase;
            this.btnJob.IconColor = System.Drawing.Color.White;
            this.btnJob.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnJob.IconSize = 30;
            this.btnJob.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnJob.Location = new System.Drawing.Point(0, 20);
            this.btnJob.Name = "btnJob";
            this.btnJob.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnJob.Size = new System.Drawing.Size(82, 70);
            this.btnJob.TabIndex = 0;
            this.btnJob.Text = "Jobs";
            this.btnJob.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJob.UseVisualStyleBackColor = false;
            // 
            // cuzDropdownManageAccount
            // 
            this.cuzDropdownManageAccount.IsMainMenu = false;
            this.cuzDropdownManageAccount.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnManage,
            this.mnChangePassword,
            this.mnLogOut});
            this.cuzDropdownManageAccount.MenuItemHeight = 25;
            this.cuzDropdownManageAccount.MenuItemTextColor = System.Drawing.Color.Empty;
            this.cuzDropdownManageAccount.Name = "cuzDropdownManageAccount";
            this.cuzDropdownManageAccount.PrimaryColor = System.Drawing.Color.Empty;
            this.cuzDropdownManageAccount.Size = new System.Drawing.Size(169, 70);
            // 
            // mnManage
            // 
            this.mnManage.Name = "mnManage";
            this.mnManage.Size = new System.Drawing.Size(168, 22);
            this.mnManage.Text = "Manage account";
            // 
            // mnChangePassword
            // 
            this.mnChangePassword.Name = "mnChangePassword";
            this.mnChangePassword.Size = new System.Drawing.Size(168, 22);
            this.mnChangePassword.Text = "Change password";
            // 
            // mnLogOut
            // 
            this.mnLogOut.Name = "mnLogOut";
            this.mnLogOut.Size = new System.Drawing.Size(168, 22);
            this.mnLogOut.Text = "Log out";
            // 
            // cvsDisplayImage
            // 
            this.cvsDisplayImage.Location = new System.Drawing.Point(0, 23);
            this.cvsDisplayImage.Margin = new System.Windows.Forms.Padding(0);
            this.cvsDisplayImage.Name = "cvsDisplayImage";
            this.cvsDisplayImage.Size = new System.Drawing.Size(374, 118);
            this.cvsDisplayImage.TabIndex = 4;
            // 
            // cvsDisplayImg
            // 
            this.cvsDisplayImg.Location = new System.Drawing.Point(0, 4);
            this.cvsDisplayImg.Margin = new System.Windows.Forms.Padding(0);
            this.cvsDisplayImg.Name = "cvsDisplayImg";
            this.cvsDisplayImg.Size = new System.Drawing.Size(384, 135);
            this.cvsDisplayImg.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1800, 900);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlMenu);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "R-LINK";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlControllButton.ResumeLayout(false);
            this.tableLayoutControl.ResumeLayout(false);
            this.pnlJobInformation.ResumeLayout(false);
            this.pnlJobInformation.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanelCheckedResult.ResumeLayout(false);
            this.pnlPictureBox.ResumeLayout(false);
            this.pnlPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.pnlCurrentCheck.ResumeLayout(false);
            this.pnlCurrentCheck.PerformLayout();
            this.pnlVerificationProcess.ResumeLayout(false);
            this.tableLayoutPanelProcess.ResumeLayout(false);
            this.pnlTotalChecked.ResumeLayout(false);
            this.pnlTotalChecked.PerformLayout();
            this.pnlCheckFailed.ResumeLayout(false);
            this.pnlCheckFailed.PerformLayout();
            this.pnlCheckPassed.ResumeLayout(false);
            this.pnlCheckPassed.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlCheckedResult.ResumeLayout(false);
            this.pnlCheckedResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckedResultLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckedResult)).EndInit();
            this.pnlDatabase.ResumeLayout(false);
            this.pnlDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabase)).EndInit();
            this.tableLayoutPanelPrintedState.ResumeLayout(false);
            this.pnlPrintedState3.ResumeLayout(false);
            this.pnlPrintedCode.ResumeLayout(false);
            this.pnlPrintedCode.PerformLayout();
            this.pnlPrintedState2.ResumeLayout(false);
            this.pnlReveied.ResumeLayout(false);
            this.pnlReveied.PerformLayout();
            this.pnlPrintedState1.ResumeLayout(false);
            this.pnlSentData.ResumeLayout(false);
            this.pnlSentData.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.cuzDropdownManageAccount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCamera01;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusPrinter01;
        private System.Windows.Forms.ToolStripStatusLabel lblSensorControllerStatus;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripOperationStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDateTime;
        private DesignUI.CuzUI.CuzDropdownMenu cuzDropdownManageAccount;
        private System.Windows.Forms.ToolStripMenuItem mnManage;
        private System.Windows.Forms.ToolStripMenuItem mnChangePassword;
        private System.Windows.Forms.ToolStripMenuItem mnLogOut;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripVersion;
        private DesignUI.CuzUI.CuzPanel pnlMenu;
        private FontAwesome.Sharp.IconButton btnJob;
        private FontAwesome.Sharp.IconButton btnAccount;
        private FontAwesome.Sharp.IconButton btnDatabase;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton btnHistory;
        private FontAwesome.Sharp.IconButton btnSettings;
        private System.Windows.Forms.Panel panel1;
        private DesignUI.CuzUI.CuzPanel pnlControllButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutControl;
        private DesignUI.CuzUI.CuzButton btnTrigger;
        private DesignUI.CuzUI.CuzButton btnStop;
        private DesignUI.CuzUI.CuzButton btnStart;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPrintedState;
        private System.Windows.Forms.Panel pnlPrintedState3;
        private DesignUI.CuzUI.CuzPanel pnlPrintedCode;
        private System.Windows.Forms.Label lblPrintedCodeValue;
        private System.Windows.Forms.Label lblPrintedCode;
        private DesignUI.CuzUI.CuzPanel cuzPanel4;
        private System.Windows.Forms.Panel pnlPrintedState2;
        private DesignUI.CuzUI.CuzPanel pnlReveied;
        private DesignUI.CuzUI.CuzPanel cuzPanel2;
        private System.Windows.Forms.Panel pnlPrintedState1;
        private DesignUI.CuzUI.CuzPanel pnlSentData;
        private System.Windows.Forms.Label lblReceivedValue;
        private System.Windows.Forms.Label lblReceived;
        private DesignUI.CuzUI.CuzPanel cuzPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCheckedResult;
        private DesignUI.CuzUI.CuzPanel pnlPictureBox;
        private DesignUI.CuzUI.CuzPanel pnlPicture;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private DesignUI.CuzUI.CuzPanel pnlCheckedResult;
        private System.Windows.Forms.Label lblCheckedResult;
        private System.Windows.Forms.Label lblSentDataValue;
        private System.Windows.Forms.Label lblSentData;
        private FontAwesome.Sharp.IconButton btnVirtualStop;
        private FontAwesome.Sharp.IconButton btnVirtualStart;
        private FontAwesome.Sharp.IconButton btnExport;
        private DesignUI.CuzUI.RoundPanel pnlCurrentCheck;
        private System.Windows.Forms.Label lblStatusResult;
        private System.Windows.Forms.Label lblProcessingTime;
        private DesignUI.CuzUI.CuzTextBox txtStatusResult;
        private DesignUI.CuzUI.CuzTextBox txtProcessingTimeResult;
        private DesignUI.CuzUI.CuzTextBox txtCodeResult;
        private System.Windows.Forms.Label lblCodeResult;
        private DesignUI.CuzUI.RoundPanel pnlVerificationProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProcess;
        private DesignUI.CuzUI.CuzPanel pnlTotalChecked;
        private System.Windows.Forms.Label lblTotalCheckedValue;
        private System.Windows.Forms.Label lblTotalChecked;
        private CircularProgressBar.CircularProgressBar prBarCheckPassed;
        private DesignUI.CuzUI.CuzPanel pnlCheckFailed;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Label lblCheckResultFailedValue;
        private DesignUI.CuzUI.CuzPanel pnlCheckPassed;
        private System.Windows.Forms.Label lblCheckResultPassedValue;
        private System.Windows.Forms.Label lblPassed;
        private DesignUI.CuzUI.RoundPanel pnlJobInformation;
        private DesignUI.CuzUI.CuzTextBox txtJobType;
        private System.Windows.Forms.Label lblJobType;
        private DesignUI.CuzUI.CuzTextBox txtTemplatePrint;
        private DesignUI.CuzUI.CuzTextBox txtPODFormat;
        private DesignUI.CuzUI.CuzTextBox txtStaticText;
        private DesignUI.CuzUI.CuzTextBox txtCompareType;
        private DesignUI.CuzUI.CuzTextBox txtJobName;
        private System.Windows.Forms.Label lblTemplatePrint;
        private System.Windows.Forms.Label lblPODFormat;
        private System.Windows.Forms.Label lblStaticText;
        private System.Windows.Forms.Label lblCompareType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblJobName;
        private DesignUI.CuzUI.CuzDataGridView dgvCheckedResult;
        private System.Windows.Forms.Button btnNull;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnInvalid;
        private System.Windows.Forms.Button btnValid;
        private DesignUI.CuzUI.CuzPanel pnlDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.PictureBox picCheckedResultLoading;
        private System.Windows.Forms.PictureBox picDatabaseLoading;
        private DesignUI.CuzUI.CuzDataGridView dgvDatabase;
        private System.Windows.Forms.Button btnViewLog;
        private Cognex.InSight.Web.Controls.CvsDisplay cvsDisplayImage;
        private Cognex.InSight.Web.Controls.CvsDisplay cvsDisplayImg;
    }
}