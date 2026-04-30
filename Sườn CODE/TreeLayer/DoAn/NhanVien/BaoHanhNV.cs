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
using DoAn.HeThong;
using DTO;

namespace DoAn.NhanVien
{
    public partial class BaoHanhNV : Form
    {
        HoaDonBUS hdBUS = new HoaDonBUS();
        BaoHanhAMBUS bhBUS = new BaoHanhAMBUS();
        private int maNV;
        private string tenNV;
        public BaoHanhNV(int nv, string ten)
        {
            InitializeComponent();
            this.maNV = nv;
            this.tenNV = ten;
        }

        private void BaoHanhNV_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            LoadDanhSachBaoHanh();
            
        }
        //########################################################################################################
        //------------------------------------------- TABPAGE DANH SÁCH HÓA ĐƠN ----------------------------------
        //########################################################################################################

        private void LoadDanhSachHoaDon()
        {
            // Gọi hàm chi tiết mới
            drvDanhSachTraCuu.DataSource = hdBUS.LayDanhSachHoaDonCT();

            if (drvDanhSachTraCuu.Columns.Count > 0)
            {
                drvDanhSachTraCuu.Columns["MaHD"].HeaderText = "Mã HD";
                drvDanhSachTraCuu.Columns["MaModel"].HeaderText = "Mã Model";
                drvDanhSachTraCuu.Columns["MaSerial"].HeaderText = "Mã Serial"; // Cột mới
                drvDanhSachTraCuu.Columns["TenSanPham"].HeaderText = "Tên Máy"; // Cột mới
                drvDanhSachTraCuu.Columns["NgayLap"].HeaderText = "Ngày Bán";
                drvDanhSachTraCuu.Columns["TenNV"].HeaderText = "Nhân Viên";
                drvDanhSachTraCuu.Columns["TenKH"].HeaderText = "Khách Hàng";
                drvDanhSachTraCuu.Columns["TrangThai"].HeaderText = "Trạng Thái";
                drvDanhSachTraCuu.Columns["NgayHetHanBaoHanh"].HeaderText = "Hạn Bảo Hành";
                drvDanhSachTraCuu.Columns["NgayHetHanBaoHanh"].DefaultCellStyle.Format = "dd/MM/yyyy";


                // Đặt dòng này dưới chỗ khai báo cột Khách Hàng
                drvDanhSachTraCuu.Columns["SDT"].HeaderText = "Số Điện Thoại";
                //drvDanhSachTraCuu.Columns["DonGiaBan"].HeaderText = "Giá Bán";
                //drvDanhSachTraCuu.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0";
                //drvDanhSachTraCuu.Columns["DonGiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                drvDanhSachTraCuu.Columns["GhiChu"].Visible = false;
                drvDanhSachTraCuu.Columns["TenSanPham"].Visible = false;
                drvDanhSachTraCuu.Columns["DonGiaBan"].Visible = false;
                drvDanhSachTraCuu.Columns["HinhThucThanhToan"].Visible = false;
                drvDanhSachTraCuu.Columns["MaModel"].Visible = false;
                drvDanhSachTraCuu.Columns["MaHD"].Width = 50;


            }
        }

