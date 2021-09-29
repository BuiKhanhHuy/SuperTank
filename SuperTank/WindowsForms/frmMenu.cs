using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SuperTank.WindowsForms
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        #region các hàm xử lí chính

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
            pnOption.Top = 40;
            pnOption.Left = 2000;
        }

        // play
        private void btnPlay_Click(object sender, EventArgs e)
        {
            pnLevel.Top = 40;
            pnLevel.Left = 0;
        }

        // about us
        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            pnAboutUs.Top = 40;
            pnAboutUs.Left = 0;
        }

        // instructions
        private void btnInstructions_Click(object sender, EventArgs e)
        {
            pnInstructions.Top = 40;
            pnInstructions.Left = 0;
        }

        // option
        private void btnOption_Click(object sender, EventArgs e)
        {
            pnOption.Top = 40;
            pnOption.Left = 0;
        }

        // exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // menu
        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.ResetPanel();
        }

        // start game with level
        private void btnLevel_Click(object sender, EventArgs e)
        {
            frmGame formGame = new frmGame(int.Parse(((Button)sender).Tag.ToString()));
            formGame.formMenu = this;
            this.Hide();
            formGame.Show();
            this.ResetPanel();
        }
        #endregion các hàm xử lí chính

        #region các hàm sự kiện thanh tiêu đề
        private Point titleClickPoint;
        private bool isZoom = false;
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




        // thoát game
        private void picMultiply_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion các hàm sự kiện thanh tiêu đề
    }
}
