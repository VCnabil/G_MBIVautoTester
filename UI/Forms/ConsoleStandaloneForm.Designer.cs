namespace G_MBIVautoTester.UI.Forms
{
    partial class ConsoleStandaloneForm
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
            this.textBox_Display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_Display
            // 
            this.textBox_Display.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_Display.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Display.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Display.ForeColor = System.Drawing.Color.Lime;
            this.textBox_Display.Location = new System.Drawing.Point(0, 9);
            this.textBox_Display.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_Display.Multiline = true;
            this.textBox_Display.Name = "textBox_Display";
            this.textBox_Display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Display.Size = new System.Drawing.Size(1974, 876);
            this.textBox_Display.TabIndex = 52;
            this.textBox_Display.Text = "CON";
            // 
            // ConsoleStandaloneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1992, 888);
            this.Controls.Add(this.textBox_Display);
            this.Name = "ConsoleStandaloneForm";
            this.Text = "ConsoleStandaloneForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Display;
    }
}