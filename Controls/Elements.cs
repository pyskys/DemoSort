using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DemoSort.Controls
{
    public partial class Elements : UserControl
    {
        //mỗi phần từ gồm text, vị trí, và hình ảnh đại diện
        private String _strText;
        private Bitmap _aBitmap;
        private Color _aColor;

        public Color aColor
        {
            get { return _aColor; }
            set { _aColor = value; lbText.ForeColor = _aColor; }
        }
        public Bitmap aBitmap
        {
            get { return _aBitmap; }
            set { _aBitmap = value; pnlImage.BackgroundImage = _aBitmap; }
        }
        public String aText
        {
            get { return _strText; }
            set { _strText = value; }
        }
        public Elements(String strText, int x, int y, Bitmap bit,Color txtColor)
        {
            InitializeComponent();
            lbText.Text = strText;

            aText = strText;
            this.Location = new Point(x, y);
            this.pnlImage.BackgroundImage = bit;
            aBitmap = bit;
            aColor = txtColor;
        }
        int iXAdd = 1, iYAdd = 1;
        int iNext = 1;
        int iDefaultDistance = 20;
        private void MoveAction(int x, int y, int ispeed)
        {
            new Thread(() =>
            {
                int dx = x - Location.X;//Vị trí mới trừ vị trí cũ
                int dy = y - Location.Y;

                //Move đến X
                while (dx != 0)
                {
                    Thread.Sleep(ispeed);

                    if (Math.Abs(x - Location.X) > iDefaultDistance)
                        iNext = iXAdd;
                    else
                        iNext = 1;

                    //Duy chuyển sang trái
                    if (dx < 0)
                    {
                        dx += iNext;
                        //Giảm giá trị X của phần tử lên cho đến khi đến đúng vị trí X cung cấp
                        this.Location = new Point(Location.X - iNext, Location.Y);

                        //Chỉnh lại 0 để thoát vòng While nếu + vượt lên 0
                        if (dx > 0) dx = 0;
                    }
                    else

                        //Duy chuyển sang phải
                        if (dx > 0)
                        {
                            dx -= iNext;
                            //Tăng giá trị X của phần tử lên cho đến khi đến đúng vị trí X cung cấp
                            this.Location = new Point(Location.X + iNext, Location.Y);

                            //Chỉnh lại 0 để thoát vòng While nếu - vượt xuống 0
                            if (dx < 0) dx = 0;
                        }
                }

                //Move đến Y
                while (dy != 0)
                {
                    Thread.Sleep(ispeed);

                    if (Math.Abs(y - Location.Y) > iDefaultDistance)
                        iNext = iYAdd;
                    else
                        iNext = 1;

                    //Duy chuyển lên trên
                    if (dy < 0)
                    {
                        dy += iNext;
                        //Giảm giá trị Y của phần tử lên cho đến khi đến đúng vị trí Y cung cấp
                        this.Location = new Point(Location.X, Location.Y - iNext);

                        //Chỉnh lại 0 để thoát vòng While nếu + vượt lên 0
                        if (dy > 0) dy = 0;
                    }

                    else

                        //Duy chuyển xuống dưới
                        if (dy > 0)
                        {
                            dy -= iNext;
                            //Tăng giá trị Y của phần tử lên cho đến khi đến đúng vị trí Y cung cấp
                            this.Location = new Point(Location.X, Location.Y + iNext);

                            //Chỉnh lại 0 để thoát vòng While nếu - vượt xuống 0
                            if (dy < 0) dy = 0;
                        }
                }
                this.Location = new Point(x, y);
            }).Start();
        }
        private void MoveActionInvoke(int x, int y, int ispeed)
        {
            int dx = x - Location.X;//Vị trí mới trừ vị trí cũ
            int dy = y - Location.Y;

            //Move đến X
            while (dx != 0)
            {
                Thread.Sleep(ispeed);

                if (Math.Abs(x - Location.X) > iDefaultDistance)
                    iNext = iXAdd;
                else
                    iNext = 1;

                //Duy chuyển sang trái
                if (dx < 0)
                {
                    dx += iNext;
                    //Giảm giá trị X của phần tử lên cho đến khi đến đúng vị trí X cung cấp
                    this.Location = new Point(Location.X - iNext, Location.Y);

                    //Chỉnh lại 0 để thoát vòng While nếu + vượt lên 0
                    if (dx > 0) dx = 0;
                }
                else

                    //Duy chuyển sang phải
                    if (dx > 0)
                    {
                        dx -= iNext;
                        //Tăng giá trị X của phần tử lên cho đến khi đến đúng vị trí X cung cấp
                        this.Location = new Point(Location.X + iNext, Location.Y);

                        //Chỉnh lại 0 để thoát vòng While nếu - vượt xuống 0
                        if (dx < 0) dx = 0;
                    }
            }

            //Move đến Y
            while (dy != 0)
            {
                Thread.Sleep(ispeed);

                if (Math.Abs(y - Location.Y) > iDefaultDistance)
                    iNext = iYAdd;
                else
                    iNext = 1;

                //Duy chuyển lên trên
                if (dy < 0)
                {
                    dy += iNext;
                    //Giảm giá trị Y của phần tử lên cho đến khi đến đúng vị trí Y cung cấp
                    this.Location = new Point(Location.X, Location.Y - iNext);

                    //Chỉnh lại 0 để thoát vòng While nếu + vượt lên 0
                    if (dy > 0) dy = 0;
                }

                else

                    //Duy chuyển xuống dưới
                    if (dy > 0)
                    {
                        dy -= iNext;
                        //Tăng giá trị Y của phần tử lên cho đến khi đến đúng vị trí Y cung cấp
                        this.Location = new Point(Location.X, Location.Y + iNext);

                        //Chỉnh lại 0 để thoát vòng While nếu - vượt xuống 0
                        if (dy < 0) dy = 0;
                    }
            }

            this.Location = new Point(x, y);
        }
        //Duy chuyển các phần tử chậm chẩm rồi biến mất
        public void MoveToExit(int x, int y, int ispeed)
        {
            iXAdd = 1;
            iYAdd = 1;
            iNext = 1;
            iDefaultDistance = 20;
            MoveAction(x, y, ispeed);
        }

        //Duy chuyển phần tử bình thường thay đổi theo tốc độ
        public void MoveTo(int x, int y, int ispeed,bool bInvoke = false)
        {
            int dx = x - Location.X;//Vị trí mới trừ vị trí cũ
            int dy = y - Location.Y;

            iXAdd = Math.Abs(dx / 50);
            if (iXAdd < 1) iXAdd = 1;
            if (iXAdd > 5) iXAdd = 5;

            iYAdd = Math.Abs(dy / 50);
            if (iYAdd < 1) iYAdd = 1;
            if (iYAdd > 5) iYAdd = 5;

            iNext = 1;

            //Khoảng cách mặt định mà đến đó làm chậm tốc độ lại
            iDefaultDistance = 20;

            if (!bInvoke)
                MoveAction(x, y, ispeed);
            else
                MoveActionInvoke(x, y, ispeed);
        }
        private void Elements_Load(object sender, EventArgs e)
        {

        }

        private void lbText_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbText_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Value: " + lbText.Text, "Elements", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
