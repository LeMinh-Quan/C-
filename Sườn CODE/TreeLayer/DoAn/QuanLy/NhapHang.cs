using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS; // Đừng quên các thư viện BUS của bạn

namespace DoAn.QuanLy
{
    public partial class NhapHang : Form
    {
        // ========================================================================
        // KHAI BÁO BIẾN TOÀN CỤC CHO TOÀN BỘ FORM
        // ========================================================================
        NhaCungCapBUS nccBUS = new NhaCungCapBUS();
        SanPhamBUS spBUS = new SanPhamBUS();
        DonDatHangBUS poBUS = new DonDatHangBUS();
        NhapKhoBUS nkBUS = new NhapKhoBUS();

        private DataTable dtGioHang = new DataTable(); // Giỏ hàng Tab 1

        private int maNVDangNhap;

        private DataTable dtKhoTam = new DataTable();  // Giỏ hàng tạm Tab 2
        private DataTable dtSanPhamCuaPO = new DataTable(); // Giữ danh sách và số lượng SP

        public NhapHang(int maNV)
        {
            InitializeComponent();
            this.maNVDangNhap = maNV;

            // Khởi tạo các dữ liệu ban đầu khi mở Form
            dateNgayDat.Value = DateTime.Now;
            loadComboBoxNhaCungCap();
            loadComboBoxSanPham();
            LoadLichSuDonHang();
            loadComboBoxPONhapKho();
            lblTrangThai.Text = "";

        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            // ==============================================================
            // CẤU HÌNH LƯỚI CHO TAB 1: LẬP ĐƠN ĐẶT HÀNG
            // ==============================================================
            dtGioHang.Columns.Add("MaModel", typeof(string));
            dtGioHang.Columns.Add("TenSanPham", typeof(string));
            dtGioHang.Columns.Add("SoLuong", typeof(int));
            dtGioHang.Columns.Add("DonGiaNhap", typeof(decimal));
            dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            drvDanhSachDatHang.DataSource = dtGioHang;

            drvDanhSachDatHang.Columns["MaModel"].HeaderText = "Mã Model";
            drvDanhSachDatHang.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            drvDanhSachDatHang.Columns["SoLuong"].HeaderText = "Số Lượng";
            drvDanhSachDatHang.Columns["DonGiaNhap"].HeaderText = "Đơn Giá Nhập";
            drvDanhSachDatHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";

            drvDanhSachDatHang.Columns["DonGiaNhap"].DefaultCellStyle.Format = "N0";
            drvDanhSachDatHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            // ==============================================================
            // CẤU HÌNH LƯỚI CHO TAB 2: NHẬP KHO (SERIAL)
            // ==============================================================
            // Tạo lưới quét mã
            dtKhoTam.Columns.Add("MaSerial", typeof(string));
            dtKhoTam.Columns.Add("MaModel", typeof(string));
            dtKhoTam.Columns.Add("TenSanPham", typeof(string));
            drvDanhSachNhapHang.DataSource = dtKhoTam;

            // định dạng màu sắc cho datagridview 
            drvDanhSachDatHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachDatHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachDatHang.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachDatHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachDatHang.EnableHeadersVisualStyles = false;
            drvDanhSachDatHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            drvDanhSachDatHang.AllowUserToAddRows = false;
            drvDanhSachDatHang.ReadOnly = true;
            drvDanhSachDatHang.SelectionMode = DataGridViewSelectionMode.CellSelect;

            drvDanhSachNhapHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachNhapHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachNhapHang.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachNhapHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachNhapHang.EnableHeadersVisualStyles = false;
            drvDanhSachNhapHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhSachNhapHang.AllowUserToAddRows = false;
            drvDanhSachNhapHang.ReadOnly = true;
            drvDanhSachNhapHang.SelectionMode = DataGridViewSelectionMode.CellSelect;

            drvLichSuDH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvLichSuDH.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvLichSuDH.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvLichSuDH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvLichSuDH.EnableHeadersVisualStyles = false;
            drvLichSuDH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvLichSuDH.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvLichSuDH.AllowUserToAddRows = false;
            drvLichSuDH.ReadOnly = true;

        }

        // ##########################################################################################
        // TAB 1: LẬP ĐƠN ĐẶT HÀNG (PO)
        // ##########################################################################################