        private void btnTimKiemHoaDon_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập gì chưa
            string tuKhoa = txtTimKiem.Text.Trim();

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
                    dt.DefaultView.RowFilter = string.Format("SDT LIKE '%{0}%' OR Convert(MaSerial, 'System.String') LIKE '%{0}%'", tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnBaoHanh_Click(object sender, EventArgs e)
        {

            // 1. Kiểm tra xem nhân viên đã bôi đen dòng nào chưa
            if (drvDanhSachTraCuu.CurrentRow == null || drvDanhSachTraCuu.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng bôi đen một máy trên lưới trước khi chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 2. Lấy ra dòng đang được bôi đen
                DataGridViewRow row = drvDanhSachTraCuu.CurrentRow;

                // ==========================================================
                // MỚI THÊM: KIỂM TRA TRẠNG THÁI TRƯỚC KHI CHO PHÉP LẬP PHIẾU
                // ==========================================================
                string trangThai = row.Cells["TrangThai"].Value?.ToString();
                if (trangThai == "Đang bảo hành" || trangThai == "Hàng lỗi")
                {
                    MessageBox.Show("Máy này đang trong quá trình bảo hành và đang nằm tại cửa hàng.\nKhông thể lập thêm phiếu mới cho thiết bị này!",
                                    "Cảnh báo trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng tại đây, không chạy code gán dữ liệu và chuyển tab bên dưới
                }
                // ==========================================================

                // 3. Gán dữ liệu sang các Label bên Tab Lập Phiếu
                lblMaHD.Text = row.Cells["MaHD"].Value?.ToString() ?? "---";
                lblTenSPValue.Text = row.Cells["TenSanPham"].Value?.ToString() ?? "---";
                lblMaModelValue.Text = row.Cells["MaModel"].Value?.ToString() ?? "---";
                lblMaSerial.Text = row.Cells["MaSerial"].Value?.ToString() ?? "---";

                lblKhachHangValue.Text = row.Cells["TenKH"].Value?.ToString() ?? "---";
                lblSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "---";

                if (row.Cells["NgayHetHanBaoHanh"].Value != DBNull.Value)
                {
                    DateTime hanBH = Convert.ToDateTime(row.Cells["NgayHetHanBaoHanh"].Value);
                    lblHanBHValue.Text = hanBH.ToString("dd/MM/yyyy");
                }
                else
                {
                    lblHanBHValue.Text = "--/--/----";
                }

                // 4. Chuyển Tab và Focus
                page.SelectedTab = tabPage2;
                txtTinhTrangLoi.Focus();
                dtpNgayTiepNhan.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn máy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadDanhSachHoaDon();
        }

        private void drvDanhSachTraCuu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Đảm bảo đang kiểm tra đúng cột "TrangThai" (bạn nhớ đổi tên cột nếu trên lưới đặt khác nhé)
            if (drvDanhSachTraCuu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();

                if (trangThai == "Đang bảo hành")
                {
                    // Màu đỏ nhạt (MistyRose hoặc LightPink) giúp nhân viên nhận diện ngay lập tức
                    drvDanhSachTraCuu.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                    drvDanhSachTraCuu.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }

                if( trangThai == "Bảo hành hoàn tất")
                {
                    drvDanhSachTraCuu.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    drvDanhSachTraCuu.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        //####################################################################################################
        //------------------------------------------- TABPAGE  LẬP PHIẾU -------------------------------------
        //########################################################################################################

        private void cboPhuongAn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn máy chưa
            string serial = lblMaSerial.Text;
            if (serial == "---" || string.IsNullOrEmpty(serial))
            {
                MessageBox.Show("Vui lòng chọn một máy từ danh sách để lập phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Thu thập thông tin từ Form
                string tenKH = lblKhachHangValue.Text.Trim();
                string hanBH = lblHanBHValue.Text.Trim();
                string maModel = lblMaModelValue.Text.Trim();
                string sdt = lblSDT.Text.Trim();

                string phuongAn = cboPhuongAn.Text; // "Xử lý tại chỗ" hoặc "Gửi trả NCC"
                string loi = txtTinhTrangLoi.Text.Trim();
                string vatLy = txtTinhTrangVL.Text.Trim();
                string ghiChu = txtGhiChu.Text.Trim();
                string tenSp = lblTenSPValue.Text.Trim();

                // =========================================================
                // MỚI: XỬ LÝ LOGIC TRẠNG THÁI THEO PHƯƠNG ÁN
                // =========================================================
                string trangThai = "";

                // Nếu chọn "Xử lý tại chỗ" (Index 0) -> Trạng thái là "Hoàn tất" (sửa xong đưa khách luôn)
                // Nếu chọn "Gửi trả NCC" (Index 1) -> Trạng thái là "Chờ gửi NCC" (lưu kho chờ gom đơn)
                if (cboPhuongAn.SelectedIndex == 0)
                {
                    trangThai = "Hoàn tất";
                }
                else
                {
                    trangThai = "Chờ gửi NCC";
                }

                // 3. Gọi BUS lưu xuống Database
                // (Hàm này giờ sẽ tự động chuyển trạng thái của máy trong kho thành "Đang bảo hành")
                int maPhieuMoi = bhBUS.LapPhieuBaoHanh1(serial, this.maNV, loi, vatLy, phuongAn, trangThai, ghiChu);

                if (maPhieuMoi > 0)
                {
                    MessageBox.Show("Lập phiếu bảo hành thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. MỞ FORM IN PHIẾU BẢO HÀNH
                    string ngayLap = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    // Truyền dữ liệu sang Form HoaDonBH
                    HoaDonBH frmIn = new HoaDonBH(maPhieuMoi);
                    HDTinhTrangBaoHanh frmInTT = new HDTinhTrangBaoHanh(maPhieuMoi);
                    frmIn.ShowDialog();
                    frmInTT.ShowDialog();

                    // 5. Load lại danh sách và Reset Form
                    LoadDanhSachHoaDon();
                    LoadDanhSachBaoHanh();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtGhiChu.Text = "";
            txtTinhTrangLoi.Text = "";
            txtTinhTrangVL.Text = "";
            cboPhuongAn.SelectedIndex = -1;
            dtpNgayTiepNhan.Value = DateTime.Now;
            lblHanBHValue.Text = "";
            lblKhachHangValue.Text = "";
            lblMaHD.Text = "";
            lblMaModelValue.Text = "";
            lblMaSerial.Text = "";
            lblSDT.Text = "";
            lblTenSPValue.Text = "";
        }


       
        //####################################################################################################
        //------------------------------------------- TABPAGE3  DANH SÁCH BẢO HÀNH-----------------------------
        //########################################################################################################


        private void LoadDanhSachBaoHanh()
        {
            try
            {
                // 1. Gọi BUS để lấy dữ liệu đổ vào DataGridView
                dgvDanhSachBH.DataSource = bhBUS.LayDanhSachBH();

                // 2. Làm đẹp các cột trên DataGridView
                if (dgvDanhSachBH.Columns.Count > 0)
                {
                    dgvDanhSachBH.Columns["MaPhieuBH"].HeaderText = "Mã Phiếu";

                    dgvDanhSachBH.Columns["MaSerial"].HeaderText = "Mã Serial";

                    dgvDanhSachBH.Columns["TenSanPham"].HeaderText = "Tên Máy";

                    dgvDanhSachBH.Columns["NgayTiepNhan"].HeaderText = "Ngày Nhận";
                    dgvDanhSachBH.Columns["NgayTiepNhan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                    dgvDanhSachBH.Columns["NgayTra"].HeaderText = "Ngày Trả";
                    dgvDanhSachBH.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvDanhSachBH.Columns["TenNV"].HeaderText = "Người Tiếp Nhận";

                    dgvDanhSachBH.Columns["TinhTrangLoi"].HeaderText = "Tình Trạng Lỗi";
                    dgvDanhSachBH.Columns["PhuongAnDeXuat"].HeaderText = "Phương Án";

                    dgvDanhSachBH.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    dgvDanhSachBH.Columns["GhiChu"].HeaderText = "Ghi Chú";

                    // Đặt chế độ tự động giãn cột cuối cùng hoặc giãn đều
                    dgvDanhSachBH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bảo hành: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã bôi đen dòng nào chưa
            if (dgvDanhSachBH.CurrentRow == null || dgvDanhSachBH.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu Bảo Hành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 


            // 2. Lấy Mã Phiếu từ dòng đang chọn
            DataGridViewRow row = dgvDanhSachBH.CurrentRow;

            // Lưu ý: Đổi "MaPhieu" thành đúng tên cột Mã Phiếu trong DataGridView của bạn
            int maPhieu = Convert.ToInt32(row.Cells["MaPhieuBH"].Value);

            // Kiểm tra nếu phiếu đã hoàn tất rồi thì không cho làm nữa
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            if (trangThai == "Hoàn tất")
            {
                MessageBox.Show("Phiếu này đã bàn giao cho khách rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if( trangThai == "Chờ gửi NCC")
            {
                MessageBox.Show("Phiếu này chưa được gửi đến nhà Cung Cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (trangThai == "Đang ở NCC")
            {
                MessageBox.Show("Phiếu này đang ở " +
                    "nhà Cung Cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // 3. Khởi tạo Form HoanTatBH và ném Mã Phiếu sang
            HoanTatBH frmPopup = new HoanTatBH(maPhieu,this.tenNV);
            frmPopup.ShowDialog();
            LoadDanhSachBaoHanh();
            LoadDanhSachHoaDon();


        }

        private void dgvDanhSachBH_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Đảm bảo lưới có dữ liệu và đang kiểm tra cột "TrangThai"
            // Lưu ý: Đổi "TrangThai" thành đúng Name của cột trạng thái trên lưới của bạn nếu nó khác
            if (dgvDanhSachBH.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();

                // 2. Nếu trạng thái là "Hoàn tất" -> Tô màu nguyên dòng
                if (trangThai == "Hoàn tất")
                {
                    // Tô nền màu xanh lá nhạt cho dễ nhìn chữ đen
                    dgvDanhSachBH.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;

                }
                else
                {
                    // Trả về màu mặc định cho các dòng chưa hoàn tất (để tránh bị lỗi tô màu lộn xộn khi cuộn chuột)
                    dgvDanhSachBH.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    dgvDanhSachBH.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnTimDSBH_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập gì chưa
            string tuKhoa = txtTimKiemDSBH.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập Mã Model để tìm kiếm!", "Thông báo");
                // Nếu trống thì hiện lại toàn bộ danh sách
                LoadDanhSachBaoHanh();
                return;
            }

            try
            {
                // 2. Lấy nguồn dữ liệu đang hiển thị trên DataGridView
                DataTable dt = (DataTable)dgvDanhSachBH.DataSource;

                if (dt != null)
                {
                    // 3. Sử dụng RowFilter để lọc (Tìm kiếm gần đúng với LIKE)
                    // Vì MaModel là kiểu VARCHAR nên không cần Convert
                    string filter = string.Format("CONVERT(MaPhieuBH, 'System.String') LIKE '%{0}%' OR MaSerial LIKE '%{0}%'", tuKhoa);
                    dt.DefaultView.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
        private void btnLamMoiDSBH_Click(object sender, EventArgs e)
        {
            txtTimKiemDSBH.Text = "";
            LoadDanhSachBaoHanh();
            LoadDanhSachHoaDon();

        }
        private void dtpNgayTiepNhan_ValueChanged(object sender, EventArgs e)
        {

        }


    }
} 
  
