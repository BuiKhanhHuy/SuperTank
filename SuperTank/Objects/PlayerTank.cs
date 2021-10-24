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
        private bool isShield;
        private Bitmap bmpShield;


        public PlayerTank()
        {
            this.moveSpeed = 10;
            this.tankBulletSpeed = 20;
            this.energy = 100;
            this.SetLocation();
            this.DirectionTank = Direction.eUp;
            this.SkinTank = Skin.eYellow;
            bmpEffect = new Bitmap(Common.path + @"\Images\effect1.png");
            bmpShield = new Bitmap(Common.path + @"\Images\shield.png");
        }

        // cập nhật vị trí xe tăng player
        public void SetLocation()
        {
            int i = 17, j = 36;
            this.RectX = i * Common.STEP;
            this.RectY = j * Common.STEP;
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

        // hiển thị xe tăng player
        public override void Show(Bitmap background)
        {
            // nếu xe tăng đang bật chế độ hoạt động sẽ hiển thị xe tăng, 
            // ngược lại hiện thị hiệu ứng xuất hiện
            if (IsActivate)
            {
                switch (directionTank)
                {
                    case Direction.eUp:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                               (int)skinTank * Common.tankSize, frx_tank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eDown:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                               (MAX_NUMBER_SPRITE_TANK - (int)skinTank) * Common.tankSize, frx_tank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eLeft:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                                 frx_tank * Common.tankSize, (MAX_NUMBER_SPRITE_TANK - (int)skinTank) * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                    case Direction.eRight:
                        Common.PaintObject(background, this.bmpObject, rect.X, rect.Y,
                            frx_tank * Common.tankSize, (int)skinTank * Common.tankSize, this.RectWidth, this.RectHeight);
                        break;
                }
                // nếu xe tăng player đang ở chế độ được bảo vệ -> show vòng tròn bảo vệ
                if (this.isShield)
                {
                    Common.PaintObject(background, this.bmpShield, rect.X, rect.Y, 0, 0, 40, 40);
                }
                //nếu xe tăng được di chuyển bánh xe sẽ xoay
                if (this.isMove)
                {
                    frx_tank--;
                    if (frx_tank == -1)
                        frx_tank = MAX_NUMBER_SPRITE_TANK;
                }
            }
            else
            {
                // hiển thị hiệu ứng xuất hiện
                Common.PaintObject(background, this.bmpEffect, this.RectX, this.RectY,
                       frx_effect * this.RectWidth, fry_effect * this.RectHeight, this.RectWidth, this.RectHeight);
                frx_effect++;
                if (frx_effect == MAX_NUMBER_SPRITE_EFFECT)
                {
                    frx_effect = 0;
                    fry_effect++;
                    if (fry_effect == MAX_NUMBER_SPRITE_EFFECT)
                    {
                        fry_effect = 0;
                        // hiệu ứng kết thúc, bật lại hoạt động của xe
                        IsActivate = true;
                    }
                }
            }
        }

        #region properties
        public bool IsShield
        {
            get { return isShield; }
            set { isShield = value; }
        }
        #endregion properties
    }
}