        private void loadComboBoxNhaCungCap()
        {
            DataTable dtNCC = nccBUS.LayToanBoNCC();
            cbbNhaCungCap.DataSource = dtNCC;
            cbbNhaCungCap.DisplayMember = "TenNCC";
            cbbNhaCungCap.ValueMember = "MaNCC";
            cbbNhaCungCap.SelectedIndex = -1;
        }

        private void loadComboBoxSanPham()
        {
            DataTable dtSP = spBUS.LayDanhSachSPChoDonHang();
            cbbTenSP.DataSource = dtSP;
            cbbTenSP.DisplayMember = "TenHienThi";
            cbbTenSP.ValueMember = "MaModel";
        }

        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataRow row in dtGioHang.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtThanhTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbTenSP.SelectedIndex == -1 || numSoLuongDat.Value <= 0 || string.IsNullOrWhiteSpace(txtDonGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm, nhập số lượng > 0 và nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chuoiDonGia = txtDonGiaNhap.Text.Replace(".", "").Replace(",", "");
            decimal donGia = 0;

            if (!decimal.TryParse(chuoiDonGia, out donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maModel = cbbTenSP.SelectedValue.ToString();
            string tenSP = cbbTenSP.Text;
            int soLuong = Convert.ToInt32(numSoLuongDat.Value);
            decimal thanhTien = soLuong * donGia;

            bool daTonTai = false;
            foreach (DataRow row in dtGioHang.Rows)
            {
                if (row["MaModel"].ToString() == maModel)
                {
                    int slCu = Convert.ToInt32(row["SoLuong"]);
                    row["SoLuong"] = slCu + soLuong;
                    row["ThanhTien"] = Convert.ToInt32(row["SoLuong"]) * donGia;
                    row["DonGiaNhap"] = donGia;
                    daTonTai = true;
                    break;
                }
            }

            if (!daTonTai)
            {
                DataRow newRow = dtGioHang.NewRow();
                newRow["MaModel"] = maModel;
                newRow["TenSanPham"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["DonGiaNhap"] = donGia;
                newRow["ThanhTien"] = thanhTien;
                dtGioHang.Rows.Add(newRow);
            }

            TinhTongTien();
            numSoLuongDat.Value = 0;
            txtDonGiaNhap.Text = "";
            cbbTenSP.Focus();
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            if (drvDanhSachDatHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
                return;
            }
            dtGioHang.Rows.RemoveAt(drvDanhSachDatHang.CurrentRow.Index);
            TinhTongTien();
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            if (cbbNhaCungCap.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Đơn hàng đang trống. Vui lòng thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maNCC = Convert.ToInt32(cbbNhaCungCap.SelectedValue);
                decimal tongTien = 0;
                foreach (DataRow row in dtGioHang.Rows)
                {
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);
                }

                int maHoaDonMoi = poBUS.LuuDonDatHang(maNCC, maNVDangNhap, tongTien, dtGioHang);

                if (maHoaDonMoi > 0)
                {
                    MessageBox.Show("Lưu đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtGioHang.Clear();
                    TinhTongTien();
                    cbbNhaCungCap.SelectedIndex = -1;

                    // Cập nhật lại dữ liệu cho các Tab khác
                    loadComboBoxPONhapKho();
                    LoadLichSuDonHang();

                    HoaDonNH frmHoaDon = new HoaDonNH(maHoaDonMoi);
                    frmHoaDon.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi lưu đơn: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            // Mở form thêm NCC mới (nếu bạn có)
            ThemNhaCC frmNhaCC = new ThemNhaCC();
            frmNhaCC.ShowDialog();
            loadComboBoxNhaCungCap();
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGiaNhap.Text == "") return;

            string soGoc = txtDonGiaNhap.Text.Replace(".", "").Replace(",", "");
            long tien;
            if (long.TryParse(soGoc, out tien))
            {
                txtDonGiaNhap.TextChanged -= txtDonGiaNhap_TextChanged;
                txtDonGiaNhap.Text = string.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:#,##0}", tien);
                txtDonGiaNhap.SelectionStart = txtDonGiaNhap.Text.Length;
                txtDonGiaNhap.TextChanged += txtDonGiaNhap_TextChanged;
            }
        }

        // ##########################################################################################
        // TAB 2: NHẬP KHO QUÉT SERIAL
        // ##########################################################################################

        private void loadComboBoxPONhapKho()
        {
            DataTable dtPO = nkBUS.LayDanhSachPO_NhapKho();
            cbbmaPONhapHang.DisplayMember = "MaPO";
            cbbmaPONhapHang.ValueMember = "MaPO";
            cbbmaPONhapHang.DataSource = dtPO;
            cbbmaPONhapHang.SelectedIndex = -1;
        }

        private void cbbmaPONhapHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbmaPONhapHang.SelectedIndex == -1 || cbbmaPONhapHang.SelectedValue is DataRowView) return;

            try
            {
                int maPO = Convert.ToInt32(cbbmaPONhapHang.SelectedValue);

                // A. Load thông tin chung (Sử dụng hàm LayThongTinChung của bạn)
                DataTable dtPOInfo = poBUS.LayThongTinChung(maPO);
                if (dtPOInfo.Rows.Count > 0)
                {
                    DataRow r = dtPOInfo.Rows[0];
                    lblThongTinMaPO.Text = "PO" + r["MaPO"].ToString();
                    lblThongTinNCC.Text = r["TenNCC"].ToString();
                    lblThongTinNgayDat.Text = Convert.ToDateTime(r["NgayTao"]).ToString("dd/MM/yyyy");

                    // Xử lý riêng trạng thái
                    string trangThai = dtPOInfo.Rows[0]["TrangThai"]?.ToString() ?? "Chờ giao";
                    lblTrangThai.Text = trangThai;
                }

                // B. Load Danh sách Sản phẩm vào ComboBox và bảng tạm bộ nhớ
                dtSanPhamCuaPO = nkBUS.LayChiTietSP_NhapKho(maPO);

                cbbSanPhamThuocPO.DisplayMember = "TenSanPham";
                cbbSanPhamThuocPO.ValueMember = "MaModel";
                cbbSanPhamThuocPO.DataSource = dtSanPhamCuaPO;
                cbbSanPhamThuocPO.SelectedIndex = -1;

                // Reset dữ liệu lưới cũ
                dtKhoTam.Clear();
                lblSLSanPham.Text = "0";
                lblSlCanNhap.Text = "0";

                // Tính tổng số lượng cần nhập cho toàn PO
                int tongPO = 0;
                foreach (DataRow r in dtSanPhamCuaPO.Rows) tongPO += Convert.ToInt32(r["SoLuongCanNhap"]);
                lblSlCanNhap.Text = tongPO.ToString();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void cbbSanPhamThuocPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSanPhamThuocPO.SelectedIndex == -1 || cbbSanPhamThuocPO.SelectedValue is DataRowView) return;

            string maModelCbb = cbbSanPhamThuocPO.SelectedValue.ToString();

            // Tìm trong bảng ảo xem nó cần nhập bao nhiêu
            DataRow[] found = dtSanPhamCuaPO.Select($"MaModel = '{maModelCbb}'");
            if (found.Length > 0)
            {
                lblSLSanPham.Text = found[0]["SoLuongCanNhap"].ToString();
            }
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            string serial = txtMaSerialNhapHang.Text.Trim();
            if (string.IsNullOrEmpty(serial) || cbbSanPhamThuocPO.SelectedIndex == -1) return;

            // Kiem tra SL
            int slConLai = Convert.ToInt32(lblSLSanPham.Text);
            if (slConLai <= 0)
            {
                MessageBox.Show("Sản phẩm này đã quét đủ số lượng yêu cầu. Vui lòng chọn sản phẩm khác!", "Đã đủ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSerialNhapHang.Text = "";
                return;
            }

            // Kiểm tra trùng mã Serial trên lưới
            if (dtKhoTam.Select($"MaSerial = '{serial}'").Length > 0)
            {
                MessageBox.Show("Mã Serial đã quét rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow[] found = dtKhoTam.Select($"MaSerial = '{serial}'");
            if (found.Length > 0)
            {
                MessageBox.Show("Mã Serial này đã được quét ở phía dưới rồi!", "Lỗi trùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSerialNhapHang.SelectAll();
                return;
            }

            // =====================================================================
            // KIỂM TRA DƯỚI DATABASE XEM ĐÃ CÓ TRONG KHO CHƯA
            if (nkBUS.KiemTraSerialTonTai(serial))
            {
                MessageBox.Show($"Mã Serial '{serial}' này ĐÃ TỒN TẠI trong kho rồi!\nCó thể máy này đã được nhập từ đợt trước. Vui lòng kiểm tra lại!",
                                "Lỗi trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSerialNhapHang.SelectAll();
                return;
            }

            // Thêm vào lưới
            string maModel = cbbSanPhamThuocPO.SelectedValue.ToString();
            string tenSP = cbbSanPhamThuocPO.Text;
            dtKhoTam.Rows.Add(serial, maModel, tenSP);

            // TRỪ SỐ LƯỢNG ẢO TRÊN GIAO DIỆN
            slConLai--;
            lblSLSanPham.Text = slConLai.ToString();

            // =====================================================================
            // SỬA LỖI Ở ĐÂY: Dùng đúng biến foundSP để kiểm tra
            DataRow[] foundSP = dtSanPhamCuaPO.Select($"MaModel = '{maModel}'");
            if (foundSP.Length > 0)
            {
                foundSP[0]["SoLuongCanNhap"] = slConLai;
            }
            // =====================================================================

            txtMaSerialNhapHang.Text = "";
            txtMaSerialNhapHang.Focus();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (drvDanhSachNhapHang.CurrentRow == null) return;

            // Lấy thông tin dòng bị xóa
            string maModelXoa = drvDanhSachNhapHang.CurrentRow.Cells["MaModel"].Value.ToString();

            // Xóa khỏi lưới
            drvDanhSachNhapHang.Rows.Remove(drvDanhSachNhapHang.CurrentRow);

            // CỘNG TRẢ LẠI SỐ LƯỢNG VÀO BẢNG ẢO
            DataRow[] found = dtSanPhamCuaPO.Select($"MaModel = '{maModelXoa}'");
            if (found.Length > 0)
            {
                int slMoi = Convert.ToInt32(found[0]["SoLuongCanNhap"]) + 1;
                found[0]["SoLuongCanNhap"] = slMoi;

                // Nếu cái Model bị xóa đang được chọn hiển thị trên CBB, thì update luôn cái Label
                if (cbbSanPhamThuocPO.SelectedValue != null && cbbSanPhamThuocPO.SelectedValue.ToString() == maModelXoa)
                {
                    lblSLSanPham.Text = slMoi.ToString();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cbbmaPONhapHang.SelectedIndex == -1 || dtKhoTam.Rows.Count == 0) return;

            try
            {
                int maPO = Convert.ToInt32(cbbmaPONhapHang.SelectedValue);
                string ghiChu = txtGhiChu.Text.Trim();

                // Gửi xuống BUS -> DAL
                if (nkBUS.LuuPhieuNhapKho(maPO, ghiChu, dtKhoTam))
                {
                    MessageBox.Show("Lưu phiếu nhập kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset Form
                    dtKhoTam.Clear();
                    cbbmaPONhapHang.SelectedIndex = -1;
                    loadComboBoxPONhapKho();
                    LoadLichSuDonHang();
                    lblThongTinMaPO.Text = "---";
                    lblThongTinNCC.Text = "---";
                    // Các label khác bạn reset nốt nhé
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi khi lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ##########################################################################################
        // TAB 3: LỊCH SỬ ĐƠN ĐẶT HÀNG
        // ##########################################################################################

        private void LoadLichSuDonHang()
        {
            try
            {
                DataTable dt = poBUS.LayDanhSachDonHang();
                drvLichSuDH.DataSource = dt;

                drvLichSuDH.Columns["MaPO"].HeaderText = "Mã Đơn";
                drvLichSuDH.Columns["NgayTao"].HeaderText = "Ngày Lập";
                drvLichSuDH.Columns["TenNCC"].HeaderText = "Nhà Cung Cấp";
                drvLichSuDH.Columns["TenNV"].HeaderText = "Nhân Viên Lập";
                drvLichSuDH.Columns["TongTien"].HeaderText = "Tổng Tiền";
                drvLichSuDH.Columns["TrangThai"].HeaderText = "Trạng Thái";

                drvLichSuDH.Columns["MaPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                drvLichSuDH.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                drvLichSuDH.Columns["TenNCC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                drvLichSuDH.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                drvLichSuDH.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                drvLichSuDH.Columns["NgayDuKienGiao"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (drvLichSuDH.CurrentRow == null || drvLichSuDH.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một Đơn đặt hàng trong danh sách để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maPO = Convert.ToInt32(drvLichSuDH.CurrentRow.Cells["MaPO"].Value);
                HoaDonNH frmHoaDon = new HoaDonNH(maPO);
                frmHoaDon.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết đơn hàng: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTimKiemPO_Click(object sender, EventArgs e)
        {
            DataTable dt = drvLichSuDH.DataSource as DataTable;
            if (dt == null) return;

            string input = txtTimKiemPO.Text.Trim();

            // TRƯỜNG HỢP 1: Ô tìm kiếm rỗng -> Xóa lọc, mở khóa CheckBox
            if (string.IsNullOrEmpty(input))
            {
                dt.DefaultView.RowFilter = "";
                ckChoGiao.Enabled = ckGiaoThieu.Enabled = true; // Viết gộp cho ngắn
                return;
            }

            // TRƯỜNG HỢP 2: Có chữ -> Kiểm tra xem có phải số không
            if (int.TryParse(input, out int maPO))
            {
                // Lọc dữ liệu
                dt.DefaultView.RowFilter = $"MaPO = {maPO}";

                // Tắt dấu tích và khóa Checkbox (Viết gộp)
                ckChoGiao.Checked = ckGiaoThieu.Checked = false;
                ckChoGiao.Enabled = ckGiaoThieu.Enabled = false;

                // Báo nếu không tìm thấy
                if (drvLichSuDH.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy Đơn hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu nhập chữ cái (A, B, C...)
                MessageBox.Show("Vui lòng chỉ nhập số vào ô Mã PO!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiemPO.Focus();
            }
        }
        private void ckChoGiao_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = drvLichSuDH.DataSource as DataTable;
            if (dt == null) return;

            if (ckChoGiao.Checked)
            {
                // KHI TÍCH VÀO: Lọc dữ liệu và Khóa các chức năng khác
                dt.DefaultView.RowFilter = "TrangThai = 'Chờ giao'";

                btnTimKiemPO.Enabled = false;
                ckGiaoThieu.Enabled = false;
                txtTimKiemPO.Enabled = false; // Khóa luôn ô gõ chữ
            }
            else
            {
                // KHI BỎ TÍCH: Xóa bộ lọc và Mở khóa lại tất cả
                dt.DefaultView.RowFilter = "";

                btnTimKiemPO.Enabled = true;
                ckGiaoThieu.Enabled = true;
                txtTimKiemPO.Enabled = true;
            }
        }

        private void ckGiaoThieu_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = drvLichSuDH.DataSource as DataTable;
            if (dt == null) return;

            if (ckGiaoThieu.Checked)
            {
                // KHI TÍCH VÀO: Lọc dữ liệu và Khóa các chức năng khác
                dt.DefaultView.RowFilter = "TrangThai = 'Giao thiếu'";

                btnTimKiemPO.Enabled = false;
                ckChoGiao.Enabled = false;
                txtTimKiemPO.Enabled = false;
            }
            else
            {
                // KHI BỎ TÍCH: Xóa bộ lọc và Mở khóa lại tất cả
                dt.DefaultView.RowFilter = "";

                btnTimKiemPO.Enabled = true;
                ckChoGiao.Enabled = true;
                txtTimKiemPO.Enabled = true;
            }
        }

        private void btnResetPO_Click(object sender, EventArgs e)
        {
            txtTimKiemPO.Text = "";
            ckChoGiao.Checked = false;
            ckGiaoThieu .Checked = false;
            btnTimKiemPO.Enabled = true;
            ckChoGiao.Enabled = true;
            ckGiaoThieu.Enabled= true;

            DataTable dt = (DataTable)drvLichSuDH.DataSource;
            if (dt != null) dt.DefaultView.RowFilter = "";

            txtTimKiemPO.Focus();
        }

        private void drvLichSuDH_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem phần mềm có đang vẽ dữ liệu cho cột "TrangThai" không
            if (drvLichSuDH.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();

                // Định dạng in đậm cho chữ dễ đọc
                e.CellStyle.Font = new Font(drvLichSuDH.Font, FontStyle.Bold);

                // Bắt đầu tô màu nền (BackColor) và màu chữ (ForeColor)
                if (trangThai == "Hoàn tất")
                {
                    e.CellStyle.BackColor = Color.SeaGreen;   // Nền xanh lá cây
                    e.CellStyle.ForeColor = Color.White;      // Chữ trắng
                }
                else if (trangThai == "Giao thiếu")
                {
                    e.CellStyle.BackColor = Color.Firebrick;  // Nền đỏ
                    e.CellStyle.ForeColor = Color.White;      // Chữ trắng
                }
                else if (trangThai == "Chờ giao")
                {
                    e.CellStyle.ForeColor = Color.Black;      // Chữ đen
                }
            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {

            int maPO = Convert.ToInt32(drvLichSuDH.CurrentRow.Cells["MaPO"].Value);

            TTCTPO frmCTPO = new TTCTPO(maPO);
            frmCTPO.ShowDialog();
        }

    }
}