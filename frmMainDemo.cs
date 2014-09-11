using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DemoSort
{
    public partial class frmMainDemo : Form
    {
        public frmMainDemo()
        {
            InitializeComponent();

           
        }



        private void frmMainDemo_Load(object sender, EventArgs e)
        {
           
        }

        private void frmMainDemo_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle rec = new Rectangle(panel2.Location.X, panel2.Location.Y, panel2.Width, panel2.Height);
            //LinearGradientBrush brush = new LinearGradientBrush(rec, Color.FromArgb(2, 4, 105), Color.Black, LinearGradientMode.ForwardDiagonal);
            //Graphics g = panel2.CreateGraphics();
            //g.FillRectangle(brush, rec);
            //label1.BackColor = Color.Transparent;
        }

        private void frmMainDemo_SizeChanged(object sender, EventArgs e)
        {
            //ctrlsMainApp1.Width = this.Width;
            //ctrlsMainApp1.Height = this.Height;
        }

        private void ctrlsMainApp1_Load(object sender, EventArgs e)
        {

        }

        private void frmMainDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctrlsMainApp1.ClosingForm();
        }

    }
}
