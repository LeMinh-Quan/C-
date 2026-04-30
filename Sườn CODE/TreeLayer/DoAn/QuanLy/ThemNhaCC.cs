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
    public partial class ThemNhaCC : Form
    {
        NhaCungCapBUS nccBus = new NhaCungCapBUS();
        public ThemNhaCC()
        {
            InitializeComponent();
            loadDs();

        }
        private void loadDs()
        {
            DataTable dt =nccBus.LayToanBoNCC();
            drvNhaCC.DataSource = dt;

            drvNhaCC.Columns["MaNCC"].HeaderText = "Mã Nhà CC";
            drvNhaCC.Columns["TenNCC"].HeaderText = "Tên Nhà CC";
            drvNhaCC.Columns["SDT"].HeaderText = "Số Điện Thoại";
            drvNhaCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";

            drvNhaCC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvNhaCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvNhaCC.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvNhaCC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvNhaCC.EnableHeadersVisualStyles = false;
            drvNhaCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvNhaCC.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvNhaCC.AllowUserToAddRows = false;
            drvNhaCC.ReadOnly = true;

        }


        private void btnLuu_Click(object sender, EventArgs e)
        {

            // Lấy dữ liệu và làm sạch khoảng trắng
            string ten = txtTenNCC.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            // 1. KIỂM TRA RỖNG CÁC TRƯỜNG BẮT BUỘC (Khóa chính và Not Null)
            if (ten == "" || sdt =="" || diaChi =="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                

                // 3. ĐÓNG GÓI DTO VÀ LƯU
                NhaCungCapDTO ncc = new NhaCungCapDTO();
               ncc.TenNCC = ten;
                ncc.Sdt = sdt;
                ncc.DiaChi = diaChi;

                if (nccBus.ThemNCCMoi(ncc))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadDs();
                    // Đóng Form nổi nếu bạn đang dùng ShowDialog()
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
