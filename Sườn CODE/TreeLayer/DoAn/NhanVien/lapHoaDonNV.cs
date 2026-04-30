using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace DoAn.NhanVien
{
    public partial class lapHoaDonNV : Form
    {
        private int maNVDangNhap;
        private string tenNVDangNhap;

        // Khai báo các lớp BUS
        KhachHangBUS khBUS = new KhachHangBUS();
        SanPhamBUS spBUS = new SanPhamBUS();
        HoaDonBUS hdBUS = new HoaDonBUS();
        public lapHoaDonNV(int maNV, string tenNV)
        {
            InitializeComponent();
            this.maNVDangNhap = maNV;
            this.tenNVDangNhap = tenNV;
            SetupGrid();
            LoadDanhSachHoaDon();
        }
        private void lapHoaDonNV_Load(object sender, EventArgs e)
        {
            dgvChiTietHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            //drvDanhSachTraCuu.DefaultCellStyle.BackColor = Color.White; // dòng 1
            //drvDanhSachTraCuu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220); // dòng 2 (xanh lá nhạt)

        }
        //#####################################################################################################
        //-------------------------------------- TABPAGE1 LẬP HÓA ĐƠN  ---------------------------------------

        private void SetupGrid()
        {
            dgvChiTietHoaDon.Columns.Clear();
            dgvChiTietHoaDon.Columns.Add("MaSerial", "Mã Serial");
            dgvChiTietHoaDon.Columns.Add("MaModel", "Mã Model");
            dgvChiTietHoaDon.Columns.Add("TenSanPham", "Tên Sản Phẩm");
            dgvChiTietHoaDon.Columns.Add("DonGia", "Đơn Giá Bán");

            // ==========================================================
            // CODE LÀM ĐẸP CỘT TIỀN
            // ==========================================================
            // 1. Format phân cách hàng nghìn (N0)
            dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";

            // 2. Căn lề phải cho cột Đơn giá để dễ nhìn, dễ cộng nhẩm
            dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdt = txtTimKiemSDT.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtKhach = khBUS.LayKhachHangTheoSDT(sdt);
            if (dtKhach.Rows.Count > 0)
            {
                // Có khách -> Đổ dữ liệu và khóa ô
                txtSoDT.Text = dtKhach.Rows[0]["SDT"].ToString();
                txtHoTen.Text = dtKhach.Rows[0]["HoTen"].ToString();
                txtEmail.Text = dtKhach.Rows[0]["Email"].ToString();
                txtDiaChi.Text = dtKhach.Rows[0]["DiaChi"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng! Vui lòng bấm '+ Thêm Khách Hàng Mới'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnThemKH_Click_1(object sender, EventArgs e)
        {
            txtSoDT.ReadOnly = false;
            txtHoTen.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSoDT.Text = txtTimKiemSDT.Text;
            txtHoTen.Focus();
        }


        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
            {
                if (!row.IsNewRow && row.Cells["DonGia"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["DonGia"].Value);
                }
            }

            // Format VNĐ hiển thị cho đẹp
            lblTongTienValue.Text = tongTien.ToString("N0") + " VNĐ";

            // Giấu con số thực (không có chữ VNĐ) vào Tag để lát nữa lưu CSDL
            lblTongTienValue.Tag = tongTien;
        }
        private void btnThemSP_Click_1(object sender, EventArgs e)
        {
            string serial = txtSerial.Text.Trim();
            if (string.IsNullOrEmpty(serial)) return;

            // Kiểm tra chống quét trùng trên lưới
            foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
            {
                if (!row.IsNewRow && row.Cells["MaSerial"].Value.ToString() == serial)
                {
                    MessageBox.Show("Máy này đã được thêm vào giỏ hàng!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSerial.Clear();
                    return;
                }
            }

            // Dùng hàm BUS tìm máy (Hàm này bạn viết SQL: SELECT * FROM CHI_TIET_SAN_PHAM JOIN SAN_PHAM... WHERE MaSerial = @seri AND TrangThai = 'Trong kho')
            DataTable dtMay = spBUS.TimMayTrongKhoTheoSerial(serial);

            if (dtMay.Rows.Count > 0)
            {
                // Nhét vào lưới
                dgvChiTietHoaDon.Rows.Add(
                    dtMay.Rows[0]["MaSerial"],
                    dtMay.Rows[0]["MaModel"],
                    dtMay.Rows[0]["TenSanPham"],
                    dtMay.Rows[0]["GiaBan"]
                );

                TinhTongTien();
                txtSerial.Clear();
                txtSerial.Focus();
            }
            else
            {
                MessageBox.Show("Không tìm thấy Serial này hoặc máy không còn trong kho!", "Lỗi Serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSerial.Text="";
            }
        }
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtTimKiemSDT.Clear();
            txtSoDT.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtSerial.Clear();
            txtGhiChu.Clear();
            cboHinhThuc.SelectedIndex = 0;
            dgvChiTietHoaDon.Rows.Clear();
            TinhTongTien();
            txtSoDT.ReadOnly = true;
            txtHoTen.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            LoadDanhSachHoaDon();
        }

        private void btnXoaSanPham_Click_1(object sender, EventArgs e)
        {

            // 1. Kiểm tra xem nhân viên có đang bôi đen dòng máy nào trên lưới không
            if (dgvChiTietHoaDon.SelectedRows.Count > 0)
            {
                // 2. Hỏi xác nhận cho chắc chắn (Tránh click nhầm)
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm đang chọn khỏi giỏ hàng không?",
                                                  "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    // 3. Tiến hành xóa các dòng đang được bôi đen
                    foreach (DataGridViewRow row in dgvChiTietHoaDon.SelectedRows)
                    {
                        if (!row.IsNewRow) // Bỏ qua cái dòng trắng mặc định ở cuối lưới
                        {
                            dgvChiTietHoaDon.Rows.Remove(row);
                        }
                    }

                    // 4. CỰC KỲ QUAN TRỌNG: Xóa xong phải gọi hàm tính lại tiền ngay lập tức!
                    TinhTongTien();
                }
            }
            else
            {
                // Cảnh báo nếu chưa chọn máy mà đã bấm xóa
                MessageBox.Show("Vui lòng bôi đen (click vào mũi tên đầu dòng) sản phẩm cần xóa!",
                                "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Thì từ chối, không cho ký tự đó hiện lên TextBox
                e.Handled = true;
            }
        }
        private void btnLapHoaDon_Click_1(object sender, EventArgs e)
        {
            // Bắt lỗi rỗng
            if (string.IsNullOrWhiteSpace(txtSoDT.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại và tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboHinhThuc.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn hình thức thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSoDT.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int soMay = 0;
            foreach (DataGridViewRow r in dgvChiTietHoaDon.Rows) if (!r.IsNewRow) soMay++;
            if (soMay == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gom dữ liệu lưới thành DataTable
            DataTable dtMua = new DataTable();
            dtMua.Columns.Add("MaModel");
            dtMua.Columns.Add("MaSerial");
            dtMua.Columns.Add("DonGiaBan", typeof(decimal));

            foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
            {
                if (!row.IsNewRow)
                {
                    dtMua.Rows.Add(
                        row.Cells["MaModel"].Value.ToString(),
                        row.Cells["MaSerial"].Value.ToString(),
                        Convert.ToDecimal(row.Cells["DonGia"].Value)
                    );
                }
            }

            try
            {
                string sdt = txtSoDT.Text.Trim();
                string ten = txtHoTen.Text.Trim();
                string httt = cboHinhThuc.Text;
                decimal tong = Convert.ToDecimal(lblTongTienValue.Tag);
                string ghiChu = txtGhiChu.Text.Trim();
                string email = txtEmail.Text.Trim();
                string diachi = txtDiaChi.Text.Trim();

                // Gọi hàm BUS đã cấu hình Transaction ở bài trước
                int maHD = hdBUS.LapHoaDonBanHang(sdt, ten, diachi, email, this.maNVDangNhap, httt, tong, ghiChu, dtMua);

                if (maHD > 0)
                {
                    MessageBox.Show("Thanh toán thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ==========================================
                    // GỌI FORM IN HÓA ĐƠN (CHUẨN RDLC MỚI)
                    // ==========================================
                    // Chỉ truyền duy nhất 1 con số là Mã Hóa Đơn vừa tạo xong sang Form In
                    HoaDon frmIn = new HoaDon(maHD);
                    frmIn.ShowDialog();

                    // Xóa trắng form để đón khách tiếp theo
                    btnLamMoi_Click_1(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập hóa đơn: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //###########################################################################################################
        //--------------------------- TABPAGE2 DANH SÁCH HÓA ĐƠN ---------------------------------------------------------------
        private void LoadDanhSachHoaDon()
        {
            drvDanhSachTraCuu.DataSource = hdBUS.LayDanhSachHoaDon();

            // Làm đẹp cột trên lưới
            if (drvDanhSachTraCuu.Columns.Count > 0)
            {
                drvDanhSachTraCuu.Columns["MaHD"].HeaderText = "Mã HD";
                drvDanhSachTraCuu.Columns["NgayLap"].HeaderText = "Ngày Lập";
                drvDanhSachTraCuu.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; // Format ngày giờ
                drvDanhSachTraCuu.Columns["TenNV"].HeaderText = "Nhân Viên Bán";
                drvDanhSachTraCuu.Columns["TenKH"].HeaderText = "Khách Hàng";
                drvDanhSachTraCuu.Columns["TongTien"].HeaderText = "Tổng Tiền";
                drvDanhSachTraCuu.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Format tiền tệ
                drvDanhSachTraCuu.Columns["HinhThucThanhToan"].HeaderText = "Thanh Toán";
                drvDanhSachTraCuu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                drvDanhSachTraCuu.Columns["GhiChu"].Visible = false;
            }
        }

        private void btnXemCT_Click(object sender, EventArgs e)
        {
            // 1. Dùng CurrentRow để hỗ trợ người dùng chỉ cần click vào 1 ô bất kỳ
            if (drvDanhSachTraCuu.CurrentRow == null || drvDanhSachTraCuu.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 2. Nhặt Mã Hóa Đơn từ cái dòng đang chứa con trỏ chuột
                DataGridViewRow row = drvDanhSachTraCuu.CurrentRow;

                // Đảm bảo tên cột "MaHD" khớp với tên cột trên DataGridView của bạn
                int maHD = Convert.ToInt32(row.Cells["MaHD"].Value);

                // 3. Mở Form In RDLC và truyền đúng 1 con số Mã Hóa Đơn sang
                HoaDon frmIn = new HoaDon(maHD);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi mở hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTimKiemDanhMuc_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập gì chưa
            string tuKhoa = txtTimKiemSDTHD.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập Mã Model để tìm kiếm!", "Thông báo");
                // Nếu trống thì hiện lại toàn bộ danh sách
                LoadDanhSachHoaDon();
                return;
            }

            try
            {
                // 2. Lấy nguồn dữ liệu đang hiển thị trên DataGridView
                DataTable dt = (DataTable)drvDanhSachTraCuu.DataSource;

                if (dt != null)
                {
                    // 3. Sử dụng RowFilter để lọc (Tìm kiếm gần đúng với LIKE)
                    // Vì MaModel là kiểu VARCHAR nên không cần Convert
                    dt.DefaultView.RowFilter = string.Format("SDT LIKE '%{0}%' OR Convert(MaHD, 'System.String') LIKE '%{0}%'", tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnResetDanhMuc_Click(object sender, EventArgs e)
        {
            txtTimKiemSDTHD.Text = "";
            LoadDanhSachHoaDon();
        }


    }
}
