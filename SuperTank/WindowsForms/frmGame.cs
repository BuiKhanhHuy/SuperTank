using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SuperTank.General;
using SuperTank.Objects;

namespace SuperTank
{
    public partial class frmGame : Form
    {
        #region Đồ họa graphics
        private Graphics graphics;
        #endregion Đồ họa graphics
        #region các thuộc tính chung
        private Bitmap background;
        private int[,] map;
        #endregion các thuộc tính chung
        #region Đối tượng
        private WallManagement wallManager;
        private ExplosionManagement explosionManager;
        private PlayerTank playerTank;
        private EnemyTankManagement enemyTankManager;
        private Item item;
        #endregion Đối tượng
        public frmGame()
        {
            Common.path = Application.StartupPath + @"\Content";
            InitializeComponent();
        }
        private void frmGame_Load(object sender, EventArgs e)
        {
            picHeart.Image = Image.FromFile(Common.path + @"\Images\heart.png");
            //picCastle.Image = Image.FromFile(Common.path + @"\Images\castle.png");
            // khởi tạo graphics
            graphics = pnMainGame.CreateGraphics();
            // khỏi tạo background
            background = new Bitmap(Common.SCREEN_WIDTH, Common.SCREEN_HEIGHT);
            // khởi tạo map
            map = new int[Common.NUMBER_OBJECT_HEIGHT, Common.NUMBER_OBJECT_WIDTH];
            // tạo đối tượng quản lí tường
            wallManager = new WallManagement();
            // tạo đối tượng quản lí vụ nổ
            explosionManager = new ExplosionManagement();
            // tạo đối tượng xe tăng player
            playerTank = new PlayerTank();
            playerTank.LoadImage(Common.path + @"\Images\tank.png");
            // khởi tạo quà
            item = new Item(10);
            // khởi tạo game
            GameStart();
        }

        // hàm khởi tạo game mới
        private void GameStart()
        {
            // load map
            Array.Copy(Common.ReadMap(String.Format("{0}{1:00}.txt", Common.path + @"\Maps\Map", 2),
                Common.NUMBER_OBJECT_HEIGHT, Common.NUMBER_OBJECT_WIDTH),
            this.map, Common.NUMBER_OBJECT_HEIGHT * Common.NUMBER_OBJECT_WIDTH);
            // tạo danh sách tường
            wallManager.CreatWall(this.map);
            // khởi tạo danh sách địch
            enemyTankManager = null;
            enemyTankManager = new EnemyTankManagement(String.Format("{0}{1:00}.txt",
                Common.path + @"\EnemyTankParameters\EnemyParameter", 2));
        }

        #region Vòng lặp game
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // xóa background
            Common.PaintClear(this.background);
            // hiển thị castle
            Common.PaintObject(this.background, new Bitmap(Common.path + @"\Images\castle.png"),
                500, 680, 0, 0, 80, 80);

            // vẽ và di chuyển đạn player
            playerTank.ShowBulletAndMove(this.background);
            // tạo và di chuyển đạn của địch
            foreach (EnemyTank enemyTank in enemyTankManager.EnemyTanks)
            {
                enemyTank.CreatBullet(Common.path + @"\Images\bullet2.png");
                enemyTank.ShowBulletAndMove(this.background);
            }

            // di chuyển toàn bộ xe tăng địch
            enemyTankManager.MoveAllEnemyTank(wallManager.Walls, playerTank);
            // hiển thị toàn bộ xe tăng địch
            enemyTankManager.ShowAllEnemyTank(this.background);

