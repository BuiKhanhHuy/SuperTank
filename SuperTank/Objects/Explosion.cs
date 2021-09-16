using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Explosion : BaseObject
    {
        #region hằng số frame lớn nhất
        private const int maxFrameX = 5;
        private const int maxFrameY = 2;
        #endregion hằng số frame lớn nhất

        #region các thông số chuyển frame
        private int framex = 0;
        private int framey = 0;
        #endregion các thông số chuyển frame

        private ExplosionSize explosionSize;
        private bool isExplosion;

        // hiển thị vụ nổ
        public override void Show(Bitmap bmpBack)
        {
            Common.PaintObject(bmpBack, this.bmpObject, this.RectX, this.RectY,
                this.framex * this.RectWidth, this.framey * this.RectHeight, this.RectWidth, this.RectHeight);
            this.framex++;
            if (framex == maxFrameX)
            {
                framex = 0;
                framey++;
                if (framey == maxFrameY)
                    this.isExplosion = false;
            }
        }

        #region properties
        public bool IsExplosion
        {
            get
            {
                return isExplosion;
            }

            set
            {
                isExplosion = value;
            }
        }
        public ExplosionSize ExplosionSize
        {
            get
            {
                return explosionSize;
            }

            set
            {
                explosionSize = value;
            }
        }
        #endregion properties
    }
}
