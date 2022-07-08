using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.General;
using System.Windows.Forms;


namespace SuperTank.WindowsForms
{
    public partial class frmMenu : Form
    {
        // mảng các button level
        private Button[] levelButtons;

        public frmMenu()
        {
            // load đường dẫn dùng chung
            Common.path = Application.StartupPath + @"\Content";
            // thiết lập âm thanh
            Sound.InitSound(Common.path);
            // thiết kế control
            InitializeComponent();
            // add các button level vào mảng
            levelButtons = new Button[] { btnLevel1, btnLevel2, btnLevel3, btnLevel4, btnLevel5,
                btnLevel6, btnLevel7, btnLevel8, btnLevel9, btnLevel10 };
          
        }

        // load form
        private void frmMenu_Load(object sender, EventArgs e)
        {
            // đọc level người chơi từ file
            PlayerInfor.ReadPlayerLevel(@"\PlayerLevel.txt");
            // hiển thị các nút level được mở 
            this.ShowOpenedLevels( PlayerInfor.level);
        }

        #region các hàm xử lí chính
        // hiển thị các level được mở
        public void ShowOpenedLevels(int level)
        {
            for (int i = 0; i < level; i++)
            {
                levelButtons[i].Enabled = true;
                levelButtons[i].ForeColor = Color.FromArgb(224, 224, 224);

            }
            levelButtons[level - 1].ForeColor = Color.Gold;
        }

        // start game with level
        private void btnLevel_Click(object sender, EventArgs e)
        {
            int level;
            level = int.Parse(((Button)sender).Tag.ToString());
            frmGame formGame = new frmGame(level);
            formGame.formMenu = this;
            this.Hide();
            formGame.Show();
            this.ResetPanel();
        }

        // reset panel
        private void ResetPanel()
        {
            pnMenu.Top = 40;
            pnMenu.Left = 0;
            pnLevel.Top = 40;
            pnLevel.Left = 500;
            pnInstructions.Top = 40;
            pnInstructions.Left = 1000;
            pnAboutUs.Top = 40;
            pnAboutUs.Left = 1500;
        }

        // play
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // phát âm thanh click
            Sound.PlayClickRoomSound();
            pnLevel.Top = 40;
            pnLevel.Left = 0;
        }

        // about us
        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            // phát âm thanh click
            Sound.PlayClickRoomSound();
            pnAboutUs.Top = 40;
            pnAboutUs.Left = 0;
        }

        // instructions
        private void btnInstructions_Click(object sender, EventArgs e)
        {
            // phát âm thanh click
            Sound.PlayClickRoomSound();
            pnInstructions.Top = 40;
            pnInstructions.Left = 0;
        }


        // exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            // phát âm thanh click
            Sound.PlayClickRoomSound();
            // lưu thông tin level người chơi lại
            PlayerInfor.WritePlayerLevel(@"\PlayerLevel.txt");
            Application.Exit();
        }

        // menu
        private void btnMenu_Click(object sender, EventArgs e)
        {
            // phát âm thanh click
            Sound.PlayClickRoomSound();
            this.ResetPanel();
        }

        #endregion các hàm xử lí chính

        #region các hàm sự kiện thanh tiêu đề
        private Point titleClickPoint;
        private int w, h;

        // chuot click tieu de
        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            titleClickPoint.X = MousePosition.X;
            titleClickPoint.Y = MousePosition.Y;
            this.w = e.X;
            this.h = e.Y;
        }
        // di chuyen form
        private void pnTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
                this.Location = new Point(MousePosition.X - w, MousePosition.Y - h);
        }

        // chuột vào nút thu nhỏ
        private void picMinus_MouseEnter(object sender, EventArgs e)
        {
            this.picMinus.BackColor = Color.Green;
        }
        // chuột rời khỏi nút thu nhỏ
        private void picMinus_MouseLeave(object sender, EventArgs e)
        {
            this.picMinus.BackColor = Color.Transparent;
        }
        // thu nhỏ cửa sổ
        private void picMinus_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // chuột vào nút thoát
        private void picMultiply_MouseEnter(object sender, EventArgs e)
        {
            this.picMultiply.BackColor = Color.Red;
        }
        // chuột rời khỏi nút thoát
        private void picMultiply_MouseLeave(object sender, EventArgs e)
        {
            this.picMultiply.BackColor = Color.Transparent;
        }

        // trước khi thoát
        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // lưu thông tin level người chơi lại
            PlayerInfor.WritePlayerLevel(@"\PlayerLevel.txt");
        }

        private void pnTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        // thoát game
        private void picMultiply_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion các hàm sự kiện thanh tiêu đề
    }
}
