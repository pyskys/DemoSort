namespace DemoSort
{
    partial class frmLoadMessage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadMessage));
            this.ritxtExpress = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTextSort = new System.Windows.Forms.Label();
            this.lbIcon = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ritxtExpress
            // 
            this.ritxtExpress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ritxtExpress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.ritxtExpress.ForeColor = System.Drawing.Color.Indigo;
            this.ritxtExpress.Location = new System.Drawing.Point(12, 37);
            this.ritxtExpress.Name = "ritxtExpress";
            this.ritxtExpress.Size = new System.Drawing.Size(427, 206);
            this.ritxtExpress.TabIndex = 0;
            this.ritxtExpress.Text = "";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(446, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(446, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Element is split by \';\'. Example: 1;2;3";
            // 
            // lbTextSort
            // 
            this.lbTextSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTextSort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextSort.ForeColor = System.Drawing.Color.Green;
            this.lbTextSort.Location = new System.Drawing.Point(441, 72);
            this.lbTextSort.Name = "lbTextSort";
            this.lbTextSort.Size = new System.Drawing.Size(111, 23);
            this.lbTextSort.TabIndex = 8;
            this.lbTextSort.Text = "Heap Sort";
            this.lbTextSort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbIcon
            // 
            this.lbIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIcon.BackColor = System.Drawing.Color.Transparent;
            this.lbIcon.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbIcon.Image = global::DemoSort.Properties.Resources.icon5;
            this.lbIcon.Location = new System.Drawing.Point(473, 37);
            this.lbIcon.Name = "lbIcon";
            this.lbIcon.Size = new System.Drawing.Size(43, 30);
            this.lbIcon.TabIndex = 7;
            this.lbIcon.Tag = "Heap Sort_icon5small";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(446, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 40);
            this.button3.TabIndex = 9;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmLoadMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(551, 255);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbTextSort);
            this.Controls.Add(this.lbIcon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ritxtExpress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoadMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadMessage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadMessage_FormClosing);
            this.Load += new System.EventHandler(this.LoadMessage_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmLoadMessage_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ritxtExpress;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTextSort;
        private System.Windows.Forms.Label lbIcon;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}