            #region đạn player và đạn địch trúng tường
            for (int i = wallManager.Walls.Count - 1; i >= 0; i--)
            {
                // chạy danh sách đạn player và kiểm tra
                for (int j = 0; j < playerTank.Bullets.Count; j++)
                {
                    // nếu đạn xe tăng player trúng tường 
                    if (Common.IsCollision(playerTank.Bullets[j].Rect, wallManager.Walls[i].Rect))
                    {
                        // viên đạn bị hủy nếu nó trúng, không phải bụi cây(4)
                        if (wallManager.Walls[i].WallNumber != 4 &&
                            wallManager.Walls[i].WallNumber != 5)
                        {
                            // thêm vụ nổ vào danh sách
                            explosionManager.CreateExplosion(ExplosionSize.eSmallExplosion, playerTank.Bullets[j].Rect);
                            // viên đạn xe tăng player này bị hủy
                            playerTank.RemoveOneBullet(j);
                        }
                        // hủy viên gạch đi khi nó là gạch có thể phá hủy
                        if (wallManager.Walls[i].WallNumber == 1)
                        {
                            Console.WriteLine("Ta bắn trúng tường có thể phá.");
                            wallManager.RemoveOneWall(i);
                        }
                        else
                        // player tự bắn trúng boss của player
                        if (wallManager.Walls[i].WallNumber == 6)
                        {
                            Console.WriteLine("player bắn trúng boss player!");
                            lblCastleBlood.Width -= 10;
                            if (lblCastleBlood.Width == 0)
                                tmrGameLoop.Stop();
                        }
                    }
                }

                // chạy danh sách đạn của từng kẻ địch 
                foreach (EnemyTank enemyTank in enemyTankManager.EnemyTanks)
                {
                    for (int h = 0; h < enemyTank.Bullets.Count; h++)
                    {
                        // nếu đạn xe tăng địch trúng tường 
                        if (Common.IsCollision(enemyTank.Bullets[h].Rect, wallManager.Walls[i].Rect))
                        {
                            // viên đạn dừng di chuyển nếu nó trúng, không phải bụi cây(4)
                            if (wallManager.Walls[i].WallNumber != 4 &&
                                wallManager.Walls[i].WallNumber != 5)
                            {
                                // thêm vụ nổ vào danh sách
                                explosionManager.CreateExplosion(ExplosionSize.eSmallExplosion, enemyTank.Bullets[h].Rect);
                                // viên đạn xe tăng địch này bị hủy
                                enemyTank.RemoveOneBullet(h);
                            }
                            // hủy viên gach đi khi nó là gạch có thể phá hủy
                            if (wallManager.Walls[i].WallNumber == 1)
                            {
                                //Console.WriteLine("Địch bắn trúng tường có thể phá.");
                                wallManager.RemoveOneWall(i);
                            }
                            else
                            // địch bắn trúng boss của player
                             if (wallManager.Walls[i].WallNumber == 6)
                            {
                                Console.WriteLine("địch bắn trúng boss player!");
                                lblCastleBlood.Width -= 10;
                                if (lblCastleBlood.Width == 0)
                                    tmrGameLoop.Stop();
                            }
                        }
                    }
                }
            }
            #endregion đạn player và đạn địch trúng tường

            #region đạn địch trúng xe tăng, đạn của xe tăng player
            // chạy danh sách xe tăng địch
            for (int i = 0; i < enemyTankManager.EnemyTanks.Count; i++)
            {
                // chạy danh sách đạn địch kiểm tra có trúng xe tăng player
                for (int j = 0; j < enemyTankManager.EnemyTanks[i].Bullets.Count; j++)
                {
                    // đạn của xe tăng địch bắn trúng xe tăng player
                    if (Common.IsCollision(enemyTankManager.EnemyTanks[i].Bullets[j].Rect, playerTank.Rect)
                        && playerTank.IsActivate)
                    {
                        //Console.WriteLine("Địch bắn trúng ta");
                        // thêm vụ nổ vào danh sách
                        explosionManager.CreateExplosion(ExplosionSize.eBigExplosion, enemyTankManager.EnemyTanks[i].Bullets[j].Rect);
                        // cập nhật lại thông tin vị trí cho xe tăng player
                        playerTank.RectX = 21 * Common.STEP;
                        playerTank.RectY = 36 * Common.STEP;
                        playerTank.IsActivate = false;
                        // viên đạn này của địch bị hủy
                        enemyTankManager.EnemyTanks[i].RemoveOneBullet(j);
                        //playerTank.Energy -= 10;
                        this.lblHpTankPlayer.Width -= 20;
                    }

                }

                // chạy danh sách đạn địch kiểm tra va chạm với ds đạn xe tăng player
                for (int j = 0; j < enemyTankManager.EnemyTanks[i].Bullets.Count; j++)
                {
                    // chạy danh sách đạn xe tăng player
                    for (int h = 0; h < playerTank.Bullets.Count; h++)
                    {
                        // đạn của xe tăng địch va chạm đạn của xe tăng player
                        if (Common.IsCollision(enemyTankManager.EnemyTanks[i].Bullets[j].Rect, playerTank.Bullets[h].Rect))
                        {
                            //Console.WriteLine("hai viên đạn trúng nhau");
                            enemyTankManager.EnemyTanks[i].RemoveOneBullet(j);
                            playerTank.RemoveOneBullet(h);
                        }

                    }
                }
                //chạy danh sách đạn xe tăng player
                for (int k = 0; k < playerTank.Bullets.Count; k++)
                {
                    // địch trúng đạn xe tăng player
                    if (Common.IsCollision(enemyTankManager.EnemyTanks[i].Rect, playerTank.Bullets[k].Rect) &&
                        enemyTankManager.EnemyTanks[i].IsActivate)
                    {
                        Console.WriteLine("Địch bị trúng đạn");
                        // thêm vụ nổ vào danh sách
                        explosionManager.CreateExplosion(ExplosionSize.eBigExplosion, playerTank.Bullets[k].Rect);
                        // cập nhật lại thông tin cho địch vừa bị bắn
                        enemyTankManager.UpdateParameter(enemyTankManager.EnemyTanks[i], enemyTankManager.EnemyTankParameters[i]);
                        enemyTankManager.EnemyTanks[i].IsActivate = false;
                        // viên đạn này của player bị hủy
                        playerTank.RemoveOneBullet(k);
                    }
                }
            }
            #endregion đạn địch trúng xe tăng, đạn của xe tăng player

