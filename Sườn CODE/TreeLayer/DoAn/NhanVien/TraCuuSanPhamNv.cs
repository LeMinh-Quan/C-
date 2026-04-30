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
using DoAn.QuanLy;
using DTO;

namespace DoAn.NhanVien
{
    public partial class TraCuuSanPhamNv : Form
    {
        SanPhamBUS sp = new SanPhamBUS();
        HangSanXuatBUS hangBUS = new HangSanXuatBUS();
        public TraCuuSanPhamNv()
        {
            InitializeComponent();
        }

        private void TraCuuSanPhamNv_Load(object sender, EventArgs e)
        {
            danhSachSP();
        }

        private void drvDanhSachLoaiSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void danhSachSP()
        {


            // 1. Gán nguồn dữ liệu
            DataTable dt = sp.LayToanBoSanPham();
            drvDanhSachLoaiSP.DataSource = dt;

            // 2. Đổi tên tiêu đề cột (HeaderText) cho thân thiện


            drvDanhSachLoaiSP.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            drvDanhSachLoaiSP.Columns["TenHang"].HeaderText = "Hãng Sản Xuất"; // Cột lấy từ phép JOIN
            drvDanhSachLoaiSP.Columns["PhanLoai"].HeaderText = "Phân Loại";
            drvDanhSachLoaiSP.Columns["GiaBan"].HeaderText = "Giá Bán (VNĐ)";
            drvDanhSachLoaiSP.Columns["TrangThai"].HeaderText = "Trạng Thái";
            drvDanhSachLoaiSP.Columns["ThoiGianBaoHanh"].HeaderText = "Thời Gian Bảo Hành ";
            drvDanhSachLoaiSP.Columns["TongSoLuong"].HeaderText = "Số lượng tồn ";

            // 3. Ẩn các cột cấu hình chi tiết để lưới không bị quá dài
            // (Khi nào click vào dòng mới hiện chi tiết sau)
            drvDanhSachLoaiSP.Columns["CPU"].Visible = false;
            drvDanhSachLoaiSP.Columns["RAM"].Visible = false;
            drvDanhSachLoaiSP.Columns["OCung"].Visible = false;
            drvDanhSachLoaiSP.Columns["VGA"].Visible = false;
            drvDanhSachLoaiSP.Columns["ManHinh"].Visible = false;
            drvDanhSachLoaiSP.Columns["MaHang"].Visible = false;
            drvDanhSachLoaiSP.Columns["HinhAnh"].Visible = false;
            drvDanhSachLoaiSP.Columns["MoTa"].Visible = false;

            // 4. Định dạng tiền tệ cho cột Giá Bán (Ví dụ: 15.000.000)
            drvDanhSachLoaiSP.Columns["GiaBan"].DefaultCellStyle.Format = "N0";



        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng có đang chọn dòng nào không
            if (drvDanhSachLoaiSP.CurrentRow == null || drvDanhSachLoaiSP.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Lấy nguyên cái dòng đang được chọn
            DataGridViewRow row = drvDanhSachLoaiSP.CurrentRow;

            try
            {
                // 3. Đóng gói dữ liệu từ dòng đó vào 1 cục DTO
                SanPhamDTO spChon = new SanPhamDTO();

                spChon.MaModel = row.Cells["MaModel"].Value?.ToString() ?? "";
                spChon.TenSanPham = row.Cells["TenSanPham"].Value?.ToString() ?? "";
                spChon.PhanLoai = row.Cells["PhanLoai"].Value?.ToString() ?? "";
                spChon.Cpu = row.Cells["CPU"].Value?.ToString() ?? "";
                spChon.Ram = row.Cells["RAM"].Value?.ToString() ?? "";
                spChon.OCung = row.Cells["OCung"].Value?.ToString() ?? "";
                spChon.Vga = row.Cells["VGA"].Value?.ToString() ?? "";
                spChon.ManHinh = row.Cells["ManHinh"].Value?.ToString() ?? "";
                spChon.TrangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";
                spChon.MoTa = row.Cells["MoTa"].Value?.ToString() ?? "";
                spChon.HinhAnh = row.Cells["HinhAnh"].Value?.ToString() ?? "";


                if (row.Cells["GiaBan"].Value != DBNull.Value)
                    spChon.GiaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                if (row.Cells["ThoiGianBaoHanh"].Value != DBNull.Value)
                    spChon.ThoiGianBaoHanh = Convert.ToInt32(row.Cells["ThoiGianBaoHanh"].Value);

                // 4. Lấy Tên Hãng từ lưới (Cột này sinh ra nhờ câu lệnh JOIN hôm trước)
                string tenHangSX = row.Cells["TenHang"].Value?.ToString() ?? "Không xác định";

                // 5. Gọi Form Chi Tiết, ném cục DTO và Tên Hãng sang đó
                ThongTinCTSanPham frmChiTiet = new ThongTinCTSanPham(spChon, tenHangSX);

                // 6. Mở Form lên (Dùng ShowDialog để người dùng phải đóng Form này mới quay lại lưới được)
                frmChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở thông tin sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập gì chưa
            string tuKhoa = txtTimKiemModel.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập Mã Model để tìm kiếm!", "Thông báo");
                // Nếu trống thì hiện lại toàn bộ danh sách
                danhSachSP();
                return;
            }

            try
            {
                // 2. Lấy nguồn dữ liệu đang hiển thị trên DataGridView
                DataTable dt = (DataTable)drvDanhSachLoaiSP.DataSource;

                if (dt != null)
                {
                    // 3. Sử dụng RowFilter để lọc (Tìm kiếm gần đúng với LIKE)
                    // Vì MaModel là kiểu VARCHAR nên không cần Convert
                    dt.DefaultView.RowFilter = string.Format("MaModel LIKE '%{0}%'", tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiemModel.Text = "";
            danhSachSP();
        }

        private void drvDanhSachLoaiSP_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in drvDanhSachLoaiSP.Rows)
            {
                if (row.Cells["TrangThai"].Value?.ToString() == "Ngừng bán")
                {
                    row.DefaultCellStyle.BackColor = Color.OrangeRed; //

                }
            }
        }
    }
}
