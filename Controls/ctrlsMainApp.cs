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
using System.IO;

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
        private List<Elements> lstBackupArray = new List<Elements>();
        Random ran = new Random();
        Thread _thread;
        //Next index Timer
        int iNext = 1;
        //Lưu control muốn tạo hiệu ứng
        Control ctrlsEff;
        
        //Lưu vị trí hiện hành của control tạo hiệu ứng
        Point pEff;
        Point pEffLb;
        bool bStopTimer = false;

        bool bDataExists = false;
        bool bAscending = true;
        int iSpeed = 10;
        private int icompare = 0;
        private int iswap = 0;
        private int iAssign = 0;
        /// <summary>
        /// Độ phức tạp của thuật toán
        /// </summary>
        private String strOn = "";//Độ phức tạp
        int iSpacePlus = 50;
        int iNextElement = 40;
        Point pStarElement; //Vị trí khởi đầu cho dãy. Ta lấy giá trị y để định hình dãy 

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
              this.Disposed += ctrlsMainApp_Disposed;
        }

        void ctrlsMainApp_Disposed(object sender, EventArgs e)
        {
            AbortThreadDemo();
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

            bStopTimer = true;
            //Hiệu ứng Click
            Point pLocation = lb.Location;
            Commons.MoveControlDetail((Control)lb, pLocation.X + 75, pLocation.Y, 10, 0,1,5,5);
            new Thread(() => 
            {
                Thread.Sleep(500);
                lb.Location = pLocation;
                bStopTimer = false;
            }).Start();
            /////////////////////
            if (bThreadStart)
            {
                PauseClicked();

                if (MessageBox.Show("Do you want to demo with " + arrstr[0] + " Algorithm?\nCurrent demo will stop if you ok, then data for array will reset.", "Sort", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //Stop Thread
                    PauseClicked();
                    AbortThreadDemo();

                    //ResetData
                    ResetDataForArray();

                    //Demo
                    new Thread(() =>
                    {
                        Thread.Sleep(500);
                        ExcuteDemoThread();
                    }).Start();        
                }
                else
                    PauseClicked();
                return;
            }
            ExcuteDemoThread();
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
            if (!threadQuestionToolStripMenuItem.Checked)
                btnPause.PerformClick();
            else
                if (bThreadStart)
                {
                    if (!ShowMessageWhenThreadStarting())
                        return;
                }

            if (bShowPanelBubble)
                //Duy chuyển ra
                Commons.MoveControlDetail((Control)pnlBubble, this.Width - 450, pnlBubble.Location.Y,10,50,1,10,10);
            else
                //Di chuyển vô
                Commons.MoveControlDetail((Control)pnlBubble, this.Width - 120, pnlBubble.Location.Y,10,50,1,10,10);

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
                Commons.MoveControlDetail((Control)pnlSort2, pnlSort2.Location.X, this.Height - 108, 20, 0, 1, 20, 20);
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
                Commons.MoveControls((Control)pnl, this.Width - 271, pnl.Location.Y, 5);
            else
                //Di chuyển vô
                Commons.MoveControls((Control)pnl, this.Width - 148, pnl.Location.Y, 5);
        }
        private void btnElements_Click(object sender, EventArgs e)
        {
            if (!threadQuestionToolStripMenuItem.Checked)
                btnPause.PerformClick();
            else
                if (bThreadStart)
                {
                    if (!ShowMessageWhenThreadStarting())
                        return;
                }

            ShowElementsPanel((Button)sender, pnlElements, bShowPanelElements);
            bShowPanelElements = !bShowPanelElements;
        }

        private void btnSort2_Click(object sender, EventArgs e)
        {
            if (!bShowSort1)
            { 
                //Nếu Sort 1 đang show
                //Move Sort 2 ra
                Commons.MoveControlDetail((Control)pnlSort2, pnlSort2.Location.X, btnSort1.Location.Y + 22, 20, 30, 1, 20, 20);
                bShowSort1 = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.FileName = "sortdata.txt";
            openFileDialog1.Filter = "Text File(*.txt)|*.txt|All File|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                StreamReader reader = new StreamReader(openFileDialog1.FileName);
                String str = String.Empty;
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                }
                reader.Close();
                if (str==String.Empty || Commons.CheckValidArray(str.Split(';'))==false)
                {
                    MessageBox.Show("None data for Array or Data is not valid!", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    InputArrayData(str);
                }
            }
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Generate array data from your file!");
        }
        private String Array2Text(List<Elements> lst)
        {
            String str = "";
            foreach (Elements item in lst)
            {
                str += item.aText + ";";
            }
            if (str.Length == 0) return "";
            return str.Substring(0, str.Length - 1);
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLoadMessage frm = new frmLoadMessage("Data for Sor", Array2Text(lstElementsArray), (Bitmap)((Label)ctrlsEff).Image, lbTextSort.Text.Split(':')[0], Array2Text(lstBackupArray));
            frm.UpdateTextMessage += frm_UpdateTextMessage;
            frm.ShowDialog();
        }
        private void InputArrayData(String strLineData)
        {
            String[] arr = strLineData.Split(';');

            bDataExists = true;

            //Tắt Thread đang chạy
            AbortThreadDemo();

            //Xóa các phần tử củ trước
            RemoveAllElement();

            //Gán số lượng phần tử cho combo
            cbbSize.Text = arr.Length.ToString();

            //Lấy vị trí thích hợp
            Point pLoca = GetPositionArray();

            //Set từng phần tử vào List
            foreach (String item in arr)
            {
                SetElementsToArray(item, pLoca.X, pLoca.Y, (Bitmap)lbBubble.Image);
                pLoca.X += iNextElement;
            }
        }
        void frm_UpdateTextMessage(object sender, LoadMessageDataArgs e)
        {
            InputArrayData(e.aData);
        }

        private void linkLabel2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Generate array data by press key data!");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ctrlsEff == null || bStopTimer) return;

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
            iAssign = 0;

            lbTextSort.Text = lbTextSort.Text.Split(':')[0];
            foreach (Elements item in lstAllElementApp)
            {
                pnlArray.Controls.Remove(item);
            }
            lstAllElementApp.Clear();
            lstElementsArray.Clear();
            lstBackupArray.Clear();
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
                SetElementsToArray(iRandom.ToString(), pLoca.X, pLoca.Y, (Bitmap)lbBubble.Image);
                pLoca.X += iNextElement;

            }
        }
        private void SetElementsToArray(String strText, int x, int y, Bitmap bit)
        {
            Elements el = GetElement(strText, x, y, bit);
            lstElementsArray.Add(el);
            lstBackupArray.Add(el);
            pnlArray.Controls.Add(el);
        }
        private void btnRandoms_Click(object sender, EventArgs e)
        {
            RandomData();
        }

        private void txtSize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender,"Maximum = 30 is beautiful array!");
        }
        public static void SetDateElements(Elements eSet, Elements eGet)
        {
            eSet.aText = eGet.aText;
            eSet.aBitmap = eGet.aBitmap;
            eSet.Location = eGet.Location;
        }
        private void ExcuteDemoThread()
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
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(SelectionSortDemo);
                    break;
                case "Insertion Sort":
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(InsertionSortDemo);
                    break;
                case "BiInsertion Sort":
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(BiInsertionSortDemo);
                    break;
                case "Interchange Sort":
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(InterchangeSort);
                    break;
                case "Bubble Sort":
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(BubbleSort);
                    break;
                case "Shake Sort":
                    strOn = "O(n) = O(n^2)";
                    _thread = new Thread(ShakeSort);
                    break;
                case "Quick Sort":
                    strOn = "O(n) = O(log[2]n)";
                    _thread = new Thread(QuickSort);
                    break;
            }
            if (_thread == null)
            {
                MessageBox.Show("None Sort algorithm!", "Algorithms", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            bThreadStart = true;
            pStarElement = GetPositionArray();//Lấy vịt trí hợp lí trước khi Demo
            _thread.Start();
        }
        private void btnExcute_Click(object sender, EventArgs e)
        {
            
        }
        private void SwapArrayElements(int i, int j)
        {
            iAssign += 3;

            Elements eTem = lstElementsArray[i];
            lstElementsArray[i] = lstElementsArray[j];
            lstElementsArray[j] = eTem;
        }

        //E1 di chuyển xuống; E2 di chuyển lên
        public void SwapElement(Elements e1, Elements e2)
        {
            iswap++;

            iSpeed = GetTimeSleep();

            Point p1 = e1.Location;
            Point p2 = e2.Location;

            e1.MoveTo(e1.Location.X, p1.Y + iSpacePlus, iSpeed / 2);
            e2.MoveTo(e2.Location.X, p2.Y - iSpacePlus, iSpeed / 2, true);

            e2.MoveTo(p1.X, pStarElement.Y, iSpeed);
            e1.MoveTo(p2.X, pStarElement.Y, iSpeed, true);

        }
        private void EndDemoThread()
        {
            bThreadStart = false;

            //bDataExists = false;
            Point pLoca = GetPositionArray();
            ResetLocationInPanel(lstElementsArray, pLoca.X, pLoca.Y);
            lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Array sorted";
            lbTextSort.Text += " |Assign: " + iAssign.ToString() + "|Compare: " + icompare.ToString() + " |Swap: " + iswap.ToString()+" |"+strOn;
            iswap = 0;
            icompare = 0;
            iAssign = 0;
        }
        private void QuickSort()
        {
            QuickSortAction(0, lstElementsArray.Count-1);
            //Kết thúc Demo
            EndDemoThread();
        }
        private void QuickSortAction(int left, int right)
        {
            icompare++;//So sánh left và right
            if (left >= right) return;

            int iLeft = left, iRight = right;
            //Lấy vị trí ở giữa
            int iMid = (left + right) / 2;
            Elements elMid = lstElementsArray[iMid]; // Phần tử ở giữa

            iAssign += 4;//phép gán iLeft, iRight, iMid, eMid
            while (iLeft < iRight)
            {
                icompare++; // so sánh iLeft và iRight

                //Duyệt từ trái sang phải
                //Tìm vị trí phần tử có giá trị lớn hơn giá trị phần tử Mid đầu tiên
                while (GetConditionSort(int.Parse(elMid.aText), int.Parse(lstElementsArray[iLeft].aText)))
                {
                    iLeft++;
                    iAssign++;//phép gán iLeft
                }
                //Duyệt từ phải sang trái
                //Tìm vị trí phần tử có giá trị nhỏ hơn giá trị phần tử Mid đầu tiên
                while (GetConditionSort(int.Parse(lstElementsArray[iRight].aText), int.Parse(elMid.aText)))
                {
                    iRight--;
                    iAssign++; //phép gán iRight
                }

                icompare++;//So sánh iLeft với iRight
                //Hoán vị
                if (iLeft <= iRight)
                {
                    if (iLeft < iRight)
                    {
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + iLeft.ToString() + "]" + " and Array[" + iRight.ToString() + "]";
                        SwapElement(lstElementsArray[iRight], lstElementsArray[iLeft]);
                        SwapArrayElements(iLeft, iRight);
                    }
                    iLeft++;
                    iRight--;
                    iAssign += 2; // 2 phép gán iLeft và iRight
                }
            }
            icompare++;// so sánh iLeft và iRight của vòng lặp

            QuickSortAction(left, iRight);
            QuickSortAction(iLeft, right);
        }
        private void ShakeSort()
        {
            int i = 0, j = 0;
            for (i = 0,iAssign++,icompare++; i < lstElementsArray.Count - 1;iAssign++, i++)
            {
                for (j = lstElementsArray.Count - 1,iAssign++,icompare++; j > i;iAssign++, j--)
                {
                    //Lượt đi: Đẩy phần tử nhỏ về đầu mảng
                    if (GetConditionSort(int.Parse(lstElementsArray[j - 1].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + j.ToString() + "]" + " and Array[" + (j - 1).ToString() + "]";
                        SwapElement(lstElementsArray[j - 1], lstElementsArray[j]);
                        SwapArrayElements(j, j - 1);
                    }
                }

                for (j = i+1,iAssign++,icompare++; j < lstElementsArray.Count - 2;iAssign++, j++)
                {
                    //Lượt về: Đẩy phần tử lớn về cuối mảng
                    if (GetConditionSort(int.Parse(lstElementsArray[j].aText), int.Parse(lstElementsArray[j+1].aText)))
                    {
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
            for (i = 0, iAssign++, icompare++; i < lstElementsArray.Count - 1; iAssign++, i++)
            {
                for (j = lstElementsArray.Count - 1, iAssign++, icompare++; j > i;iAssign++, j--)
                {
                    if (GetConditionSort(int.Parse(lstElementsArray[j-1].aText), int.Parse(lstElementsArray[j].aText)))
                    {
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
            for (i = 0,iAssign++,icompare++; i < lstElementsArray.Count - 1;iAssign++, i++)
            {
                for (j = i + 1,iAssign++,icompare++; j < lstElementsArray.Count;iAssign++, j++)
                {
                    if (GetConditionSort(int.Parse(lstElementsArray[i].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Swap Array[" + i.ToString() + "]" + " and Array[" + j.ToString() + "]"; 
                        SwapElement(lstElementsArray[j], lstElementsArray[i]);
                        SwapArrayElements(i, j);
                    }
                }   
            }
            //Kết thúc Demo
            EndDemoThread();
        }
        private bool GetConditionSort(int x1, int x2)
        {
            icompare++;

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
            for (i = 0,iAssign++,icompare++; i < lstElementsArray.Count;iAssign++, i++)
            {
                eX = lstElementsArray[i];

                //Bên trái
                l = 0;

                //Bên phải
                r = i - 1;
                iAssign += 3; // 3 phép gán eX, l, r
                
                while (l <= r)
                {
                    //vị trí ở giữa dãy đã sắp xếp
                    mpos = (l + r) / 2;

                    icompare++;//So sánh l và r
                    iAssign++;//Phép gán mpos

                    if (GetConditionSort(int.Parse(lstElementsArray[mpos].aText), int.Parse(eX.aText)))
                    {
                        r = mpos - 1;
                        iAssign++; //phép gán r
                    }
                    else
                    {
                        l = mpos + 1;
                        iAssign++; // phép gán r
                    }
                }
                icompare++;//So sáng l và r của vòng lặp
                iAssign++;//Phép gán ipos
                icompare++;// So sánh ipos và i-1

                ipos = l - 1;
                //Tìm thấy vị trí di chuyển
                if (ipos != i - 1)
                {
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
                        lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, pStarElement.Y, iSpeed);
                        lstElementsArray[j] = lstElementsArray[j - 1];
                    }
                    lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, pStarElement.Y, iSpeed);


                    //Duy chuyển i đến pos
                    eX.MoveTo(pFinish.X, pStarElement.Y, iSpeed, true);
                    lstElementsArray[ipos + 1] = eX;

                    iAssign++;//Gán iPos+1
                    iswap++; // Hoán vị iPos+1
                }
            }

            //Kết thúc Demo
            EndDemoThread();
        }
        public void InsertionSortDemo()
        {
            int i = 0, j = 0, ipos = 0;
            Elements eX = null;
            for (i = 0,iAssign++,icompare++; i < lstElementsArray.Count; iAssign++,i++)
            {
                eX = lstElementsArray[i];
                ipos = i-1;
                iAssign += 2;//2 phép gán ipos và eX
                while (ipos >= 0 && GetConditionSort(int.Parse(lstElementsArray[ipos].aText),int.Parse(eX.aText)))
                {
                    ipos--;
                    iAssign+=2; // Số gán phần tử và iposs--
                    iswap++;//dịch chuyển phần tử bên trái sang phải 1 nấc
                    icompare++;//So sánh ipos với 0
                }
                icompare++;//So sánh ipos với 0
                icompare++;//so sánh ipos vơi si-1

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
                        lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X+iNextElement,pStarElement.Y, iSpeed);
                        lstElementsArray[j] = lstElementsArray[j - 1];
                    }
                    lstElementsArray[j].MoveTo(lstElementsArray[j].Location.X + iNextElement, pStarElement.Y, iSpeed);
                    

                    //Duy chuyển i đến pos
                    eX.MoveTo(pFinish.X, pStarElement.Y, iSpeed, true);
                    lstElementsArray[ipos+1] = eX;

                    iAssign++;//Gán iPos+1
                    iswap++; // Hoán vị iPos+1
                }
            }

            //Kết thúc Demo
            EndDemoThread();
        }
        public void SelectionSortDemo()
        {
            int i = 0, j = 0,iMin=0;
            for (i = 0, iAssign++, icompare++; i < lstElementsArray.Count - 1;iAssign++, i++)
            {
                iMin = i;
                iAssign++;//Phép gán iMin
                for (j = i + 1, iAssign++, icompare++; j < lstElementsArray.Count; iAssign++, j++)
                {
                    if (GetConditionSort(int.Parse(lstElementsArray[iMin].aText), int.Parse(lstElementsArray[j].aText)))
                    {
                        iMin = j;
                        iAssign++; // Phép gán iMin
                    }
                }
                icompare++; // So sánh iMin và i
                if (iMin != i)
                {
 
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
                lbTextSort.Text = lbTextSort.Text.Split(':')[0] + ": Demo is pausing....!";
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
        private void PauseClicked()
        {
            if (btnPause.Text == "Pause")
            {
                if (!bThreadStart)
                {
                    MessageBox.Show("Demo was finish!", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnPause.Text = "Resume";
                PauseThread();
            }
            else
            {
                btnPause.Text = "Pause";
                ResumeThread();
            }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseClicked();
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
            if (!threadQuestionToolStripMenuItem.Checked)
                btnPause.PerformClick();
            else
                if (bThreadStart)
                {
                    if (!ShowMessageWhenThreadStarting())
                        return;
                }
            ShowElementsPanel((Button)sender, pnlColor, bShowPanelColor);
            bShowPanelColor = !bShowPanelColor;
        }
        private bool ShowMessageWhenThreadStarting()
        {
            if (MessageBox.Show("Demo is excuting...!\nDo you want to pause thread for moving?", "Sorting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                btnPause.PerformClick();
                return true;
            }
            return false;
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

        private void resetArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "Reset data for current array!");
        }
        private void ResetDataForArray()
        {
            for (int i = 0; i < lstBackupArray.Count; i++)
            {
                lstElementsArray[i] = lstBackupArray[i];
            }
            lbTextSort.Text = lbTextSort.Text.Split(':')[0];
            Point pLoca = GetPositionArray();
            ResetLocationInPanel(lstElementsArray, pLoca.X, pLoca.Y);
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to reset data for this array?", "Sort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetDataForArray();
            }
        }

        private void threadQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            threadQuestionToolStripMenuItem.Checked = !threadQuestionToolStripMenuItem.Checked;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
