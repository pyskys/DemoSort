using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DemoSort.CommonsData
{
    public class Commons
    {

        //Hàm duy chuyển panel xuất hiện ra vị trí định sẵn
        public static void MovePanel(Panel pnl, int XFinish = 0, int YFinish = 0, int iSleep = 0)
        {
            if (pnl == null) return;
            new Thread(() =>
            {
                //Khoản cách từ vị trí hiện tại đến điểm kết thúc
                int dx = pnl.Location.X - XFinish;
                int dy = pnl.Location.Y - YFinish;

                //iXAdd và iYAdd là các giá trị dương dược cộng thêm để dịch chuyển
                int iXAdd = Math.Abs(dx / 50);
                int iYAdd = Math.Abs(dy / 50);

                iXAdd = iXAdd > 5 ? 5 : iXAdd;
                iXAdd = iXAdd < 1 ? 1 : iXAdd;

                iYAdd = iYAdd > 5 ? 5 : iYAdd;
                iYAdd = iYAdd < 1 ? 1 : iYAdd;

                //Giá trị khoản cách mặc định làm tốc độ giảm xuống
                int iDefaultDistance = 50;
                int iNext = 1;


                //Duy chuyển từ phải sang trái
                while (dx > 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(pnl.Location.X - XFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iXAdd;

                    dx -= iNext;
                    pnl.Location = new Point(pnl.Location.X - iNext, pnl.Location.Y);

                    //Tăng về 0 nếu trừ vượt xuống 0
                    if (dx < 0) dx = 0;
                }

                //dx<=0. di chuyển từ trái sang phải
                while (dx < 0)
                {
                    Thread.Sleep(iSleep);

                    //Tính lại khoản cách. nếu dưới mặc định thì giảm tốc độ
                    if (Math.Abs(pnl.Location.X - XFinish) <= iDefaultDistance)
                        iNext = 1;
                    else
                        iNext = iXAdd;

                    dx += iNext;
                    pnl.Location = new Point(pnl.Location.X + iNext, pnl.Location.Y);

                    //Giảm về 0 nếu trừ vượt xuống 0
                    if (dx > 0) dx = 0;
                }

            }).Start();
        }
    }
}
