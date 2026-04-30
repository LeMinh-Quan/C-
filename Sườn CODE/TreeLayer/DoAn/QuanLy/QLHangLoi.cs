using System;
using System.Collections.Generic; // Mặc dù using có ở đây nhưng ta sẽ không dùng List trong logic chính
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DoAn.HeThong;

namespace DoAn.QuanLy
{
    public partial class QLHangLoi : Form
    {
        private int maNVDangNhap; // Biến lưu mã NV truyền từ Form đăng nhập
        private string tenNVDangNhap;
        BaoHanhAMBUS bhBUS = new BaoHanhAMBUS();
        ReportHoaDonBUS rp =new ReportHoaDonBUS();
        public QLHangLoi(int maNV, string tenNV)
        {
            InitializeComponent();
            this.maNVDangNhap = maNV; // Lấy Mã NV từ Constructor nạp vào biến toàn cục của Form
            this.tenNVDangNhap = tenNV;
            dtpNgayGui.Value= DateTime.Now;

        }

        private void QLHangLoi_Load(object sender, EventArgs e)
        {
            LoadDanhSachBaoHanh();
            LoadNhaCC();
            LoadDanhSachPhieuGui();

            dgvDanhSachMay.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvDanhSachMay.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvDanhSachMay.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            dgvDanhSachMay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDanhSachMay.EnableHeadersVisualStyles = false;
            dgvDanhSachMay.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDanhSachMay.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDanhSachMay.AllowUserToAddRows = false;
            dgvDanhSachMay.ReadOnly = true;
            dgvDanhSachMay.DefaultCellStyle.ForeColor = Color.Black;
            dgvDanhSachMay.DefaultCellStyle.Font = new Font("Segoe UI", 10);
        }

