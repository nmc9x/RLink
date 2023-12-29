namespace BarcodeVerificationSystem.View
{
    partial class UcPrinterSettings
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
            this.grbPrinter = new System.Windows.Forms.GroupBox();
            this.lblPrinterOperSys = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radSupported = new System.Windows.Forms.RadioButton();
            this.radUnsuported = new System.Windows.Forms.RadioButton();
            this.lblPODProtocol = new System.Windows.Forms.GroupBox();
            this.lblPODChangedWarning = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radNewPODProtocol = new System.Windows.Forms.RadioButton();
            this.radOldPODProtocol = new System.Windows.Forms.RadioButton();
            this.btnSetupPrinter = new System.Windows.Forms.Button();
            this.numPortRemote = new System.Windows.Forms.NumericUpDown();
            this.numPrinterPort = new System.Windows.Forms.NumericUpDown();
            this.txtPrinterIP = new IPAddressControlLib.IPAddressControl();
            this.lblPortRemote = new System.Windows.Forms.Label();
            this.lblPrinterPort = new System.Windows.Forms.Label();
            this.lblIPrinterIP = new System.Windows.Forms.Label();
            this.grbPrinter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.lblPODProtocol.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPortRemote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrinterPort)).BeginInit();
            this.SuspendLayout();
            // 
            // grbPrinter
            // 
            this.grbPrinter.BackColor = System.Drawing.Color.White;
            this.grbPrinter.Controls.Add(this.lblPrinterOperSys);
            this.grbPrinter.Controls.Add(this.tableLayoutPanel2);
            this.grbPrinter.Controls.Add(this.lblPODProtocol);
            this.grbPrinter.Controls.Add(this.btnSetupPrinter);
            this.grbPrinter.Controls.Add(this.numPortRemote);
            this.grbPrinter.Controls.Add(this.numPrinterPort);
            this.grbPrinter.Controls.Add(this.txtPrinterIP);
            this.grbPrinter.Controls.Add(this.lblPortRemote);
            this.grbPrinter.Controls.Add(this.lblPrinterPort);
            this.grbPrinter.Controls.Add(this.lblIPrinterIP);
            this.grbPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbPrinter.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbPrinter.Location = new System.Drawing.Point(0, 0);
            this.grbPrinter.Name = "grbPrinter";
            this.grbPrinter.Size = new System.Drawing.Size(996, 282);
            this.grbPrinter.TabIndex = 20;
            this.grbPrinter.TabStop = false;
            this.grbPrinter.Text = "Printer";
            // 
            // lblPrinterOperSys
            // 
            this.lblPrinterOperSys.AutoSize = true;
            this.lblPrinterOperSys.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterOperSys.Location = new System.Drawing.Point(518, 37);
            this.lblPrinterOperSys.Name = "lblPrinterOperSys";
            this.lblPrinterOperSys.Size = new System.Drawing.Size(182, 20);
            this.lblPrinterOperSys.TabIndex = 45;
            this.lblPrinterOperSys.Text = "Check all printer settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.radSupported, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radUnsuported, 1, 0);
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(518, 64);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(440, 38);
            this.tableLayoutPanel2.TabIndex = 44;
            // 
            // radSupported
            // 
            this.radSupported.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSupported.BackColor = System.Drawing.Color.White;
            this.radSupported.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSupported.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radSupported.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radSupported.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSupported.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radSupported.Location = new System.Drawing.Point(0, 0);
            this.radSupported.Margin = new System.Windows.Forms.Padding(0);
            this.radSupported.Name = "radSupported";
            this.radSupported.Size = new System.Drawing.Size(220, 38);
            this.radSupported.TabIndex = 3;
            this.radSupported.TabStop = true;
            this.radSupported.Text = "Enable";
            this.radSupported.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSupported.UseVisualStyleBackColor = false;
            // 
            // radUnsuported
            // 
            this.radUnsuported.Appearance = System.Windows.Forms.Appearance.Button;
            this.radUnsuported.BackColor = System.Drawing.Color.White;
            this.radUnsuported.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radUnsuported.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radUnsuported.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radUnsuported.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radUnsuported.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radUnsuported.Location = new System.Drawing.Point(220, 0);
            this.radUnsuported.Margin = new System.Windows.Forms.Padding(0);
            this.radUnsuported.Name = "radUnsuported";
            this.radUnsuported.Size = new System.Drawing.Size(220, 38);
            this.radUnsuported.TabIndex = 4;
            this.radUnsuported.TabStop = true;
            this.radUnsuported.Text = "Disable";
            this.radUnsuported.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radUnsuported.UseVisualStyleBackColor = false;
            // 
            // lblPODProtocol
            // 
            this.lblPODProtocol.Controls.Add(this.lblPODChangedWarning);
            this.lblPODProtocol.Controls.Add(this.tableLayoutPanel1);
            this.lblPODProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPODProtocol.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lblPODProtocol.Location = new System.Drawing.Point(501, 124);
            this.lblPODProtocol.Name = "lblPODProtocol";
            this.lblPODProtocol.Size = new System.Drawing.Size(478, 137);
            this.lblPODProtocol.TabIndex = 43;
            this.lblPODProtocol.TabStop = false;
            this.lblPODProtocol.Text = "POD Protocol";
            this.lblPODProtocol.Visible = false;
            // 
            // lblPODChangedWarning
            // 
            this.lblPODChangedWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPODChangedWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblPODChangedWarning.Location = new System.Drawing.Point(17, 77);
            this.lblPODChangedWarning.Name = "lblPODChangedWarning";
            this.lblPODChangedWarning.Size = new System.Drawing.Size(431, 47);
            this.lblPODChangedWarning.TabIndex = 43;
            this.lblPODChangedWarning.Text = "This feature requires an app restart to take effect!";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.radNewPODProtocol, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radOldPODProtocol, 1, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 38);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // radNewPODProtocol
            // 
            this.radNewPODProtocol.Appearance = System.Windows.Forms.Appearance.Button;
            this.radNewPODProtocol.BackColor = System.Drawing.Color.White;
            this.radNewPODProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radNewPODProtocol.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radNewPODProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radNewPODProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNewPODProtocol.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radNewPODProtocol.Location = new System.Drawing.Point(0, 0);
            this.radNewPODProtocol.Margin = new System.Windows.Forms.Padding(0);
            this.radNewPODProtocol.Name = "radNewPODProtocol";
            this.radNewPODProtocol.Size = new System.Drawing.Size(220, 38);
            this.radNewPODProtocol.TabIndex = 3;
            this.radNewPODProtocol.TabStop = true;
            this.radNewPODProtocol.Text = "New version";
            this.radNewPODProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radNewPODProtocol.UseVisualStyleBackColor = false;
            // 
            // radOldPODProtocol
            // 
            this.radOldPODProtocol.Appearance = System.Windows.Forms.Appearance.Button;
            this.radOldPODProtocol.BackColor = System.Drawing.Color.White;
            this.radOldPODProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radOldPODProtocol.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radOldPODProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radOldPODProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radOldPODProtocol.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radOldPODProtocol.Location = new System.Drawing.Point(220, 0);
            this.radOldPODProtocol.Margin = new System.Windows.Forms.Padding(0);
            this.radOldPODProtocol.Name = "radOldPODProtocol";
            this.radOldPODProtocol.Size = new System.Drawing.Size(220, 38);
            this.radOldPODProtocol.TabIndex = 4;
            this.radOldPODProtocol.TabStop = true;
            this.radOldPODProtocol.Text = "Old version";
            this.radOldPODProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radOldPODProtocol.UseVisualStyleBackColor = false;
            // 
            // btnSetupPrinter
            // 
            this.btnSetupPrinter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(151)))), ((int)(((byte)(149)))));
            this.btnSetupPrinter.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSetupPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupPrinter.ForeColor = System.Drawing.Color.White;
            this.btnSetupPrinter.Location = new System.Drawing.Point(25, 204);
            this.btnSetupPrinter.Name = "btnSetupPrinter";
            this.btnSetupPrinter.Size = new System.Drawing.Size(428, 57);
            this.btnSetupPrinter.TabIndex = 41;
            this.btnSetupPrinter.Text = "Advanced printer settings";
            this.btnSetupPrinter.UseVisualStyleBackColor = false;
            // 
            // numPortRemote
            // 
            this.numPortRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPortRemote.Location = new System.Drawing.Point(25, 148);
            this.numPortRemote.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPortRemote.Name = "numPortRemote";
            this.numPortRemote.Size = new System.Drawing.Size(428, 26);
            this.numPortRemote.TabIndex = 37;
            this.numPortRemote.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // numPrinterPort
            // 
            this.numPrinterPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrinterPort.Location = new System.Drawing.Point(314, 65);
            this.numPrinterPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPrinterPort.Name = "numPrinterPort";
            this.numPrinterPort.Size = new System.Drawing.Size(139, 26);
            this.numPrinterPort.TabIndex = 37;
            // 
            // txtPrinterIP
            // 
            this.txtPrinterIP.AllowInternalTab = false;
            this.txtPrinterIP.AutoHeight = true;
            this.txtPrinterIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrinterIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPrinterIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrinterIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrinterIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPrinterIP.Location = new System.Drawing.Point(25, 64);
            this.txtPrinterIP.MinimumSize = new System.Drawing.Size(126, 26);
            this.txtPrinterIP.Name = "txtPrinterIP";
            this.txtPrinterIP.ReadOnly = false;
            this.txtPrinterIP.Size = new System.Drawing.Size(265, 26);
            this.txtPrinterIP.TabIndex = 32;
            this.txtPrinterIP.Text = "...";
            // 
            // lblPortRemote
            // 
            this.lblPortRemote.AutoSize = true;
            this.lblPortRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortRemote.Location = new System.Drawing.Point(21, 119);
            this.lblPortRemote.Name = "lblPortRemote";
            this.lblPortRemote.Size = new System.Drawing.Size(92, 20);
            this.lblPortRemote.TabIndex = 25;
            this.lblPortRemote.Text = "Port remote";
            // 
            // lblPrinterPort
            // 
            this.lblPrinterPort.AutoSize = true;
            this.lblPrinterPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterPort.Location = new System.Drawing.Point(310, 37);
            this.lblPrinterPort.Name = "lblPrinterPort";
            this.lblPrinterPort.Size = new System.Drawing.Size(38, 20);
            this.lblPrinterPort.TabIndex = 25;
            this.lblPrinterPort.Text = "Port";
            // 
            // lblIPrinterIP
            // 
            this.lblIPrinterIP.AutoSize = true;
            this.lblIPrinterIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPrinterIP.Location = new System.Drawing.Point(22, 37);
            this.lblIPrinterIP.Name = "lblIPrinterIP";
            this.lblIPrinterIP.Size = new System.Drawing.Size(85, 20);
            this.lblIPrinterIP.TabIndex = 21;
            this.lblIPrinterIP.Text = "IP address";
            // 
            // ucPrinterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbPrinter);
            this.Name = "ucPrinterSettings";
            this.Size = new System.Drawing.Size(996, 282);
            this.grbPrinter.ResumeLayout(false);
            this.grbPrinter.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.lblPODProtocol.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPortRemote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrinterPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbPrinter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radNewPODProtocol;
        private System.Windows.Forms.RadioButton radOldPODProtocol;
        private System.Windows.Forms.NumericUpDown numPrinterPort;
        private IPAddressControlLib.IPAddressControl txtPrinterIP;
        private System.Windows.Forms.Label lblPrinterPort;
        private System.Windows.Forms.Label lblIPrinterIP;
        private System.Windows.Forms.Button btnSetupPrinter;
        private System.Windows.Forms.NumericUpDown numPortRemote;
        private System.Windows.Forms.Label lblPortRemote;
        private System.Windows.Forms.GroupBox lblPODProtocol;
        private System.Windows.Forms.Label lblPODChangedWarning;
        private System.Windows.Forms.Label lblPrinterOperSys;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton radSupported;
        private System.Windows.Forms.RadioButton radUnsuported;
    }
}
