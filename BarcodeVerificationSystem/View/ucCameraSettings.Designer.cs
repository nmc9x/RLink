namespace BarcodeVerificationSystem.View
{
    partial class ucCameraSettings
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
            this.grbCamera = new System.Windows.Forms.GroupBox();
            this.lblOutputSignal = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radOutputEnable = new System.Windows.Forms.RadioButton();
            this.radOutputDisable = new System.Windows.Forms.RadioButton();
            this.txtIPAddress = new IPAddressControlLib.IPAddressControl();
            this.lblAutoReconnect = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radAutoReconnectEnable = new System.Windows.Forms.RadioButton();
            this.radAutoReconnectDisable = new System.Windows.Forms.RadioButton();
            this.txtNoReadOutputString = new System.Windows.Forms.TextBox();
            this.lblNoReadOuputString = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.comboBoxCamType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbCamera.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbCamera
            // 
            this.grbCamera.BackColor = System.Drawing.Color.White;
            this.grbCamera.Controls.Add(this.label1);
            this.grbCamera.Controls.Add(this.comboBoxCamType);
            this.grbCamera.Controls.Add(this.labelPort);
            this.grbCamera.Controls.Add(this.textBoxPort);
            this.grbCamera.Controls.Add(this.lblOutputSignal);
            this.grbCamera.Controls.Add(this.tableLayoutPanel1);
            this.grbCamera.Controls.Add(this.txtIPAddress);
            this.grbCamera.Controls.Add(this.lblAutoReconnect);
            this.grbCamera.Controls.Add(this.tableLayoutPanel2);
            this.grbCamera.Controls.Add(this.txtNoReadOutputString);
            this.grbCamera.Controls.Add(this.lblNoReadOuputString);
            this.grbCamera.Controls.Add(this.txtPassword);
            this.grbCamera.Controls.Add(this.lblPassword);
            this.grbCamera.Controls.Add(this.lblSerialNumber);
            this.grbCamera.Controls.Add(this.txtSerialNumber);
            this.grbCamera.Controls.Add(this.lblIPAddress);
            this.grbCamera.Controls.Add(this.lblModel);
            this.grbCamera.Controls.Add(this.txtModel);
            this.grbCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbCamera.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbCamera.Location = new System.Drawing.Point(0, 0);
            this.grbCamera.Name = "grbCamera";
            this.grbCamera.Size = new System.Drawing.Size(990, 291);
            this.grbCamera.TabIndex = 18;
            this.grbCamera.TabStop = false;
            this.grbCamera.Text = "Camera";
            // 
            // lblOutputSignal
            // 
            this.lblOutputSignal.AutoSize = true;
            this.lblOutputSignal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputSignal.Location = new System.Drawing.Point(18, 195);
            this.lblOutputSignal.Name = "lblOutputSignal";
            this.lblOutputSignal.Size = new System.Drawing.Size(103, 20);
            this.lblOutputSignal.TabIndex = 34;
            this.lblOutputSignal.Text = "Output signal";
            this.lblOutputSignal.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.radOutputEnable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radOutputDisable, 1, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(22, 223);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(301, 38);
            this.tableLayoutPanel1.TabIndex = 33;
            this.tableLayoutPanel1.Visible = false;
            // 
            // radOutputEnable
            // 
            this.radOutputEnable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radOutputEnable.BackColor = System.Drawing.Color.White;
            this.radOutputEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radOutputEnable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radOutputEnable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radOutputEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radOutputEnable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radOutputEnable.Location = new System.Drawing.Point(0, 0);
            this.radOutputEnable.Margin = new System.Windows.Forms.Padding(0);
            this.radOutputEnable.Name = "radOutputEnable";
            this.radOutputEnable.Size = new System.Drawing.Size(150, 38);
            this.radOutputEnable.TabIndex = 3;
            this.radOutputEnable.TabStop = true;
            this.radOutputEnable.Text = "Enable";
            this.radOutputEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radOutputEnable.UseVisualStyleBackColor = false;
            // 
            // radOutputDisable
            // 
            this.radOutputDisable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radOutputDisable.BackColor = System.Drawing.Color.White;
            this.radOutputDisable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radOutputDisable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radOutputDisable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radOutputDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radOutputDisable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radOutputDisable.Location = new System.Drawing.Point(150, 0);
            this.radOutputDisable.Margin = new System.Windows.Forms.Padding(0);
            this.radOutputDisable.Name = "radOutputDisable";
            this.radOutputDisable.Size = new System.Drawing.Size(151, 38);
            this.radOutputDisable.TabIndex = 4;
            this.radOutputDisable.TabStop = true;
            this.radOutputDisable.Text = "Disable";
            this.radOutputDisable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radOutputDisable.UseVisualStyleBackColor = false;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.AllowInternalTab = false;
            this.txtIPAddress.AutoHeight = true;
            this.txtIPAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtIPAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIPAddress.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIPAddress.Location = new System.Drawing.Point(21, 59);
            this.txtIPAddress.MinimumSize = new System.Drawing.Size(126, 26);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.ReadOnly = false;
            this.txtIPAddress.Size = new System.Drawing.Size(302, 26);
            this.txtIPAddress.TabIndex = 32;
            this.txtIPAddress.Text = "...";
            // 
            // lblAutoReconnect
            // 
            this.lblAutoReconnect.AutoSize = true;
            this.lblAutoReconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoReconnect.Location = new System.Drawing.Point(375, 195);
            this.lblAutoReconnect.Name = "lblAutoReconnect";
            this.lblAutoReconnect.Size = new System.Drawing.Size(118, 20);
            this.lblAutoReconnect.TabIndex = 30;
            this.lblAutoReconnect.Text = "Auto reconnect";
            this.lblAutoReconnect.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.radAutoReconnectEnable, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radAutoReconnectDisable, 1, 0);
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(379, 223);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(303, 38);
            this.tableLayoutPanel2.TabIndex = 29;
            this.tableLayoutPanel2.Visible = false;
            // 
            // radAutoReconnectEnable
            // 
            this.radAutoReconnectEnable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radAutoReconnectEnable.BackColor = System.Drawing.Color.White;
            this.radAutoReconnectEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radAutoReconnectEnable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radAutoReconnectEnable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radAutoReconnectEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAutoReconnectEnable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radAutoReconnectEnable.Location = new System.Drawing.Point(0, 0);
            this.radAutoReconnectEnable.Margin = new System.Windows.Forms.Padding(0);
            this.radAutoReconnectEnable.Name = "radAutoReconnectEnable";
            this.radAutoReconnectEnable.Size = new System.Drawing.Size(151, 38);
            this.radAutoReconnectEnable.TabIndex = 3;
            this.radAutoReconnectEnable.TabStop = true;
            this.radAutoReconnectEnable.Text = "Enable";
            this.radAutoReconnectEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radAutoReconnectEnable.UseVisualStyleBackColor = false;
            // 
            // radAutoReconnectDisable
            // 
            this.radAutoReconnectDisable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radAutoReconnectDisable.BackColor = System.Drawing.Color.White;
            this.radAutoReconnectDisable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radAutoReconnectDisable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radAutoReconnectDisable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radAutoReconnectDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAutoReconnectDisable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radAutoReconnectDisable.Location = new System.Drawing.Point(151, 0);
            this.radAutoReconnectDisable.Margin = new System.Windows.Forms.Padding(0);
            this.radAutoReconnectDisable.Name = "radAutoReconnectDisable";
            this.radAutoReconnectDisable.Size = new System.Drawing.Size(152, 38);
            this.radAutoReconnectDisable.TabIndex = 4;
            this.radAutoReconnectDisable.TabStop = true;
            this.radAutoReconnectDisable.Text = "Disable";
            this.radAutoReconnectDisable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radAutoReconnectDisable.UseVisualStyleBackColor = false;
            // 
            // txtNoReadOutputString
            // 
            this.txtNoReadOutputString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoReadOutputString.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtNoReadOutputString.Location = new System.Drawing.Point(674, 59);
            this.txtNoReadOutputString.Margin = new System.Windows.Forms.Padding(2);
            this.txtNoReadOutputString.Name = "txtNoReadOutputString";
            this.txtNoReadOutputString.Size = new System.Drawing.Size(303, 26);
            this.txtNoReadOutputString.TabIndex = 28;
            // 
            // lblNoReadOuputString
            // 
            this.lblNoReadOuputString.AutoSize = true;
            this.lblNoReadOuputString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoReadOuputString.Location = new System.Drawing.Point(670, 33);
            this.lblNoReadOuputString.Name = "lblNoReadOuputString";
            this.lblNoReadOuputString.Size = new System.Drawing.Size(153, 20);
            this.lblNoReadOuputString.TabIndex = 27;
            this.lblNoReadOuputString.Text = "No read ouput string";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtPassword.Location = new System.Drawing.Point(346, 59);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(304, 26);
            this.txtPassword.TabIndex = 26;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(342, 33);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(78, 20);
            this.lblPassword.TabIndex = 25;
            this.lblPassword.Text = "Password";
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(342, 117);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(107, 20);
            this.lblSerialNumber.TabIndex = 23;
            this.lblSerialNumber.Text = "Serial number";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSerialNumber.Location = new System.Drawing.Point(346, 143);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.ReadOnly = true;
            this.txtSerialNumber.Size = new System.Drawing.Size(304, 26);
            this.txtSerialNumber.TabIndex = 24;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPAddress.Location = new System.Drawing.Point(18, 33);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(85, 20);
            this.lblIPAddress.TabIndex = 21;
            this.lblIPAddress.Text = "IP address";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(18, 117);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(52, 20);
            this.lblModel.TabIndex = 4;
            this.lblModel.Text = "Model";
            // 
            // txtModel
            // 
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtModel.Location = new System.Drawing.Point(21, 143);
            this.txtModel.Margin = new System.Windows.Forms.Padding(2);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(302, 26);
            this.txtModel.TabIndex = 19;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPort.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxPort.Location = new System.Drawing.Point(674, 143);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(303, 26);
            this.textBoxPort.TabIndex = 35;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort.Location = new System.Drawing.Point(670, 117);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(38, 20);
            this.labelPort.TabIndex = 36;
            this.labelPort.Text = "Port";
            // 
            // comboBoxCamType
            // 
            this.comboBoxCamType.FormattingEnabled = true;
            this.comboBoxCamType.Items.AddRange(new object[] {
            "DM Series",
            "IS Series"});
            this.comboBoxCamType.Location = new System.Drawing.Point(807, 223);
            this.comboBoxCamType.Name = "comboBoxCamType";
            this.comboBoxCamType.Size = new System.Drawing.Size(138, 28);
            this.comboBoxCamType.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(738, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 38;
            this.label1.Text = "Type";
            // 
            // ucCameraSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbCamera);
            this.Name = "ucCameraSettings";
            this.Size = new System.Drawing.Size(990, 291);
            this.grbCamera.ResumeLayout(false);
            this.grbCamera.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbCamera;
        private System.Windows.Forms.Label lblOutputSignal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radOutputEnable;
        private System.Windows.Forms.RadioButton radOutputDisable;
        private IPAddressControlLib.IPAddressControl txtIPAddress;
        private System.Windows.Forms.Label lblAutoReconnect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton radAutoReconnectEnable;
        private System.Windows.Forms.RadioButton radAutoReconnectDisable;
        private System.Windows.Forms.TextBox txtNoReadOutputString;
        private System.Windows.Forms.Label lblNoReadOuputString;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCamType;
    }
}
