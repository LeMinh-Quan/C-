namespace DoAn.NhanVien
{
    partial class FormMainNV
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
            this.pnlMenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnThongTin = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoHanh = new Guna.UI2.WinForms.Guna2Button();
            this.btnLapHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnTraCuuSP = new Guna.UI2.WinForms.Guna2Button();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.Transparent;
            this.pnlMenu.BorderColor = System.Drawing.Color.Black;
            this.pnlMenu.Controls.Add(this.btnLogout);
            this.pnlMenu.Controls.Add(this.btnThongTin);
            this.pnlMenu.Controls.Add(this.btnBaoHanh);
            this.pnlMenu.Controls.Add(this.btnLapHoaDon);
            this.pnlMenu.Controls.Add(this.btnTraCuuSP);
            this.pnlMenu.Controls.Add(this.picLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.FillColor = System.Drawing.Color.White;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(280, 753);
            this.pnlMenu.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BorderRadius = 10;
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(40, 673);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 45);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "ĐĂNG XUẤT";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnThongTin
            // 
            this.btnThongTin.Animated = true;
            this.btnThongTin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.btnThongTin.BorderThickness = 1;
            this.btnThongTin.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnThongTin.Checked = true;
            this.btnThongTin.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnThongTin.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThongTin.FillColor = System.Drawing.Color.Transparent;
            this.btnThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnThongTin.ForeColor = System.Drawing.Color.Black;
            this.btnThongTin.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.btnThongTin.Location = new System.Drawing.Point(0, 170);
            this.btnThongTin.Name = "btnThongTin";
            this.btnThongTin.Size = new System.Drawing.Size(280, 65);
            this.btnThongTin.TabIndex = 5;
            this.btnThongTin.Text = "THÔNG TIN CÁ NHÂN";
            this.btnThongTin.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThongTin.TextOffset = new System.Drawing.Point(20, 0);
            this.btnThongTin.Click += new System.EventHandler(this.btnThongTin_Click);
            // 
            // btnBaoHanh
            // 
            this.btnBaoHanh.Animated = true;
            this.btnBaoHanh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.btnBaoHanh.BorderThickness = 1;
            this.btnBaoHanh.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnBaoHanh.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnBaoHanh.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnBaoHanh.FillColor = System.Drawing.Color.Transparent;
            this.btnBaoHanh.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnBaoHanh.ForeColor = System.Drawing.Color.Black;
            this.btnBaoHanh.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.btnBaoHanh.Location = new System.Drawing.Point(0, 383);
            this.btnBaoHanh.Name = "btnBaoHanh";
            this.btnBaoHanh.Size = new System.Drawing.Size(280, 65);
            this.btnBaoHanh.TabIndex = 3;
            this.btnBaoHanh.Text = "BẢO HÀNH";
            this.btnBaoHanh.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBaoHanh.TextOffset = new System.Drawing.Point(20, 0);
            this.btnBaoHanh.Click += new System.EventHandler(this.btnBaoHanh_Click);
            // 
            // btnLapHoaDon
            // 
            this.btnLapHoaDon.Animated = true;
            this.btnLapHoaDon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.btnLapHoaDon.BorderThickness = 1;
            this.btnLapHoaDon.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnLapHoaDon.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnLapHoaDon.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDon.FillColor = System.Drawing.Color.Transparent;
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.ForeColor = System.Drawing.Color.Black;
            this.btnLapHoaDon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.btnLapHoaDon.Location = new System.Drawing.Point(0, 312);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(280, 65);
            this.btnLapHoaDon.TabIndex = 2;
            this.btnLapHoaDon.Text = "LẬP HÓA ĐƠN";
            this.btnLapHoaDon.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLapHoaDon.TextOffset = new System.Drawing.Point(20, 0);
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);
            // 
            // btnTraCuuSP
            // 
            this.btnTraCuuSP.Animated = true;
            this.btnTraCuuSP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.btnTraCuuSP.BorderThickness = 1;
            this.btnTraCuuSP.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnTraCuuSP.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTraCuuSP.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnTraCuuSP.FillColor = System.Drawing.Color.Transparent;
            this.btnTraCuuSP.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnTraCuuSP.ForeColor = System.Drawing.Color.Black;
            this.btnTraCuuSP.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.btnTraCuuSP.Location = new System.Drawing.Point(0, 241);
            this.btnTraCuuSP.Name = "btnTraCuuSP";
            this.btnTraCuuSP.Size = new System.Drawing.Size(280, 65);
            this.btnTraCuuSP.TabIndex = 1;
            this.btnTraCuuSP.Text = "TRA CỨU SẢN PHẨM";
            this.btnTraCuuSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTraCuuSP.TextOffset = new System.Drawing.Point(20, 0);
            this.btnTraCuuSP.Click += new System.EventHandler(this.btnTraCuuSP_Click);
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.Image = global::DoAn.Properties.Resources.LOGOFORM;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(280, 170);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FillColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Location = new System.Drawing.Point(280, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1102, 753);
            this.pnlMain.TabIndex = 2;
            // 
            // FormMainNV
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMainNV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMainNV";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMainNV_FormClosed);
            this.Load += new System.EventHandler(this.FormMainNV_Load);
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMenu;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Button btnThongTin;
        private Guna.UI2.WinForms.Guna2Button btnBaoHanh;
        private Guna.UI2.WinForms.Guna2Button btnLapHoaDon;
        private Guna.UI2.WinForms.Guna2Button btnTraCuuSP;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
    }
}