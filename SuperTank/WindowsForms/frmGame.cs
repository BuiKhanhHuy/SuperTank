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
        #region thuộc tính thông tin
        private PictureBox[] picNumberEnemyTanks;
        #endregion thuộc tính thông tin
        public frmGame()
        {
            Common.path = Application.StartupPath + @"\Content";
            InitializeComponent();

        }
        private void frmGame_Load(object sender, EventArgs e)
        {
            // load ảnh heart cho hp playertank
            picHeart.Image = Image.FromFile(Common.path + @"\Images\heart.png");
            // add picture box vào mảng hiển thị số lượng địch
            picNumberEnemyTanks = new PictureBox[]{picTank00, picTank01, picTank02,
            picTank03, picTank04, picTank05, picTank06, picTank07, picTank08, picTank09, picTank10,
            picTank11, picTank12, picTank13, picTank14, picTank15, picTank16, picTank17, picTank18, picTank19};
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
            item = new Item();
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
            // hiển thị số lượng xe tăng địch cần tiêu diệt bên bảng thông tin
            ShowNumberEnemyTankDestroy(enemyTankManager.NumberEnemyTank());
            // cập nhật năng lượng xe tăng player 
            playerTank.Energy = 100;
            // cập nhật thông tin máu hiển thị của xe tăng player
            this.lblHpTankPlayer.Width = playerTank.Energy;
            // cập nhật thông tin máu hiển thị của thành
            this.lblCastleBlood.Width = 80;
            // set thời gian item và chạy item
            timeItem = 50;
            tmrShowItem.Start();
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
                            //Console.WriteLine("player bắn trúng boss player!");
                            lblCastleBlood.Width -= 8;
                            if (lblCastleBlood.Width == 0)
                            {
                                // game over
                                this.GameOver();
                            }
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
                            // hủy viên gạch đi khi nó là gạch có thể phá hủy
                            if (wallManager.Walls[i].WallNumber == 1)
                            {
                                //Console.WriteLine("Địch bắn trúng tường có thể phá.");
                                wallManager.RemoveOneWall(i);
                            }
                            else
                            // địch bắn trúng boss của player
                             if (wallManager.Walls[i].WallNumber == 6)
                            {
                                //Console.WriteLine("địch bắn trúng boss player!");
                                lblCastleBlood.Width -= 8;
                                if (lblCastleBlood.Width == 0)
                                {
                                    // game over
                                    this.GameOver();
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region đạn địch trúng xe tăng hoặc trúng đạn của xe tăng player
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
                        // cập nhật năng lượng của xe tăng player
                        playerTank.Energy -= 10;
                        this.lblHpTankPlayer.Width = playerTank.Energy;
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
                    // xe tăng player bắn trúng địch
                    if (Common.IsCollision(enemyTankManager.EnemyTanks[i].Rect, playerTank.Bullets[k].Rect) &&
                        enemyTankManager.EnemyTanks[i].IsActivate)
                    {
                        Console.WriteLine("Địch bị trúng đạn");
                        // thêm vụ nổ vào danh sách
                        explosionManager.CreateExplosion(ExplosionSize.eBigExplosion, playerTank.Bullets[k].Rect);

                        #region kiểm tra cập nhật vị trí xe tăng địch
                        // trừ năng lượng của địch
                        enemyTankManager.EnemyTanks[i].Energy -= 10;
                        if (enemyTankManager.EnemyTanks[i].Energy > 0)
                        {
                            // đổi màu skin
                            enemyTankManager.EnemyTanks[i].SkinTank = enemyTankManager.SkinEnemyTank(enemyTankManager.EnemyTanks[i]);
                        }
                        else
                        {
                            enemyTankManager.EnemyTankParameters[i].maxNumberEnemyTank--;
                            if (enemyTankManager.EnemyTankParameters[i].maxNumberEnemyTank > 0)
                            {
                                enemyTankManager.UpdateParameter(enemyTankManager.EnemyTanks[i], enemyTankManager.EnemyTankParameters[i]);
                                enemyTankManager.EnemyTanks[i].IsActivate = false;
                            }
                            else
                            {
                                enemyTankManager.EnemyTanks.RemoveAt(i);
                                enemyTankManager.EnemyTankParameters.RemoveAt(i);
                            }
                            // tiêu diệt được một kẻ địch
                            enemyTankManager.NumberEnemyTankDestroy--;
                            // cập nhật lại thông tin số địch còn lại lên pic
                            picNumberEnemyTanks[enemyTankManager.NumberEnemyTankDestroy].Image = null;
                        }
                        #endregion kiểm tra cập nhật vị trí xe tăng địch

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
                    timeItem = 50;
                    // XE TĂNG ĂN VẬT PHẨM
                    switch (item.ItemType)
                    {
                        // vật phẩm là máu
                        case ItemType.eItemHeart:
                            playerTank.Energy = 100;
                            lblHpTankPlayer.Width = playerTank.Energy;
                            break;
                        // vật phẩm là khiên chắn bảo vệ
                        case ItemType.eItemShield:
                            Console.WriteLine("khiên");
                            break;
                        // vật phẩm là lựu đạn
                        case ItemType.eItemGrenade:
                            Console.WriteLine("lựu đạn");
                            break;
                        // vật phẩm là thời gian
                        case ItemType.eItemTimer:
                            //foreach (EnemyTank enemyTank in enemyTankManager.EnemyTanks)
                            //{
                            //    enemyTank.IsActivate = false;
                            //}
                            Console.WriteLine("thời gian");
                            break;
                    }
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
        private int timeItem = 50;
        private void tmrShowItem_Tick(object sender, EventArgs e)
        {
            timeItem -= 1;
            this.Text = timeItem.ToString();
            if (timeItem == 20)
            {
                item.IsOn = true;
                item.CreateItem();
                picItem.Image = item.BmpObject;
            }
            else
                if (timeItem < 0)
            {
                timeItem = 50;
                item.IsOn = false;
                picItem.Image = null;
            }
        }
        #endregion xử lí thời gian hiển thị lại vật phẩm

        #region các hàm xử lí chính
        private void PlayerBulletHitsWall()
        {

        }

        // game over
        private void GameOver()
        {
            tmrGameLoop.Stop();
            tmrShowItem.Stop();
            tmrGameOver.Start();
            pnGameOver.Enabled = true;
        }

        // game next
        private void GameNext()
        {
            tmrGameLoop.Stop();
            tmrShowItem.Stop();
            tmrNextLevel.Start();
            pnNextLevel.Enabled = true;
        }

        // game win
        private void GameWin()
        {
            tmrGameLoop.Stop();
            tmrShowItem.Stop();
            tmrGameWin.Start();
            pnGameWin.Enabled = true;
        }
        #endregion các hàm xử lí chính

        #region các hàm hiển thị thông tin
        // hiển thị số lượng địch phải tiêu diệt bên bản thông tin
        private void ShowNumberEnemyTankDestroy(int n)
        {
            for (int i = 0; i < n; i++)
            {
                picNumberEnemyTanks[i].Image = Image.FromFile(Common.path + @"\Images\icon_enemyTank.png");
            }
        }

        // hiển thị gameover
        private void tmrGameOver_Tick(object sender, EventArgs e)
        {
            pnGameOver.Top += 10;
            if (pnGameOver.Top >= 0)
            {
                tmrGameOver.Stop();
                pnGameOver.Top += 3;
            }
        }

        // hiển thị nextlevel
        private void tmrNextLevel_Tick(object sender, EventArgs e)
        {
            pnNextLevel.Top += 10;
            if (pnNextLevel.Top >= 0)
            {
                tmrNextLevel.Stop();
                pnNextLevel.Top += 3;
            }   
        }

        // hiển thị gamewin
        private void tmrGameWin_Tick(object sender, EventArgs e)
        {
            pnGameWin.Top += 10;
            if (pnGameWin.Top >= 0)
            {
                tmrGameWin.Stop();
                pnGameWin.Top += 3;
            }
        }

        #endregion các hàm hiển thị thông tin
    }
}
