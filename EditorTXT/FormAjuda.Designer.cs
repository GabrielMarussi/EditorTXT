﻿
namespace EditorTXT
{
    partial class FormAjuda
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
        /// the contents of this m  ethod with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Webrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Webrowser
            // 
            this.Webrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Webrowser.Location = new System.Drawing.Point(0, 0);
            this.Webrowser.Name = "Webrowser";
            this.Webrowser.Size = new System.Drawing.Size(800, 450);
            this.Webrowser.TabIndex = 0;
            // 
            // FormAjuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Webrowser);
            this.Name = "FormAjuda";
            this.Text = "FormAjuda";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Webrowser;
    }
}