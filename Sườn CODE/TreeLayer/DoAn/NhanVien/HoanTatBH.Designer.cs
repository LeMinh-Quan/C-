namespace DoAn.NhanVien
{
    partial class HoanTatBH
    {
        private System.ComponentModel.IContainer components = null;

        // KHAI BÁO CÁC CONTROLS
        private System.Windows.Forms.Label lblTieuDe;

        // GroupBox 1: Thông tin khách & Phiếu
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblNgayNhan;

        // GroupBox 2: Thông tin thiết bị
        private System.Windows.Forms.GroupBox gbThietBi;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblLoiBanDau;

        // GroupBox 3: Kết quả từ NCC (Quan trọng nhất)
        private System.Windows.Forms.GroupBox gbKetQua;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Label lblChiPhi;
        private System.Windows.Forms.Label lblVND;

        // Thao tác xác nhận
        private System.Windows.Forms.Label lblGhiChuTra;
        private System.Windows.Forms.CheckBox chkDaKiemTra;

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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtKhachHang = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNgayNhan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaPhieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.lblNgayNhan = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.gbThietBi = new System.Windows.Forms.GroupBox();
            this.txtLoiBanDau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSerial = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenSP = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblLoiBanDau = new System.Windows.Forms.Label();
            this.gbKetQua = new System.Windows.Forms.GroupBox();
            this.txtChiPhi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.txtKetQua = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblChiPhi = new System.Windows.Forms.Label();
            this.lblVND = new System.Windows.Forms.Label();
            this.lblGhiChuTra = new System.Windows.Forms.Label();
            this.chkDaKiemTra = new System.Windows.Forms.CheckBox();
            this.txtGhiChuTra = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacNhanTra = new Guna.UI2.WinForms.Guna2Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbThongTin.SuspendLayout();
            this.gbThietBi.SuspendLayout();
            this.gbKetQua.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.AliceBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(868, 51);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "XÁC NHẬN GIAO TRẢ MÁY BẢO HÀNH";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.txtSDT);
            this.gbThongTin.Controls.Add(this.txtKhachHang);
            this.gbThongTin.Controls.Add(this.txtNgayNhan);
            this.gbThongTin.Controls.Add(this.txtMaPhieu);
            this.gbThongTin.Controls.Add(this.lblMaPhieu);
            this.gbThongTin.Controls.Add(this.lblNgayNhan);
            this.gbThongTin.Controls.Add(this.lblKhachHang);
            this.gbThongTin.Controls.Add(this.lblSDT);
            this.gbThongTin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.gbThongTin.Location = new System.Drawing.Point(30, 70);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(800, 139);
            this.gbThongTin.TabIndex = 1;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "1. Thông tin chung";
            // 
            // txtSDT
            // 
            this.txtSDT.BorderRadius = 5;
            this.txtSDT.BorderThickness = 0;
            this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDT.DefaultText = "";
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSDT.ForeColor = System.Drawing.Color.Black;
            this.txtSDT.Location = new System.Drawing.Point(546, 75);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSDT.Multiline = true;
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.PasswordChar = '\0';
            this.txtSDT.PlaceholderText = "";
            this.txtSDT.ReadOnly = true;
            this.txtSDT.SelectedText = "";
            this.txtSDT.Size = new System.Drawing.Size(232, 42);
            this.txtSDT.TabIndex = 17;
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.BorderRadius = 5;
            this.txtKhachHang.BorderThickness = 0;
            this.txtKhachHang.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKhachHang.DefaultText = "";
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKhachHang.ForeColor = System.Drawing.Color.Black;
            this.txtKhachHang.Location = new System.Drawing.Point(138, 75);
            this.txtKhachHang.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtKhachHang.Multiline = true;
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.PasswordChar = '\0';
            this.txtKhachHang.PlaceholderText = "";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.SelectedText = "";
            this.txtKhachHang.Size = new System.Drawing.Size(232, 42);
            this.txtKhachHang.TabIndex = 16;
            // 
            // txtNgayNhan
            // 
            this.txtNgayNhan.BorderRadius = 5;
            this.txtNgayNhan.BorderThickness = 0;
            this.txtNgayNhan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNgayNhan.DefaultText = "";
            this.txtNgayNhan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNgayNhan.ForeColor = System.Drawing.Color.Black;
            this.txtNgayNhan.Location = new System.Drawing.Point(546, 28);
            this.txtNgayNhan.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNgayNhan.Multiline = true;
            this.txtNgayNhan.Name = "txtNgayNhan";
            this.txtNgayNhan.PasswordChar = '\0';
            this.txtNgayNhan.PlaceholderText = "";
            this.txtNgayNhan.ReadOnly = true;
            this.txtNgayNhan.SelectedText = "";
            this.txtNgayNhan.Size = new System.Drawing.Size(232, 42);
            this.txtNgayNhan.TabIndex = 18;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.BorderRadius = 5;
            this.txtMaPhieu.BorderThickness = 0;
            this.txtMaPhieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieu.DefaultText = "";
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.txtMaPhieu.Location = new System.Drawing.Point(153, 28);
            this.txtMaPhieu.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMaPhieu.Multiline = true;
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.PasswordChar = '\0';
            this.txtMaPhieu.PlaceholderText = "";
            this.txtMaPhieu.ReadOnly = true;
            this.txtMaPhieu.SelectedText = "";
            this.txtMaPhieu.Size = new System.Drawing.Size(216, 42);
            this.txtMaPhieu.TabIndex = 15;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Font = new System.Drawing.Font("Arial", 10F);
            this.lblMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.lblMaPhieu.Location = new System.Drawing.Point(30, 40);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(110, 19);
            this.lblMaPhieu.TabIndex = 0;
            this.lblMaPhieu.Text = "Mã Phiếu BH:";
            // 
            // lblNgayNhan
            // 
            this.lblNgayNhan.AutoSize = true;
            this.lblNgayNhan.Font = new System.Drawing.Font("Arial", 10F);
            this.lblNgayNhan.ForeColor = System.Drawing.Color.Black;
            this.lblNgayNhan.Location = new System.Drawing.Point(407, 40);
            this.lblNgayNhan.Name = "lblNgayNhan";
            this.lblNgayNhan.Size = new System.Drawing.Size(124, 19);
            this.lblNgayNhan.TabIndex = 2;
            this.lblNgayNhan.Text = "Ngày tiếp nhận:";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Arial", 10F);
            this.lblKhachHang.ForeColor = System.Drawing.Color.Black;
            this.lblKhachHang.Location = new System.Drawing.Point(30, 88);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(102, 19);
            this.lblKhachHang.TabIndex = 4;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Arial", 10F);
            this.lblSDT.ForeColor = System.Drawing.Color.Black;
            this.lblSDT.Location = new System.Drawing.Point(420, 88);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(111, 19);
            this.lblSDT.TabIndex = 6;
            this.lblSDT.Text = "Số điện thoại:";
            // 
            // gbThietBi
            // 
            this.gbThietBi.Controls.Add(this.txtLoiBanDau);
            this.gbThietBi.Controls.Add(this.txtSerial);
            this.gbThietBi.Controls.Add(this.txtTenSP);
            this.gbThietBi.Controls.Add(this.lblTenSP);
            this.gbThietBi.Controls.Add(this.lblSerial);
            this.gbThietBi.Controls.Add(this.lblLoiBanDau);
            this.gbThietBi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gbThietBi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.gbThietBi.Location = new System.Drawing.Point(30, 215);
            this.gbThietBi.Name = "gbThietBi";
            this.gbThietBi.Size = new System.Drawing.Size(800, 171);
            this.gbThietBi.TabIndex = 2;
            this.gbThietBi.TabStop = false;
            this.gbThietBi.Text = "2. Thông tin thiết bị";
            // 
            // txtLoiBanDau
            // 
            this.txtLoiBanDau.BorderRadius = 5;
            this.txtLoiBanDau.BorderThickness = 0;
            this.txtLoiBanDau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLoiBanDau.DefaultText = "";
            this.txtLoiBanDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLoiBanDau.ForeColor = System.Drawing.Color.Black;
            this.txtLoiBanDau.Location = new System.Drawing.Point(135, 80);
            this.txtLoiBanDau.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtLoiBanDau.Multiline = true;
            this.txtLoiBanDau.Name = "txtLoiBanDau";
            this.txtLoiBanDau.PasswordChar = '\0';
            this.txtLoiBanDau.PlaceholderText = "";
            this.txtLoiBanDau.ReadOnly = true;
            this.txtLoiBanDau.SelectedText = "";
            this.txtLoiBanDau.Size = new System.Drawing.Size(632, 73);
            this.txtLoiBanDau.TabIndex = 21;
            // 
            // txtSerial
            // 
            this.txtSerial.BorderRadius = 5;
            this.txtSerial.BorderThickness = 0;
            this.txtSerial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSerial.DefaultText = "";
            this.txtSerial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSerial.ForeColor = System.Drawing.Color.Black;
            this.txtSerial.Location = new System.Drawing.Point(537, 28);
            this.txtSerial.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSerial.Multiline = true;
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.PasswordChar = '\0';
            this.txtSerial.PlaceholderText = "";
            this.txtSerial.ReadOnly = true;
            this.txtSerial.SelectedText = "";
            this.txtSerial.Size = new System.Drawing.Size(232, 42);
            this.txtSerial.TabIndex = 20;
            // 
            // txtTenSP
            // 
            this.txtTenSP.BorderRadius = 5;
            this.txtTenSP.BorderThickness = 0;
            this.txtTenSP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSP.DefaultText = "";
            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenSP.ForeColor = System.Drawing.Color.Black;
            this.txtTenSP.Location = new System.Drawing.Point(124, 28);
            this.txtTenSP.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTenSP.Multiline = true;
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.PasswordChar = '\0';
            this.txtTenSP.PlaceholderText = "";
            this.txtTenSP.ReadOnly = true;
            this.txtTenSP.SelectedText = "";
            this.txtTenSP.Size = new System.Drawing.Size(232, 42);
            this.txtTenSP.TabIndex = 19;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Arial", 10F);
            this.lblTenSP.ForeColor = System.Drawing.Color.Black;
            this.lblTenSP.Location = new System.Drawing.Point(30, 40);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(88, 19);
            this.lblTenSP.TabIndex = 0;
            this.lblTenSP.Text = "Sản phẩm:";
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Arial", 10F);
            this.lblSerial.ForeColor = System.Drawing.Color.Black;
            this.lblSerial.Location = new System.Drawing.Point(448, 40);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(83, 19);
            this.lblSerial.TabIndex = 2;
            this.lblSerial.Text = "Mã Serial:";
            // 
            // lblLoiBanDau
            // 
            this.lblLoiBanDau.AutoSize = true;
            this.lblLoiBanDau.Font = new System.Drawing.Font("Arial", 10F);
            this.lblLoiBanDau.ForeColor = System.Drawing.Color.Black;
            this.lblLoiBanDau.Location = new System.Drawing.Point(26, 91);
            this.lblLoiBanDau.Name = "lblLoiBanDau";
            this.lblLoiBanDau.Size = new System.Drawing.Size(103, 19);
            this.lblLoiBanDau.TabIndex = 4;
            this.lblLoiBanDau.Text = "Lỗi khi nhận:";
            // 
            // gbKetQua
            // 
            this.gbKetQua.Controls.Add(this.txtChiPhi);
            this.gbKetQua.Controls.Add(this.lblKetQua);
            this.gbKetQua.Controls.Add(this.txtKetQua);
            this.gbKetQua.Controls.Add(this.lblChiPhi);
            this.gbKetQua.Controls.Add(this.lblVND);
            this.gbKetQua.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gbKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.gbKetQua.Location = new System.Drawing.Point(30, 392);
            this.gbKetQua.Name = "gbKetQua";
            this.gbKetQua.Size = new System.Drawing.Size(800, 182);
            this.gbKetQua.TabIndex = 3;
            this.gbKetQua.TabStop = false;
            this.gbKetQua.Text = "3. Kết quả xử lý từ Hãng / Nhà cung cấp";
            // 
            // txtChiPhi
            // 
            this.txtChiPhi.BorderRadius = 5;
            this.txtChiPhi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtChiPhi.DefaultText = "";
            this.txtChiPhi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtChiPhi.ForeColor = System.Drawing.Color.Red;
            this.txtChiPhi.Location = new System.Drawing.Point(510, 64);
            this.txtChiPhi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtChiPhi.Multiline = true;
            this.txtChiPhi.Name = "txtChiPhi";
            this.txtChiPhi.PasswordChar = '\0';
            this.txtChiPhi.PlaceholderText = "";
            this.txtChiPhi.SelectedText = "";
            this.txtChiPhi.Size = new System.Drawing.Size(213, 47);
            this.txtChiPhi.TabIndex = 24;
            this.txtChiPhi.TextChanged += new System.EventHandler(this.txtChiPhi_TextChanged);
            this.txtChiPhi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiPhi_KeyPress);
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Font = new System.Drawing.Font("Arial", 10F);
            this.lblKetQua.ForeColor = System.Drawing.Color.Black;
            this.lblKetQua.Location = new System.Drawing.Point(24, 40);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(105, 19);
            this.lblKetQua.TabIndex = 0;
            this.lblKetQua.Text = "Chi tiết xử lý:";
            // 
            // txtKetQua
            // 
            this.txtKetQua.BorderRadius = 5;
            this.txtKetQua.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKetQua.DefaultText = "";
            this.txtKetQua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKetQua.ForeColor = System.Drawing.Color.Black;
            this.txtKetQua.Location = new System.Drawing.Point(34, 64);
            this.txtKetQua.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtKetQua.Multiline = true;
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.PasswordChar = '\0';
            this.txtKetQua.PlaceholderText = "";
            this.txtKetQua.SelectedText = "";
            this.txtKetQua.Size = new System.Drawing.Size(350, 82);
            this.txtKetQua.TabIndex = 22;
            // 
            // lblChiPhi
            // 
            this.lblChiPhi.AutoSize = true;
            this.lblChiPhi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblChiPhi.ForeColor = System.Drawing.Color.Red;
            this.lblChiPhi.Location = new System.Drawing.Point(506, 40);
            this.lblChiPhi.Name = "lblChiPhi";
            this.lblChiPhi.Size = new System.Drawing.Size(169, 19);
            this.lblChiPhi.TabIndex = 2;
            this.lblChiPhi.Text = "Chi phí (Thu khách):";
            // 
            // lblVND
            // 
            this.lblVND.AutoSize = true;
            this.lblVND.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblVND.ForeColor = System.Drawing.Color.Red;
            this.lblVND.Location = new System.Drawing.Point(743, 75);
            this.lblVND.Name = "lblVND";
            this.lblVND.Size = new System.Drawing.Size(51, 24);
            this.lblVND.TabIndex = 4;
            this.lblVND.Text = "VNĐ";
            // 
            // lblGhiChuTra
            // 
            this.lblGhiChuTra.AutoSize = true;
            this.lblGhiChuTra.Font = new System.Drawing.Font("Arial", 10F);
            this.lblGhiChuTra.Location = new System.Drawing.Point(34, 608);
            this.lblGhiChuTra.Name = "lblGhiChuTra";
            this.lblGhiChuTra.Size = new System.Drawing.Size(131, 19);
            this.lblGhiChuTra.TabIndex = 4;
            this.lblGhiChuTra.Text = "Ghi chú trả máy:";
            // 
            // chkDaKiemTra
            // 
            this.chkDaKiemTra.AutoSize = true;
            this.chkDaKiemTra.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.chkDaKiemTra.Location = new System.Drawing.Point(183, 673);
            this.chkDaKiemTra.Name = "chkDaKiemTra";
            this.chkDaKiemTra.Size = new System.Drawing.Size(419, 24);
            this.chkDaKiemTra.TabIndex = 2;
            this.chkDaKiemTra.Text = "Khách hàng đã kiểm tra máy hoạt động bình thường.";
            this.chkDaKiemTra.UseVisualStyleBackColor = true;
            // 
            // txtGhiChuTra
            // 
            this.txtGhiChuTra.BorderRadius = 5;
            this.txtGhiChuTra.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChuTra.DefaultText = "";
            this.txtGhiChuTra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChuTra.ForeColor = System.Drawing.Color.Black;
            this.txtGhiChuTra.Location = new System.Drawing.Point(183, 608);
            this.txtGhiChuTra.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGhiChuTra.Multiline = true;
            this.txtGhiChuTra.Name = "txtGhiChuTra";
            this.txtGhiChuTra.PasswordChar = '\0';
            this.txtGhiChuTra.PlaceholderText = "";
            this.txtGhiChuTra.SelectedText = "";
            this.txtGhiChuTra.Size = new System.Drawing.Size(582, 57);
            this.txtGhiChuTra.TabIndex = 14;
            // 
            // btnXacNhanTra
            // 
            this.btnXacNhanTra.BorderRadius = 5;
            this.btnXacNhanTra.FillColor = System.Drawing.Color.SeaGreen;
            this.btnXacNhanTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanTra.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanTra.Location = new System.Drawing.Point(605, 741);
            this.btnXacNhanTra.Name = "btnXacNhanTra";
            this.btnXacNhanTra.Size = new System.Drawing.Size(251, 45);
            this.btnXacNhanTra.TabIndex = 15;
            this.btnXacNhanTra.Text = "✔ XÁC NHẬN TRẢ MÁY";
            this.btnXacNhanTra.Click += new System.EventHandler(this.btnXacNhanTra_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(469, 741);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 45);
            this.button1.TabIndex = 76;
            this.button1.Text = "ĐÓNG";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HoanTatBH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(868, 798);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnXacNhanTra);
            this.Controls.Add(this.txtGhiChuTra);
            this.Controls.Add(this.chkDaKiemTra);
            this.Controls.Add(this.lblGhiChuTra);
            this.Controls.Add(this.gbKetQua);
            this.Controls.Add(this.gbThietBi);
            this.Controls.Add(this.gbThongTin);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HoanTatBH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechZone - Giao Trả Máy Bảo Hành";
            this.Load += new System.EventHandler(this.HoanTatBH_Load);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbThietBi.ResumeLayout(false);
            this.gbThietBi.PerformLayout();
            this.gbKetQua.ResumeLayout(false);
            this.gbKetQua.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtGhiChuTra;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtKhachHang;
        private Guna.UI2.WinForms.Guna2TextBox txtNgayNhan;
        private Guna.UI2.WinForms.Guna2TextBox txtMaPhieu;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSP;
        private Guna.UI2.WinForms.Guna2TextBox txtSerial;
        private Guna.UI2.WinForms.Guna2TextBox txtLoiBanDau;
        private Guna.UI2.WinForms.Guna2TextBox txtKetQua;
        private Guna.UI2.WinForms.Guna2TextBox txtChiPhi;
        private Guna.UI2.WinForms.Guna2Button btnXacNhanTra;
        private System.Windows.Forms.Button button1;
    }
}