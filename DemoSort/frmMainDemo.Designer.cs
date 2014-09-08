namespace DemoSort
{
    partial class frmMainDemo
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
            this.ctrlsMainApp1 = new DemoSort.Controls.ctrlsMainApp();
            this.SuspendLayout();
            // 
            // ctrlsMainApp1
            // 
            this.ctrlsMainApp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlsMainApp1.Location = new System.Drawing.Point(0, 0);
            this.ctrlsMainApp1.Name = "ctrlsMainApp1";
            this.ctrlsMainApp1.Size = new System.Drawing.Size(610, 594);
            this.ctrlsMainApp1.TabIndex = 0;
            // 
            // frmMainDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 594);
            this.Controls.Add(this.ctrlsMainApp1);
            this.Name = "frmMainDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort Demo";
            this.Load += new System.EventHandler(this.frmMainDemo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMainDemo_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlsMainApp ctrlsMainApp1;

    }
}

