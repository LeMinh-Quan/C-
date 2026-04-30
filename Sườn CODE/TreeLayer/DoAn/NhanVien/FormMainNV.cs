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
    public partial class FormMainNV : Form
    {
        NhanVienDTO nvDTO;
        NhanVienBUS nvBUS = new NhanVienBUS();
        public FormMainNV(NhanVienDTO nv)
        {
            InitializeComponent();
            this.nvDTO = nv;
        }

        private void FormMainNV_Load(object sender, EventArgs e)
        {
            // Mở Form Thông tin nhân viên
            OpenChildForm(new TTNhanVien(nvDTO));

        }
        public void OpenChildForm(Form childForm)
        {
            pnlMain.Controls.Clear(); // xóa form cũ

            childForm.TopLevel = false; // bắt buộc
            childForm.FormBorderStyle = FormBorderStyle.None; // bỏ viền
            childForm.Dock = DockStyle.Fill; // full panel

            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;

            childForm.Show();
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kéo toàn bộ danh sách mới nhất từ Database lên
                DataTable dt = nvBUS.LayDanhSachNhanVien();

                // 2. Dùng lệnh Select (mà ta đã học ở bài đếm số lượng) để tìm chính mình
                DataRow[] rows = dt.Select("MaNV = " + nvDTO.MaNV);

                // 3. Nếu tìm thấy, cập nhật lại toàn bộ cục RAM (nvDTO) bằng dữ liệu mới nhất
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    nvDTO.HoTen = row["HoTen"].ToString();
                    nvDTO.Cccd = row["CCCD"].ToString();
                    nvDTO.Email = row["Email"].ToString();
                    nvDTO.Sdt = row["SDT"].ToString();
                    nvDTO.DiaChi = row["DiaChi"].ToString();
                    nvDTO.GioiTinh = row["GioiTinh"].ToString();
                    nvDTO.TrangThai = row["TrangThai"].ToString();
                    nvDTO.VaiTro = row["VaiTro"].ToString();

                    // Xử lý Ngày tháng an toàn
                    if (row["NgaySinh"] != DBNull.Value)
                        nvDTO.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
                    if (row["NgayVaoLam"] != DBNull.Value)
                        nvDTO.NgayVaoLam = Convert.ToDateTime(row["NgayVaoLam"]);

                    // Xử lý cập nhật Ảnh
                    nvDTO.HinhAnh = row["HinhAnh"]?.ToString() ?? "";
                }

                // 4. Mở Form với dữ liệu đã được làm mới tinh tươm
                OpenChildForm(new TTNhanVien(nvDTO));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnTraCuuSP_Click(object sender, EventArgs e)
        {
            OpenChildForm( new TraCuuSanPhamNv());
        }

        private void btnBaoHanh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaoHanhNV(nvDTO.MaNV, nvDTO.HoTen));
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm( new lapHoaDonNV(nvDTO.MaNV, nvDTO.HoTen));
        }

        private void FormMainNV_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Bấm nút Đăng xuất -> Tìm form Đăng nhập đang ẩn và mở nó lên lại
            Form frmLogin = Application.OpenForms["DangNhap"];
            if (frmLogin != null)
            {
                frmLogin.Show();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Khởi động lại toàn bộ phần mềm (Mọi Form sẽ bị dọn sạch, cực kỳ an toàn)
                Application.Restart();
            }
        }
    }
}
