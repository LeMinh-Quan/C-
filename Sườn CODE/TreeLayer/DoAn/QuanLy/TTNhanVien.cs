using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace DoAn.QuanLy
{
    public partial class TTNhanVien : Form
    {
        private NhanVienDTO nvHienTai;
        NhanVienBUS nvBus = new NhanVienBUS();

        public string tenHinhAnh = "";
        private string imagePath = Path.Combine(Application.StartupPath, "Images");
        public TTNhanVien(NhanVienDTO nv)
        {
            InitializeComponent();
            this.nvHienTai = nv;
        }

        private void TTNhanVien_Load(object sender, EventArgs e)
        {
            LoadTT();

        }
        private void LoadTT()
        {
            if (nvHienTai != null)
            {
                txtMaNV.Text = nvHienTai.MaNV.ToString();
                txtHoTen.Text = nvHienTai.HoTen;
                txtDiaChi.Text = nvHienTai.DiaChi;
                txtCCCD.Text = nvHienTai.Cccd;
                txtEmail.Text = nvHienTai.Email;
                txtGioiTinh.Text = nvHienTai.GioiTinh;
                txtSDT.Text = nvHienTai.Sdt;
                txtVaiTro.Text = nvHienTai.VaiTro;
                txtNgaySinh.Value = nvHienTai.NgaySinh;
                txtNgayVaoLam.Text = nvHienTai.NgayVaoLam.ToString("dd/MM/yyyy");

                if (!string.IsNullOrEmpty(nvHienTai.HinhAnh))
                {
                    string existingImgPath = Path.Combine(imagePath, nvHienTai.HinhAnh);
                    this.tenHinhAnh = nvHienTai.HinhAnh;
                    if (File.Exists(existingImgPath))
                    {
                        // Tuyệt chiêu "Nhân bản ảnh" để không bao giờ bị lỗi và không khóa file:
                        // 1. Mở file ảnh gốc lên (tạm gọi là imgTemp)
                        using (Image imgTemp = Image.FromFile(existingImgPath))
                        {
                            // 2. Tạo ra một bản COPY ĐỘC LẬP (new Bitmap) và gán lên PictureBox
                            picHinhAnh.Image = new Bitmap(imgTemp);
                        }
                        // 3. Chạy đến đây, chữ 'using' sẽ tự động tiêu hủy imgTemp và MỞ KHÓA file gốc.
                        // Tấm ảnh trên màn hình bây giờ là bản copy độc lập, không còn liên quan gì đến file vật lý nữa!

                        picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

            }
        }

        private void btnCapNhatAnh_Click(object sender, EventArgs e)
        {
            // Kiểm tra trước khi tạo cho chắc ăn
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Tai len";
            ofd.Filter = "Anh (*.jpg; *.png;*.gif)| *.jpg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tenHinhAnh = Path.GetFileName(ofd.FileName);
                    string des = Path.Combine(imagePath, tenHinhAnh);

                    // Copy file vào thư mục Images
                    if (!File.Exists(des))
                    {
                        File.Copy(ofd.FileName, des);
                    }

                    // ========================================================
                    // SỬA TẠI ĐÂY: Đọc ảnh bằng FileStream để KHÔNG BỊ KHÓA FILE
                    // Đọc trực tiếp từ file 'des' (file đã copy vào dự án)
                    // ========================================================
                    using (FileStream fs = new FileStream(des, FileMode.Open, FileAccess.Read))
                    {
                        picHinhAnh.Image = Image.FromStream(fs);
                    }

                    // Ép ảnh co giãn cho vừa khung (Tùy chọn cho đẹp)
                    picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
        }

        private void btnCapNhatTT_Click(object sender, EventArgs e)
        {
            txtHoTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtCCCD.Enabled = true;
            txtEmail.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
            btnCapNhatAnh.Enabled = true;
            
        }

        private void btnLuuTT_Click(object sender, EventArgs e)
        {
            
            // 2. KIỂM TRA DỮ LIỆU ĐẦU VÀO
            if (txtCCCD.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == "" || txtHoTen.Text == "" ||
                txtSDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
               
                // (Giả sử bạn đặt tên control là dtpNgaySinh)
                DateTime ngaySinh = txtNgaySinh.Value.Date;

                // Tính tuổi theo năm
                int tuoi = DateTime.Now.Year - ngaySinh.Year;

                // Trừ 1 tuổi nếu năm nay chưa tới tháng sinh, hoặc cùng tháng nhưng chưa tới ngày sinh
                if (DateTime.Now.Month < ngaySinh.Month || (DateTime.Now.Month == ngaySinh.Month && DateTime.Now.Day < ngaySinh.Day))
                {
                    tuoi--;
                }

                // Bắt lỗi nếu chưa đủ 18
                if (tuoi < 18)
                {
                    MessageBox.Show("Nhân viên này mới " + tuoi + " tuổi. Yêu cầu phải đủ 18 tuổi trở lên!", "Vi phạm quy định", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgaySinh.Focus();
                    return; // Dừng lại, không cho lưu
                }


                // ==========================================
                // 3. GHI ĐÈ DỮ LIỆU VÀO ĐỐI TƯỢNG (Nếu đã đủ 18 tuổi)
                // ==========================================
                nvHienTai.HoTen = txtHoTen.Text.Trim();
                nvHienTai.DiaChi = txtDiaChi.Text.Trim();
                nvHienTai.Cccd = txtCCCD.Text.Trim();
                nvHienTai.Email = txtEmail.Text.Trim();
                nvHienTai.GioiTinh = txtGioiTinh.Text.Trim();
                nvHienTai.Sdt = txtSDT.Text.Trim();
                nvHienTai.NgaySinh = ngaySinh; // Gán ngày sinh hợp lệ vào biến
                nvHienTai.HinhAnh = this.tenHinhAnh;

                // (Biến HinhAnh đã được xử lý riêng ở nút "Cập nhật ảnh")

                // ==========================================
                // 4. GỌI TẦNG BUS ĐỂ LƯU XUỐNG DATABASE
                // ==========================================
                if (nvBus.CapNhatThongTin(nvHienTai))
                {
                    MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTT();

                    txtHoTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtCCCD.Enabled = false;
                    txtEmail.Enabled = false;
                    txtGioiTinh.Enabled = false;
                    txtNgaySinh.Enabled = false;
                    txtSDT.Enabled = false;
                    btnCapNhatAnh.Enabled = false;
                    // Khóa lại các ô textbox sau khi lưu xong để tránh sửa nhầm

                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Thì từ chối, không cho ký tự đó hiện lên TextBox
                e.Handled = true;
            }
        }
    }
}
