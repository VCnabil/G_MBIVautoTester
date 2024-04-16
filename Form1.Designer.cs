namespace G_MBIVautoTester
{
    partial class Form1
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
            this.cb_com = new System.Windows.Forms.ComboBox();
            this.btn_ComClose = new System.Windows.Forms.Button();
            this.btn_ComOpen = new System.Windows.Forms.Button();
            this.lbl_com = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_com
            // 
            this.cb_com.FormattingEnabled = true;
            this.cb_com.Location = new System.Drawing.Point(12, 41);
            this.cb_com.Name = "cb_com";
            this.cb_com.Size = new System.Drawing.Size(151, 33);
            this.cb_com.TabIndex = 162;
            // 
            // btn_ComClose
            // 
            this.btn_ComClose.Location = new System.Drawing.Point(12, 114);
            this.btn_ComClose.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ComClose.Name = "btn_ComClose";
            this.btn_ComClose.Size = new System.Drawing.Size(151, 37);
            this.btn_ComClose.TabIndex = 164;
            this.btn_ComClose.Text = "Close";
            this.btn_ComClose.UseVisualStyleBackColor = true;
            // 
            // btn_ComOpen
            // 
            this.btn_ComOpen.Location = new System.Drawing.Point(12, 77);
            this.btn_ComOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ComOpen.Name = "btn_ComOpen";
            this.btn_ComOpen.Size = new System.Drawing.Size(151, 37);
            this.btn_ComOpen.TabIndex = 163;
            this.btn_ComOpen.Text = "Open";
            this.btn_ComOpen.UseVisualStyleBackColor = true;
            // 
            // lbl_com
            // 
            this.lbl_com.AutoSize = true;
            this.lbl_com.Location = new System.Drawing.Point(12, 9);
            this.lbl_com.Name = "lbl_com";
            this.lbl_com.Size = new System.Drawing.Size(101, 25);
            this.lbl_com.TabIndex = 165;
            this.lbl_com.Text = "Com Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1848, 1044);
            this.Controls.Add(this.lbl_com);
            this.Controls.Add(this.btn_ComClose);
            this.Controls.Add(this.btn_ComOpen);
            this.Controls.Add(this.cb_com);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_com;
        private System.Windows.Forms.Button btn_ComClose;
        private System.Windows.Forms.Button btn_ComOpen;
        private System.Windows.Forms.Label lbl_com;
    }
}

