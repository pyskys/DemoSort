using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DemoSort.CommonsData;

namespace DemoSort.Controls
{
    public partial class ctrlsMainApp : UserControl
    {

        bool bShowPanelBubble = true;

        public ctrlsMainApp()
        {
            InitializeComponent();
             pnlBubble.Location = new Point(this.Width -120, pnlBubble.Location.Y);

             //Tạo sự kiện Click chuột cho các lable chứa Iamge quả bóng
              foreach (Control item in pnlBubble.Controls)
             {
                 if(item.GetType()==typeof(Label))
                    item.MouseClick += item_MouseClick;
             }
        }
        void item_MouseClick(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            lbBubble.Image = lb.Image;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbBubble_Click(object sender, EventArgs e)
        {
            if (bShowPanelBubble)
                //Duy chuyển ra
                Commons.MovePanel(pnlBubble, this.Width - 371, pnlBubble.Location.Y, 10);
            else
                //Di chuyển vô
                Commons.MovePanel(pnlBubble, this.Width - 120, pnlBubble.Location.Y, 10);

            bShowPanelBubble = !bShowPanelBubble;
        }
    }
}
