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
using DoAn.QuanLy;
using DTO;

namespace DoAn
{
    public partial class DangNhap : Form
    {
        NhanVienBUS nv = new NhanVienBUS();
        public DangNhap()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }



        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string user = txtUsername.Text;
                string pass = txtPassword.Text;
                if (user == "" || pass == "")
                {
                    DialogResult tb = MessageBox.Show("Vui lòng nhập dữ liệu đầy đủ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (tb == DialogResult.OK)
                    {
                        Application.DoEvents();
                    }
                    return;
                }


                NhanVienDTO nvDangNhap = nv.DangNhap(user, pass);

                // Nếu nvDangNhap khác null nghĩa là có người dùng (đăng nhập đúng)
                if (nvDangNhap != null)
                {
                    // Nhờ DAL đã đóng gói sẵn, giờ ta chỉ cần lôi thuộc tính VaiTro ra dùng
                    if (nvDangNhap.VaiTro == "Quản lý")
                    {
                        FormMenu f = new FormMenu(nvDangNhap);
                        f.Show();
                        this.Hide(); // Ẩn form login
                    }
                    else if (nvDangNhap.VaiTro == "Nhân viên")
                    {
                        FormMainNV f = new FormMainNV(nvDangNhap);
                        f.Show();
                        this.Hide(); // Ẩn form login
                    }
                    else
                    {
                        MessageBox.Show("Vai trò không hợp lê!");

                    }
                        txtPassword.Text = "";
                    
                    
                }
                else
                {
                    lblError.Text = "Mật khẩu hoặc Tài khoản không đúng!";
                    txtPassword.Focus();
                }
                // ... (Toàn bộ code kiểm tra tài khoản, gọi DAL/BUS, tạo NhanVienDTO, và gọi FormMenu của bạn để hết ở đây) ...
            }
            catch (Exception ex)
            {
                // ĐÂY MỚI LÀ LƯỚI BẮT CÁ CHÍNH XÁC NHẤT!
                MessageBox.Show("Sập ở Form Đăng Nhập rồi!\n\nChi tiết lỗi: " + ex.Message, "Bắt Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
