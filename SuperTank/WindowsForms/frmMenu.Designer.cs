
namespace SuperTank.WindowsForms
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btLevel = new System.Windows.Forms.PictureBox();
            this.btPlay = new System.Windows.Forms.PictureBox();
            this.btAbout = new System.Windows.Forms.PictureBox();
            this.btOption = new System.Windows.Forms.PictureBox();
            this.btEXIT = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btEXIT)).BeginInit();
            this.SuspendLayout();
            // 
            // btLevel
            // 
            this.btLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btLevel.BackColor = System.Drawing.Color.Transparent;
            this.btLevel.Image = global::SuperTank.Properties.Resources.LEVEL;
            this.btLevel.Location = new System.Drawing.Point(157, 130);
            this.btLevel.Name = "btLevel";
            this.btLevel.Size = new System.Drawing.Size(185, 64);
            this.btLevel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btLevel.TabIndex = 0;
            this.btLevel.TabStop = false;
            this.btLevel.MouseEnter += new System.EventHandler(this.btLevel_MouseEnter);
            this.btLevel.MouseLeave += new System.EventHandler(this.btLevel_MouseLeave);
            // 
            // btPlay
            // 
            this.btPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btPlay.BackColor = System.Drawing.Color.Transparent;
            this.btPlay.Image = global::SuperTank.Properties.Resources.PLAYbtn;
            this.btPlay.Location = new System.Drawing.Point(157, 21);
            this.btPlay.Name = "btPlay";
            this.btPlay.Size = new System.Drawing.Size(185, 64);
            this.btPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btPlay.TabIndex = 0;
            this.btPlay.TabStop = false;
            this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
            this.btPlay.MouseEnter += new System.EventHandler(this.btPlay_MouseEnter);
            this.btPlay.MouseLeave += new System.EventHandler(this.btPlay_MouseLeave);
            // 
            // btAbout
            // 
            this.btAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btAbout.BackColor = System.Drawing.Color.Transparent;
            this.btAbout.Image = global::SuperTank.Properties.Resources.ABOUTUS;
            this.btAbout.Location = new System.Drawing.Point(157, 239);
            this.btAbout.Name = "btAbout";
            this.btAbout.Size = new System.Drawing.Size(185, 64);
            this.btAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btAbout.TabIndex = 0;
            this.btAbout.TabStop = false;
            this.btAbout.MouseEnter += new System.EventHandler(this.btAbout_MouseEnter);
            this.btAbout.MouseLeave += new System.EventHandler(this.btAbout_MouseLeave);
            // 
            // btOption
            // 
            this.btOption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btOption.BackColor = System.Drawing.Color.Transparent;
            this.btOption.Image = global::SuperTank.Properties.Resources.OPTION;
            this.btOption.Location = new System.Drawing.Point(157, 348);
            this.btOption.Name = "btOption";
            this.btOption.Size = new System.Drawing.Size(185, 64);
            this.btOption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btOption.TabIndex = 0;
            this.btOption.TabStop = false;
            this.btOption.MouseEnter += new System.EventHandler(this.btOption_MouseEnter);
            this.btOption.MouseLeave += new System.EventHandler(this.btOption_MouseLeave);
            // 
            // btEXIT
            // 
            this.btEXIT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btEXIT.BackColor = System.Drawing.Color.Transparent;
            this.btEXIT.Image = global::SuperTank.Properties.Resources.EXIT;
            this.btEXIT.Location = new System.Drawing.Point(157, 457);
            this.btEXIT.Name = "btEXIT";
            this.btEXIT.Size = new System.Drawing.Size(185, 64);
            this.btEXIT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btEXIT.TabIndex = 0;
            this.btEXIT.TabStop = false;
            this.btEXIT.MouseEnter += new System.EventHandler(this.btEXIT_MouseEnter);
            this.btEXIT.MouseLeave += new System.EventHandler(this.btEXIT_MouseLeave);
            // 
            // frmMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(482, 553);
            this.Controls.Add(this.btEXIT);
            this.Controls.Add(this.btAbout);
            this.Controls.Add(this.btOption);
            this.Controls.Add(this.btLevel);
            this.Controls.Add(this.btPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMenu";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMenu_Paint);
            this.Resize += new System.EventHandler(this.frmMenu_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.btLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btEXIT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btPlay;
        private System.Windows.Forms.PictureBox btLevel;
        private System.Windows.Forms.PictureBox btAbout;
        private System.Windows.Forms.PictureBox btOption;
        private System.Windows.Forms.PictureBox btEXIT;
    }
}