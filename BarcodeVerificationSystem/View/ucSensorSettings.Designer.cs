namespace BarcodeVerificationSystem.View
{
    partial class ucSensorSettings
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
            this.lblSensorControllerIP = new System.Windows.Forms.Label();
            this.lblSensorControllerPort = new System.Windows.Forms.Label();
            this.lblSensorControllerPulseEncoder = new System.Windows.Forms.Label();
            this.lblSensorControllerEncoderDiameter = new System.Windows.Forms.Label();
            this.txtSensorControllerIP = new IPAddressControlLib.IPAddressControl();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.radSensorControllerEnable = new System.Windows.Forms.RadioButton();
            this.radSensorControllerDisable = new System.Windows.Forms.RadioButton();
            this.numSensorControllerPort = new System.Windows.Forms.NumericUpDown();
            this.numSensorControllerPulseEncoder = new System.Windows.Forms.NumericUpDown();
            this.numSensorControllerEncoderDiameter = new System.Windows.Forms.NumericUpDown();
            this.lblSensorControllerDelayBefore = new System.Windows.Forms.Label();
            this.lblSensorControllerDelayAfter = new System.Windows.Forms.Label();
            this.numSensorControllerDelayBefore = new System.Windows.Forms.NumericUpDown();
            this.numSensorControllerDelayAfter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblContentResponse = new System.Windows.Forms.Label();
            this.richTXTContentResponse = new System.Windows.Forms.RichTextBox();
            this.btnContenResponseClear = new System.Windows.Forms.Button();
            this.grbSensorController = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerPulseEncoder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerEncoderDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerDelayBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerDelayAfter)).BeginInit();
            this.grbSensorController.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSensorControllerIP
            // 
            this.lblSensorControllerIP.AutoSize = true;
            this.lblSensorControllerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerIP.Location = new System.Drawing.Point(21, 115);
            this.lblSensorControllerIP.Name = "lblSensorControllerIP";
            this.lblSensorControllerIP.Size = new System.Drawing.Size(85, 20);
            this.lblSensorControllerIP.TabIndex = 21;
            this.lblSensorControllerIP.Text = "IP address";
            // 
            // lblSensorControllerPort
            // 
            this.lblSensorControllerPort.AutoSize = true;
            this.lblSensorControllerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerPort.Location = new System.Drawing.Point(259, 115);
            this.lblSensorControllerPort.Name = "lblSensorControllerPort";
            this.lblSensorControllerPort.Size = new System.Drawing.Size(38, 20);
            this.lblSensorControllerPort.TabIndex = 25;
            this.lblSensorControllerPort.Text = "Port";
            // 
            // lblSensorControllerPulseEncoder
            // 
            this.lblSensorControllerPulseEncoder.AutoSize = true;
            this.lblSensorControllerPulseEncoder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerPulseEncoder.Location = new System.Drawing.Point(493, 115);
            this.lblSensorControllerPulseEncoder.Name = "lblSensorControllerPulseEncoder";
            this.lblSensorControllerPulseEncoder.Size = new System.Drawing.Size(110, 20);
            this.lblSensorControllerPulseEncoder.TabIndex = 27;
            this.lblSensorControllerPulseEncoder.Text = "Pulse encoder";
            // 
            // lblSensorControllerEncoderDiameter
            // 
            this.lblSensorControllerEncoderDiameter.AutoSize = true;
            this.lblSensorControllerEncoderDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerEncoderDiameter.Location = new System.Drawing.Point(739, 115);
            this.lblSensorControllerEncoderDiameter.Name = "lblSensorControllerEncoderDiameter";
            this.lblSensorControllerEncoderDiameter.Size = new System.Drawing.Size(135, 20);
            this.lblSensorControllerEncoderDiameter.TabIndex = 30;
            this.lblSensorControllerEncoderDiameter.Text = "Encoder diameter";
            // 
            // txtSensorControllerIP
            // 
            this.txtSensorControllerIP.AllowInternalTab = false;
            this.txtSensorControllerIP.AutoHeight = true;
            this.txtSensorControllerIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtSensorControllerIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtSensorControllerIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSensorControllerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSensorControllerIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSensorControllerIP.Location = new System.Drawing.Point(24, 142);
            this.txtSensorControllerIP.MinimumSize = new System.Drawing.Size(126, 26);
            this.txtSensorControllerIP.Name = "txtSensorControllerIP";
            this.txtSensorControllerIP.ReadOnly = false;
            this.txtSensorControllerIP.Size = new System.Drawing.Size(213, 26);
            this.txtSensorControllerIP.TabIndex = 32;
            this.txtSensorControllerIP.Text = "...";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.radSensorControllerEnable, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.radSensorControllerDisable, 1, 0);
            this.tableLayoutPanel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel5.Location = new System.Drawing.Point(24, 52);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(439, 38);
            this.tableLayoutPanel5.TabIndex = 33;
            // 
            // radSensorControllerEnable
            // 
            this.radSensorControllerEnable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSensorControllerEnable.BackColor = System.Drawing.Color.White;
            this.radSensorControllerEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSensorControllerEnable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radSensorControllerEnable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radSensorControllerEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSensorControllerEnable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radSensorControllerEnable.Location = new System.Drawing.Point(0, 0);
            this.radSensorControllerEnable.Margin = new System.Windows.Forms.Padding(0);
            this.radSensorControllerEnable.Name = "radSensorControllerEnable";
            this.radSensorControllerEnable.Size = new System.Drawing.Size(219, 38);
            this.radSensorControllerEnable.TabIndex = 3;
            this.radSensorControllerEnable.TabStop = true;
            this.radSensorControllerEnable.Text = "Enable";
            this.radSensorControllerEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSensorControllerEnable.UseVisualStyleBackColor = false;
            // 
            // radSensorControllerDisable
            // 
            this.radSensorControllerDisable.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSensorControllerDisable.BackColor = System.Drawing.Color.White;
            this.radSensorControllerDisable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSensorControllerDisable.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.radSensorControllerDisable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radSensorControllerDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSensorControllerDisable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radSensorControllerDisable.Location = new System.Drawing.Point(219, 0);
            this.radSensorControllerDisable.Margin = new System.Windows.Forms.Padding(0);
            this.radSensorControllerDisable.Name = "radSensorControllerDisable";
            this.radSensorControllerDisable.Size = new System.Drawing.Size(220, 38);
            this.radSensorControllerDisable.TabIndex = 4;
            this.radSensorControllerDisable.TabStop = true;
            this.radSensorControllerDisable.Text = "Disable";
            this.radSensorControllerDisable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSensorControllerDisable.UseVisualStyleBackColor = false;
            // 
            // numSensorControllerPort
            // 
            this.numSensorControllerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSensorControllerPort.Location = new System.Drawing.Point(263, 142);
            this.numSensorControllerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSensorControllerPort.Name = "numSensorControllerPort";
            this.numSensorControllerPort.Size = new System.Drawing.Size(200, 26);
            this.numSensorControllerPort.TabIndex = 34;
            // 
            // numSensorControllerPulseEncoder
            // 
            this.numSensorControllerPulseEncoder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSensorControllerPulseEncoder.Location = new System.Drawing.Point(497, 142);
            this.numSensorControllerPulseEncoder.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSensorControllerPulseEncoder.Name = "numSensorControllerPulseEncoder";
            this.numSensorControllerPulseEncoder.Size = new System.Drawing.Size(186, 26);
            this.numSensorControllerPulseEncoder.TabIndex = 35;
            // 
            // numSensorControllerEncoderDiameter
            // 
            this.numSensorControllerEncoderDiameter.DecimalPlaces = 2;
            this.numSensorControllerEncoderDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSensorControllerEncoderDiameter.Location = new System.Drawing.Point(743, 143);
            this.numSensorControllerEncoderDiameter.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numSensorControllerEncoderDiameter.Name = "numSensorControllerEncoderDiameter";
            this.numSensorControllerEncoderDiameter.Size = new System.Drawing.Size(186, 26);
            this.numSensorControllerEncoderDiameter.TabIndex = 36;
            // 
            // lblSensorControllerDelayBefore
            // 
            this.lblSensorControllerDelayBefore.AutoSize = true;
            this.lblSensorControllerDelayBefore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerDelayBefore.Location = new System.Drawing.Point(493, 33);
            this.lblSensorControllerDelayBefore.Name = "lblSensorControllerDelayBefore";
            this.lblSensorControllerDelayBefore.Size = new System.Drawing.Size(101, 20);
            this.lblSensorControllerDelayBefore.TabIndex = 37;
            this.lblSensorControllerDelayBefore.Text = "Delay sensor";
            // 
            // lblSensorControllerDelayAfter
            // 
            this.lblSensorControllerDelayAfter.AutoSize = true;
            this.lblSensorControllerDelayAfter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensorControllerDelayAfter.Location = new System.Drawing.Point(739, 33);
            this.lblSensorControllerDelayAfter.Name = "lblSensorControllerDelayAfter";
            this.lblSensorControllerDelayAfter.Size = new System.Drawing.Size(114, 20);
            this.lblSensorControllerDelayAfter.TabIndex = 38;
            this.lblSensorControllerDelayAfter.Text = "Disable sensor";
            // 
            // numSensorControllerDelayBefore
            // 
            this.numSensorControllerDelayBefore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSensorControllerDelayBefore.Location = new System.Drawing.Point(497, 60);
            this.numSensorControllerDelayBefore.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSensorControllerDelayBefore.Name = "numSensorControllerDelayBefore";
            this.numSensorControllerDelayBefore.Size = new System.Drawing.Size(186, 26);
            this.numSensorControllerDelayBefore.TabIndex = 39;
            // 
            // numSensorControllerDelayAfter
            // 
            this.numSensorControllerDelayAfter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSensorControllerDelayAfter.Location = new System.Drawing.Point(743, 60);
            this.numSensorControllerDelayAfter.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSensorControllerDelayAfter.Name = "numSensorControllerDelayAfter";
            this.numSensorControllerDelayAfter.Size = new System.Drawing.Size(186, 26);
            this.numSensorControllerDelayAfter.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(689, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "ppr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(689, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 42;
            this.label2.Text = "mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(935, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(935, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "mm";
            // 
            // lblContentResponse
            // 
            this.lblContentResponse.AutoSize = true;
            this.lblContentResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentResponse.Location = new System.Drawing.Point(21, 250);
            this.lblContentResponse.Name = "lblContentResponse";
            this.lblContentResponse.Size = new System.Drawing.Size(136, 20);
            this.lblContentResponse.TabIndex = 48;
            this.lblContentResponse.Text = "Content response";
            // 
            // richTXTContentResponse
            // 
            this.richTXTContentResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTXTContentResponse.Location = new System.Drawing.Point(25, 278);
            this.richTXTContentResponse.Name = "richTXTContentResponse";
            this.richTXTContentResponse.ReadOnly = true;
            this.richTXTContentResponse.Size = new System.Drawing.Size(438, 160);
            this.richTXTContentResponse.TabIndex = 49;
            this.richTXTContentResponse.Text = "";
            // 
            // btnContenResponseClear
            // 
            this.btnContenResponseClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.btnContenResponseClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContenResponseClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContenResponseClear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnContenResponseClear.Location = new System.Drawing.Point(277, 215);
            this.btnContenResponseClear.Name = "btnContenResponseClear";
            this.btnContenResponseClear.Size = new System.Drawing.Size(186, 34);
            this.btnContenResponseClear.TabIndex = 50;
            this.btnContenResponseClear.Text = "Clear";
            this.btnContenResponseClear.UseVisualStyleBackColor = true;
            // 
            // grbSensorController
            // 
            this.grbSensorController.BackColor = System.Drawing.Color.White;
            this.grbSensorController.Controls.Add(this.btnContenResponseClear);
            this.grbSensorController.Controls.Add(this.richTXTContentResponse);
            this.grbSensorController.Controls.Add(this.lblContentResponse);
            this.grbSensorController.Controls.Add(this.label4);
            this.grbSensorController.Controls.Add(this.label3);
            this.grbSensorController.Controls.Add(this.label2);
            this.grbSensorController.Controls.Add(this.label1);
            this.grbSensorController.Controls.Add(this.numSensorControllerDelayAfter);
            this.grbSensorController.Controls.Add(this.numSensorControllerDelayBefore);
            this.grbSensorController.Controls.Add(this.lblSensorControllerDelayAfter);
            this.grbSensorController.Controls.Add(this.lblSensorControllerDelayBefore);
            this.grbSensorController.Controls.Add(this.numSensorControllerEncoderDiameter);
            this.grbSensorController.Controls.Add(this.numSensorControllerPulseEncoder);
            this.grbSensorController.Controls.Add(this.numSensorControllerPort);
            this.grbSensorController.Controls.Add(this.tableLayoutPanel5);
            this.grbSensorController.Controls.Add(this.txtSensorControllerIP);
            this.grbSensorController.Controls.Add(this.lblSensorControllerEncoderDiameter);
            this.grbSensorController.Controls.Add(this.lblSensorControllerPulseEncoder);
            this.grbSensorController.Controls.Add(this.lblSensorControllerPort);
            this.grbSensorController.Controls.Add(this.lblSensorControllerIP);
            this.grbSensorController.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSensorController.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.grbSensorController.Location = new System.Drawing.Point(0, 0);
            this.grbSensorController.Name = "grbSensorController";
            this.grbSensorController.Size = new System.Drawing.Size(990, 514);
            this.grbSensorController.TabIndex = 21;
            this.grbSensorController.TabStop = false;
            this.grbSensorController.Text = "Sensor controller";
            // 
            // ucSensorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbSensorController);
            this.Name = "ucSensorSettings";
            this.Size = new System.Drawing.Size(990, 514);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerPulseEncoder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerEncoderDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerDelayBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSensorControllerDelayAfter)).EndInit();
            this.grbSensorController.ResumeLayout(false);
            this.grbSensorController.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSensorControllerIP;
        private System.Windows.Forms.Label lblSensorControllerPort;
        private System.Windows.Forms.Label lblSensorControllerPulseEncoder;
        private System.Windows.Forms.Label lblSensorControllerEncoderDiameter;
        private IPAddressControlLib.IPAddressControl txtSensorControllerIP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.RadioButton radSensorControllerEnable;
        private System.Windows.Forms.RadioButton radSensorControllerDisable;
        private System.Windows.Forms.NumericUpDown numSensorControllerPort;
        private System.Windows.Forms.NumericUpDown numSensorControllerPulseEncoder;
        private System.Windows.Forms.NumericUpDown numSensorControllerEncoderDiameter;
        private System.Windows.Forms.Label lblSensorControllerDelayBefore;
        private System.Windows.Forms.Label lblSensorControllerDelayAfter;
        private System.Windows.Forms.NumericUpDown numSensorControllerDelayBefore;
        private System.Windows.Forms.NumericUpDown numSensorControllerDelayAfter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblContentResponse;
        private System.Windows.Forms.RichTextBox richTXTContentResponse;
        private System.Windows.Forms.Button btnContenResponseClear;
        private System.Windows.Forms.GroupBox grbSensorController;
    }
}