            // xe tăng player di chuyển
            if (!playerTank.IsWallCollision(wallManager.Walls, playerTank.DirectionTank) &&
                !playerTank.IsEnemyTankCollisions(enemyTankManager.EnemyTanks))
                playerTank.Move();

            // vật phẩm được phép hiển thị
            if (item.IsOn)
            {
                item.Show(this.background);
                // xe tăng player ăn vật phẩm
                if (Common.IsCollision(playerTank.Rect, item.Rect))
                {
                    item.IsOn = false;
                    picItem.Image = null;
                    item.RectX = -20;
                    item.RectY = -20;
                    timeItem = 90;
                }
            }

            // hiển thị xe tăng của player
            playerTank.Show(this.background);
            // hiển thị tất cả tường lên background
            wallManager.ShowAllWall(this.background);
            //hiển thị vụ nổ
            explosionManager.ShowAllExplosion(this.background);
            // vẽ lại Bitmap background lên form
            graphics.DrawImageUnscaled(this.background, 0, 0);
        }
        #endregion Vòng lặp game

        #region sự kiện phím
        // nhấn phím di chuyển 
        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            // biến kiểm tra xem có phải ấn nút di chuyển hay không
            bool isMove_local;

            isMove_local = false;
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    playerTank.Left = true;
                    playerTank.Right = playerTank.Up = playerTank.Down = false;
                    isMove_local = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    playerTank.Right = true;
                    playerTank.Left = playerTank.Up = playerTank.Down = false;
                    isMove_local = true;
                    break;
                case Keys.Up:
                case Keys.W:
                    playerTank.Up = true;
                    playerTank.Right = playerTank.Left = playerTank.Down = false;
                    isMove_local = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    playerTank.Down = true;
                    playerTank.Up = playerTank.Right = playerTank.Left = false;
                    isMove_local = true;
                    break;
            }
            if (isMove_local)
            {
                playerTank.IsMove = true;
                playerTank.RotateFrame();
                isMove_local = false;
            }
        }
        // dừng di chuyển; bắn đạn
        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            bool isMove_local = true;
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    isMove_local = false;
                    playerTank.Left = false;
                    break;
                case Keys.Right:
                case Keys.D:
                    isMove_local = false;
                    playerTank.Right = false;
                    break;
                case Keys.Up:
                case Keys.W:
                    isMove_local = false;
                    playerTank.Up = false;
                    break;
                case Keys.Down:
                case Keys.S:
                    isMove_local = false;
                    playerTank.Down = false;
                    break;
            }
            if (!isMove_local)
            {
                playerTank.IsMove = false;
                isMove_local = true;
            }
            // bắn đạn
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                //Console.WriteLine("Bắn");
                //playerTank.IsFire = true;
                playerTank.CreatBullet(Common.path + @"\Images\bullet1.png");
            }
        }
        #endregion sự kiện phím

        #region xử lí thời gian hiển thị lại vật phẩm
        private int timeItem = 90;
        private void tmrShowItem_Tick(object sender, EventArgs e)
        {
            timeItem -= 1;
            this.Text = timeItem.ToString();
            if (timeItem == 30)
            {
                item.IsOn = true;
                item.CreateItem();
                picItem.Image = item.BmpObject;
            }
            else
                if (timeItem < 0)
            {
                timeItem = 90;
                item.IsOn = false;
                picItem.Image = null;
            }
        }
        #endregion xử lí thời gian hiển thị lại vật phẩm
    }
}
