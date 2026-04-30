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
using DoAn.NhanVien;

namespace DoAn.QuanLy
{
    public partial class TTCTHoaDon : Form
    {
        private ThongKeBUS  tkBUS = new ThongKeBUS();
        private int maHD_Current;

        public TTCTHoaDon(int maHD)
        {
            InitializeComponent();
            this.maHD_Current = maHD;
        }

        private void TTCTHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTietSanPham();
            LoadThongTinChung();

        }
        // HÀM 1: ĐỔ DỮ LIỆU VÀO CÁC LABEL (Thông tin chung & Khách hàng)
        private void LoadThongTinChung()
        {
            DataTable dtChung = tkBUS.LayThongTinChungHoaDon(maHD_Current);

            if (dtChung != null && dtChung.Rows.Count > 0)
            {
                DataRow r = dtChung.Rows[0]; // Lấy dòng đầu tiên (vì Mã HĐ là duy nhất)

                // 1. Nhóm Thông tin Hóa Đơn
                lblMaHD.Text = r["MaHD"].ToString();

                // Định dạng ngày giờ chuẩn Việt Nam
                if (r["NgayLap"] != DBNull.Value)
                    lblNgayLap.Text = Convert.ToDateTime(r["NgayLap"]).ToString("dd/MM/yyyy HH:mm");

                lblNhanVien.Text = r["TenNhanVien"].ToString();
                lblThanhToan.Text = r["HinhThucThanhToan"].ToString();

                // 2. Nhóm Thông tin Khách Hàng
                // Nếu khách lẻ không lưu tên thì để chữ "Khách vãng lai"
                lblTenKhach.Text = r["TenKhachHang"] != DBNull.Value ? r["TenKhachHang"].ToString() : "Khách lẻ (Không lưu tên)";
                lblSDTKhach.Text = r["SDT_Khach"] != DBNull.Value ? r["SDT_Khach"].ToString() : "Trống";

                lblTrangThai.Text = r["TrangThai"].ToString();

                // Đổi màu Label Trạng thái cho sinh động
                if (lblTrangThai.Text == "Hoàn thành")
                    lblTrangThai.ForeColor = Color.Green;
                else
                    lblTrangThai.ForeColor = Color.Orange;

                // 3. Hiển thị Tổng tiền ở góc phải dưới
                if (r["TongTien"] != DBNull.Value)
                {
                    decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                    lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
        }

        // HÀM 2: ĐỔ DỮ LIỆU VÀO LƯỚI (Danh sách sản phẩm)
        private void LoadChiTietSanPham()
        {
            DataTable dtChiTiet = tkBUS.LayDanhSachChiTietHoaDon(maHD_Current);
            dgvChiTietHoaDon.DataSource = dtChiTiet;

            // Định dạng lưới
            if (dgvChiTietHoaDon.Columns.Count > 0)
            {
                dgvChiTietHoaDon.Columns["MaSerial"].HeaderText = "Mã Serial (IMEI)";
                dgvChiTietHoaDon.Columns["TenSanPham"].HeaderText = "Tên Dòng Laptop";
                dgvChiTietHoaDon.Columns["PhanLoai"].HeaderText = "Phân Loại";

                dgvChiTietHoaDon.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Căn chỉnh cột số thứ tự nếu cần (tùy chọn)
                dgvChiTietHoaDon.RowHeadersVisible = false;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
