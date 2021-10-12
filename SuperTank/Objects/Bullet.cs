using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Bullet : BaseObject
    {
        private Direction directionBullet;
        private int speedBullet;
        private int power;
            
        // viên đạn di chuyển
        public void BulletMove()
        {
            switch (directionBullet)
            {
                case Direction.eLeft:
                    this.RectX -= speedBullet;
                    break;
                case Direction.eRight:
                    this.RectX += speedBullet;
                    break;
                case Direction.eUp:
                    this.RectY -= speedBullet;
                    break;
                case Direction.eDown:
                    this.RectY += speedBullet;
                    break;
            }
        }

        #region properties
        public Direction DirectionBullet
        {
            get
            {
                return directionBullet;
            }

            set
            {
                directionBullet = value;
            }
        }
        public int SpeedBullet
        {
            get
            {
                return speedBullet;
            }

            set
            {
                speedBullet = value;
            }
        }

        public int Power {
            get
            {
                return power;
            }
            set
            {
                power = value;
            }
        }
        #endregion
    }
}
