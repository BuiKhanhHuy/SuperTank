using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.Objects;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Tank : BaseObject
    {
        #region số frame lớn nhất
        protected const int MAX_NUMBER_SPRITE_TANK = 7;
        protected const int MAX_NUMBER_SPRITE_EFFECT = 6;
        #endregion
        #region Số làm việc với frame (tank: có 8 frame 0-7; effect: có 6 frame 0-5)
        protected int frx_tank = 7;
        protected int frx_effect = 0;
        protected int fry_effect = 0;
        #endregion
        protected int moveSpeed;
        protected int tankBulletSpeed;
        protected int energy;
        private BulletType bulletType;
        protected Skin skinTank;
        protected bool isMove;
        private bool isActivate;
        protected bool left, right, up, down;
        protected Direction directionTank;
        private List<Bullet> bullets;
        protected Bitmap bmpEffect;
        // contructor
        public Tank()
        {
            this.isActivate = false;
            this.RectWidth = Common.tankSize;
            this.RectHeight = Common.tankSize;
            this.Bullets = new List<Bullet>();
            this.BulletType = BulletType.eTriangleBullet;
        }

        // hiển thị xe tăng
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
                // nếu xe tăng được di chuyển bánh xe sẽ xoay
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

        // xoay frame xe tăng
        public void RotateFrame()
        {
            // xoay ảnh phù hợp với frame xe tăng
            if ((left == true && this.DirectionTank == Direction.eDown) ||
                (right == true && this.DirectionTank == Direction.eUp) ||
                (up == true && this.DirectionTank == Direction.eLeft) ||
                (down == true && this.DirectionTank == Direction.eRight))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if ((left == true && this.DirectionTank == Direction.eUp) ||
               (right == true && this.DirectionTank == Direction.eDown) ||
               (up == true && this.DirectionTank == Direction.eRight) ||
               (down == true && this.DirectionTank == Direction.eLeft))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else if ((left == true && this.DirectionTank == Direction.eRight) ||
               (right == true && this.DirectionTank == Direction.eLeft) ||
               (up == true && this.DirectionTank == Direction.eDown) ||
               (down == true && this.DirectionTank == Direction.eUp))
            {
                this.bmpObject.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else
            {
                this.bmpObject.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            }
            // cập nhật hướng của xe tăng
            if (left)
                directionTank = Direction.eLeft;
            else if (right)
                directionTank = Direction.eRight;
            else if (up)
                directionTank = Direction.eUp;
            else if (down)
                directionTank = Direction.eDown;
        }

        // tạo đạn cho xe tăng
        public void CreatBullet(string pathRoundBullet, string pathRocketBullet)
        {
            if (this.bullets.Count == 0 && this.IsActivate)
            {
                // đạn
                Bullet bullet;
                bullet = new Bullet();
                bullet.SpeedBullet = this.TankBulletSpeed;

                // set loại bullet
                switch (this.bulletType)
                {
                    case BulletType.eTriangleBullet:
                        bullet.LoadImage(Common.path + pathRoundBullet);
                        // đạn tam giác có kích thước 8x8
                        bullet.RectWidth =8;
                        bullet.RectHeight = 8;
                        // năng lượng của đạn tam giác mặc định là 10
                        bullet.Power = 10;
                        break;
                    case BulletType.eRocketBullet:
                        bullet.LoadImage(Common.path + pathRocketBullet);
                        // đạn rocket có kích thước 12x12
                        bullet.RectWidth = 12;
                        bullet.RectHeight = 12;
                        // năng lượng của đạn rocket mặc định là 40
                        bullet.Power = 30;
                        break;
                }
                // hướng của xe tăng
                switch (directionTank)
                {
                    case Direction.eLeft:
                        bullet.DirectionBullet = Direction.eLeft;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        bullet.RectX = this.RectX + bullet.RectWidth;
                        bullet.RectY = this.RectY + this.RectHeight / 2 - bullet.RectHeight / 2;
                        break;
                    case Direction.eRight:
                        bullet.DirectionBullet = Direction.eRight;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        bullet.RectX = this.RectX + this.RectWidth - bullet.RectWidth;
                        bullet.RectY = this.RectY + this.RectHeight / 2 - bullet.RectHeight / 2;
                        break;
                    case Direction.eUp:
                        bullet.DirectionBullet = Direction.eUp;
                        bullet.RectY = this.RectY + bullet.RectHeight;
                        bullet.RectX = this.RectX + this.RectWidth / 2 - bullet.RectWidth / 2;
                        break;
                    case Direction.eDown:
                        bullet.DirectionBullet = Direction.eDown;
                        bullet.BmpObject.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bullet.RectY = this.RectY + this.RectHeight - bullet.RectHeight;
                        bullet.RectX = this.RectX + this.RectWidth / 2 - bullet.RectWidth / 2;
                        break;
                }
                this.bullets.Add(bullet);
                bullet = null;
            }
        }

        // hủy một viên đạn
        public void RemoveOneBullet(int index)
        {
            this.bullets[index] = null;
            this.bullets.RemoveAt(index);
        }

        // di chuyển và hiển thị đạn xe tăng
        public void ShowBulletAndMove(Bitmap background)
        {
            for (int i = 0; i < this.Bullets.Count; i++)
            {
                this.Bullets[i].BulletMove();
                this.Bullets[i].Show(background);
            }
        }

        // kiểm tra va chạm của xe tăng với một đối tượng
        public bool IsObjectCollision(Rectangle rectObj)
        {
            switch (this.directionTank)
            {
                case Direction.eLeft:
                    if (this.Rect.Left == rectObj.Right)
                        if (this.Rect.Top >= rectObj.Top && this.Rect.Top < rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Top && this.Rect.Bottom <= rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Bottom && this.Rect.Top < rectObj.Top)
                        {
                            return true;
                        }
                    break;
                case Direction.eRight:
                    if (this.Rect.Right == rectObj.Left)
                        if (this.Rect.Top >= rectObj.Top && this.Rect.Top < rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Top && this.Rect.Bottom <= rectObj.Bottom ||
                            this.Rect.Bottom > rectObj.Bottom && this.Rect.Top < rectObj.Top)
                        {
                            return true;
                        }
                    break;
                case Direction.eUp:
                    if (this.Rect.Top == rectObj.Bottom)
                        if (this.Rect.Left < rectObj.Right && this.Rect.Left >= rectObj.Left ||
                            this.Rect.Right > rectObj.Left && this.Rect.Right <= rectObj.Right ||
                            this.Rect.Right > rectObj.Right && this.Rect.Left < rectObj.Left)
                        {
                            return true;
                        }
                    break;
                case Direction.eDown:
                    if (this.Rect.Bottom == rectObj.Top)
                        if (this.Rect.Left < rectObj.Right && this.Rect.Left >= rectObj.Left ||
                            this.Rect.Right > rectObj.Left && this.Rect.Right <= rectObj.Right ||
                            this.Rect.Right >= rectObj.Right && this.Rect.Left <= rectObj.Left)
                        {
                            return true;
                        }
                    break;
            }
            return false;
        }

        // kiểm tra xe tăng chạm tường
        public bool IsWallCollision(List<Wall> walls, Direction directionTank)
        {
            foreach (Wall wall in walls)
                // nếu không phải bụi cây thì xét va chạm
                if (wall.WallNumber != 4)
                    if (IsObjectCollision(wall.Rect))
                        return true;
            return false;
        }

        // xe tăng di chuyển
        public void Move()
        {
            if (this.IsActivate)
            {
                if (left)
                {
                    this.RectX -= this.MoveSpeed;
                }
                else if (right)
                {
                    this.RectX += this.MoveSpeed;
                }
                else if (up)
                {
                    this.RectY -= this.MoveSpeed;
                }
                else if (down)
                {
                    this.RectY += this.MoveSpeed;
                }
            }
        }

        #region properties
        public int MoveSpeed
        {
            get
            {
                return moveSpeed;
            }

            set
            {
                moveSpeed = value;
            }
        }
        public int TankBulletSpeed
        {
            get
            {
                return tankBulletSpeed;
            }

            set
            {
                tankBulletSpeed = value;
            }
        }
        public int Energy
        {
            get
            {
                return energy;
            }

            set
            {
                energy = value;
            }
        }
        public bool Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }
        public bool Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }
        public bool Up
        {
            get
            {
                return up;
            }

            set
            {
                up = value;
            }
        }
        public bool Down
        {
            get
            {
                return down;
            }

            set
            {
                down = value;
            }
        }
        public Direction DirectionTank
        {
            get
            {
                return directionTank;
            }

            set
            {
                directionTank = value;
            }
        }
        public bool IsMove
        {
            get
            {
                return isMove;
            }

            set
            {
                isMove = value;
            }
        }
        public List<Bullet> Bullets
        {
            get
            {
                return bullets;
            }

            set
            {
                bullets = value;
            }
        }
        public Skin SkinTank
        {
            get
            {
                return skinTank;
            }

            set
            {
                skinTank = value;
            }
        }

        public bool IsActivate
        {
            get
            {
                return isActivate;
            }

            set
            {
                isActivate = value;
            }
        }

        public BulletType BulletType {
            get
            {
                return bulletType;
            }
            set
            {
                bulletType = value;
            }
        }
        #endregion property
    }
}