        //###############################################################################################################################
        //-------------------------------------------- TABPAGE 1 -----------------------------------------------------------------------
        //###############################################################################################################################
        private void LoadDanhSachBaoHanh()
        {
            try
            {
                DataTable dtBaoHanh = bhBUS.LayDanhSachBaoHanhTH();
                dgvDanhSachBaoHanh.DataSource = dtBaoHanh;

                if (dgvDanhSachBaoHanh.Columns.Count > 0)
                {
                    dgvDanhSachBaoHanh.Columns["MaPhieuBH"].HeaderText = "Mã Phiếu";
                    dgvDanhSachBaoHanh.Columns["MaPhieuBH"].FillWeight = 60;

                    dgvDanhSachBaoHanh.Columns["MaSerial"].HeaderText = "Mã Serial";
                    dgvDanhSachBaoHanh.Columns["MaSerial"].FillWeight = 80;

                    dgvDanhSachBaoHanh.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                    dgvDanhSachBaoHanh.Columns["TenSanPham"].FillWeight = 120;

                    dgvDanhSachBaoHanh.Columns["TinhTrangLoi"].HeaderText = "Tình Trạng Lỗi";
                    dgvDanhSachBaoHanh.Columns["TinhTrangLoi"].FillWeight = 150;

                    dgvDanhSachBaoHanh.Columns["PhuongAnDeXuat"].HeaderText = "Đề Xuất";
                    dgvDanhSachBaoHanh.Columns["TrangThai"].HeaderText = "Trạng Thái";

                    dgvDanhSachBaoHanh.Columns["NgayTiepNhan"].HeaderText = "Ngày Nhận";
                    dgvDanhSachBaoHanh.Columns["NgayTiepNhan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                    dgvDanhSachBaoHanh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                    dgvDanhSachBaoHanh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgvDanhSachBaoHanh.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
                    dgvDanhSachBaoHanh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dgvDanhSachBaoHanh.EnableHeadersVisualStyles = false;
                    dgvDanhSachBaoHanh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgvDanhSachBaoHanh.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgvDanhSachBaoHanh.AllowUserToAddRows = false;
                    dgvDanhSachBaoHanh.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bảo hành: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkChoGuiHang_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDanhSachBaoHanh.DataSource;
            if (dt == null) return;
            if (chkChoGuiHang.Checked)
            {
                txtTimKiem.Enabled = false;
                btnTimKiem.Enabled = false;
                dt.DefaultView.RowFilter = "TrangThai = 'Chờ gửi NCC'";
            }
            else
            {
                txtTimKiem.Enabled = true;
                btnTimKiem.Enabled = true;
                LoadDanhSachBaoHanh(); // Hoặc dt.DefaultView.RowFilter = ""; sẽ mượt hơn
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Mã Bảo Hành để tìm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chkChoGuiHang.Enabled = false;
            string tuKhoa = txtTimKiem.Text.Trim();
            DataTable dt = (DataTable)dgvDanhSachBaoHanh.DataSource;
            try
            {
                dt.DefaultView.RowFilter = $"Convert(MaPhieuBH, 'System.String') LIKE '%{tuKhoa}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lọc dữ liệu: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;
            btnTimKiem.Enabled = true;
            chkChoGuiHang.Enabled = true;
            chkChoGuiHang.Checked = false;
            txtTimKiem.Text = "";
            LoadDanhSachBaoHanh();
        }

        private void btnLapPhieuGui_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachBaoHanh.CurrentRow == null || dgvDanhSachBaoHanh.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phiếu bảo hành từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soMayKhongHopLe = 0;
            int soMayMoiThem = 0;

            // ========================================================
            // BƯỚC ĐỆM (KHÔNG DÙNG LIST): Dùng Mảng Array [] cơ bản
            // ========================================================
            DataGridViewRow[] dsDongCanXuLy;

            if (dgvDanhSachBaoHanh.SelectedRows.Count > 0)
            {
                // Khởi tạo mảng có kích thước bằng số dòng được chọn và chép dữ liệu sang
                dsDongCanXuLy = new DataGridViewRow[dgvDanhSachBaoHanh.SelectedRows.Count];
                dgvDanhSachBaoHanh.SelectedRows.CopyTo(dsDongCanXuLy, 0);
            }
            else
            {
                // Chỉ tạo mảng 1 phần tử chứa đúng cái dòng đang được click
                dsDongCanXuLy = new DataGridViewRow[] { dgvDanhSachBaoHanh.CurrentRow };
            }

            // ========================================================
            // CHẠY VÒNG LẶP TRÊN MẢNG VỪA TẠO
            // ========================================================
            foreach (DataGridViewRow row in dsDongCanXuLy)
            {
                if (row.IsNewRow) continue;

                string trangThai = row.Cells["TrangThai"].Value?.ToString();

                if (trangThai == "Chờ gửi NCC")
                {
                    string maPhieu = row.Cells["MaPhieuBH"].Value?.ToString();
                    string maSerial = row.Cells["MaSerial"].Value?.ToString();
                    string tenSP = row.Cells["TenSanPham"].Value?.ToString();
                    string tinhTrang = row.Cells["TinhTrangLoi"].Value?.ToString();

                    // THUẬT TOÁN QUÉT CHỐNG TRÙNG LẶP
                    bool daTonTai = false;
                    foreach (DataGridViewRow rowDaThem in dgvDanhSachMay.Rows)
                    {
                        if (rowDaThem.IsNewRow) continue;

                        foreach (DataGridViewCell cell in rowDaThem.Cells)
                        {
                            if (cell.Value != null && cell.Value.ToString() == maPhieu)
                            {
                                daTonTai = true;
                                break;
                            }
                        }
                        if (daTonTai) break;
                    }

                    // Thêm vào Tab 2 nếu chưa tồn tại
                    if (!daTonTai)
                    {
                        dgvDanhSachMay.Rows.Add(maPhieu, maSerial, tenSP, tinhTrang);
                        soMayMoiThem++;
                    }
                }
                else
                {
                    soMayKhongHopLe++;
                }
            }

            if (soMayKhongHopLe > 0)
            {
                MessageBox.Show($"Đã tự động loại bỏ {soMayKhongHopLe} dòng vì máy không ở trạng thái chờ gửi.", "Lọc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Chuyển sang Tab 2
            guna2TabControl1.SelectedIndex = 2;
        }
        //###############################################################################################################################
        //-------------------------------------------- TABPAGE 2 -----------------------------------------------------------------------
        //###############################################################################################################################

        private void LoadDanhSachPhieuGui()
        {
            try
            {
                // 1. Gọi BUS lấy dữ liệu
                DataTable dtPhieuGui = bhBUS.LayDanhSachPhieuGuiNCC();

                // 2. Gán vào DataGridView
                drvDanhSachPhieuLap.DataSource = dtPhieuGui;

                // 3. Đổi tên cột tiếng Việt và căn chỉnh độ rộng
                if (drvDanhSachPhieuLap.Columns.Count > 0)
                {
                    drvDanhSachPhieuLap.Columns["MaPhieuGui"].HeaderText = "Mã Phiếu";
                    drvDanhSachPhieuLap.Columns["MaPhieuGui"].FillWeight = 80;

                    drvDanhSachPhieuLap.Columns["TenNCC"].HeaderText = "Hãng / NCC";
                    drvDanhSachPhieuLap.Columns["TenNCC"].FillWeight = 150;

                    drvDanhSachPhieuLap.Columns["TenAdmin"].HeaderText = "Họ Tên Admin";
                    drvDanhSachPhieuLap.Columns["TenAdmin"].FillWeight = 80;

                    drvDanhSachPhieuLap.Columns["NgayGui"].HeaderText = "Ngày Gửi";
                    drvDanhSachPhieuLap.Columns["NgayGui"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; // Định dạng ngày giờ
                    drvDanhSachPhieuLap.Columns["NgayGui"].FillWeight = 120;

                    drvDanhSachPhieuLap.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    drvDanhSachPhieuLap.Columns["TrangThai"].FillWeight = 120;

                    drvDanhSachPhieuLap.Columns["GhiChu"].HeaderText = "Ghi Chú";
                    drvDanhSachPhieuLap.Columns["GhiChu"].FillWeight = 200;

                    // (Tùy chọn) Cho lưới tự động chia đều chiều ngang của Form
                    drvDanhSachPhieuLap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    drvDanhSachPhieuLap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                    drvDanhSachPhieuLap.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    drvDanhSachPhieuLap.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
                    drvDanhSachPhieuLap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    drvDanhSachPhieuLap.EnableHeadersVisualStyles = false;
                    drvDanhSachPhieuLap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    drvDanhSachPhieuLap.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    drvDanhSachPhieuLap.AllowUserToAddRows = false;
                    drvDanhSachPhieuLap.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phiếu gửi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTietPL_Click(object sender, EventArgs e)
        {
            // 1. Bắt lỗi nếu chưa bôi đen dòng nào
            if (drvDanhSachPhieuLap.CurrentRow == null || drvDanhSachPhieuLap.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng bôi đen 1 phiếu gửi để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 2. Chỉ cần nhặt đúng cái Mã Phiếu Gửi từ dòng đang được chọn
                DataGridViewRow row = drvDanhSachPhieuLap.CurrentRow;
                int maPhieuGui = Convert.ToInt32(row.Cells["MaPhieuGui"].Value);

                // 3. Mở Form In RDLC và truyền Mã Phiếu sang (Để Form In tự lo phần lấy dữ liệu)
                HoaDonBaoHanh frmIn = new HoaDonBaoHanh(maPhieuGui);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi mở chi tiết phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTimMaPhieu_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Mã Bảo Hành để tìm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ckDangGui.Enabled = false;
            string tuKhoa = txtMaPhieu.Text.Trim();
            DataTable dt = (DataTable)drvDanhSachPhieuLap.DataSource;
            try
            {
                dt.DefaultView.RowFilter = $"Convert(MaPhieuGui, 'System.String') LIKE '%{tuKhoa}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lọc dữ liệu: " + ex.Message);
            }
        }

        private void ckDangGui_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)drvDanhSachPhieuLap.DataSource;
            if (dt == null) return;
            if (ckDangGui.Checked)
            {
                txtMaPhieu.Enabled = false;
                btnTimMaPhieu.Enabled = false;
                dt.DefaultView.RowFilter = "TrangThai = 'Đang gửi NCC'";
            }
            else
            {
                txtMaPhieu.Enabled = true;
                btnTimMaPhieu.Enabled = true;
                LoadDanhSachPhieuGui(); // Hoặc dt.DefaultView.RowFilter = ""; sẽ mượt hơn
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Enabled = true;
            btnTimMaPhieu.Enabled = true;
            ckDangGui.Enabled = true;
            ckDangGui.Checked = false;
            txtMaPhieu.Text = "";
            LoadDanhSachPhieuGui();
        }

        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn Phiếu gửi nào chưa
            if (drvDanhSachPhieuLap.CurrentRow == null || drvDanhSachPhieuLap.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng bôi đen 1 Phiếu gửi trên danh sách để nhận hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ lưới
            DataGridViewRow row = drvDanhSachPhieuLap.CurrentRow;
            string trangThai = row.Cells["TrangThai"].Value?.ToString();

            // 2. Chặn lỗi: Tránh cập nhật lại phiếu đã nhận rồi
            if (trangThai == "NCC đã trả hàng")
            {
                MessageBox.Show("Phiếu gửi này đã được Hãng trả hàng rồi, không thể cập nhật nữa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int maPhieuGui = Convert.ToInt32(row.Cells["MaPhieuGui"].Value);

            // 3. Hỏi xác nhận trước khi làm
            DialogResult dr = MessageBox.Show($"Xác nhận Hãng đã trả TOÀN BỘ máy của Lô hàng (Mã Phiếu: {maPhieuGui}) về cửa hàng?",
                                              "Nhận hàng theo lô", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // 4. Gọi hàm BUS xử lý
                    if (bhBUS.CapNhatToanBoLohangVeKho(maPhieuGui))
                    {
                        MessageBox.Show("Đã cập nhật Phiếu gửi và toàn bộ các máy bên trong về kho thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Load lại lưới để thấy trạng thái Phiếu gửi đổi thành 'NCC đã trả hàng'
                        LoadDanhSachBaoHanh();
                        LoadDanhSachPhieuGui();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nghiệp vụ: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        //###############################################################################################################################
        //-------------------------------------------- TABPAGE 3 -----------------------------------------------------------------------
        //###############################################################################################################################

        private void LoadNhaCC()
        {
            cboNCC.DataSource = bhBUS.LayDanhSachNhaCungCap();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
            cboNCC.SelectedIndex = -1;
        }

        private void btnTaoPhieu_Click_1(object sender, EventArgs e)
        {
            try
            {
                // ==========================================
                // 1. KIỂM TRA VÀ THU THẬP DỮ LIỆU ĐẦU VÀO
                // ==========================================
                if (cboNCC.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn Nhà Cung Cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maHieuNCC = Convert.ToInt32(cboNCC.SelectedValue);
                int maAdminTrucCa = this.maNVDangNhap;
                DateTime ngayGui = dtpNgayGui.Value;
                string ghiChu = txtGhiChu.Text.Trim();

                // Đếm số lượng máy trên lưới (Tab 2)
                int soMay = 0;
                foreach (DataGridViewRow row in dgvDanhSachMay.Rows)
                {
                    if (!row.IsNewRow) soMay++;
                }

                if (soMay == 0)
                {
                    MessageBox.Show("Chưa có máy nào để gửi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đưa mã phiếu vào mảng
                int[] mangMaPhieu = new int[soMay];
                int idx = 0;
                foreach (DataGridViewRow row in dgvDanhSachMay.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        mangMaPhieu[idx++] = Convert.ToInt32(row.Cells[0].Value);
                    }
                }

                // ==========================================
                // 2. GỌI BUS LƯU VÀO DATABASE
                // ==========================================
                if (bhBUS.TaoPhieuGuiNCC1(maHieuNCC, maAdminTrucCa, ngayGui, ghiChu, mangMaPhieu))
                {
                    MessageBox.Show("Lập phiếu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ==========================================
                    // 3. LẤY MÃ PHIẾU VÀ HIỂN THỊ FORM IN RDLC
                    // ==========================================

                    // Lấy mã phiếu vừa tạo. Nếu DAL của bạn yêu cầu truyền tham số Mã NV thì truyền biến maAdminTrucCa vào.
                    // Nếu không cần tham số thì bạn xóa chữ maAdminTrucCa trong ngoặc đi nhé.
                    int maPhieuVuaTao = bhBUS.LayMaPhieuGuiMoiNhat(maAdminTrucCa);

                    if (maPhieuVuaTao > 0)
                    {
                        // Chỉ truyền duy nhất 1 Mã Phiếu sang Form in (Chuẩn RDLC)
                        HoaDonBaoHanh frmIn = new HoaDonBaoHanh(maPhieuVuaTao);
                        frmIn.ShowDialog();
                    }

                    // ==========================================
                    // 4. DỌN DẸP GIAO DIỆN
                    // ==========================================
                    dgvDanhSachMay.Rows.Clear(); // Xóa sạch lưới ở Tab 2
                    txtGhiChu.Clear();
                    LoadDanhSachBaoHanh();       // Load lại dữ liệu Tab 1
                                                 // LoadDanhSachPhieuGui();   // Mở comment dòng này nếu bạn có hàm load lưới danh sách phiếu đã gửi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachBaoHanh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Kiểm tra xem có đang ở cột Trạng Thái không
            // Cột "TrangThai" phải khớp với tên Name (hoặc HeaderText) bạn đã đặt
            if (dgvDanhSachBaoHanh.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                // 2. Nếu giá trị của ô đó không bị rỗng
                if (e.Value != null)
                {
                    // 3. Kiểm tra xem có phải "Đã về kho" không
                    if (e.Value.ToString() == "Đã về kho")
                    {
                        // 4. Tô màu nền cả dòng thành màu xanh nhạt (GreenYellow, LightGreen...)
                        dgvDanhSachBaoHanh.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;

                        // (Tùy chọn) Tô màu chữ thành Đen cho dễ đọc trên nền Xanh
                        dgvDanhSachBaoHanh.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    // Mở rộng thêm: Nếu trạng thái khác thì màu khác cho sinh động
                    else if (e.Value.ToString() == "Đang ở NCC")
                    {
                        dgvDanhSachBaoHanh.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSalmon; // Màu cam nhạt
                    }
                    else if (e.Value.ToString() == "Hoàn tất")
                    {
                        dgvDanhSachBaoHanh.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray; // Máy đã trả khách thì màu xám
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Bỏ chọn ComboBox Nhà Cung Cấp
            cboNCC.SelectedIndex = -1;

            // Xóa trắng ô Ghi chú
            txtGhiChu.Clear();

            // Đặt lại ngày gửi là ngày hôm nay
            dtpNgayGui.Value = DateTime.Now;

            // Xóa sạch các máy đã được bốc vào lưới
            dgvDanhSachMay.Rows.Clear();
        }

    }
}