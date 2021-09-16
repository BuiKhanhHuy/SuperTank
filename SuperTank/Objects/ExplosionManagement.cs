using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.General;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperTank.Objects
{
    class ExplosionManagement
    {
        private List<Explosion> explosions;

        public ExplosionManagement()
        {
            explosions = new List<Explosion>();
        }

        // tạo vụ nổ
        public void CreateExplosion(ExplosionSize explosionSize, Rectangle rectBullet)
        {
            Explosion explosion;
            explosion = new Explosion();
            switch (explosionSize)
            {
                case ExplosionSize.eSmallExplosion:
                    explosion.RectWidth = 40;
                    explosion.RectHeight = 40;
                    explosion.LoadImage(Common.path + @"\Images\explosion.png");
                        break;
                case ExplosionSize.eBigExplosion:
                    explosion.RectWidth = 60;
                    explosion.RectHeight = 60;
                    explosion.LoadImage(Common.path + @"\Images\explosion1.png");
                    break;
            }
            explosion.RectX = (rectBullet.X + rectBullet.Width / 2) -  explosion.RectWidth/2;
            explosion.RectY = (rectBullet.Y + rectBullet.Height / 2) - explosion.RectHeight / 2; 
            explosion.IsExplosion = true;
            this.explosions.Add(explosion);
            explosion = null;
        }

        // hiển thị toàn bộ danh sách vụ nổ
        public void ShowAllExplosion(Bitmap background)
        {
            for (int i = 0; i < this.Explosions.Count; i++)
            {
                // nếu còn cho phép nổ
                if (this.Explosions[i].IsExplosion)
                {
                    this.Explosions[i].Show(background);
                }
                else
                {
                    this.Explosions[i] = null;
                    this.Explosions.RemoveAt(i);
                }
            }
        }

        #region properties
        public List<Explosion> Explosions
        {
            get
            {
                return explosions;
            }

            set
            {
                explosions = value;
            }
        }
        #endregion properties
    }
}
