using DemoSort.CommonsData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DemoSort
{
    public partial class frmLoadMessage : Form
    {
        public frmLoadMessage(String strTitle,String strEx,Bitmap bit, String strTextSort,String strInput)
        {
            InitializeComponent();
            this.Text = strTitle;
            ritxtExpress.Text = strEx;
            lbIcon.Image = bit;
            lbTextSort.Text = strTextSort;
            pEffLb = lbIcon.Location;
            strDataInput = strInput;
        }
        int iNext = 1;
        Point pEffLb;
        private String strDataInput = "";
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void LoadMessage_Load(object sender, EventArgs e)
        {

        }

        private void LoadMessage_FormClosing(object sender, FormClosingEventArgs e)
        {  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private LoadMessageDataArgs eVentUserMessage;
        public event ExitLoadMessage UpdateTextMessage;
        public void SetEvent(LoadMessageDataArgs e)
        {
            if (UpdateTextMessage != null)
                UpdateTextMessage(null, e);
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Commons.CheckValidArray(ritxtExpress.Text.Trim().Split(';')) == false)
            {
                MessageBox.Show("Data isn't valid!", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            eVentUserMessage = new LoadMessageDataArgs(ritxtExpress.Text);
            SetEvent(eVentUserMessage);

            //Kick hoat nut Close
            button2.PerformClick();
        }

        private void frmLoadMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Tạo thời gian ngưng bằng cách lặp lại hàm này n-5 lần
            //Trong đây lấy n=15;
            if (iNext >= 5)
                iNext++;
            iNext = iNext == 15 ? 1 : iNext;
            ///////////////////////////////////////////////////////

            switch (iNext)
            {
                case 1:
                    lbIcon.Location = new Point(lbIcon.Location.X - 1, lbIcon.Location.Y + 1);
                    iNext = 2;
                    break;
                case 2:
                    lbIcon.Location = new Point(lbIcon.Location.X + 1, lbIcon.Location.Y - 1);
                    iNext = 3;
                    break;
                case 3:
                    lbIcon.Location = new Point(lbIcon.Location.X + 1, lbIcon.Location.Y + 1);
                    iNext = 4;
                    break;
                case 4:
                    lbIcon.Location = new Point(lbIcon.Location.X-1, lbIcon.Location.Y - 1);
                    iNext = 5;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "DataSorted.txt";
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|All file(*.*)|*.*";
            saveFileDialog1.InitialDirectory = Application.ExecutablePath;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter wr = new StreamWriter(saveFileDialog1.FileName);
                wr.Write("Data Input: " + strDataInput);
                wr.WriteLine();
                wr.Write("Data sorted: " + ritxtExpress.Text);
                wr.Close();

                MessageBox.Show("Save successfully!", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
