using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;

namespace SuperTank.Objects
{
    class BaseObject
    {
        protected Rectangle rect;
        protected Bitmap bmpObject;

        #region properties
        public Rectangle Rect
        {
            get
            {
                return rect;
            }

            set
            {
                rect = value;
            }
        }
        public int RectX
        {
            get
            {
                return rect.X;
            }

            set
            {
                rect.X = value;
            }
        }
        public int RectY
        {
            get
            {
                return rect.Y;
            }

            set
            {
                rect.Y = value;
            }
        }
        public int RectWidth
        {
            get
            {
                return rect.Width;
            }

            set
            {
                rect.Width = value;
            }
        }
        public int RectHeight
        {
            get
            {
                return rect.Height;
            }

            set
            {
                rect.Height = value;
            }
        }
        public Bitmap BmpObject
        {
            get
            {
                return bmpObject;
            }

            set
            {
                bmpObject = value;
            }
        }
        #endregion

        // load ảnh đối tượng
        public void LoadImage(string path)
        {
            this.bmpObject = new Bitmap(path);
        }

        // vẽ đối tượng vào bitmap nền
        public virtual void Show(Bitmap bmpBack)
        {
            Common.PaintObject(bmpBack, this.bmpObject, rect.X, rect.Y, 
                0, 0, this.RectWidth, this.RectHeight);
        }
    }
}
