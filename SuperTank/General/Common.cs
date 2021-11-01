using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using SuperTank.Objects;

namespace SuperTank.General
{
    class Common
    {
        #region Hằng số các thông số cố định
        public const int SCREEN_WIDTH = 1100;
        public const int SCREEN_HEIGHT = 900;
        public const int NUMBER_OBJECT_WIDTH = 45;
        public const int NUMBER_OBJECT_HEIGHT = 40;
        public const int MAX_LEVEL = 10;
        public const int STEP = 20;
        public const int tankSize = 40;
        #endregion
        public static string path;

        // load hình ảnh
        public static Bitmap LoadImage(string fileName)
        {
            return new Bitmap(Common.path + fileName);
        }

        // vẽ lên bitmap
        public static void PaintObject(Bitmap bmpBack, Bitmap front, int x, int y, int xFrame, int yFrame, int wFrame, int hFrame)
        {
            Graphics g = Graphics.FromImage(bmpBack);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawImage(front, x, y, new Rectangle(xFrame, yFrame, wFrame, hFrame), GraphicsUnit.Pixel);
            //g.DrawRectangle(new Pen(Color.Yellow, 2)
            //    , x, y, wFrame, hFrame);
            g.Dispose();
        }

        // clear bitmap
        public static void PaintClear(Bitmap bmpBack)
        {
            Graphics g = Graphics.FromImage(bmpBack);
            g.Clear(Color.Black);
            g.Dispose();
        }

        // đọc map vào ma trận
        public static int[,] ReadMap(string path, int numberObjectHeight, int numberObjectWidth)
        {
            int[,] arrayObject;
            string s = "";
            using (StreamReader reader = new StreamReader(path))
            {
                arrayObject = new int[numberObjectHeight, numberObjectWidth];
                for (int i = 0; i < numberObjectHeight; i++)
                {
                    s = reader.ReadLine();
                    for (int j = 0; j < numberObjectWidth; j++)
                        arrayObject[i, j] = int.Parse(s[j].ToString());
                }
                return arrayObject;
            }
        }

        // kiểm tra va chạm giữa hai hình chữ nhật
        public static bool IsCollision(Rectangle rect1, Rectangle rect2)
        {
            // góc dưới phải
            if (rect1.Left > rect2.Left && rect1.Left < rect2.Right)
            {
                if (rect1.Top > rect2.Top && rect1.Top < rect2.Bottom)
                {
                    return true;
                }
            }
            // góc trên phải
            if (rect1.Left > rect2.Left && rect1.Left < rect2.Right)
            {
                if (rect1.Bottom > rect2.Top && rect1.Bottom < rect2.Bottom)
                {
                    return true;
                }
            }
            // góc dưới trái
            if (rect1.Right > rect2.Left && rect1.Right < rect2.Right)
            {
                if (rect1.Top > rect2.Top && rect1.Top < rect2.Bottom)
                {
                    return true;
                }
            }
            // góc trên trái
            if (rect1.Right > rect2.Left && rect1.Right < rect2.Right)
            {
                if (rect1.Bottom > rect2.Top && rect1.Bottom < rect2.Bottom)
                {
                    return true;
                }
            }
            //=============================================================
            // góc dưới phải
            if (rect2.Left > rect1.Left && rect2.Left < rect1.Right)
            {
                if (rect2.Top > rect1.Top && rect2.Top < rect1.Bottom)
                {
                    return true;
                }
            }
            // góc trên phải
            if (rect2.Left > rect1.Left && rect2.Left < rect1.Right)
            {
                if (rect2.Bottom > rect1.Top && rect2.Bottom < rect1.Bottom)
                {
                    return true;
                }
            }
            // góc dưới trái
            if (rect2.Right > rect1.Left && rect2.Right < rect1.Right)
            {
                if (rect2.Top > rect1.Top && rect2.Top < rect1.Bottom)
                {
                    return true;
                }
            }
            // góc trên trái
            if (rect2.Right > rect1.Left && rect2.Right < rect1.Right)
            {
                if (rect2.Bottom > rect1.Top && rect2.Bottom < rect1.Bottom)
                {
                    return true;
                }
            }
            //=============================================================
            if (rect1.Left == rect2.Left && rect1.Right == rect2.Right &&
                rect1.Top == rect2.Top && rect1.Bottom == rect2.Bottom)
                return true;
            return false;
        }

    }
}
