using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;
using SuperTank.Objects;
using System.IO;

namespace SuperTank.Objects
{
    class EnemyTankManagement
    {
        private List<EnemyTank> enemyTanks;
        private List<EnemyTankParameter> enemyTankParameters;
        private int numberEnemyTankDestroy;

        public EnemyTankManagement()
        {
            EnemyTanks = new List<EnemyTank>();
            EnemyTankParameters = new List<EnemyTankParameter>();
        }

        // load danh sách địch
        public void Init_EnemyTankManagement(string path)
        {
            this.numberEnemyTankDestroy = 0;
            // đọc thông số tất cả xe tăng địch
            this.ReadEnemyTankParameters(path);
            // tạo xe tăng địch
            this.CreateListEnemyTank();
        }

        // giải phóng danh sách địch
        public void EnemyTanksClear()
        {
            this.enemyTankParameters.Clear();
            this.enemyTanks.Clear();
        }

        #region Các hàm khởi tạo xe tăng địch nội bộ
        // đọc thông số xe tăng địch vào danh sách
        private void ReadEnemyTankParameters(string path)
        {
            string s = "";
            using (StreamReader reader = new StreamReader(path))
            {
                EnemyTankParameter enemyTankParameter;
                while ((s = reader.ReadLine()) != null)
                {
                    string[] token = s.Split('#');
                    enemyTankParameter = new EnemyTankParameter();
                    enemyTankParameter.Type = int.Parse(token[0]);
                    enemyTankParameter.Energy = int.Parse(token[1]);
                    enemyTankParameter.TankMoveSpeed = int.Parse(token[2]);
                    enemyTankParameter.TankBulletSpeed = int.Parse(token[3]);
                    enemyTankParameter.XEnemyTank = int.Parse(token[4]);
                    enemyTankParameter.YEnemyTank = int.Parse(token[5]);
                    enemyTankParameter.maxNumberEnemyTank = int.Parse(token[6]);
                    this.EnemyTankParameters.Add(enemyTankParameter);
                }
                enemyTankParameter = null;
                s = null;
            }
            //Console.WriteLine("Số lượng địch: " + this.enemyTankParameters.Count);
        }
        // tạo danh sách xe tăng địch đưa và danh sách
        private void CreateListEnemyTank()
        {
            foreach (EnemyTankParameter enemyTankParameter in EnemyTankParameters)
            {
                // thêm từng xe tăng địch vào danh sách
                this.EnemyTanks.Add(this.CreateOneEnemyTank(enemyTankParameter));
                // đếm số lượng địch cần tiêu diệt
                numberEnemyTankDestroy += enemyTankParameter.maxNumberEnemyTank;
            }
            //Console.WriteLine("Số lượng địch cần tiêu diệt: " + numberEnemyTankDestroy);
            //Console.WriteLine("Đã tạo " + this.NumberEnemyTank() + " xe tăng địch");
        }
        // skin xe tăng địch thay đổi theo năng lượng địch
        public Skin SkinEnemyTank(EnemyTank enemyTank)
        {
            switch (enemyTank.Energy)
            {
                case 70:
                    // skin màu đỏ
                    return Skin.eRed;
                case 60:
                    // skin màu xanh cam
                    return Skin.eOrange;
                case 50:
                    // skin màu xanh dương
                    return Skin.eBlue;
                case 40:
                    // skin màu tím
                    return Skin.ePurple;
                case 30:
                    // skin màu hồng
                    return Skin.ePink;
                case 20:
                    // skin màu xanh lục
                    return Skin.eGreen;
                case 10:
                    // skin màu xanh sáng
                    return Skin.eLightBlue;
            }
            return Skin.eLightBlue;
        }
        // tạo một xe tăng địch
        private EnemyTank CreateOneEnemyTank(EnemyTankParameter enemyTankParameter)
        {
            EnemyTank enemyTank;
            enemyTank = new EnemyTank();
            this.UpdateParameter(enemyTank, enemyTankParameter);
            return enemyTank;
        }
        #endregion Các hàm khởi tạo xe tăng địch nội bộ

        // cập nhật tham số xe tăng địch
        public void UpdateParameter(EnemyTank enemyTank, EnemyTankParameter enemyTankParameter)
        {

            enemyTank.DirectionTank = Direction.eUp;
            enemyTank.IsMove = true;
            // thiết lập loại xe tăng (0: normal; 1: medium)
            switch (enemyTankParameter.Type)
            {
                case 0:
                    enemyTank.EnemyType = EnemyTankType.eNormal;
                    enemyTank.LoadImage(Common.path + @"\Images\tank0.png");
                    break;
                case 1:
                    enemyTank.EnemyType = EnemyTankType.eMedium;
                    enemyTank.LoadImage(Common.path + @"\Images\tank1.png");
                    break;
            }
            enemyTank.Energy = enemyTankParameter.Energy;
            enemyTank.MoveSpeed = enemyTankParameter.TankMoveSpeed;
            enemyTank.TankBulletSpeed = enemyTankParameter.TankBulletSpeed;
            // update skin xe tăng địch khi biết năng lượng
            enemyTank.SkinTank = this.SkinEnemyTank(enemyTank);
            enemyTank.RectX = enemyTankParameter.XEnemyTank * Common.STEP;
            enemyTank.RectY = enemyTankParameter.YEnemyTank * Common.STEP;
        }

        // di chuyển toàn bộ danh sách xe tăng địch 
        public void MoveAllEnemyTank(List<Wall> walls, PlayerTank playerTank)
        {
            bool isMove_local = false;
            foreach (EnemyTank enemyTank in EnemyTanks)
            {
                switch (enemyTank.EnemyType)
                {
                    case EnemyTankType.eNormal:
                        isMove_local = enemyTank.HandleMoveNormal(walls, playerTank,this.EnemyTanks);
                        break;
                    case EnemyTankType.eMedium:
                        isMove_local = enemyTank.HandleMoveMedium(walls, playerTank, this.EnemyTanks);
                        break;
                }
                // di chuyển xe tăng địch khi đã qua xử lí là đã được phép di chuyển
                if (isMove_local)
                    enemyTank.Move();
            }
        }

        // hiển thị toàn bộ danh sách xe tăng địch
        public void ShowAllEnemyTank(Bitmap background)
        {
            foreach (EnemyTank enemyTank in EnemyTanks)
            {
                enemyTank.Show(background);
            }
        }

        // số lượng xe tăng địch
        public int NumberEnemyTank()
        {
            return this.numberEnemyTankDestroy;
        }

        #region properties
        public List<EnemyTank> EnemyTanks
        {
            get
            {
                return enemyTanks;
            }

            set
            {
                enemyTanks = value;
            }
        }
        public List<EnemyTankParameter> EnemyTankParameters
        {
            get
            {
                return enemyTankParameters;
            }

            set
            {
                enemyTankParameters = value;
            }
        }
        public int NumberEnemyTankDestroy
        {
            set { numberEnemyTankDestroy = value; }
            get { return numberEnemyTankDestroy; }
        }
        #endregion properties
    }
}
