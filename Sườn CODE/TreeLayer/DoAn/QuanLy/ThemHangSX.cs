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

namespace DoAn.QuanLy
{
    public partial class ThemHangSX : Form
    {
        HangSanXuatBUS hangBus = new HangSanXuatBUS();

        public ThemHangSX()
        {
            InitializeComponent();
            loadDs();
        }

        private void loadDs()
        {
            DataTable dt = hangBus.LayToanBoHang();
            drvDanhHang.DataSource = dt;

            drvDanhHang.Columns["MaHang"].HeaderText = "Mã hãng";
            drvDanhHang.Columns["TenHang"].HeaderText = "Tên Hãng";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Giả sử textbox của bạn tên là txtTenHang
            string tenHangMoi = txtHangSX.Text.Trim();

            // 2. Kiểm tra rỗng trên giao diện
            if (string.IsNullOrEmpty(tenHangMoi))
            {
                MessageBox.Show("Vui lòng nhập tên Hãng sản xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHangSX.Focus();
                return;
            }


            // =========================================================
            // 2. 🚀 KIỂM TRA TRÙNG TÊN HÃNG
            // =========================================================
            if (hangBus.KiemTraTenHangTonTai(tenHangMoi))
            {
                MessageBox.Show("Tên hãng '" + tenHangMoi + "' đã tồn tại trong hệ thống. Vui lòng nhập tên khác!", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tenHangMoi = "";
                txtHangSX.Focus();
                return;
            }

            // =========================================================
            try
            {
                if (tenHangMoi.Length > 0)
                {
                    // Cắt lấy chữ cái đầu tiên (vị trí 0) đem in hoa, rồi ghép với khúc đuôi còn lại
                    tenHangMoi = tenHangMoi.Substring(0, 1).ToUpper() + tenHangMoi.Substring(1);
                }

                // 3. Gọi BUS thực hiện thêm mới
                if (hangBus.ThemHangMoi(tenHangMoi))
                {

                    MessageBox.Show("Thêm Hãng sản xuất mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDs();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThemHangSX_Load(object sender, EventArgs e)
        {
            drvDanhHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhHang.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhHang.EnableHeadersVisualStyles = false;
            drvDanhHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhHang.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvDanhHang.AllowUserToAddRows = false;
            drvDanhHang.ReadOnly = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
