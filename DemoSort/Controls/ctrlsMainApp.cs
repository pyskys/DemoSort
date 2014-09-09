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
        bool bShowPanelElements = true;
        bool bShowSort1 = false;

        //Lưu control muốn tạo hiệu ứng
        Control ctrlsEff;

        //Lưu vị trí hiện hành của control tạo hiệu ứng
        Point pEff;

        List<Elements> lstElementsArray = new List<Elements>();
        public ctrlsMainApp()
        {
            InitializeComponent();
            ctrlsEff = label1;
            pEff = label1.Location;
             // Điều chỉnh các panel về vị trí thích hợp
             pnlBubble.Location = new Point(this.Width -120, pnlBubble.Location.Y);
             pnlElements.Location = new Point(this.Width - 148, this.Height - 135);
             pnlSort2.Location = new Point(pnlSort1.Location.X, this.Height - 108);

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
              lstBtns.Add(btnElements);
              foreach (Button item in lstBtns)
              {
                  item.MouseHover += item_MouseHover;
                  item.MouseLeave += item_MouseLeave;
              }

              this.SizeChanged += ctrlsMainApp_SizeChanged;

              // Xử lý cho các icon sort 1 
              foreach (Control sort_item in pnlSort1.Controls)
              {
                  if (sort_item.GetType() == typeof(Label))
                  {
                      String str = (String)sort_item.Tag;
                      //Chỉ xử lý cho những label có hình ảnh (icon sort)
                      if (str != null)
                          sort_item.Cursor = System.Windows.Forms.Cursors.Hand;
                      sort_item.MouseClick += sort_item_MouseClick;
                      sort_item.MouseHover += sort_item_MouseHover;
                      sort_item.MouseLeave += sort_item_MouseLeave;
                  }
              }
              // Xử lý cho các icon sort 2 
              foreach (Control sort_item in pnlSort2.Controls)
              {
                  if (sort_item.GetType() == typeof(Label))
                  {
                      String str = (String)sort_item.Tag;
                      //Chỉ xử lý cho những label có hình ảnh (icon sort)
                      if (str != null)
                         sort_item.Cursor = System.Windows.Forms.Cursors.Hand;

                      sort_item.MouseClick += sort_item_MouseClick;
                      sort_item.MouseHover += sort_item_MouseHover;
                      sort_item.MouseLeave += sort_item_MouseLeave;
                  }
              }
            //////////////////////////////////////////////////////////////
        }
        void ctrlsMainApp_SizeChanged(object sender, EventArgs e)
        {
            //Sort 2 đang show
            if (bShowSort1)
                //Cập nhặt vị trí hiện tại cho Sort 2
                pnlSort2.Location = new Point(pnlSort2.Location.X, btnSort1.Location.Y + 22);
        }

        private void sort_item_MouseLeave(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            String str = (String)lb.Tag;

            //Chỉ xử lý cho những label có hình ảnh (icon sort)
            if (str == null) return;
            lb.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void sort_item_MouseHover(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            String str = (String)lb.Tag;

            //Chỉ xử lý cho những label có hình ảnh (icon sort)
            if (str == null) return;

            lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void sort_item_MouseClick(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            String str = (String)lb.Tag;

            //Chỉ xử lý cho những label có hình ảnh (icon sort)
            if (str == null) return;

            //Tách 2 chuỗi ra
            String[] arrstr = str.Split('_');
            lbTextSort.Text = arrstr[0];

            lbIconSmall.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(arrstr[1]);
            ctrlsEff = lb;
            pEff = lb.Location;
        }
        private void item_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.Green;
        }

        private void item_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.Indigo;
        }
        private void item_MouseClick(object sender, MouseEventArgs e)
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
                Commons.MovePanelDetail(pnlBubble, this.Width - 450, pnlBubble.Location.Y,10,50,1,10,10);
            else
                //Di chuyển vô
                Commons.MovePanelDetail(pnlBubble, this.Width - 120, pnlBubble.Location.Y,10,50,1,10,10);

            bShowPanelBubble = !bShowPanelBubble;
        }
        private void btnSort1_Click(object sender, EventArgs e)
        {
            Button btn = (Button) sender;
            Commons.ColorClickEffect(btn, btn.BackColor, Color.DarkOrange);

            //Nếu Sort 2 đang show
            if (bShowSort1)
            {
                //Ẩn hình ảnh trên Sort 2 đi chống lag hình ảnh bên Sort 1
                foreach (Control item in pnlSort2.Controls)
                {
                    if (item.GetType() == typeof(Label))
                        item.Visible = false;
                }

                //Move Sort 2 vào
                Commons.MovePanelDetail(pnlSort2, pnlSort2.Location.X, this.Height - 108, 20, 0, 1, 20, 20);
                bShowSort1 = false;

                new Thread(() =>
                {
                    //Hiện hình ảnh trên Sort 2 ra
                    Thread.Sleep(1000);
                    foreach (Control item in pnlSort2.Controls)
                    {
                        if (item.GetType() == typeof(Label))
                            item.Visible = true;
                    }
                }).Start();
            }
        }

        private void btnSort2_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Commons.ColorClickEffect(btn, btn.BackColor, Color.DarkOrange);

        }

        private void btnElements_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Image = Properties.Resources.btn2;
        }

        private void btnElements_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Image = Properties.Resources.btn1;
        }

        private void btnElements_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Commons.ImageClickEffect(btn, Properties.Resources.btn1, Properties.Resources.btn3);

            if (bShowPanelElements)
                //Duy chuyển ra
                Commons.MovePanel(pnlElements, this.Width - 271, pnlElements.Location.Y, 5);
            else
                //Di chuyển vô
                Commons.MovePanel(pnlElements, this.Width - 148, pnlElements.Location.Y, 5);

            bShowPanelElements = !bShowPanelElements;
        }

        private void btnSort2_Click(object sender, EventArgs e)
        {
            if (!bShowSort1)
            { 
                //Nếu Sort 1 đang show
                //Move Sort 2 ra
                Commons.MovePanelDetail(pnlSort2, pnlSort2.Location.X, btnSort1.Location.Y + 22, 20, 30, 1, 20, 20);
                bShowSort1 = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Generate array data from your file!");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Generate array data by press key data!");
        }
        int iNext = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ctrlsEff == null) return;

            //Tạo thời gian ngưng bằng cách lặp lại hàm này n-5 lần
            //Trong đây lấy n=15;
            if(iNext>=5)
                iNext++;
            iNext=iNext==15?1:iNext;
            ///////////////////////////////////////////////////////

            switch (iNext)
            { 
                case 1:
                    ctrlsEff.Location = new Point(pEff.X - 1, pEff.Y + 1);
                    iNext = 2;
                    break;
                case 2:
                    ctrlsEff.Location = new Point(pEff.X, pEff.Y);
                    iNext = 3;
                    break;
                case 3:
                    ctrlsEff.Location = new Point(pEff.X + 1, pEff.Y + 1);
                    iNext = 4;
                    break;
                case 4:
                    ctrlsEff.Location = new Point(pEff.X , pEff.Y);
                    iNext = 5;
                    break;
            }
        }
        private List<Elements> lstAllElementApp = new List<Elements>();
        private void RemoveAllElement()
        {
            foreach (Elements item in lstAllElementApp)
            {
                pnlArray.Controls.Remove(item);
            }
            lstAllElementApp.Clear();
            lstElementsArray.Clear();
        }
        public Elements GetElement(String strText, int x, int y,Bitmap bit)
        {
            Elements el = new Elements(strText,
                   x, y,
                   bit
                   );

            //Luu lai de Remove
            lstAllElementApp.Add(el);
            return el;
        }
        Random ran = new Random();
        private int RandomNumber(int iStart, int iEnd)
        {
            
            return ran.Next(iStart, iEnd);
        }
        private void btnRandoms_Click(object sender, EventArgs e)
        {
            //Xóa các phần tử củ trước
            RemoveAllElement();

            int iNumber = int.Parse(txtSize.Text.Trim());
            int iXArray = pnlArray.Width/2 -150;
            int iYArray = pnlArray.Height / 2 - 100;
            for (int i = 0; i < iNumber; i++)
            {
                int iRandom = RandomNumber(0,i*3);
                Elements el = GetElement(iRandom.ToString(), iXArray, iYArray, (Bitmap)lbBubble.Image);
                iXArray += 40;

                lstElementsArray.Add(el);
                pnlArray.Controls.Add(el);
            }
        }
    }
}