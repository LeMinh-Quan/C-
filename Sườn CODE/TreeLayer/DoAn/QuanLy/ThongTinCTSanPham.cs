using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DTO;


namespace DoAn.QuanLy
{
    public partial class ThongTinCTSanPham : Form
    {
        private SanPhamDTO spDTO;
        private string tenHangsx;
        private string imagePath = Path.Combine(Application.StartupPath, "ImagesSP");
        public ThongTinCTSanPham( SanPhamDTO sp, string tenHang)
        {
            InitializeComponent();
            this.spDTO = sp;
            this.tenHangsx = tenHang;
        }

        private void ThongTinCTSanPham_Load_1(object sender, EventArgs e)
        {
            if (spDTO != null)
            {
                txtMaModel.Text = spDTO.MaModel;
                txtTenSP.Text = spDTO.TenSanPham;
                txtHangSX.Text = tenHangsx;
                txtPhanLoai.Text = spDTO.PhanLoai;
                txtTrangThai.Text = spDTO.TrangThai;

                txtRAM.Text = spDTO.Ram;
                txtCPU.Text = spDTO.Cpu;
                txtOCung.Text = spDTO.OCung;
                txtVGA.Text = spDTO.Vga;
                txtManHinh.Text = spDTO.ManHinh;
                txtGiaBan.Text = spDTO.GiaBan.ToString("N0") + " VNĐ";
                txtBaoHanh.Text = spDTO.ThoiGianBaoHanh.ToString() + " Tháng";
                txtMoTa.Text = spDTO.MoTa;

                if (!string.IsNullOrEmpty(spDTO.HinhAnh))
                {
                    string existingImgPath = Path.Combine(imagePath, spDTO.HinhAnh);

                    if (File.Exists(existingImgPath))
                    {
                        // Dùng FileStream đọc ảnh để không bị khóa file
                        using (FileStream fs = new FileStream(existingImgPath, FileMode.Open, FileAccess.Read))
                        {
                            pitHinh.Image = Image.FromStream(fs);
                        }
                        pitHinh.SizeMode = PictureBoxSizeMode.Zoom; // Căn ảnh vừa khung
                    }
                }
            }

        }
        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
