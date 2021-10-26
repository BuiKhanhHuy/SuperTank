using SuperTank.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SuperTank.General;

namespace SuperTank
{
    class EnemyTank : Tank
    {
        private EnemyTankType enemyType;
        private int enemyTankDistance;
        public EnemyTank()
        {
            bmpEffect = new Bitmap(Common.path + @"\Images\effect2.png");
        }

        // kiểm tra xe tăng đich va chạm xe tăng player
        public bool IsPlayerTankCollision(PlayerTank playerTank)
        {
            if (this.IsObjectCollision(playerTank.Rect))
                return true;
            return false;
        }

        // kiểm tra xe tăng địch va chạm với xe tăng địch đồng minh
        public bool IsAlliedTanksCollision(List<EnemyTank> alliedTanks)
        {
            foreach (EnemyTank enemyTank in alliedTanks)
            {
                if (this.IsObjectCollision(enemyTank.Rect))
                    return true;
            }
            return false;
        }

        #region bộ não xử lí cách di chuyển của xe tăng địch
        // xử lí di chuyển của xe tăng type = normal
        public bool HandleMoveNormal(List<Wall> walls, PlayerTank playerTank, List<EnemyTank> alliedTanks)
        {
            // kiểm tra xe tăng địch có va chạm tường
            bool isWallCollision;
            // kiểm tra xe tăng địch có va chạm xe tăng player
            bool isPlayerTankCollision;
            // kiểm tra xe tăng địch có va chạm với xe tăng địch đồng minh
            bool isAlliedTanksCollision;

            isWallCollision = this.IsWallCollision(walls, this.directionTank);
            isPlayerTankCollision = this.IsPlayerTankCollision(playerTank);
            isAlliedTanksCollision = this.IsAlliedTanksCollision(alliedTanks);
            // nếu va chạm tường, player hoặc xe tăng đồng minh của địch thì xử lí đổi hướng
            if (isWallCollision || isAlliedTanksCollision || isPlayerTankCollision)
            {
                Random rand = new Random();
                // random ngẫu nhiên hướng di chuyển (0: left; 1:right; 2: up; 3: down)
                switch (rand.Next(0, 4))
                {
                    case 0:
                        Left = true;
                        Right = Up = Down = false;
                        break;
                    case 1:
                        Right = true;
                        Left = Up = Down = false;
                        break;
                    case 2:
                        Up = true;
                        Left = Right = Down = false;
                        break;
                    case 3:
                        Down = true;
                        Left = Right = Up = false;
                        break;
                }
                this.RotateFrame();
                rand = null;
                return false;
            }
            else
            {
                // xe tăng được phép di chuyển khi không va chạm gì
                this.IsMove = true;
                return true;
            }
        }


        // xử lí di chuyển của xe tăng type = medium
        bool isPriority = false;
        public bool HandleMoveMedium(List<Wall> walls, PlayerTank playerTank, List<EnemyTank> alliedTanks)
        {
            // kiểm tra xe tăng địch có va chạm tường
            bool isWallCollision;
            // kiểm tra xe tăng địch có va chạm xe tăng player
            bool isPlayerTankCollision;
            // kiểm tra xe tăng địch có va chạm với xe tắng địch đồng minh
            bool isAlliedTanksCollision;
            bool flag;
            flag = false;

            isWallCollision = this.IsWallCollision(walls, this.directionTank);
            isPlayerTankCollision = this.IsPlayerTankCollision(playerTank);
            isAlliedTanksCollision = this.IsAlliedTanksCollision(alliedTanks);
            // nếu va chạm tường hoặc xe tăng đồng minh của địch thì xử lí đổi hướng
            if ((isWallCollision || isAlliedTanksCollision || isPlayerTankCollision) && isPriority == false)
            {
                Random rand = new Random();
                // random ngẫu nhiên hướng di chuyển (0: left; 1:right; 2: up; 3: down)
                switch (rand.Next(0, 4))
                {
                    case 0:
                        Left = true;
                        Right = Up = Down = false;
                        break;
                    case 1:
                        Right = true;
                        Left = Up = Down = false;
                        break;
                    case 2:
                        Up = true;
                        Left = Right = Down = false;
                        break;
                    case 3:
                        Down = true;
                        Left = Right = Up = false;
                        break;
                }
                this.RotateFrame();
                rand = null;
                return false;
            }
            else
            if (playerTank.RectX != 17 * Common.STEP || playerTank.RectY != 36 * Common.STEP)
            {
                if (this.Rect.Top + this.Rect.Height / 2 > playerTank.Rect.Top &&
                   this.Rect.Top + this.Rect.Height / 2 < playerTank.Rect.Bottom &&
                   this.RectX > playerTank.RectX)
                {
                    Left = true;
                    Down = Up = Right = false;
                    flag = true;
                }
                else
                   if (this.Rect.Top + this.Rect.Height / 2 > playerTank.Rect.Top &&
                   this.Rect.Top + this.Rect.Height / 2 < playerTank.Rect.Bottom &&
                   this.RectX < playerTank.RectX)
                {
                    Right = true;
                    Down = Up = Left = false;
                    flag = true;
                }
                else
                   if (this.Rect.Left + this.Rect.Width / 2 > playerTank.Rect.Left &&
                   this.Rect.Left + this.Rect.Width / 2 < playerTank.Rect.Right &&
                   this.RectY > playerTank.RectY)
                {
                    Up = true;
                    Left = Down = Right = false;
                    flag = true;
                }
                else
                   if (this.Rect.Left + this.Rect.Width / 2 > playerTank.Rect.Left &&
                   this.Rect.Left + this.Rect.Width / 2 < playerTank.Rect.Right &&
                   this.RectY < playerTank.RectY)
                {
                    Down = true;
                    Left = Up = Right = false;
                    flag = true;
                }
                if (flag)
                {
                    isPriority = true;
                    flag = false;
                    this.RotateFrame();
                    this.isMove = false;
                    return false;
                }
                else
                {
                    if (isPriority == true)
                    {
                        isPriority = false;
                        return false;
                    }
                    else
                    {
                        this.isMove = true;
                        return true;
                    }
                }
            }
            else
            {
                isPriority = false;
                this.isMove = true;
                return true;
            }
        }
        #endregion bộ não xử lí cách di chuyển của xe tăng địch

        #region properties
        public EnemyTankType EnemyType
        {
            get
            {
                return enemyType;
            }

            set
            {
                enemyType = value;
            }
        }
        public int EnemyTankDistance
        {
            get
            {
                return enemyTankDistance;
            }

            set
            {
                enemyTankDistance = value;
            }
        }
        #endregion properties
    }
}
