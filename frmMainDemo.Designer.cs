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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainDemo));
            this.ctrlsMainApp1 = new DemoSort.Controls.ctrlsMainApp();
            this.SuspendLayout();
            // 
            // ctrlsMainApp1
            // 
            this.ctrlsMainApp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlsMainApp1.Location = new System.Drawing.Point(0, 0);
            this.ctrlsMainApp1.Name = "ctrlsMainApp1";
            this.ctrlsMainApp1.Size = new System.Drawing.Size(750, 604);
            this.ctrlsMainApp1.TabIndex = 0;
            this.ctrlsMainApp1.Load += new System.EventHandler(this.ctrlsMainApp1_Load);
            // 
            // frmMainDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 604);
            this.Controls.Add(this.ctrlsMainApp1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainDemo_FormClosing);
            this.Load += new System.EventHandler(this.frmMainDemo_Load);
            this.SizeChanged += new System.EventHandler(this.frmMainDemo_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMainDemo_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlsMainApp ctrlsMainApp1;

    }
}

