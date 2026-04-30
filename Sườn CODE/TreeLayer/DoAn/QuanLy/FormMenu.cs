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
using DTO;

namespace DoAn.QuanLy
{
    public partial class FormMenu : Form
    {

        NhanVienDTO nvDTO;
        NhanVienBUS nvBUS = new NhanVienBUS();
        public FormMenu(NhanVienDTO nv)
        {
            InitializeComponent();
            this.nvDTO = nv;
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
                // Mở Form Thông tin nhân viên
                OpenChildForm(new TTNhanVien(nvDTO));
            

        }


//===========================================================================================================
//-----------------------NHÚNG FORM------------------------------------------------------------
        private Form activeForm = null;
        public void OpenChildForm(Form childForm)
        {
            // 1. Kiểm tra và đóng Form cũ để giải phóng tài nguyên
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose(); // Giải phóng hoàn toàn bộ nhớ
            }

            activeForm = childForm;

            // 2. Cấu hình Form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // 3. Quan trọng: Xóa sạch các control cũ đang có trong Panel
            pnlMain.Controls.Clear();

            // 4. Thêm Form con vào Panel
            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;

            // 5. Hiển thị
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyNhanVien(nvDTO.MaNV));
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLySP());
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo Form Nhập Hàng và truyền Mã nhân viên từ nvDTO vào
            NhapHang frmLapDon = new NhapHang(nvDTO.MaNV);

            // 2. Gọi hàm nhúng Form con và truyền chính cái Form vừa tạo vào trong ngoặc
            OpenChildForm(frmLapDon);

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaoCaoThongKe());
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
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLHangLoi(nvDTO.MaNV,nvDTO.HoTen));
        }

        //--------------- ĐĂNG XUẤT----------------------------
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Khởi động lại toàn bộ phần mềm (Mọi Form sẽ bị dọn sạch, cực kỳ an toàn)
                Application.Restart();
            }
        }

        private void FormMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Bấm nút Đăng xuất -> Tìm form Đăng nhập đang ẩn và mở nó lên lại
            Form frmLogin = Application.OpenForms["DangNhap"];
            if (frmLogin != null)
            {
                frmLogin.Show();
            }
        }

    }
}
