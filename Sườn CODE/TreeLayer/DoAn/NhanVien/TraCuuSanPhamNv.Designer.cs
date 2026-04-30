namespace DoAn.NhanVien
{
    partial class TraCuuSanPhamNv
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpTraCuu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnXemChiTiet = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.txtTimKiemModel = new Guna.UI2.WinForms.Guna2TextBox();
            this.drvDanhSachLoaiSP = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpTraCuu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drvDanhSachLoaiSP)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTraCuu
            // 
            this.grpTraCuu.Controls.Add(this.btnXemChiTiet);
            this.grpTraCuu.Controls.Add(this.btnLamMoi);
            this.grpTraCuu.Controls.Add(this.btnTimKiem);
            this.grpTraCuu.Controls.Add(this.txtTimKiemModel);
            this.grpTraCuu.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.grpTraCuu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTraCuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTraCuu.ForeColor = System.Drawing.Color.White;
            this.grpTraCuu.Location = new System.Drawing.Point(0, 0);
            this.grpTraCuu.Name = "grpTraCuu";
            this.grpTraCuu.Size = new System.Drawing.Size(1096, 120);
            this.grpTraCuu.TabIndex = 4;
            this.grpTraCuu.Text = "Bộ lọc & Tìm kiếm nhanh";
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.BorderRadius = 5;
            this.btnXemChiTiet.FillColor = System.Drawing.Color.SeaGreen;
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(842, 60);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(242, 40);
            this.btnXemChiTiet.TabIndex = 4;
            this.btnXemChiTiet.Text = "📄 THÔNG TIN CHI TIẾT";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(406, 60);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(139, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BorderRadius = 5;
            this.btnTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(256, 60);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(144, 40);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "🔍 TÌM KIẾM";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiemModel
            // 
            this.txtTimKiemModel.BorderRadius = 5;
            this.txtTimKiemModel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiemModel.DefaultText = "";
            this.txtTimKiemModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiemModel.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiemModel.Location = new System.Drawing.Point(30, 60);
            this.txtTimKiemModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiemModel.Name = "txtTimKiemModel";
            this.txtTimKiemModel.PasswordChar = '\0';
            this.txtTimKiemModel.PlaceholderText = "Nhập mã Model...";
            this.txtTimKiemModel.SelectedText = "";
            this.txtTimKiemModel.Size = new System.Drawing.Size(220, 40);
            this.txtTimKiemModel.TabIndex = 0;
            // 
            // drvDanhSachLoaiSP
            // 
            this.drvDanhSachLoaiSP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.drvDanhSachLoaiSP.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.drvDanhSachLoaiSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drvDanhSachLoaiSP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.drvDanhSachLoaiSP.Location = new System.Drawing.Point(0, 161);
            this.drvDanhSachLoaiSP.Name = "drvDanhSachLoaiSP";
            this.drvDanhSachLoaiSP.RowHeadersWidth = 51;
            this.drvDanhSachLoaiSP.RowTemplate.Height = 24;
            this.drvDanhSachLoaiSP.Size = new System.Drawing.Size(1096, 596);
            this.drvDanhSachLoaiSP.TabIndex = 5;
            this.drvDanhSachLoaiSP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.drvDanhSachLoaiSP_CellContentClick);
            this.drvDanhSachLoaiSP.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.drvDanhSachLoaiSP_DataBindingComplete);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 120);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1096, 38);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "DANH SÁCH SẢN PHẨM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TraCuuSanPhamNv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1096, 757);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.drvDanhSachLoaiSP);
            this.Controls.Add(this.grpTraCuu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TraCuuSanPhamNv";
            this.Text = "Tra Cứu Sản Phẩm";
            this.Load += new System.EventHandler(this.TraCuuSanPhamNv_Load);
            this.grpTraCuu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drvDanhSachLoaiSP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GroupBox grpTraCuu;
        private Guna.UI2.WinForms.Guna2Button btnXemChiTiet;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button btnTimKiem;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiemModel;
        private System.Windows.Forms.DataGridView drvDanhSachLoaiSP;
        private System.Windows.Forms.Label lblTitle;
    }
}