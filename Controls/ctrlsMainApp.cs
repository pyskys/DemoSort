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
        bool bShowPanelColor = true;
        bool bShowSort1 = false;
        bool bThreadStart = false;
        private List<Elements> lstAllElementApp = new List<Elements>();
        Random ran = new Random();
        Thread _thread;
        //Next index Timer
        int iNext = 1;
        //Lưu control muốn tạo hiệu ứng
        Control ctrlsEff;
        
        //Lưu vị trí hiện hành của control tạo hiệu ứng
        Point pEff;
        Point pEffLb;

        bool bDataExists = false;
        bool bAscending = true;
        int iSpeed = 10;
        private int icompare = 0;
        private int iswap = 0;
        int iSpacePlus = 50;
        int iNextElement = 40;

        List<Elements> lstElementsArray = new List<Elements>();
        public ctrlsMainApp()
        {
            InitializeComponent();
            ctrlsEff = label1;
            pEff = label1.Location;
            pEffLb = lbIconSmall.Location;

             // Điều chỉnh các panel về vị trí thích hợp
             pnlBubble.Location = new Point(this.Width -120, pnlBubble.Location.Y);
             pnlElements.Location = new Point(this.Width - 148, this.Height - 135);
             pnlColor.Location = new Point(this.Width - 148, this.Height - 135*2+50);
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
              this.Load += ctrlsMainApp_Load;
        }
        public void ClosingForm()
        {
            AbortThreadDemo();
        }
        void ctrlsMainApp_Load(object sender, EventArgs e)
        {
            ShowElementsPanel(btnElements,pnlElements,bShowPanelElements);
            bShowPanelElements = !bShowPanelElements;
        }
        void ctrlsMainApp_SizeChanged(object sender, EventArgs e)
        {
            //Sort 2 đang show
            if (bShowSort1)
                //Cập nhặt vị trí hiện tại cho Sort 2
                pnlSort2.Location = new Point(pnlSort2.Location.X, btnSort1.Location.Y + 22);
            Point pLoca = GetPositionArray();
            ResetLocationInPanel(lstElementsArray, pLoca.X, pLoca.Y);
        }
        private int GetTimeSleep()
        {
            try
            {
                int iNumber = int.Parse(cbbSpeed.Text.Trim());
                return iNumber;
            }
            catch (Exception)
            {
                return iSpeed;
            }
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
            
            foreach (Elements item in lstElementsArray)
            {
                item.aBitmap = (Bitmap)lb.Image;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbBubble_Click(object sender, EventArgs e)
        {
            if (bThreadStart)
            {
                ShowMessageWhenThreadStarting();
                return;
            }

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
        private void ShowElementsPanel(Button btn, Panel pnl, bool bShow)
        {
            Commons.ImageClickEffect(btn, Properties.Resources.btn1, Properties.Resources.btn3);
            if (bShow)
                //Duy chuyển ra
                Commons.MovePanel(pnl, this.Width - 271, pnl.Location.Y, 5);
            else
                //Di chuyển vô
                Commons.MovePanel(pnl, this.Width - 148, pnl.Location.Y, 5);
        }
        private void btnElements_Click(object sender, EventArgs e)
        {
            if (bThreadStart)
            {
                ShowMessageWhenThreadStarting();
                return;
            }
            ShowElementsPanel((Button)sender,pnlElements,bShowPanelElements);
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
                    lbIconSmall.Location = new Point(pEffLb.X - 1, pEffLb.X + 1);
                    iNext = 2;
                    break;
                case 2:
                    ctrlsEff.Location = new Point(pEff.X, pEff.Y);
                    lbIconSmall.Location = new Point(pEffLb.X, pEffLb.X);
                    iNext = 3;
                    break;
                case 3:
                    ctrlsEff.Location = new Point(pEff.X + 1, pEff.Y + 1);
                    lbIconSmall.Location = new Point(pEffLb.X + 1, pEffLb.X - 1);
                    iNext = 4;
                    break;
                case 4:
                    ctrlsEff.Location = new Point(pEff.X , pEff.Y);
                    lbIconSmall.Location = new Point(pEffLb.X, pEffLb.X);
                    iNext = 5;
                    break;
            }
        }

        private void RemoveAllElement()
        {
            icompare = 0;
            iswap = 0;
            lbTextSort.Text = lbTextSort.Text.Split(':')[0];
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
                   ,lbColor.BackColor);

            //Luu lai de Remove
            lstAllElementApp.Add(el);
            return el;
        }
        private int RandomNumber(int iStart, int iEnd)
        { 
            return ran.Next(iStart, iEnd);
        }
        private void ResetLocationInPanel(List<Elements> lstEles, int xStart, int yStart)
        {
            foreach (Elements item in lstEles)
            {
                item.Location = new Point(xStart, yStart);
                xStart += 40;
            }
        }
        private Point GetPositionArray()
        {
            Point pLoca = new Point(0,0);

            int iNumber = 15;
            try
            {
                iNumber = int.Parse(cbbSize.Text.Trim());
            }
            catch (Exception)
            { 
            
            }

            int iXArray = Math.Abs((pnlElements.Location.X + btnRandoms.Width - iNumber * 42) / 2);
            int iYArray = Math.Abs((pnlElements.Location.Y + btnElements.Height - 40) / 2);
            pLoca.X = iXArray;
            pLoca.Y = iYArray;
            return pLoca;
        }
        private void RandomData()
        {
            bDataExists = true;

            //Tắt Thread đang chạy
            AbortThreadDemo();

            //Xóa các phần tử củ trước
            RemoveAllElement();
            int iNumber = 15;
            try
            {
                iNumber = int.Parse(cbbSize.Text.Trim());
            }
            catch (Exception)
            {

            }

            //Lấy vị trí xuất phát của Array trên panel
            Point pLoca = GetPositionArray();

            for (int i = 0; i < iNumber; i++)
            {
                int iRandom = RandomNumber(0, i * 30);
                Elements el = GetElement(iRandom.ToString(), pLoca.X, pLoca.Y, (Bitmap)lbBubble.Image);
                pLoca.X += iNextElement;

                lstElementsArray.Add(el);
                pnlArray.Controls.Add(el);
            }
        }

        private void btnRandoms_Click(object sender, EventArgs e)
        {
            RandomData();
        }

        private void txtSize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender,"Maximum = 30 is beautiful array!");
        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            //Nếu chưa có dữ liệu thì thông báo chưa có và thoát
            if (!bDataExists)
            {
                MessageBox.Show("Data is not valid!", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            bAscending = this.chboxSortAscending.Checked;

            btnPause.Text = "Pause";
            //Nếu có rồi thì Sort

            //Tắt Thread trước đó.
            AbortThreadDemo();

            _thread = null;
            //Tạo Thread mới
            switch (lbTextSort.Text.Split(':')[0])
            {
                case "Selection Sort":
                    _thread = new Thread(SelectionSortDemo);
                    break;
                case "Insertion Sort":
                    _thread = new Thread(InsertionSortDemo);
                    break;
                case "BiInsertion Sort":
                    _thread = new Thread(BiInsertionSortDemo);
                    break;
                case "Interchange Sort":
                    _thread = new Thread(InterchangeSort);
                    break;
                case "Bubble Sort":
                    _thread = new Thread(BubbleSort);
                    break;
                case "Shake Sort":
                    _thread = new Thread(ShakeSort);
                    break;
                case "Quick Sort":
                    _thread = new Thread(QuickSort);
                    break;
            }
            if (_thread == null)
            {
                MessageBox.Show("None Sort algorithm!", "Algorithms", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            bThreadStart = true;
            _thread.Start();
        }
        private void QuickSort()
        {
            QuickSortAction(0, lstElementsArray.Count-1);
            //Kết thúc Demo
            EndDemoThread();
        }
        private void QuickSortAction(int left, int right)
        {
            if (left >= right) return;

            int iLeft = left, iRight = right;
            //Lấy vị trí ở giữa
            int iMid = (left + right) / 2;
            Elements elMid = lstElementsArray[iMid]; // Phần tử ở giữa
            icompare++;
            while (iLeft <= iRight)
            {
                icompare++;
                //Duyệt từ trái sang phải
                //Tìm vị trí phần tử có giá trị lớn hơn giá trị phần tử Mid đầu tiên
                while (GetConditionSort(int.Parse(elMid.aText),int.Parse(lstElementsArray[iLeft].aText)))
                    iLeft++;

                //Duyệt từ phải sang trái
                //Tìm vị trí phần tử có giá trị nhỏ hơn giá trị phần tử Mid đầu tiên
                while (GetConditionSort(int.Parse(lstElementsArray[iRight].aText), int.Parse(elMid.aText)))
                    iRight--;

                icompare++;
                //Hoán vị
                if (iLeft <= iRight)
                {
                    if (iLeft < iRight)
                    {
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + iLeft.ToString() + "]" + " and Array[" + iRight.ToString() + "]";
                        iswap++;
                        SwapElement(lstElementsArray[iRight], lstElementsArray[iLeft]);
                        SwapArrayElements(iLeft, iRight);
                    }
                    iLeft++;
                    iRight--;
                }
            }
            QuickSortAction(left, iRight);
            QuickSortAction(iLeft, right);
        }
        private void ShakeSort()
        {
            int i = 0, j = 0;
            for (i = 0; i < lstElementsArray.Count - 1; i++)
            {
                for (j = lstElementsArray.Count - 1; j > i; j--)
                {
                    icompare++;

                    //Lượt đi: Đẩy phần tử nhỏ về đầu mảng
                    if (GetConditionSort(int.Parse(lstElementsArray[j - 1].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        iswap++;
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + j.ToString() + "]" + " and Array[" + (j - 1).ToString() + "]";
                        SwapElement(lstElementsArray[j - 1], lstElementsArray[j]);
                        SwapArrayElements(j, j - 1);
                    }
                }

                for (j = i+1; j < lstElementsArray.Count - 2; j++)
                {
                    icompare++;
                    //Lượt về: Đẩy phần tử lớn về cuối mảng
                    if (GetConditionSort(int.Parse(lstElementsArray[j].aText), int.Parse(lstElementsArray[j+1].aText)))
                    {
                        iswap++;
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + j.ToString() + "]" + " and Array[" + (j + 1).ToString() + "]";
                        SwapElement(lstElementsArray[j + 1], lstElementsArray[j]);
                        SwapArrayElements(j, j +1);
                    }
                }
            }
            //Kết thúc Demo
            EndDemoThread();
        }
        private void BubbleSort()
        {
            int i = 0, j = 0;
            for (i = 0; i < lstElementsArray.Count-1; i++)
            {
                for (j=lstElementsArray.Count-1; j>i; j--)
                {
                    icompare++;
                    if (GetConditionSort(int.Parse(lstElementsArray[j-1].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        iswap++;
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + j.ToString() + "]" + " and Array[" + (j-1).ToString() + "]";
                        SwapElement(lstElementsArray[j-1], lstElementsArray[j]);
                        SwapArrayElements(j, j-1);
                    }
                }
            }
            //Kết thúc Demo
            EndDemoThread();
        }
        private void InterchangeSort()
        {
            int i = 0, j = 0;
            for (i = 0; i < lstElementsArray.Count - 1; i++)
            {
                for (j = i + 1; j < lstElementsArray.Count; j++)
                {
                    icompare++;
                    if (GetConditionSort(int.Parse(lstElementsArray[i].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        iswap++;
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + i.ToString() + "]" + " and Array[" + j.ToString() + "]"; 
                        SwapElement(lstElementsArray[j], lstElementsArray[i]);
                        SwapArrayElements(i, j);
                    }
                }   
            }
            //Kết thúc Demo
            EndDemoThread();
        }
        private void SwapArrayElements(int i, int j)
        {
            Elements eTem = lstElementsArray[i];
            lstElementsArray[i] = lstElementsArray[j];
            lstElementsArray[j] = eTem;
        }

        //E1 di chuyển xuống; E2 di chuyển lên
        public void SwapElement(Elements e1, Elements e2)
        {
            iSpeed = GetTimeSleep();

            Point p1 = e1.Location;
            Point p2 = e2.Location;

            e1.MoveTo(e1.Location.X, p1.Y + iSpacePlus, iSpeed / 2);
            e2.MoveTo(e2.Location.X, p2.Y - iSpacePlus, iSpeed / 2, true);

            e2.MoveTo(p1.X, p1.Y, iSpeed);
            e1.MoveTo(p2.X, p2.Y, iSpeed, true);

        }
        public static void SetDateElements(Elements eSet, Elements eGet)
        {
            eSet.aText = eGet.aText;
            eSet.aBitmap = eGet.aBitmap;
            eSet.Location = eGet.Location;
        }
        private void EndDemoThread()
        {
            bThreadStart = false;

            //bDataExists = false;
            Point pLoca = GetPositionArray();
            ResetLocationInPanel(lstElementsArray, pLoca.X, pLoca.Y);
            lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Array sorted";
            lbTextSort.Text+=" |Compare: "+icompare.ToString() + " |Swap: "+iswap.ToString();
            iswap = 0;
            icompare = 0;
        }
        private bool GetConditionSort(int x1, int x2)
        {
            if (bAscending)
            {
                //Sort tăng
                if (x1 > x2)
                    return true;
                else
                    return false;
            }
            else
            {
                //Sort giảm
                if (x1 < x2)
                    return true;
                else
                    return false;
            }
        }
        public void BiInsertionSortDemo()
        {
            int i = 0, j = 0, mpos = 0,l=0,r=0,ipos=0;
            Elements eX = null;
            for (i = 0; i < lstElementsArray.Count; i++)
            {
                eX = lstElementsArray[i];

                //Bên trái
                l = 0;

                //Bên phải
                r = i - 1;
                icompare++;
                while (l <= r)
                {
                    //vị trí ở giữa dãy đã sắp xếp
                    mpos = (l + r) / 2;
                    icompare++;
                    if (GetConditionSort(int.Parse(lstElementsArray[mpos].aText),int.Parse(eX.aText)))
                        r = mpos - 1;
                    else
                        l = mpos + 1;
                }
                ipos = l - 1;
                //Tìm thấy vị trí di chuyển
                if (ipos != i - 1)
                {
                    iswap++;
                    lbTextSort.Text =lbTextSort.Text.Split(':')[0] + ": Finded Array[" + i.ToString() + "]" + " to swap with Array[" + (ipos + 1).ToString() + "]";
                    //Lưu vị trí cần dịch đến lại
                    Point pFinish = lstElementsArray[ipos + 1].Location;

                    iSpeed = GetTimeSleep();

                    //Di chuyển xuống 
                    eX.MoveTo(eX.Location.X, eX.Location.Y + iSpacePlus, iSpeed, true);


                    lstElementsArray[i] = lstElementsArray[i - 1];
                    //Di chuyển tất cả các phần tử từ ipos đến gần i sang phải 1 nấc
                    for (j = i - 1; j > ipos + 1; j--)
                    {
                        lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, lstElementsArray[j].Location.Y, iSpeed);
                        lstElementsArray[j] = lstElementsArray[j - 1];
                    }
                    lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, lstElementsArray[j].Location.Y, iSpeed);


                    //Duy chuyển i đến pos
                    eX.MoveTo(pFinish.X, pFinish.Y, iSpeed, true);
                    lstElementsArray[ipos + 1] = eX;
                }
            }

            //Kết thúc Demo
            EndDemoThread();
        }
        public void InsertionSortDemo()
        {
            int i = 0, j = 0, ipos = 0;
            Elements eX = null;
            for (i = 0; i < lstElementsArray.Count; i++)
            {
                eX = lstElementsArray[i];
                ipos = i-1;
                icompare+=2;
                while (ipos >= 0 && GetConditionSort(int.Parse(lstElementsArray[ipos].aText),int.Parse(eX.aText)))
                {
                    ipos--;
                    iswap++;
                }
                
                //Tìm thấy vị trí di chuyển
                if (ipos != i - 1)
                {
                    lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Finded Array[" + i.ToString() + "]" + " to swap with Array[" + (ipos+1).ToString() + "]";
                    //Lưu vị trí cần dịch đến lại
                    Point pFinish = lstElementsArray[ipos + 1].Location;

                    iSpeed = GetTimeSleep();

                    //Di chuyển xuống 
                    eX.MoveTo(eX.Location.X, eX.Location.Y + iSpacePlus, iSpeed, true);


                    lstElementsArray[i] = lstElementsArray[i-1];
                    //Di chuyển tất cả các phần tử từ ipos đến gần i sang phải 1 nấc
                    for (j = i-1; j >ipos+1; j--)
                    {
                        lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X+iNextElement, lstElementsArray[j].Location.Y, iSpeed);
                        lstElementsArray[j] = lstElementsArray[j - 1];
                    }
                    lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, lstElementsArray[j].Location.Y, iSpeed);
                    

                    //Duy chuyển i đến pos
                    eX.MoveTo(pFinish.X, pFinish.Y, iSpeed, true);
                    lstElementsArray[ipos+1] = eX;
                }
            }

            //Kết thúc Demo
            EndDemoThread();
        }
        public void SelectionSortDemo()
        {
            int i = 0, j = 0,iMin=0;
            for (i = 0; i < lstElementsArray.Count - 1; i++)
            {
                iMin = i;
                for (j = i + 1; j < lstElementsArray.Count; j++)
                {
                    icompare++;
                    if (GetConditionSort(int.Parse(lstElementsArray[iMin].aText),int.Parse(lstElementsArray[j].aText)))
                        iMin = j;
                }
                if (iMin != i)
                {
                    iswap++;
                    lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + i.ToString() + "]" + " and Array[" + iMin.ToString() + "]"; 
                    SwapElement(lstElementsArray[iMin], lstElementsArray[i]);
                    SwapArrayElements(iMin, i);
                }
            }
            //Kết thúc Demo
            EndDemoThread();
        }
        private void AbortThreadDemo()
        {
            try
            {
                bThreadStart = false;

                _thread.Abort();
            }
            catch (Exception)
            { 
            }
        }
        private void PauseThread()
        {
            try
            {
                bThreadStart = false;

                _thread.Suspend();
            }
            catch (Exception)
            { 
            
            }
        }
        private void ResumeThread()
        {
            try
            {
                bThreadStart = true;

                _thread.Resume();
            }
            catch (Exception)
            {

            }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                btnPause.Text = "Resume";
                PauseThread();
            }
            else
            {
                btnPause.Text = "Pause";
                ResumeThread();
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Image = Properties.Resources.btn2;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Image = Properties.Resources.btn1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bThreadStart)
            {
                ShowMessageWhenThreadStarting();
                return;
            }
            ShowElementsPanel((Button)sender, pnlColor, bShowPanelColor);
            bShowPanelColor = !bShowPanelColor;
        }
        private void ShowMessageWhenThreadStarting()
        {
            MessageBox.Show("Demo is excuting...!", "Sorting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void label55_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lbColor.BackColor = colorDialog1.Color;
                foreach (Elements item in lstElementsArray)
                {
                    item.aColor = colorDialog1.Color;
                }
            }
        }

        private void cbbSpeed_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void lbColor_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Number' color on the ball!");
        }
    }
}