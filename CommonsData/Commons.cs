﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DemoSort.CommonsData
{
    /// <summary>
    /// Lớp chứa các hàm static dùng chung cho chương trình
    /// </summary>
    public class Commons
    {
        /// <summary>
        /// Hàm di chuyển Control xuất hiện ra vị trí định sẵn
        /// </summary>
        /// <param name="ctr">Control cần di chuyển</param>
        /// <param name="XFinish">Tọa độ X cần di chuyển đến</param>
        /// <param name="YFinish">Tọa độ Y cần di chuyển đến</param>
        /// <param name="iSleep">Thời gian tạm dừng để tạo hiệu ứng</param>
        public static void MoveControls(Control ctr, int XFinish = 0, int YFinish = 0, int iSleep = 0, int iDefaultDistance = 50/*Giá trị khoản cách mặc định làm tốc độ giảm xuống*/)
        {
            //Khoản cách từ vị trí hiện tại đến điểm kết thúc
            int dx = ctr.Location.X - XFinish;
            int dy = ctr.Location.Y - YFinish;

            //iXAdd và iYAdd là các giá trị dương dược cộng thêm để dịch chuyển
            int iXAdd = Math.Abs(dx / 50);
            int iYAdd = Math.Abs(dy / 50);

            iXAdd = iXAdd > 5 ? 5 : iXAdd;
            iXAdd = iXAdd < 1 ? 1 : iXAdd;

            iYAdd = iYAdd > 5 ? 5 : iYAdd;
            iYAdd = iYAdd < 1 ? 1 : iYAdd;

            int iNext = 1;

            //Gọi hàm detail
            MoveControlDetail(ctr, XFinish, YFinish, iSleep, iDefaultDistance, iNext, iXAdd, iYAdd);
        }
        /// <summary>
        /// Hàm di chuyển Control xuất hiện ra vị trí định sẵn
        /// </summary>
        /// <param name="ctr">Control cần di chuyển</param>
        /// <param name="XFinish">Tọa độ X cần di chuyển đến</param>
        /// <param name="YFinish">Tọa độ Y cần di chuyển đến</param>
        /// <param name="iSleep">Thời gian tạm dừng để tạo hiệu ứng</param>
        /// <param name="iDefaultDistance">Khoảng cách mặc định giảm tốc độ</param>
        /// <param name="iNext">Độ lệch khoảng cách khi giảm tốc độ</param>
        /// <param name="iXAdd">Độ lệch khoảng cách X khi tăng tốc độ</param>
        /// <param name="iYAdd">Độ lệch khoảng cách Y khi tăng tốc độ</param>
        public static void MoveControlDetail(Control ctr, int XFinish = 0, int YFinish = 0, int iSleep = 0, int iDefaultDistance = 50/*Giá trị khoản cách mặc định làm tốc độ giảm xuống*/, int iNext = 1, int iXAdd = 5, int iYAdd = 5)
        {
            if (ctr == null) return;
            new Thread(() =>
            {
                //Khoản cách từ vị trí hiện tại đến điểm kết thúc
                int dx = ctr.Location.X - XFinish;
                int dy = ctr.Location.Y - YFinish;

                //Duy chuyển từ phải sang trái
                while (dx > 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(ctr.Location.X - XFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iXAdd;

                    dx -= iNext;
                    ctr.Location = new Point(ctr.Location.X - iNext, ctr.Location.Y);

                    //Tăng về 0 nếu trừ vượt xuống 0
                    if (dx < 0) dx = 0;
                }

                //dx<=0. di chuyển từ trái sang phải
                while (dx < 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(ctr.Location.X - XFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iXAdd;

                    dx += iNext;
                    ctr.Location = new Point(ctr.Location.X + iNext, ctr.Location.Y);

                    //Giảm về 0 nếu trừ vượt xuống 0
                    if (dx > 0) dx = 0;
                }

                //Duy chuyển từ dưới lên trên
                while (dy > 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(ctr.Location.Y - YFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iYAdd;

                    dy -= iNext;
                    ctr.Location = new Point(ctr.Location.X, ctr.Location.Y - iNext);

                    //Tăng về 0 nếu trừ vượt xuống 0
                    if (dy < 0) dy = 0;
                }

                //dx<=0. di chuyển từ trên xuống dưới
                while (dy < 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(ctr.Location.Y - XFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iYAdd;

                    dy += iNext;
                    ctr.Location = new Point(ctr.Location.X, ctr.Location.Y + iNext);

                    //Giảm về 0 nếu trừ vượt xuống 0
                    if (dy > 0) dy = 0;
                }
                ctr.Location = new Point(XFinish, YFinish);
            }).Start();
        }
        /// <summary>
        /// Hàm tạo hiệu ứng màu sắc cho việc Click chuột
        /// </summary>
        /// <param name="btn">Button cần tạo hiệu ứng</param>
        /// <param name="clr1">Màu sắc mặc định của Button</param>
        /// <param name="clr2">Màu sắc tạo hiệu ứng sau khi Click</param>
        /// <param name="iSleep">Thời gian tạm dừng</param>
        public static void ColorClickEffect(Button btn, Color clr1, Color clr2,int iSleep = 150)
        {
         new Thread(()=>{
             btn.BackColor = clr2;
             Thread.Sleep(iSleep);
             btn.BackColor = clr1;
         }).Start();
        }

        /// <summary>
        /// Hàm tạo hiệu ứng hình ảnh cho việc Click chuột
        /// </summary>
        /// <param name="btn">Button cần tạo hiệu ứng</param>
        /// <param name="bit1">Hình ảnh mặc định</param>
        /// <param name="bit2">Hình ảnh hiệu ứng sau khi Click</param>
        /// <param name="iSleep">Thời gian đứng im</param>
        public static void ImageClickEffect(Button btn, Bitmap bit1, Bitmap bit2, int iSleep = 150)
        {
            new Thread(() =>
            {
                btn.Image = bit2;
                Thread.Sleep(iSleep);
                btn.Image = bit1;
            }).Start();
        }
        public static bool CheckValidArray(String[] arr)
        {
            if (arr == null || arr.Length == 0) return false;
            foreach (String item in arr)
            {
                try
                {
                    int.Parse(item);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public delegate void ExitLoadMessage(Object sender, LoadMessageDataArgs e);
    public class LoadMessageDataArgs : EventArgs
    {
        private String _strData;

        public String aData
        {
            get { return _strData; }
            set { _strData = value; }
        }
        public LoadMessageDataArgs(String str)
        {
            aData = str;
        }
    }
}
