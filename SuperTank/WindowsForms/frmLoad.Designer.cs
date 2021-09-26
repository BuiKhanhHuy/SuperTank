
namespace SuperTank.WindowsForms
{
    partial class frmLoad
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
            this.components = new System.ComponentModel.Container();
            this.pnFrontLoading = new System.Windows.Forms.Panel();
            this.pnBackLoading = new System.Windows.Forms.Panel();
            this.tmrLoading = new System.Windows.Forms.Timer(this.components);
            this.picTankTitle = new System.Windows.Forms.PictureBox();
            this.picTank = new System.Windows.Forms.PictureBox();
            this.pnBackLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTankTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTank)).BeginInit();
            this.SuspendLayout();
            // 
            // pnFrontLoading
            // 
            this.pnFrontLoading.BackColor = System.Drawing.Color.Red;
            this.pnFrontLoading.Location = new System.Drawing.Point(3, 0);
            this.pnFrontLoading.Name = "pnFrontLoading";
            this.pnFrontLoading.Size = new System.Drawing.Size(1, 20);
            this.pnFrontLoading.TabIndex = 0;
            // 
            // pnBackLoading
            // 
            this.pnBackLoading.Controls.Add(this.pnFrontLoading);
            this.pnBackLoading.Location = new System.Drawing.Point(21, 460);
            this.pnBackLoading.Name = "pnBackLoading";
            this.pnBackLoading.Size = new System.Drawing.Size(440, 20);
            this.pnBackLoading.TabIndex = 5;
            // 
            // tmrLoading
            // 
            this.tmrLoading.Enabled = true;
            this.tmrLoading.Tick += new System.EventHandler(this.tmrLoading_Tick);
            // 
            // picTankTitle
            // 
            this.picTankTitle.Image = global::SuperTank.Properties.Resources.WIP_tank_turntable_newcolors;
            this.picTankTitle.Location = new System.Drawing.Point(88, 27);
            this.picTankTitle.Name = "picTankTitle";
            this.picTankTitle.Size = new System.Drawing.Size(317, 217);
            this.picTankTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTankTitle.TabIndex = 6;
            this.picTankTitle.TabStop = false;
            // 
            // picTank
            // 
            this.picTank.Image = global::SuperTank.Properties.Resources.ZC9M;
            this.picTank.Location = new System.Drawing.Point(-42, 420);
            this.picTank.Name = "picTank";
            this.picTank.Size = new System.Drawing.Size(66, 37);
            this.picTank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTank.TabIndex = 7;
            this.picTank.TabStop = false;
            // 
            // frmLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(482, 553);
            this.Controls.Add(this.picTank);
            this.Controls.Add(this.pnBackLoading);
            this.Controls.Add(this.picTankTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLoad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoad_FormClosing);
            this.pnBackLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTankTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnFrontLoading;
        private System.Windows.Forms.Panel pnBackLoading;
        private System.Windows.Forms.Timer tmrLoading;
        private System.Windows.Forms.PictureBox picTankTitle;
        private System.Windows.Forms.PictureBox picTank;
    }
}