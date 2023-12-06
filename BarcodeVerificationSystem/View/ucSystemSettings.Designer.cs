﻿namespace BarcodeVerificationSystem.View
{
    partial class ucSystemSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbLanguage = new System.Windows.Forms.GroupBox();
            this.cboLanguages = new System.Windows.Forms.ComboBox();
            this.grbImageExport = new System.Windows.Forms.GroupBox();
            this.btnBrowserImageExportPath = new System.Windows.Forms.Button();
            this.txtImageExportPath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.radEnableImageExport = new System.Windows.Forms.RadioButton();
            this.radDisableImageExport = new System.Windows.Forms.RadioButton();
            this.lblImageExportWarning = new System.Windows.Forms.Label();
            this.lblImageExportPath = new System.Windows.Forms.Label();
            this.grbOutput = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radEnableOutput = new System.Windows.Forms.RadioButton();
            this.radDisableOutput = new System.Windows.Forms.RadioButton();
            this.grbJobFormat = new System.Windows.Forms.GroupBox();
            this.btnDefaultFileNameJob = new System.Windows.Forms.Button();
            this.btnDefaultDatimeJob = new System.Windows.Forms.Button();
            this.txtJobFileName = new System.Windows.Forms.TextBox();
            this.txtJobDateTimeFormat = new System.Windows.Forms.TextBox();
            this.lblJobFileName = new System.Windows.Forms.Label();
            this.lblJobDatetimeFormat = new System.Windows.Forms.Label();
            this.lblCheckedResultPath = new System.Windows.Forms.Label();
            this.lblDataCheckedFileName = new System.Windows.Forms.Label();
            this.grbExportCheckedResult = new System.Windows.Forms.GroupBox();
            this.btnGenerateDataCheckedFileName = new System.Windows.Forms.Button();
            this.btnBrowserCheckedResultPath = new System.Windows.Forms.Button();
            this.txtDataCheckedFileName = new System.Windows.Forms.TextBox();
            this.txtCheckedResultPath = new System.Windows.Forms.TextBox();
            this.grbStopCondition = new System.Windows.Forms.GroupBox();
            this.tblpStopCondition = new System.Windows.Forms.TableLayoutPanel();
            this.radEnableTotalCode = new System.Windows.Forms.RadioButton();
            this.radEnableTotalPassed = new System.Windows.Forms.RadioButton();
            this.grbSentDataMethod = new System.Windows.Forms.GroupBox();
            this.btnDefaultFailedR = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radBasic = new System.Windows.Forms.RadioButton();
            this.radCompare = new System.Windows.Forms.RadioButton();
            this.txtFailedREdit = new System.Windows.Forms.TextBox();
            this.lblFailureEdit = new System.Windows.Forms.Label();
            this.lblPrintField = new System.Windows.Forms.Label();
            this.txtPrintField = new System.Windows.Forms.TextBox();
            this.btnSelectPrintField = new System.Windows.Forms.Button();
            this.grbLanguage.SuspendLayout();
            this.grbImageExport.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.grbOutput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grbJobFormat.SuspendLayout();
            this.grbExportCheckedResult.SuspendLayout();
            this.grbStopCondition.SuspendLayout();
            this.tblpStopCondition.SuspendLayout();
            this.grbSentDataMethod.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbLanguage
            // 
            this.grbLanguage.Controls.Add(this.cboLanguages);
            this.grbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLanguage.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbLanguage.Location = new System.Drawing.Point(3, 3);
            this.grbLanguage.Name = "grbLanguage";
            this.grbLanguage.Size = new System.Drawing.Size(470, 96);
            this.grbLanguage.TabIndex = 23;
            this.grbLanguage.TabStop = false;
            this.grbLanguage.Text = "Language";
            // 
            // cboLanguages
            // 
            this.cboLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLanguages.FormattingEnabled = true;
            this.cboLanguages.ItemHeight = 20;
            this.cboLanguages.Location = new System.Drawing.Point(20, 41);
            this.cboLanguages.Name = "cboLanguages";
            this.cboLanguages.Size = new System.Drawing.Size(433, 28);
            this.cboLanguages.TabIndex = 0;
            // 
            // grbImageExport
            // 
            this.grbImageExport.Controls.Add(this.btnBrowserImageExportPath);
            this.grbImageExport.Controls.Add(this.txtImageExportPath);
            this.grbImageExport.Controls.Add(this.tableLayoutPanel12);
            this.grbImageExport.Controls.Add(this.lblImageExportWarning);
            this.grbImageExport.Controls.Add(this.lblImageExportPath);
            this.grbImageExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbImageExport.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbImageExport.Location = new System.Drawing.Point(490, 116);
            this.grbImageExport.Name = "grbImageExport";
            this.grbImageExport.Size = new System.Drawing.Size(484, 220);
            this.grbImageExport.TabIndex = 22;
            this.grbImageExport.TabStop = false;
            this.grbImageExport.Text = "Image export";
            // 
            // btnBrowserImageExportPath
            // 
            this.btnBrowserImageExportPath.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnBrowserImageExportPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowserImageExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowserImageExportPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnBrowserImageExportPath.Location = new System.Drawing.Point(335, 92);
            this.btnBrowserImageExportPath.Name = "btnBrowserImageExportPath";
            this.btnBrowserImageExportPath.Size = new System.Drawing.Size(119, 40);
            this.btnBrowserImageExportPath.TabIndex = 27;
            this.btnBrowserImageExportPath.Text = "...";
            this.btnBrowserImageExportPath.UseVisualStyleBackColor = true;
            // 
            // txtImageExportPath
            // 
            this.txtImageExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageExportPath.Location = new System.Drawing.Point(21, 139);
            this.txtImageExportPath.Name = "txtImageExportPath";
            this.txtImageExportPath.ReadOnly = true;
            this.txtImageExportPath.Size = new System.Drawing.Size(433, 26);
            this.txtImageExportPath.TabIndex = 26;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.Controls.Add(this.radEnableImageExport, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.radDisableImageExport, 1, 0);
            this.tableLayoutPanel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel12.Location = new System.Drawing.Point(21, 35);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(432, 38);
            this.tableLayoutPanel12.TabIndex = 21;
            // 
            // radEnableImageExport
            // 
            this.radEnableImageExport.Appearance = System.Windows.Forms.Appearance.Button;
            this.radEnableImageExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radEnableImageExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radEnableImageExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radEnableImageExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnableImageExport.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radEnableImageExport.Location = new System.Drawing.Point(0, 0);
            this.radEnableImageExport.Margin = new System.Windows.Forms.Padding(0);
            this.radEnableImageExport.Name = "radEnableImageExport";
            this.radEnableImageExport.Size = new System.Drawing.Size(216, 38);
            this.radEnableImageExport.TabIndex = 3;
            this.radEnableImageExport.TabStop = true;
            this.radEnableImageExport.Text = "Enable";
            this.radEnableImageExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radEnableImageExport.UseVisualStyleBackColor = true;
            // 
            // radDisableImageExport
            // 
            this.radDisableImageExport.Appearance = System.Windows.Forms.Appearance.Button;
            this.radDisableImageExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDisableImageExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radDisableImageExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radDisableImageExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDisableImageExport.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radDisableImageExport.Location = new System.Drawing.Point(216, 0);
            this.radDisableImageExport.Margin = new System.Windows.Forms.Padding(0);
            this.radDisableImageExport.Name = "radDisableImageExport";
            this.radDisableImageExport.Size = new System.Drawing.Size(216, 38);
            this.radDisableImageExport.TabIndex = 4;
            this.radDisableImageExport.TabStop = true;
            this.radDisableImageExport.Text = "Disable";
            this.radDisableImageExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radDisableImageExport.UseVisualStyleBackColor = true;
            // 
            // lblImageExportWarning
            // 
            this.lblImageExportWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageExportWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblImageExportWarning.Location = new System.Drawing.Point(17, 172);
            this.lblImageExportWarning.Name = "lblImageExportWarning";
            this.lblImageExportWarning.Size = new System.Drawing.Size(436, 33);
            this.lblImageExportWarning.TabIndex = 4;
            this.lblImageExportWarning.Text = "This function will affect the performance of the system!";
            // 
            // lblImageExportPath
            // 
            this.lblImageExportPath.AutoSize = true;
            this.lblImageExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageExportPath.Location = new System.Drawing.Point(17, 112);
            this.lblImageExportPath.Name = "lblImageExportPath";
            this.lblImageExportPath.Size = new System.Drawing.Size(42, 20);
            this.lblImageExportPath.TabIndex = 4;
            this.lblImageExportPath.Text = "Path";
            // 
            // grbOutput
            // 
            this.grbOutput.Controls.Add(this.tableLayoutPanel1);
            this.grbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOutput.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbOutput.Location = new System.Drawing.Point(490, 3);
            this.grbOutput.Name = "grbOutput";
            this.grbOutput.Size = new System.Drawing.Size(484, 96);
            this.grbOutput.TabIndex = 20;
            this.grbOutput.TabStop = false;
            this.grbOutput.Text = "Output signal";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.radEnableOutput, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radDisableOutput, 1, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 35);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 38);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // radEnableOutput
            // 
            this.radEnableOutput.Appearance = System.Windows.Forms.Appearance.Button;
            this.radEnableOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radEnableOutput.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radEnableOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radEnableOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnableOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radEnableOutput.Location = new System.Drawing.Point(0, 0);
            this.radEnableOutput.Margin = new System.Windows.Forms.Padding(0);
            this.radEnableOutput.Name = "radEnableOutput";
            this.radEnableOutput.Size = new System.Drawing.Size(219, 38);
            this.radEnableOutput.TabIndex = 3;
            this.radEnableOutput.TabStop = true;
            this.radEnableOutput.Text = "Enable";
            this.radEnableOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radEnableOutput.UseVisualStyleBackColor = true;
            // 
            // radDisableOutput
            // 
            this.radDisableOutput.Appearance = System.Windows.Forms.Appearance.Button;
            this.radDisableOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDisableOutput.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radDisableOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radDisableOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDisableOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radDisableOutput.Location = new System.Drawing.Point(219, 0);
            this.radDisableOutput.Margin = new System.Windows.Forms.Padding(0);
            this.radDisableOutput.Name = "radDisableOutput";
            this.radDisableOutput.Size = new System.Drawing.Size(220, 38);
            this.radDisableOutput.TabIndex = 4;
            this.radDisableOutput.TabStop = true;
            this.radDisableOutput.Text = "Disable";
            this.radDisableOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radDisableOutput.UseVisualStyleBackColor = true;
            // 
            // grbJobFormat
            // 
            this.grbJobFormat.Controls.Add(this.btnDefaultFileNameJob);
            this.grbJobFormat.Controls.Add(this.btnDefaultDatimeJob);
            this.grbJobFormat.Controls.Add(this.txtJobFileName);
            this.grbJobFormat.Controls.Add(this.txtJobDateTimeFormat);
            this.grbJobFormat.Controls.Add(this.lblJobFileName);
            this.grbJobFormat.Controls.Add(this.lblJobDatetimeFormat);
            this.grbJobFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbJobFormat.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbJobFormat.Location = new System.Drawing.Point(2, 116);
            this.grbJobFormat.Name = "grbJobFormat";
            this.grbJobFormat.Size = new System.Drawing.Size(471, 220);
            this.grbJobFormat.TabIndex = 23;
            this.grbJobFormat.TabStop = false;
            this.grbJobFormat.Text = "Jobs format";
            // 
            // btnDefaultFileNameJob
            // 
            this.btnDefaultFileNameJob.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDefaultFileNameJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultFileNameJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultFileNameJob.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDefaultFileNameJob.Location = new System.Drawing.Point(331, 123);
            this.btnDefaultFileNameJob.Name = "btnDefaultFileNameJob";
            this.btnDefaultFileNameJob.Size = new System.Drawing.Size(122, 40);
            this.btnDefaultFileNameJob.TabIndex = 27;
            this.btnDefaultFileNameJob.Text = "Default";
            this.btnDefaultFileNameJob.UseVisualStyleBackColor = true;
            // 
            // btnDefaultDatimeJob
            // 
            this.btnDefaultDatimeJob.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDefaultDatimeJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultDatimeJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultDatimeJob.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDefaultDatimeJob.Location = new System.Drawing.Point(331, 25);
            this.btnDefaultDatimeJob.Name = "btnDefaultDatimeJob";
            this.btnDefaultDatimeJob.Size = new System.Drawing.Size(122, 40);
            this.btnDefaultDatimeJob.TabIndex = 27;
            this.btnDefaultDatimeJob.Text = "Default";
            this.btnDefaultDatimeJob.UseVisualStyleBackColor = true;
            // 
            // txtJobFileName
            // 
            this.txtJobFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobFileName.Location = new System.Drawing.Point(21, 173);
            this.txtJobFileName.Name = "txtJobFileName";
            this.txtJobFileName.Size = new System.Drawing.Size(432, 26);
            this.txtJobFileName.TabIndex = 27;
            // 
            // txtJobDateTimeFormat
            // 
            this.txtJobDateTimeFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobDateTimeFormat.Location = new System.Drawing.Point(21, 75);
            this.txtJobDateTimeFormat.Name = "txtJobDateTimeFormat";
            this.txtJobDateTimeFormat.Size = new System.Drawing.Size(432, 26);
            this.txtJobDateTimeFormat.TabIndex = 27;
            // 
            // lblJobFileName
            // 
            this.lblJobFileName.AutoSize = true;
            this.lblJobFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobFileName.Location = new System.Drawing.Point(17, 146);
            this.lblJobFileName.Name = "lblJobFileName";
            this.lblJobFileName.Size = new System.Drawing.Size(128, 20);
            this.lblJobFileName.TabIndex = 4;
            this.lblJobFileName.Text = "File name format";
            // 
            // lblJobDatetimeFormat
            // 
            this.lblJobDatetimeFormat.AutoSize = true;
            this.lblJobDatetimeFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobDatetimeFormat.Location = new System.Drawing.Point(17, 48);
            this.lblJobDatetimeFormat.Name = "lblJobDatetimeFormat";
            this.lblJobDatetimeFormat.Size = new System.Drawing.Size(124, 20);
            this.lblJobDatetimeFormat.TabIndex = 4;
            this.lblJobDatetimeFormat.Text = "Datetime format";
            // 
            // lblCheckedResultPath
            // 
            this.lblCheckedResultPath.AutoSize = true;
            this.lblCheckedResultPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckedResultPath.Location = new System.Drawing.Point(19, 43);
            this.lblCheckedResultPath.Name = "lblCheckedResultPath";
            this.lblCheckedResultPath.Size = new System.Drawing.Size(42, 20);
            this.lblCheckedResultPath.TabIndex = 4;
            this.lblCheckedResultPath.Text = "Path";
            // 
            // lblDataCheckedFileName
            // 
            this.lblDataCheckedFileName.AutoSize = true;
            this.lblDataCheckedFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataCheckedFileName.Location = new System.Drawing.Point(20, 143);
            this.lblDataCheckedFileName.Name = "lblDataCheckedFileName";
            this.lblDataCheckedFileName.Size = new System.Drawing.Size(176, 20);
            this.lblDataCheckedFileName.TabIndex = 21;
            this.lblDataCheckedFileName.Text = "Data checked file name";
            // 
            // grbExportCheckedResult
            // 
            this.grbExportCheckedResult.Controls.Add(this.btnGenerateDataCheckedFileName);
            this.grbExportCheckedResult.Controls.Add(this.btnBrowserCheckedResultPath);
            this.grbExportCheckedResult.Controls.Add(this.txtDataCheckedFileName);
            this.grbExportCheckedResult.Controls.Add(this.txtCheckedResultPath);
            this.grbExportCheckedResult.Controls.Add(this.lblDataCheckedFileName);
            this.grbExportCheckedResult.Controls.Add(this.lblCheckedResultPath);
            this.grbExportCheckedResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbExportCheckedResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbExportCheckedResult.Location = new System.Drawing.Point(3, 466);
            this.grbExportCheckedResult.Name = "grbExportCheckedResult";
            this.grbExportCheckedResult.Size = new System.Drawing.Size(470, 213);
            this.grbExportCheckedResult.TabIndex = 21;
            this.grbExportCheckedResult.TabStop = false;
            this.grbExportCheckedResult.Text = "Checked result";
            this.grbExportCheckedResult.Visible = false;
            // 
            // btnGenerateDataCheckedFileName
            // 
            this.btnGenerateDataCheckedFileName.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGenerateDataCheckedFileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateDataCheckedFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateDataCheckedFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnGenerateDataCheckedFileName.Location = new System.Drawing.Point(322, 123);
            this.btnGenerateDataCheckedFileName.Name = "btnGenerateDataCheckedFileName";
            this.btnGenerateDataCheckedFileName.Size = new System.Drawing.Size(130, 40);
            this.btnGenerateDataCheckedFileName.TabIndex = 27;
            this.btnGenerateDataCheckedFileName.Text = "Default";
            this.btnGenerateDataCheckedFileName.UseVisualStyleBackColor = true;
            // 
            // btnBrowserCheckedResultPath
            // 
            this.btnBrowserCheckedResultPath.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnBrowserCheckedResultPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowserCheckedResultPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowserCheckedResultPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnBrowserCheckedResultPath.Location = new System.Drawing.Point(325, 25);
            this.btnBrowserCheckedResultPath.Name = "btnBrowserCheckedResultPath";
            this.btnBrowserCheckedResultPath.Size = new System.Drawing.Size(128, 40);
            this.btnBrowserCheckedResultPath.TabIndex = 27;
            this.btnBrowserCheckedResultPath.Text = "...";
            this.btnBrowserCheckedResultPath.UseVisualStyleBackColor = true;
            // 
            // txtDataCheckedFileName
            // 
            this.txtDataCheckedFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCheckedFileName.Location = new System.Drawing.Point(23, 173);
            this.txtDataCheckedFileName.Name = "txtDataCheckedFileName";
            this.txtDataCheckedFileName.ReadOnly = true;
            this.txtDataCheckedFileName.Size = new System.Drawing.Size(429, 26);
            this.txtDataCheckedFileName.TabIndex = 27;
            // 
            // txtCheckedResultPath
            // 
            this.txtCheckedResultPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckedResultPath.Location = new System.Drawing.Point(23, 75);
            this.txtCheckedResultPath.Name = "txtCheckedResultPath";
            this.txtCheckedResultPath.ReadOnly = true;
            this.txtCheckedResultPath.Size = new System.Drawing.Size(429, 26);
            this.txtCheckedResultPath.TabIndex = 27;
            // 
            // grbStopCondition
            // 
            this.grbStopCondition.Controls.Add(this.tblpStopCondition);
            this.grbStopCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbStopCondition.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbStopCondition.Location = new System.Drawing.Point(3, 360);
            this.grbStopCondition.Name = "grbStopCondition";
            this.grbStopCondition.Size = new System.Drawing.Size(470, 96);
            this.grbStopCondition.TabIndex = 24;
            this.grbStopCondition.TabStop = false;
            this.grbStopCondition.Text = "Stop condition";
            // 
            // tblpStopCondition
            // 
            this.tblpStopCondition.ColumnCount = 2;
            this.tblpStopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpStopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpStopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblpStopCondition.Controls.Add(this.radEnableTotalCode, 0, 0);
            this.tblpStopCondition.Controls.Add(this.radEnableTotalPassed, 1, 0);
            this.tblpStopCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblpStopCondition.Location = new System.Drawing.Point(24, 35);
            this.tblpStopCondition.Margin = new System.Windows.Forms.Padding(0);
            this.tblpStopCondition.Name = "tblpStopCondition";
            this.tblpStopCondition.RowCount = 1;
            this.tblpStopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpStopCondition.Size = new System.Drawing.Size(429, 38);
            this.tblpStopCondition.TabIndex = 21;
            // 
            // radEnableTotalCode
            // 
            this.radEnableTotalCode.Appearance = System.Windows.Forms.Appearance.Button;
            this.radEnableTotalCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radEnableTotalCode.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radEnableTotalCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radEnableTotalCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnableTotalCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radEnableTotalCode.Location = new System.Drawing.Point(0, 0);
            this.radEnableTotalCode.Margin = new System.Windows.Forms.Padding(0);
            this.radEnableTotalCode.Name = "radEnableTotalCode";
            this.radEnableTotalCode.Size = new System.Drawing.Size(214, 38);
            this.radEnableTotalCode.TabIndex = 3;
            this.radEnableTotalCode.TabStop = true;
            this.radEnableTotalCode.Text = "Total check";
            this.radEnableTotalCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radEnableTotalCode.UseVisualStyleBackColor = true;
            // 
            // radEnableTotalPassed
            // 
            this.radEnableTotalPassed.Appearance = System.Windows.Forms.Appearance.Button;
            this.radEnableTotalPassed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radEnableTotalPassed.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radEnableTotalPassed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radEnableTotalPassed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnableTotalPassed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radEnableTotalPassed.Location = new System.Drawing.Point(214, 0);
            this.radEnableTotalPassed.Margin = new System.Windows.Forms.Padding(0);
            this.radEnableTotalPassed.Name = "radEnableTotalPassed";
            this.radEnableTotalPassed.Size = new System.Drawing.Size(215, 38);
            this.radEnableTotalPassed.TabIndex = 4;
            this.radEnableTotalPassed.TabStop = true;
            this.radEnableTotalPassed.Text = "Total passed";
            this.radEnableTotalPassed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radEnableTotalPassed.UseVisualStyleBackColor = true;
            // 
            // grbSentDataMethod
            // 
            this.grbSentDataMethod.Controls.Add(this.btnSelectPrintField);
            this.grbSentDataMethod.Controls.Add(this.btnDefaultFailedR);
            this.grbSentDataMethod.Controls.Add(this.txtPrintField);
            this.grbSentDataMethod.Controls.Add(this.tableLayoutPanel2);
            this.grbSentDataMethod.Controls.Add(this.lblPrintField);
            this.grbSentDataMethod.Controls.Add(this.txtFailedREdit);
            this.grbSentDataMethod.Controls.Add(this.lblFailureEdit);
            this.grbSentDataMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSentDataMethod.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbSentDataMethod.Location = new System.Drawing.Point(490, 360);
            this.grbSentDataMethod.Name = "grbSentDataMethod";
            this.grbSentDataMethod.Size = new System.Drawing.Size(484, 319);
            this.grbSentDataMethod.TabIndex = 28;
            this.grbSentDataMethod.TabStop = false;
            this.grbSentDataMethod.Text = "Sent data method";
            // 
            // btnDefaultFailedR
            // 
            this.btnDefaultFailedR.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDefaultFailedR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultFailedR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultFailedR.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDefaultFailedR.Location = new System.Drawing.Point(341, 190);
            this.btnDefaultFailedR.Name = "btnDefaultFailedR";
            this.btnDefaultFailedR.Size = new System.Drawing.Size(113, 40);
            this.btnDefaultFailedR.TabIndex = 28;
            this.btnDefaultFailedR.Text = "Default";
            this.btnDefaultFailedR.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.radBasic, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radCompare, 1, 0);
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(24, 35);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(430, 38);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // radBasic
            // 
            this.radBasic.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radBasic.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radBasic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radBasic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBasic.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radBasic.Location = new System.Drawing.Point(0, 0);
            this.radBasic.Margin = new System.Windows.Forms.Padding(0);
            this.radBasic.Name = "radBasic";
            this.radBasic.Size = new System.Drawing.Size(215, 38);
            this.radBasic.TabIndex = 3;
            this.radBasic.TabStop = true;
            this.radBasic.Text = "Basic";
            this.radBasic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBasic.UseVisualStyleBackColor = true;
            // 
            // radCompare
            // 
            this.radCompare.Appearance = System.Windows.Forms.Appearance.Button;
            this.radCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radCompare.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCompare.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radCompare.Location = new System.Drawing.Point(215, 0);
            this.radCompare.Margin = new System.Windows.Forms.Padding(0);
            this.radCompare.Name = "radCompare";
            this.radCompare.Size = new System.Drawing.Size(215, 38);
            this.radCompare.TabIndex = 4;
            this.radCompare.TabStop = true;
            this.radCompare.Text = "Compare";
            this.radCompare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radCompare.UseVisualStyleBackColor = true;
            // 
            // txtFailedREdit
            // 
            this.txtFailedREdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFailedREdit.Location = new System.Drawing.Point(24, 239);
            this.txtFailedREdit.Name = "txtFailedREdit";
            this.txtFailedREdit.Size = new System.Drawing.Size(430, 26);
            this.txtFailedREdit.TabIndex = 27;
            // 
            // lblFailureEdit
            // 
            this.lblFailureEdit.AutoSize = true;
            this.lblFailureEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailureEdit.Location = new System.Drawing.Point(20, 212);
            this.lblFailureEdit.Name = "lblFailureEdit";
            this.lblFailureEdit.Size = new System.Drawing.Size(175, 20);
            this.lblFailureEdit.TabIndex = 4;
            this.lblFailureEdit.Text = "Send failed data format";
            // 
            // lblPrintField
            // 
            this.lblPrintField.AutoSize = true;
            this.lblPrintField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintField.Location = new System.Drawing.Point(20, 114);
            this.lblPrintField.Name = "lblPrintField";
            this.lblPrintField.Size = new System.Drawing.Size(74, 20);
            this.lblPrintField.TabIndex = 4;
            this.lblPrintField.Text = "Print field";
            // 
            // txtPrintField
            // 
            this.txtPrintField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintField.Location = new System.Drawing.Point(24, 141);
            this.txtPrintField.Name = "txtPrintField";
            this.txtPrintField.Size = new System.Drawing.Size(430, 26);
            this.txtPrintField.TabIndex = 27;
            // 
            // btnSelectPrintField
            // 
            this.btnSelectPrintField.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSelectPrintField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPrintField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPrintField.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSelectPrintField.Location = new System.Drawing.Point(341, 92);
            this.btnSelectPrintField.Name = "btnSelectPrintField";
            this.btnSelectPrintField.Size = new System.Drawing.Size(113, 40);
            this.btnSelectPrintField.TabIndex = 28;
            this.btnSelectPrintField.Text = "Select";
            this.btnSelectPrintField.UseVisualStyleBackColor = true;
            // 
            // ucSystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grbSentDataMethod);
            this.Controls.Add(this.grbStopCondition);
            this.Controls.Add(this.grbJobFormat);
            this.Controls.Add(this.grbLanguage);
            this.Controls.Add(this.grbImageExport);
            this.Controls.Add(this.grbExportCheckedResult);
            this.Controls.Add(this.grbOutput);
            this.Name = "ucSystemSettings";
            this.Size = new System.Drawing.Size(984, 686);
            this.grbLanguage.ResumeLayout(false);
            this.grbImageExport.ResumeLayout(false);
            this.grbImageExport.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.grbOutput.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grbJobFormat.ResumeLayout(false);
            this.grbJobFormat.PerformLayout();
            this.grbExportCheckedResult.ResumeLayout(false);
            this.grbExportCheckedResult.PerformLayout();
            this.grbStopCondition.ResumeLayout(false);
            this.tblpStopCondition.ResumeLayout(false);
            this.grbSentDataMethod.ResumeLayout(false);
            this.grbSentDataMethod.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbLanguage;
        private System.Windows.Forms.GroupBox grbImageExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.RadioButton radEnableImageExport;
        private System.Windows.Forms.RadioButton radDisableImageExport;
        private System.Windows.Forms.Label lblImageExportWarning;
        private System.Windows.Forms.Label lblImageExportPath;
        private System.Windows.Forms.GroupBox grbOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radEnableOutput;
        private System.Windows.Forms.RadioButton radDisableOutput;
        private System.Windows.Forms.GroupBox grbJobFormat;
        private System.Windows.Forms.Label lblJobFileName;
        private System.Windows.Forms.Label lblJobDatetimeFormat;
        private System.Windows.Forms.Label lblCheckedResultPath;
        private System.Windows.Forms.Label lblDataCheckedFileName;
        private System.Windows.Forms.GroupBox grbExportCheckedResult;
        private System.Windows.Forms.TextBox txtImageExportPath;
        private System.Windows.Forms.TextBox txtJobDateTimeFormat;
        private System.Windows.Forms.TextBox txtJobFileName;
        private System.Windows.Forms.TextBox txtDataCheckedFileName;
        private System.Windows.Forms.TextBox txtCheckedResultPath;
        private System.Windows.Forms.Button btnBrowserImageExportPath;
        private System.Windows.Forms.Button btnDefaultFileNameJob;
        private System.Windows.Forms.Button btnDefaultDatimeJob;
        private System.Windows.Forms.Button btnGenerateDataCheckedFileName;
        private System.Windows.Forms.Button btnBrowserCheckedResultPath;
        private System.Windows.Forms.ComboBox cboLanguages;
        private System.Windows.Forms.GroupBox grbStopCondition;
        private System.Windows.Forms.TableLayoutPanel tblpStopCondition;
        private System.Windows.Forms.RadioButton radEnableTotalCode;
        private System.Windows.Forms.RadioButton radEnableTotalPassed;
        private System.Windows.Forms.GroupBox grbSentDataMethod;
        private System.Windows.Forms.Button btnDefaultFailedR;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton radBasic;
        private System.Windows.Forms.RadioButton radCompare;
        private System.Windows.Forms.TextBox txtFailedREdit;
        private System.Windows.Forms.Label lblFailureEdit;
        private System.Windows.Forms.Button btnSelectPrintField;
        private System.Windows.Forms.TextBox txtPrintField;
        private System.Windows.Forms.Label lblPrintField;
    }
}
