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

        private void frmMenu_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image.FromFile(Application.StartupPath + @"\content\Images\background.jpg"), 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        private void frmMenu_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            frmLoad load = new frmLoad();
            load.Show(this);
            this.Hide();
        }

        private void btPlay_MouseEnter(object sender, EventArgs e)
        {
            btPlay.Image = Properties.Resources.PLAYhover;
        }

        private void btPlay_MouseLeave(object sender, EventArgs e)
        {
            btPlay.Image = Properties.Resources.PLAYbtn;
        }

        private void btLevel_MouseEnter(object sender, EventArgs e)
        {
            btLevel.Image = Properties.Resources.LEVELhover;
        }

        private void btLevel_MouseLeave(object sender, EventArgs e)
        {
            btLevel.Image = Properties.Resources.LEVEL;
        }

        private void btAbout_MouseEnter(object sender, EventArgs e)
        {
            btAbout.Image = Properties.Resources.ABOUTUShover;
        }

        private void btAbout_MouseLeave(object sender, EventArgs e)
        {
            btAbout.Image = Properties.Resources.ABOUTUS;
        }

        private void btOption_MouseEnter(object sender, EventArgs e)
        {
            btOption.Image = Properties.Resources.OPTIONhover;
        }

        private void btOption_MouseLeave(object sender, EventArgs e)
        {
            btOption.Image = Properties.Resources.OPTION;
        }

        private void btEXIT_MouseEnter(object sender, EventArgs e)
        {
            btEXIT.Image = Properties.Resources.EXIThover;
        }

        private void btEXIT_MouseLeave(object sender, EventArgs e)
        {
            btEXIT.Image = Properties.Resources.EXIT;
        }
    }
}
