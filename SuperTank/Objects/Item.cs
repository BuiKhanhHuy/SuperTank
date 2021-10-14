using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperTank.General;

namespace SuperTank.Objects
{
    class Item : BaseObject
    {
        #region hằng số vị trí mặc đinh
        private const int x_default = -20;
        private const int y_default = -20;
        #endregion  hằng số vị trí mặc đinh

        private bool isOn;
        private ItemType itemType;

        public Item()
        {
            this.RectX = x_default;
            this.RectY = y_default;
            this.RectWidth = this.RectHeight = Common.tankSize;
            isOn = false;
        }

        // hiển thị vật phẩm ra giao diện chơi
        public void CreateItem(Point itemPoint)
        {
            // tạo vật phẩm và bắt đầu chạy thời gian hiển thị vật phẩm
            // tìm vị trí cho item
            this.RectX = itemPoint.X;
            this.RectY = itemPoint.Y;
            Random rand = new Random();
            switch (rand.Next(0, 4))
            {
                case 0:
                    this.ItemType = ItemType.eItemHeart;
                    this.LoadImage(Common.path + @"\Images\icon_heart.png");
                    break;
                case 1:
                    this.ItemType = ItemType.eItemShield;
                    this.LoadImage(Common.path + @"\Images\icon_shield.png");
                    break;
                case 2:
                    this.ItemType = ItemType.eItemGrenade;
                    this.LoadImage(Common.path + @"\Images\icon_grenade.png");
                    break;
                case 3:
                    this.ItemType = ItemType.eItemRocket;
                    this.LoadImage(Common.path + @"\Images\icon_rocket.png");
                    break;
            }
            rand = null;
        }

        #region properties
        public ItemType ItemType
        {
            get
            {
                return itemType;
            }

            set
            {
                itemType = value;
            }
        }
        public bool IsOn
        {
            get
            {
                return isOn;
            }

            set
            {
                isOn = value;
            }
        }
        #endregion properties

    }
}
