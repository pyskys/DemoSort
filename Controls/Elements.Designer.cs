namespace DemoSort.Controls
{
    partial class Elements
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
            this.pnlImage = new System.Windows.Forms.Panel();
            this.lbText = new System.Windows.Forms.Label();
            this.pnlImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImage
            // 
            this.pnlImage.BackgroundImage = global::DemoSort.Properties.Resources.ball2;
            this.pnlImage.Controls.Add(this.lbText);
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(40, 40);
            this.pnlImage.TabIndex = 1;
            // 
            // lbText
            // 
            this.lbText.BackColor = System.Drawing.Color.Transparent;
            this.lbText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbText.ForeColor = System.Drawing.Color.White;
            this.lbText.Location = new System.Drawing.Point(3, -1);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(38, 40);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "12";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbText.Click += new System.EventHandler(this.lbText_Click);
            this.lbText.DoubleClick += new System.EventHandler(this.lbText_DoubleClick);
            // 
            // Elements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pnlImage);
            this.Name = "Elements";
            this.Size = new System.Drawing.Size(40, 40);
            this.Load += new System.EventHandler(this.Elements_Load);
            this.pnlImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Label lbText;
    }
}
