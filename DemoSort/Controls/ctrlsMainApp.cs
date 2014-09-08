using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DemoSort.CommonsData;
using System.Threading;

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

              //Tạo hiệu ứng Mouse Hover và Leave cho các Button
              List<Button> lstBtns = new List<Button>();
              lstBtns.Add(btnExcute);
              lstBtns.Add(btnPause);
              lstBtns.Add(btnSort1);
              lstBtns.Add(btnRandoms);
              lstBtns.Add(btnSort2);
              foreach (Button item in lstBtns)
              {
                  item.MouseHover += item_MouseHover;
                  item.MouseLeave += item_MouseLeave;
              }
        }

        void item_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.Green;
        }

        void item_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.Indigo;
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

        private void btnSort1_Click(object sender, EventArgs e)
        {
            Button btn = (Button) sender;
            Commons.ColorClickEffect(btn, btn.BackColor, Color.DarkOrange);
        }

        private void btnSort2_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Commons.ColorClickEffect(btn, btn.BackColor, Color.DarkOrange);

        }
    }
}
