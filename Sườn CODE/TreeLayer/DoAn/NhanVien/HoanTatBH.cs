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
    public partial class HoanTatBH : Form
    {
        BaoHanhAMBUS bhBUS = new BaoHanhAMBUS();
        private string _tenNhanVien;
        private int maPhieuBH;
        public HoanTatBH(int maPhieu, string tenNhanVien)
        {
            InitializeComponent();
            this.maPhieuBH=maPhieu;
            this._tenNhanVien = tenNhanVien;
        }

        private void HoanTatBH_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieu();
        }
        private void LoadThongTinPhieu()
        {
            try {  
                DataTable dt = bhBUS.ChiTietPhieuBHTraKhach(this.maPhieuBH);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // ==========================================
                    // GÁN DỮ LIỆU PHẦN 1: THÔNG TIN CHUNG
                    // ==========================================
                    txtMaPhieu.Text = "PBH-" + maPhieuBH.ToString("D5");
                    MessageBox.Show("ten khach hang la:", row["KhachHang"].ToString());
                    txtKhachHang.Text = row["KhachHang"].ToString();

                    // Format ngày giờ cho đẹp
                    if (row["NgayTiepNhan"] != DBNull.Value)
                    {
                        txtNgayNhan.Text = Convert.ToDateTime(row["NgayTiepNhan"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    txtSDT.Text = row["SDT"].ToString();

                    // ==========================================
                    // GÁN DỮ LIỆU PHẦN 2: THÔNG TIN THIẾT BỊ
                    // ==========================================
                    txtTenSP.Text = row["TenSanPham"].ToString();
                    txtSerial.Text = row["MaSerial"].ToString();
                    txtLoiBanDau.Text = row["TinhTrangLoi"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho mã phiếu này!", "Lỗi truy xuất", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu lên Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtChiPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chặn tất cả các ký tự KHÔNG PHẢI là: Phím điều khiển, Số, và Dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bắt giữ lại, không cho in ra màn hình
            }
        }

        private void txtChiPhi_TextChanged(object sender, EventArgs e)
        {
                // Lấy chuỗi hiện tại và xóa hết dấu chấm/phẩy cũ đi để thành số nguyên thủy
                string rawText = txtChiPhi.Text.Replace(".", "").Replace(",", "");

                if (!string.IsNullOrEmpty(rawText))
                {
                    // Ép kiểu sang decimal
                    decimal money = Convert.ToDecimal(rawText);

                    // Format lại thành chuẩn có dấu phẩy hàng nghìn, rồi ép nó thành dấu chấm (chuẩn VN)
                    txtChiPhi.Text = money.ToString("#,##0").Replace(',', '.');

                    // Đưa con trỏ chuột về vị trí cuối cùng để gõ tiếp mượt mà
                    txtChiPhi.SelectionStart = txtChiPhi.Text.Length;
                }
            
        }
        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            // 1. Validate bắt buộc
            if (!chkDaKiemTra.Checked)
            {
                MessageBox.Show("Vui lòng xác nhận khách hàng đã kiểm tra máy hoạt động bình thường!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy dữ liệu từ giao diện
                string ghiChu = txtGhiChuTra.Text.Trim();
                string serial = txtSerial.Text.Trim();

                // MỚI: Lấy kết quả từ hãng (Bạn kiểm tra lại xem tên textbox "Chi tiết xử lý" trên form có đúng là txtChiTietXuLy không nhé, nếu không thì sửa lại tên cho khớp)
                string ketQua = txtKetQua.Text.Trim();

                // MỚI: Ép kiểu an toàn cho Chi Phí. Nếu để trống thì mặc định là 0.
                decimal chiPhi = 0; // Mặc định là 0 đồng
                string chuoiTien = txtChiPhi.Text.Trim();

                if (!string.IsNullOrEmpty(chuoiTien))
                {
                    // Lột sạch các dấu chấm, dấu phẩy người dùng nhập vào
                    chuoiTien = chuoiTien.Replace(".", "").Replace(",", "");

                    // Bây giờ chuỗi "1.000.000" đã thành "1000000", ép kiểu an toàn 100%
                    chiPhi = Convert.ToDecimal(chuoiTien);
                }
                // 3. Gọi BUS xử lý (Đã truyền đủ 5 tham số)
                if (bhBUS.XacNhanTraMay(this.maPhieuBH, ghiChu, serial, ketQua, chiPhi))
                {
                    MessageBox.Show("Lập phiếu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Truyền dữ liệu sang Form HoaDonSaoBH
                    HoaDonSauBH frmIn = new HoaDonSauBH(maPhieuBH);

                    frmIn.ShowDialog(); // Form in sẽ nổi lên

                    // Đóng Form nổi xác nhận này lại sau khi đã in xong
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
