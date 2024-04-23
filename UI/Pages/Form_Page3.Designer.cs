namespace G_MBIVautoTester.UI.Pages
{
    partial class Form_Page3
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
            this.btn_openLabjack = new System.Windows.Forms.Button();
            this.btn_openSerial = new System.Windows.Forms.Button();
            this.lbl_LabjackStatus = new System.Windows.Forms.Label();
            this.lbl_MBSerialStatus = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_RX = new System.Windows.Forms.Label();
            this.lbl_TX = new System.Windows.Forms.Label();
            this.cb_cmd_diosafety_9 = new System.Windows.Forms.CheckBox();
            this.LBL_AD0 = new System.Windows.Forms.Label();
            this.lbl_AD11 = new System.Windows.Forms.Label();
            this.lbl_AD16 = new System.Windows.Forms.Label();
            this.LBL_AD9 = new System.Windows.Forms.Label();
            this.lbl_AD10 = new System.Windows.Forms.Label();
            this.lbl_AD15 = new System.Windows.Forms.Label();
            this.lbl_AD14 = new System.Windows.Forms.Label();
            this.lbl_AD13 = new System.Windows.Forms.Label();
            this.LBL_AD7 = new System.Windows.Forms.Label();
            this.lbl_AD12 = new System.Windows.Forms.Label();
            this.LBL_AD8 = new System.Windows.Forms.Label();
            this.LBL_AD1 = new System.Windows.Forms.Label();
            this.LBL_AD5 = new System.Windows.Forms.Label();
            this.LBL_AD6 = new System.Windows.Forms.Label();
            this.LBL_AD4 = new System.Windows.Forms.Label();
            this.LBL_AD2 = new System.Windows.Forms.Label();
            this.LBL_AD3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_curVal = new System.Windows.Forms.Label();
            this.lbl_DAC0 = new System.Windows.Forms.Label();
            this.tkb_DAC0 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_EIO4 = new System.Windows.Forms.CheckBox();
            this.cb_EIO2 = new System.Windows.Forms.CheckBox();
            this.cb_EIO5 = new System.Windows.Forms.CheckBox();
            this.cb_EIO3 = new System.Windows.Forms.CheckBox();
            this.lbl_calcMinmax = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_DAC0)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_openLabjack
            // 
            this.btn_openLabjack.Location = new System.Drawing.Point(264, 37);
            this.btn_openLabjack.Name = "btn_openLabjack";
            this.btn_openLabjack.Size = new System.Drawing.Size(131, 39);
            this.btn_openLabjack.TabIndex = 562;
            this.btn_openLabjack.Text = "lab on";
            this.btn_openLabjack.UseVisualStyleBackColor = true;
            // 
            // btn_openSerial
            // 
            this.btn_openSerial.Location = new System.Drawing.Point(12, 37);
            this.btn_openSerial.Name = "btn_openSerial";
            this.btn_openSerial.Size = new System.Drawing.Size(141, 39);
            this.btn_openSerial.TabIndex = 560;
            this.btn_openSerial.Text = "open";
            this.btn_openSerial.UseVisualStyleBackColor = true;
            // 
            // lbl_LabjackStatus
            // 
            this.lbl_LabjackStatus.AutoSize = true;
            this.lbl_LabjackStatus.BackColor = System.Drawing.Color.IndianRed;
            this.lbl_LabjackStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_LabjackStatus.Location = new System.Drawing.Point(264, 79);
            this.lbl_LabjackStatus.Name = "lbl_LabjackStatus";
            this.lbl_LabjackStatus.Size = new System.Drawing.Size(131, 25);
            this.lbl_LabjackStatus.TabIndex = 559;
            this.lbl_LabjackStatus.Text = "LabjackCom";
            // 
            // lbl_MBSerialStatus
            // 
            this.lbl_MBSerialStatus.AutoSize = true;
            this.lbl_MBSerialStatus.BackColor = System.Drawing.Color.IndianRed;
            this.lbl_MBSerialStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_MBSerialStatus.Location = new System.Drawing.Point(18, 79);
            this.lbl_MBSerialStatus.Name = "lbl_MBSerialStatus";
            this.lbl_MBSerialStatus.Size = new System.Drawing.Size(135, 25);
            this.lbl_MBSerialStatus.TabIndex = 558;
            this.lbl_MBSerialStatus.Text = "SerialCom   .";
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(30, 9);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(97, 25);
            this.lbl_version.TabIndex = 557;
            this.lbl_version.Text = "Version: ";
            // 
            // lbl_RX
            // 
            this.lbl_RX.AutoSize = true;
            this.lbl_RX.Location = new System.Drawing.Point(553, 9);
            this.lbl_RX.Name = "lbl_RX";
            this.lbl_RX.Size = new System.Drawing.Size(30, 25);
            this.lbl_RX.TabIndex = 556;
            this.lbl_RX.Text = "rx";
            // 
            // lbl_TX
            // 
            this.lbl_TX.AutoSize = true;
            this.lbl_TX.Location = new System.Drawing.Point(2143, 9);
            this.lbl_TX.Name = "lbl_TX";
            this.lbl_TX.Size = new System.Drawing.Size(29, 25);
            this.lbl_TX.TabIndex = 555;
            this.lbl_TX.Text = "tx";
            this.lbl_TX.Visible = false;
            // 
            // cb_cmd_diosafety_9
            // 
            this.cb_cmd_diosafety_9.AutoSize = true;
            this.cb_cmd_diosafety_9.Checked = true;
            this.cb_cmd_diosafety_9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_cmd_diosafety_9.Enabled = false;
            this.cb_cmd_diosafety_9.Location = new System.Drawing.Point(170, 78);
            this.cb_cmd_diosafety_9.Margin = new System.Windows.Forms.Padding(4);
            this.cb_cmd_diosafety_9.Name = "cb_cmd_diosafety_9";
            this.cb_cmd_diosafety_9.Size = new System.Drawing.Size(88, 29);
            this.cb_cmd_diosafety_9.TabIndex = 554;
            this.cb_cmd_diosafety_9.Text = "Safe";
            this.cb_cmd_diosafety_9.UseVisualStyleBackColor = true;
            this.cb_cmd_diosafety_9.Visible = false;
            // 
            // LBL_AD0
            // 
            this.LBL_AD0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD0.AutoSize = true;
            this.LBL_AD0.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD0.Location = new System.Drawing.Point(3, 96);
            this.LBL_AD0.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD0.Name = "LBL_AD0";
            this.LBL_AD0.Size = new System.Drawing.Size(130, 33);
            this.LBL_AD0.TabIndex = 579;
            this.LBL_AD0.Text = "cur MV val";
            this.LBL_AD0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD11
            // 
            this.lbl_AD11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD11.AutoSize = true;
            this.lbl_AD11.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD11.Location = new System.Drawing.Point(3, 886);
            this.lbl_AD11.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD11.Name = "lbl_AD11";
            this.lbl_AD11.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD11.TabIndex = 574;
            this.lbl_AD11.Text = "AD11: 1000";
            this.lbl_AD11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD16
            // 
            this.lbl_AD16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD16.AutoSize = true;
            this.lbl_AD16.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD16.Location = new System.Drawing.Point(3, 1246);
            this.lbl_AD16.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD16.Name = "lbl_AD16";
            this.lbl_AD16.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD16.TabIndex = 578;
            this.lbl_AD16.Text = "AD16: 1000";
            this.lbl_AD16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD9
            // 
            this.LBL_AD9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD9.AutoSize = true;
            this.LBL_AD9.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD9.Location = new System.Drawing.Point(3, 742);
            this.LBL_AD9.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD9.Name = "LBL_AD9";
            this.LBL_AD9.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD9.TabIndex = 571;
            this.LBL_AD9.Text = "AD09: 1000";
            this.LBL_AD9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD10
            // 
            this.lbl_AD10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD10.AutoSize = true;
            this.lbl_AD10.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD10.Location = new System.Drawing.Point(3, 814);
            this.lbl_AD10.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD10.Name = "lbl_AD10";
            this.lbl_AD10.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD10.TabIndex = 572;
            this.lbl_AD10.Text = "AD10: 1000";
            this.lbl_AD10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD15
            // 
            this.lbl_AD15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD15.AutoSize = true;
            this.lbl_AD15.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD15.Location = new System.Drawing.Point(3, 1174);
            this.lbl_AD15.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD15.Name = "lbl_AD15";
            this.lbl_AD15.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD15.TabIndex = 577;
            this.lbl_AD15.Text = "AD15: 1000";
            this.lbl_AD15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD14
            // 
            this.lbl_AD14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD14.AutoSize = true;
            this.lbl_AD14.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD14.Location = new System.Drawing.Point(3, 1102);
            this.lbl_AD14.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD14.Name = "lbl_AD14";
            this.lbl_AD14.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD14.TabIndex = 576;
            this.lbl_AD14.Text = "AD14: 1000";
            this.lbl_AD14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD13
            // 
            this.lbl_AD13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD13.AutoSize = true;
            this.lbl_AD13.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD13.Location = new System.Drawing.Point(3, 1030);
            this.lbl_AD13.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD13.Name = "lbl_AD13";
            this.lbl_AD13.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD13.TabIndex = 575;
            this.lbl_AD13.Text = "AD13: 1000";
            this.lbl_AD13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD7
            // 
            this.LBL_AD7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD7.AutoSize = true;
            this.LBL_AD7.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD7.Location = new System.Drawing.Point(3, 598);
            this.LBL_AD7.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD7.Name = "LBL_AD7";
            this.LBL_AD7.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD7.TabIndex = 569;
            this.LBL_AD7.Text = "AD07: 1000";
            this.LBL_AD7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AD12
            // 
            this.lbl_AD12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AD12.AutoSize = true;
            this.lbl_AD12.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AD12.Location = new System.Drawing.Point(3, 958);
            this.lbl_AD12.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_AD12.Name = "lbl_AD12";
            this.lbl_AD12.Size = new System.Drawing.Size(145, 33);
            this.lbl_AD12.TabIndex = 573;
            this.lbl_AD12.Text = "AD12: 1000";
            this.lbl_AD12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD8
            // 
            this.LBL_AD8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD8.AutoSize = true;
            this.LBL_AD8.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD8.Location = new System.Drawing.Point(3, 670);
            this.LBL_AD8.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD8.Name = "LBL_AD8";
            this.LBL_AD8.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD8.TabIndex = 570;
            this.LBL_AD8.Text = "AD08: 1000";
            this.LBL_AD8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD1
            // 
            this.LBL_AD1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD1.AutoSize = true;
            this.LBL_AD1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD1.Location = new System.Drawing.Point(3, 166);
            this.LBL_AD1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD1.Name = "LBL_AD1";
            this.LBL_AD1.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD1.TabIndex = 563;
            this.LBL_AD1.Text = "AD01: 1000";
            this.LBL_AD1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD5
            // 
            this.LBL_AD5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD5.AutoSize = true;
            this.LBL_AD5.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD5.Location = new System.Drawing.Point(3, 454);
            this.LBL_AD5.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD5.Name = "LBL_AD5";
            this.LBL_AD5.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD5.TabIndex = 567;
            this.LBL_AD5.Text = "AD05: 1000";
            this.LBL_AD5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD6
            // 
            this.LBL_AD6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD6.AutoSize = true;
            this.LBL_AD6.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD6.Location = new System.Drawing.Point(3, 526);
            this.LBL_AD6.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD6.Name = "LBL_AD6";
            this.LBL_AD6.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD6.TabIndex = 568;
            this.LBL_AD6.Text = "AD06: 1000";
            this.LBL_AD6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD4
            // 
            this.LBL_AD4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD4.AutoSize = true;
            this.LBL_AD4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD4.Location = new System.Drawing.Point(3, 382);
            this.LBL_AD4.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD4.Name = "LBL_AD4";
            this.LBL_AD4.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD4.TabIndex = 565;
            this.LBL_AD4.Text = "AD04: 1000";
            this.LBL_AD4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD2
            // 
            this.LBL_AD2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD2.AutoSize = true;
            this.LBL_AD2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD2.Location = new System.Drawing.Point(3, 238);
            this.LBL_AD2.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD2.Name = "LBL_AD2";
            this.LBL_AD2.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD2.TabIndex = 564;
            this.LBL_AD2.Text = "AD02: 1000";
            this.LBL_AD2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_AD3
            // 
            this.LBL_AD3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_AD3.AutoSize = true;
            this.LBL_AD3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_AD3.Location = new System.Drawing.Point(3, 310);
            this.LBL_AD3.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.LBL_AD3.Name = "LBL_AD3";
            this.LBL_AD3.Size = new System.Drawing.Size(145, 33);
            this.LBL_AD3.TabIndex = 566;
            this.LBL_AD3.Text = "AD03: 1000";
            this.LBL_AD3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LBL_AD0);
            this.groupBox1.Controls.Add(this.LBL_AD3);
            this.groupBox1.Controls.Add(this.lbl_AD11);
            this.groupBox1.Controls.Add(this.LBL_AD2);
            this.groupBox1.Controls.Add(this.lbl_AD16);
            this.groupBox1.Controls.Add(this.LBL_AD4);
            this.groupBox1.Controls.Add(this.LBL_AD9);
            this.groupBox1.Controls.Add(this.LBL_AD6);
            this.groupBox1.Controls.Add(this.lbl_AD10);
            this.groupBox1.Controls.Add(this.LBL_AD5);
            this.groupBox1.Controls.Add(this.lbl_AD15);
            this.groupBox1.Controls.Add(this.LBL_AD1);
            this.groupBox1.Controls.Add(this.lbl_AD14);
            this.groupBox1.Controls.Add(this.LBL_AD8);
            this.groupBox1.Controls.Add(this.lbl_AD13);
            this.groupBox1.Controls.Add(this.lbl_AD12);
            this.groupBox1.Controls.Add(this.LBL_AD7);
            this.groupBox1.Location = new System.Drawing.Point(196, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 1304);
            this.groupBox1.TabIndex = 580;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RX values AIN";
            // 
            // lbl_curVal
            // 
            this.lbl_curVal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_curVal.AutoSize = true;
            this.lbl_curVal.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_curVal.Location = new System.Drawing.Point(552, 221);
            this.lbl_curVal.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_curVal.Name = "lbl_curVal";
            this.lbl_curVal.Size = new System.Drawing.Size(86, 33);
            this.lbl_curVal.TabIndex = 580;
            this.lbl_curVal.Text = "cur val";
            this.lbl_curVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DAC0
            // 
            this.lbl_DAC0.AutoSize = true;
            this.lbl_DAC0.Location = new System.Drawing.Point(1720, 309);
            this.lbl_DAC0.Name = "lbl_DAC0";
            this.lbl_DAC0.Size = new System.Drawing.Size(70, 25);
            this.lbl_DAC0.TabIndex = 583;
            this.lbl_DAC0.Text = "label1";
            // 
            // tkb_DAC0
            // 
            this.tkb_DAC0.Location = new System.Drawing.Point(1150, 297);
            this.tkb_DAC0.Maximum = 500;
            this.tkb_DAC0.Name = "tkb_DAC0";
            this.tkb_DAC0.Size = new System.Drawing.Size(553, 90);
            this.tkb_DAC0.TabIndex = 582;
            this.tkb_DAC0.Value = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_EIO4);
            this.groupBox3.Controls.Add(this.cb_EIO2);
            this.groupBox3.Controls.Add(this.cb_EIO5);
            this.groupBox3.Controls.Add(this.cb_EIO3);
            this.groupBox3.Location = new System.Drawing.Point(1272, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(489, 100);
            this.groupBox3.TabIndex = 581;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // cb_EIO4
            // 
            this.cb_EIO4.AutoSize = true;
            this.cb_EIO4.Location = new System.Drawing.Point(149, 64);
            this.cb_EIO4.Margin = new System.Windows.Forms.Padding(4);
            this.cb_EIO4.Name = "cb_EIO4";
            this.cb_EIO4.Size = new System.Drawing.Size(91, 29);
            this.cb_EIO4.TabIndex = 446;
            this.cb_EIO4.Text = "EIO4";
            this.cb_EIO4.UseVisualStyleBackColor = true;
            // 
            // cb_EIO2
            // 
            this.cb_EIO2.AutoSize = true;
            this.cb_EIO2.Location = new System.Drawing.Point(398, 64);
            this.cb_EIO2.Margin = new System.Windows.Forms.Padding(4);
            this.cb_EIO2.Name = "cb_EIO2";
            this.cb_EIO2.Size = new System.Drawing.Size(91, 29);
            this.cb_EIO2.TabIndex = 430;
            this.cb_EIO2.Text = "EIO2";
            this.cb_EIO2.UseVisualStyleBackColor = true;
            // 
            // cb_EIO5
            // 
            this.cb_EIO5.AutoSize = true;
            this.cb_EIO5.Location = new System.Drawing.Point(37, 64);
            this.cb_EIO5.Margin = new System.Windows.Forms.Padding(4);
            this.cb_EIO5.Name = "cb_EIO5";
            this.cb_EIO5.Size = new System.Drawing.Size(91, 29);
            this.cb_EIO5.TabIndex = 447;
            this.cb_EIO5.Text = "EIO5";
            this.cb_EIO5.UseVisualStyleBackColor = true;
            // 
            // cb_EIO3
            // 
            this.cb_EIO3.AutoSize = true;
            this.cb_EIO3.Location = new System.Drawing.Point(271, 64);
            this.cb_EIO3.Margin = new System.Windows.Forms.Padding(4);
            this.cb_EIO3.Name = "cb_EIO3";
            this.cb_EIO3.Size = new System.Drawing.Size(91, 29);
            this.cb_EIO3.TabIndex = 445;
            this.cb_EIO3.Text = "EIO3";
            this.cb_EIO3.UseVisualStyleBackColor = true;
            // 
            // lbl_calcMinmax
            // 
            this.lbl_calcMinmax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_calcMinmax.AutoSize = true;
            this.lbl_calcMinmax.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_calcMinmax.Location = new System.Drawing.Point(719, 221);
            this.lbl_calcMinmax.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lbl_calcMinmax.Name = "lbl_calcMinmax";
            this.lbl_calcMinmax.Size = new System.Drawing.Size(86, 33);
            this.lbl_calcMinmax.TabIndex = 584;
            this.lbl_calcMinmax.Text = "cur val";
            this.lbl_calcMinmax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(452, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 39);
            this.button1.TabIndex = 585;
            this.button1.Text = "lab on";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form_Page3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2369, 1496);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_calcMinmax);
            this.Controls.Add(this.lbl_DAC0);
            this.Controls.Add(this.tkb_DAC0);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbl_curVal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_openLabjack);
            this.Controls.Add(this.btn_openSerial);
            this.Controls.Add(this.lbl_LabjackStatus);
            this.Controls.Add(this.lbl_MBSerialStatus);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.lbl_RX);
            this.Controls.Add(this.lbl_TX);
            this.Controls.Add(this.cb_cmd_diosafety_9);
            this.Name = "Form_Page3";
            this.Text = "Form_Page3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_DAC0)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_openLabjack;
        private System.Windows.Forms.Button btn_openSerial;
        private System.Windows.Forms.Label lbl_LabjackStatus;
        private System.Windows.Forms.Label lbl_MBSerialStatus;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_RX;
        private System.Windows.Forms.Label lbl_TX;
        internal System.Windows.Forms.CheckBox cb_cmd_diosafety_9;
        private System.Windows.Forms.Label LBL_AD0;
        private System.Windows.Forms.Label lbl_AD11;
        private System.Windows.Forms.Label lbl_AD16;
        private System.Windows.Forms.Label LBL_AD9;
        private System.Windows.Forms.Label lbl_AD10;
        private System.Windows.Forms.Label lbl_AD15;
        private System.Windows.Forms.Label lbl_AD14;
        private System.Windows.Forms.Label lbl_AD13;
        private System.Windows.Forms.Label LBL_AD7;
        private System.Windows.Forms.Label lbl_AD12;
        private System.Windows.Forms.Label LBL_AD8;
        private System.Windows.Forms.Label LBL_AD1;
        private System.Windows.Forms.Label LBL_AD5;
        private System.Windows.Forms.Label LBL_AD6;
        private System.Windows.Forms.Label LBL_AD4;
        private System.Windows.Forms.Label LBL_AD2;
        private System.Windows.Forms.Label LBL_AD3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_curVal;
        private System.Windows.Forms.Label lbl_DAC0;
        private System.Windows.Forms.TrackBar tkb_DAC0;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox cb_EIO4;
        internal System.Windows.Forms.CheckBox cb_EIO2;
        internal System.Windows.Forms.CheckBox cb_EIO5;
        internal System.Windows.Forms.CheckBox cb_EIO3;
        private System.Windows.Forms.Label lbl_calcMinmax;
        private System.Windows.Forms.Button button1;
    }
}