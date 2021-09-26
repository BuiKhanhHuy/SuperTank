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
    public partial class frmLoad : Form
    {
        public frmLoad()
        {
            InitializeComponent();
        }

        // loading game
        private void tmrLoading_Tick(object sender, EventArgs e)
        {
            pnFrontLoading.Width += 20;
            if (pnFrontLoading.Width >= 460)
            {
                tmrLoading.Stop();
                frmGame fm2 = new frmGame();
                fm2.Show();
                this.Hide();
            }
            picTank.Left += 20;
        }

        private void frmLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
