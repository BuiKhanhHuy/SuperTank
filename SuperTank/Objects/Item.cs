using System;
using System.Collections.Generic;
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
        //private Timer timer;
        private ItemType itemType;
        //private int time;
        //private int time_copy;

        public Item(int time)
        {
            // bộ đếm thời gian item
            //timer = new Timer();
            //timer.Tick += Timer_Tick;
            //timer.Interval = 1000;
            //this.time = time;
            //this.time_copy = time;
            this.RectX = x_default;
            this.RectY = y_default;
            this.RectWidth = this.RectHeight = Common.tankSize;
            isOn = false;
        }

        //// bộ đếm thời gian hiển thị vật phẩm
        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    time_copy--;
        //    if (time_copy < 0)
        //    {
        //        this.RectX = x_default;
        //        this.RectY = y_default;
        //        this.time_copy = this.time;
        //        timer.Stop();
        //    }
        //}

        // hiển thị vật phẩm ra giao diện chơi
        public void CreateItem()
        {
            // tạo vật phẩm và bắt đầu chạy thời gian hiển thị vật phẩm
            //timer.Start();
            this.RectX = 200;
            this.RectY = 200;
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
                    this.ItemType = ItemType.eItemTimer;
                    this.LoadImage(Common.path + @"\Images\icon_timer.png");
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
