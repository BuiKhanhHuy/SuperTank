using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;
using System.Windows.Forms;

namespace SuperTank.Objects
{
    class PlayerTank : Tank
    {
        public PlayerTank()
        {
            this.moveSpeed = 10;
            this.tankBulletSpeed = 20;
            this.energy = 100;
            int i = 21, j = 39;
            this.RectX = i * Common.STEP;
            this.RectY = j * Common.STEP;
            this.DirectionTank = Direction.eUp;
            this.SkinTank = Skin.eYellow;
            bmpEffect = new Bitmap(Common.path + @"\Images\effect1.png");
        }

        // kiểm tra xe tăng player va chạm với xe tăng địch
        public bool IsEnemyTankCollisions(List<EnemyTank> enemyTanks)
        {
            foreach (EnemyTank enemyTank in enemyTanks)
            {
                if (this.IsObjectCollision(enemyTank.Rect))
                    return true;
            }
            return false;
        }
    }
}
