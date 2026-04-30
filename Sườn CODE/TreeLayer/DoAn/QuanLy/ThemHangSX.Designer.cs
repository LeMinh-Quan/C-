namespace DoAn.QuanLy
{
    partial class ThemHangSX
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Guna.UI2.WinForms.Guna2GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2TextBox txtHangSX;
        private System.Windows.Forms.Label lblHang;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private System.Windows.Forms.Label lblTieuDeChinh;
        private System.Windows.Forms.DataGridView drvDanhHang;

        private void InitializeComponent()
        {
            this.groupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.txtHangSX = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHang = new System.Windows.Forms.Label();
            this.lblTieuDeChinh = new System.Windows.Forms.Label();
            this.drvDanhHang = new System.Windows.Forms.DataGridView();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drvDanhHang)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BorderRadius = 8;
            this.groupBox1.Controls.Add(this.btnDong);
            this.groupBox1.Controls.Add(this.btnLuu);
            this.groupBox1.Controls.Add(this.txtHangSX);
            this.groupBox1.Controls.Add(this.lblHang);
            this.groupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 196);
            this.groupBox1.TabIndex = 106;
            this.groupBox1.Text = "THÊM HÃNG SẢN XUẤT";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.Gray;
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(474, 134);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(130, 45);
            this.btnDong.TabIndex = 109;
            this.btnDong.Text = "THOÁT";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 5;
            this.btnLuu.FillColor = System.Drawing.Color.SeaGreen;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(610, 134);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(118, 45);
            this.btnLuu.TabIndex = 109;
            this.btnLuu.Text = "➕ THÊM";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtHangSX
            // 
            this.txtHangSX.BorderRadius = 5;
            this.txtHangSX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHangSX.DefaultText = "";
            this.txtHangSX.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHangSX.ForeColor = System.Drawing.Color.Black;
            this.txtHangSX.Location = new System.Drawing.Point(228, 69);
            this.txtHangSX.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtHangSX.Name = "txtHangSX";
            this.txtHangSX.PasswordChar = '\0';
            this.txtHangSX.PlaceholderText = "";
            this.txtHangSX.SelectedText = "";
            this.txtHangSX.Size = new System.Drawing.Size(250, 36);
            this.txtHangSX.TabIndex = 3;
            // 
            // lblHang
            // 
            this.lblHang.AutoSize = true;
            this.lblHang.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHang.ForeColor = System.Drawing.Color.Black;
            this.lblHang.Location = new System.Drawing.Point(64, 82);
            this.lblHang.Name = "lblHang";
            this.lblHang.Size = new System.Drawing.Size(158, 23);
            this.lblHang.TabIndex = 4;
            this.lblHang.Text = "Tên hãng sản xuất:";
            // 
            // lblTieuDeChinh
            // 
            this.lblTieuDeChinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTieuDeChinh.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeChinh.ForeColor = System.Drawing.Color.White;
            this.lblTieuDeChinh.Location = new System.Drawing.Point(12, 211);
            this.lblTieuDeChinh.Name = "lblTieuDeChinh";
            this.lblTieuDeChinh.Size = new System.Drawing.Size(741, 41);
            this.lblTieuDeChinh.TabIndex = 107;
            this.lblTieuDeChinh.Text = "DANH SÁCH HÃNG ";
            this.lblTieuDeChinh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // drvDanhHang
            // 
            this.drvDanhHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.drvDanhHang.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.drvDanhHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drvDanhHang.Location = new System.Drawing.Point(19, 255);
            this.drvDanhHang.Name = "drvDanhHang";
            this.drvDanhHang.RowHeadersWidth = 51;
            this.drvDanhHang.RowTemplate.Height = 24;
            this.drvDanhHang.Size = new System.Drawing.Size(734, 315);
            this.drvDanhHang.TabIndex = 108;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 4;
            this.guna2Panel1.Controls.Add(this.groupBox1);
            this.guna2Panel1.Controls.Add(this.drvDanhHang);
            this.guna2Panel1.Controls.Add(this.lblTieuDeChinh);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(765, 582);
            this.guna2Panel1.TabIndex = 110;
            // 
            // ThemHangSX
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(765, 582);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemHangSX";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThemHangSX";
            this.Load += new System.EventHandler(this.ThemHangSX_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drvDanhHang)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDong;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}