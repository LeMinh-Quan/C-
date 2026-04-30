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
using Guna.UI2.WinForms;

namespace DoAn.QuanLy
{
    public partial class QuanLySP : Form
    {
        SanPhamBUS sp = new SanPhamBUS();
        HangSanXuatBUS hangBUS = new HangSanXuatBUS();
        CTSanPhamBUS ctBUS = new CTSanPhamBUS();
        private string maModelDangChon = "";
        public QuanLySP()
        {
            InitializeComponent();
            btnCapNhatSP2.Enabled = false;
            cbbTrangThai.SelectedIndex = 0;
            drvDanhSachLoaiSP.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachLoaiSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachLoaiSP.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachLoaiSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachLoaiSP.EnableHeadersVisualStyles = false;
            drvDanhSachLoaiSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhSachLoaiSP.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvDanhSachLoaiSP.AllowUserToAddRows = false;
            drvDanhSachLoaiSP.ReadOnly = true;

            drvDanhSachCTSP.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachCTSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachCTSP.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachCTSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachCTSP.EnableHeadersVisualStyles = false;
            drvDanhSachCTSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhSachCTSP.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvDanhSachCTSP.AllowUserToAddRows = false;
            drvDanhSachCTSP.ReadOnly = true;

        }

        private void QuanLySP_Load(object sender, EventArgs e)
        {
            danhSachSP();
            loadComboBoxHang();
            loadDanhSachCTSP();
            
        }

        private void lamMoi()
        {
            btnCapNhatSP2.Enabled = false;
            btnThem.Enabled = true;
            cbbHangSX.Enabled = true;
            txtMaModel.Enabled = true;
            txtMaModel.Text = "";
            txtGiaBan.Text = "";
            txtCPU.Text = "";
            txtOCung.Text = "";
            txtRAM.Text = "";
            txtTenSP.Text = "";
            txtVGA.Text = "";
            txtManHinh.Text = "";
            cbbPhanLoai.SelectedIndex = -1;
            cbbTrangThai.SelectedIndex = 0;
            cbbHangSX.SelectedIndex = -1;
            numBaoHanh.Value = 0;
            txtMoTa.Text = "";
            pitHinh.Image = null;
        }

        //###########################--DANH MỤC--########################################################
        // =========================LOAD DANH SÁCH LÊN DATAGRIDVIEW DANH MỤC=============================
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
            drvDanhSachLoaiSP.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);


        }

        //=============== TÔ MÀU CHO SẢN PHẨM NGỪNG BÁN===========================
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
        //============================== TÌM KIẾM DANH MỤC========================================
        private void btnTimKiemDanhMuc_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập gì chưa
            string tuKhoa = txtTimKiemDanhMuc.Text.Trim();

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
        //===================== RESET =========================
        private void btnResetDanhMuc_Click(object sender, EventArgs e)
        {
            txtTimKiemDanhMuc.Text = "";
            danhSachSP();
        }
        //====================== XEM CHI TIẾT SẢN PHẨM======================
        private void btnXemChiTietSP_Click(object sender, EventArgs e)
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


        //===================CẬP NHẬT SẢN PHẨM( CHỌN 1 DÒNG BÊN TRANG DANH MỤC)====================
        private void btnCapNhatSP_Click(object sender, EventArgs e)
        {
            btnCapNhatSP2.Enabled = true;
            // 1. KIỂM TRA XEM NGƯỜI DÙNG ĐÃ CHỌN DÒNG NÀO TRÊN LƯỚI CHƯA
            if (drvDanhSachLoaiSP.CurrentRow == null || drvDanhSachLoaiSP.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trong danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = drvDanhSachLoaiSP.CurrentRow;
            try
            {
                // 1. LƯU MÃ MODEL VÀ KHÓA Ô TEXTBOX (Rất quan trọng)
                maModelDangChon = row.Cells["MaModel"].Value?.ToString() ?? "";
                txtMaModel.Text = maModelDangChon;
                cbbHangSX.Enabled = false;
                txtMaModel.Enabled = false; // Khóa lại không cho sửa Khóa Chính
                btnThem.Enabled = false;
                guna2TabControl1.SelectedTab = tabPage3;

                // 2. ĐỔ DỮ LIỆU CHỮ LÊN TEXTBOX
                txtTenSP.Text = row.Cells["TenSanPham"].Value?.ToString();
                txtCPU.Text = row.Cells["CPU"].Value?.ToString();
                txtRAM.Text = row.Cells["RAM"].Value?.ToString();
                txtOCung.Text = row.Cells["OCung"].Value?.ToString();
                txtVGA.Text = row.Cells["VGA"].Value?.ToString();
                txtManHinh.Text = row.Cells["ManHinh"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();

                // 3. ĐỔ DỮ LIỆU LÊN COMBOBOX
                cbbPhanLoai.Text = row.Cells["PhanLoai"].Value?.ToString();
                cbbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();

                // Ép ComboBox Hãng Sản Xuất nhảy về đúng Hãng đang chọn (Dùng Mã Hãng)
                if (row.Cells["MaHang"].Value != DBNull.Value)
                {
                    cbbHangSX.SelectedValue = row.Cells["MaHang"].Value;
                }

                // 4. ĐỔ DỮ LIỆU SỐ TIỀN & BẢO HÀNH
                if (row.Cells["GiaBan"].Value != DBNull.Value)
                    txtGiaBan.Text = Convert.ToDecimal(row.Cells["GiaBan"].Value).ToString("0");

                if (row.Cells["ThoiGianBaoHanh"].Value != DBNull.Value)
                    numBaoHanh.Value = Convert.ToInt32(row.Cells["ThoiGianBaoHanh"].Value);

                // 5. XỬ LÝ LẤY LẠI ẢNH CŨ (Dùng tuyệt chiêu ReadAllBytes để chống Crash Form)
                tenHinhAnh = row.Cells["HinhAnh"].Value?.ToString() ?? "";


                if (!string.IsNullOrEmpty(tenHinhAnh))
                {
                    string duongDanThucTe = Path.Combine(imagePath, tenHinhAnh);

                    if (File.Exists(duongDanThucTe))
                    {
                        // Lại dùng FileStream để chống khóa file
                        using (FileStream fs = new FileStream(duongDanThucTe, FileMode.Open, FileAccess.Read))
                        {
                            pitHinh.Image = Image.FromStream(fs);
                        }
                        pitHinh.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        pitHinh.Image = null; // Mất file vật lý
                    }
                }
                else
                {
                    pitHinh.Image = null; // Sản phẩm chưa có ảnh
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lên form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==================XEM DANH SÁCH TIẾT SẢN PHẨM==========
        private void btnXemDSSP_Click(object sender, EventArgs e)
        {
            // 1.Kiểm tra xem đã chọn dòng nào trên bảng drvDanhSachLoaiSP chưa
            if (drvDanhSachLoaiSP.CurrentRow == null || drvDanhSachLoaiSP.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một loại sản phẩm để xem danh sách máy chi tiết!", "Thông báo");
                return;
            }

            try
            {
                // 2. Lấy Mã Model từ dòng đang chọn
                string maModelChon = drvDanhSachLoaiSP.CurrentRow.Cells["MaModel"].Value.ToString();

                // 3. Chuyển sang TabPage3 (Giả sử TabControl của bạn tên là guna2TabControl1 hoặc tabControl1)
                // Bạn hãy thay đúng tên TabControl của mình vào đây
                guna2TabControl1.SelectedTab = tabPage2;

                // 4. Gọi hàm load dữ liệu lên bảng drvDanhSachCTSP (Hàm này chúng ta đã viết ở bài trước)
                loadDanhSachSerial(maModelChon);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển hướng dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //#############################----TABPAGE 2 DANH SÁCH SẢN PHẨM THEO MÃ SERIAL---##########################################
        //#######################################################################################


        //--------------- lOAD DANH SÁCH CT SẢN PHẨM---------------------------
        private void loadDanhSachCTSP()
        {
            DataTable dt = ctBUS.LayDanhSachSerial();
            drvDanhSachCTSP.DataSource = dt;

            drvDanhSachCTSP.Columns["MaSerial"].HeaderText = "Mã Serial";
            drvDanhSachCTSP.Columns["MaModel"].HeaderText = "Mã Model";
            drvDanhSachCTSP.Columns["MaPO"].HeaderText = "Mã PO";
            drvDanhSachCTSP.Columns["NgayNhapKho"].HeaderText = "Ngày Nhập Kho";
            drvDanhSachCTSP.Columns["TrangThai"].HeaderText = "Trạng Thái";

            drvDanhSachCTSP.Columns[4].Visible = false;

        }
        //================ LOAD DANH SÁCH SERIAL THEO MÃ MODEL================
        private void loadDanhSachSerial(string maModel)
        {
            // 1. Gọi lại chính hàm load cũ của bạn để kéo tất cả dữ liệu và định dạng cột
            loadDanhSachCTSP();

            // 2. Ép kiểu DataSource về DataTable để sử dụng bộ lọc
            DataTable dt = (DataTable)drvDanhSachCTSP.DataSource;

            if (dt != null)
            {
                // 3. Lọc danh sách: Chỉ giữ lại những dòng có MaModel trùng với mã vừa truyền vào
                dt.DefaultView.RowFilter = string.Format("MaModel = '{0}'", maModel);
            }
        }

        private void btnResetSP_Click_1(object sender, EventArgs e)
        {
            loadDanhSachCTSP();
            txtTimSerial.Text = "";
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string tuKhoaSerial = txtTimSerial.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoaSerial))
            {
                MessageBox.Show("Vui lòng nhập Mã Serial cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimSerial.Focus(); // Nháy chuột lại vào ô nhập
                return;
            }

            DataTable dt = (DataTable)drvDanhSachCTSP.DataSource;
            if (dt != null)
                dt.DefaultView.RowFilter = string.Format("MaSerial LIKE '%{0}%'", tuKhoaSerial);
        }

        //############################### TABPAGE 3 THÊM SẢN PHẨM  ##############################
        //####################################################################################
        //###########################################################

        //==============THÊM HÃNG SẢN XUẤT=======================
        private void btnThemHangSX_Click(object sender, EventArgs e)
        {
            ThemHangSX frmHangSanXuat = new ThemHangSX();

            frmHangSanXuat.ShowDialog();
        }
        //================CẬP NHẬT================================
        private void btnCapNhatSP2_Click(object sender, EventArgs e)
        {
            

            if (txtMaModel.Text == "" || txtGiaBan.Text == "" || txtCPU.Text == "" ||
                txtOCung.Text == "" || txtRAM.Text == "" || txtTenSP.Text == "" || txtVGA.Text == ""
                || txtManHinh.Text == "" || cbbPhanLoai.SelectedIndex == -1 || cbbTrangThai.SelectedIndex == -1
                || cbbHangSX.SelectedIndex == -1 || numBaoHanh.Value == 0 || txtGiaBan.Text == "")
            {
                MessageBox.Show("Dữ liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaModel.Focus();
                return;
            }


            try
            {
                // 4. ĐÓNG GÓI DỮ LIỆU
                SanPhamDTO spSua = new SanPhamDTO();
                // ÉP BUỘC dùng mã model đang nhớ, không dùng Textbox nhỡ bị sửa bậy
                spSua.MaModel = maModelDangChon;
                
                spSua.TenSanPham = txtTenSP.Text.Trim();
                spSua.MaHang = Convert.ToInt32(cbbHangSX.SelectedValue);
                spSua.PhanLoai = cbbPhanLoai.Text;
                spSua.TrangThai = cbbTrangThai.Text;

                spSua.Cpu = txtCPU.Text.Trim();
                spSua.Ram = txtRAM.Text.Trim();
                spSua.OCung = txtOCung.Text.Trim();
                spSua.Vga = txtVGA.Text.Trim();
                spSua.ManHinh = txtManHinh.Text.Trim();


                string chuoiGiaBan = txtGiaBan.Text.Replace(".", "").Replace(",", "");

                // 2. ÉP KIỂU CÁI CHUỖI VỪA ĐƯỢC XÓA DẤU
                decimal.TryParse(chuoiGiaBan, out decimal giaBan);

                // 3. GÁN VÀO DTO
                spSua.GiaBan = giaBan;
                spSua.GiaBan = giaBan;
                spSua.ThoiGianBaoHanh = Convert.ToInt32(numBaoHanh.Value);

                spSua.MoTa = txtMoTa.Text.Trim();
                spSua.HinhAnh = tenHinhAnh; // Lấy từ biến nhớ ảnh

                // 5. GỌI BUS THỰC THI
                if (sp.CapNhatThongTinSP(spSua))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa rỗng form và mở khóa lại ô Mã Model cho lần Thêm mới tiếp theo
                    lamMoi();
                    maModelDangChon = ""; // Xóa bộ nhớ
                    btnThem.Enabled = true;

                    // Load lại lưới
                    danhSachSP();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            // 1. KIỂM TRA DỮ LIỆU ĐẦU VÀO CƠ BẢN
            if (txtMaModel.Text.Length  <= 4 || txtGiaBan.Text == "" || txtCPU.Text == "" ||
                txtOCung.Text == "" || txtRAM.Text == "" || txtTenSP.Text == "" || txtVGA.Text == ""
                || txtManHinh.Text == "" || cbbPhanLoai.SelectedIndex == -1 || cbbTrangThai.SelectedIndex == -1
                || cbbHangSX.SelectedIndex == -1 || numBaoHanh.Value == 0 || txtGiaBan.Text == "")
            {
                MessageBox.Show("Dữ liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaModel.Focus();
                return;
            }

            try
            {
                // 3. ĐÓNG GÓI VÀO DTO
                SanPhamDTO spMoi = new SanPhamDTO();

                spMoi.MaModel = txtMaModel.Text.Trim();
                spMoi.TenSanPham = txtTenSP.Text.Trim();


                spMoi.MaHang = cbbHangSX.SelectedIndex + 1;

                spMoi.PhanLoai = cbbPhanLoai.Text;
                spMoi.TrangThai = cbbTrangThai.Text;

                // Cấu hình
                spMoi.Cpu = txtCPU.Text.Trim();
                spMoi.Ram = txtRAM.Text.Trim();
                spMoi.OCung = txtOCung.Text.Trim();
                spMoi.Vga = txtVGA.Text.Trim();
                spMoi.ManHinh = txtManHinh.Text.Trim();
                // 1. LẤY CHỮ RA VÀ XÓA DẤU CHẤM TRƯỚC
                string chuoiGiaBan = txtGiaBan.Text.Replace(".", "").Replace(",", "");

                // 2. ÉP KIỂU CÁI CHUỖI VỪA ĐƯỢC XÓA DẤU
                decimal.TryParse(chuoiGiaBan, out decimal giaBan);

                // 3. GÁN VÀO DTO
                spMoi.GiaBan = giaBan;

                // Ô Bảo Hành (NumericUpDown thường lấy .Value)
                spMoi.ThoiGianBaoHanh = Convert.ToInt32(numBaoHanh.Value);

                spMoi.MoTa = txtMoTa.Text.Trim();
                spMoi.HinhAnh = tenHinhAnh; // Lấy từ biến toàn cục lúc upload ảnh

                // 4. GỌI BUS THÊM VÀO DB
                if (sp.ThemSanPhamMoi(spMoi))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    danhSachSP();
                    lamMoi();

                    // Xóa rỗng form và load lại lưới dữ liệu

                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Có thể Mã Model này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================LOAD DANH SÁCH LÊN COMBOBOX=============================
        private void loadComboBoxHang()
        {
            DataTable dt = hangBUS.LayToanBoHang();

            // 1. Gán nguồn dữ liệu
            cbbHangSX.DataSource = dt;

            // 2. Thiết lập hiển thị: Hiện cột TenHang lên ComboBox
            cbbHangSX.DisplayMember = "TenHang";

            // 3. Thiết lập giá trị ngầm: Lưu MaHang bên dưới
            cbbHangSX.ValueMember = "MaHang";

            // (Tùy chọn) Để mặc định không chọn hãng nào lúc mới mở
            cbbHangSX.SelectedIndex = -1;
        }

        public string tenHinhAnh = "";
        public string imagePath = Path.Combine(Application.StartupPath, "ImagesSP");

        private void btnChonAnh_Click_1(object sender, EventArgs e)
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
                        pitHinh.Image = Image.FromStream(fs);
                    }

                    // Ép ảnh co giãn cho vừa khung (Tùy chọn cho đẹp)
                    pitHinh.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
        }


        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            lamMoi();
        }
        //============= XỬ LÝ TẠO TÊN MODEL DỰA  TRÊN  CBB HÃNG==========
        private void cbbHangSX_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem ComboBox có đang được chọn dữ liệu hợp lệ không
            if (cbbHangSX.SelectedIndex != -1 && !string.IsNullOrEmpty(cbbHangSX.Text))
            {
                // 2. Lấy tên hãng đang được hiển thị (Ví dụ: "Dell")
                string tenHang = cbbHangSX.Text.Trim();

                if (tenHang.Length > 0)
                {
                    // 3. Cắt lấy ký tự ĐẦU TIÊN (vị trí 0, độ dài 1), viết hoa nó lên và cộng thêm chữ "LT"
                    string tienTo = tenHang.Substring(0, 1).ToUpper() + "LT-";

                    // 4. Gán tiền tố này vào ô Textbox Mã Model
                    txtMaModel.Text = tienTo;

                    // 5. (Mẹo UX) Đưa con trỏ chuột nhấp nháy về cuối cùng của chữ vừa tạo 
                    // Để người dùng gõ tiếp số model luôn mà không cần dùng chuột click lại
                    txtMaModel.SelectionStart = txtMaModel.Text.Length;
                }
            }
        }

        //========= CHỈ CHO NHẬP SÔ===========
        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chặn tất cả các ký tự KHÔNG PHẢI là: Phím điều khiển, Số, và Dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar !='.')
            {
                e.Handled = true; // Bắt giữ lại, không cho in ra màn hình
            }
        }
        //=========== ĐỊNH DẠNG LẠI GIÁ TIỀN=======================
        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {
            // Nếu ô trống thì không làm gì cả
            if (txtGiaBan.Text == "") return;

            // 1. Dọn dẹp: Xóa sạch các dấu chấm, phẩy cũ để lấy ra con số nguyên thủy
            string soGoc = txtGiaBan.Text.Replace(".", "").Replace(",", "");

            long tien;
            // 2. Chuyển chuỗi thành số Long (dùng Long để chứa được số tiền tỷ)
            if (long.TryParse(soGoc, out tien))
            {
                // 3. Tạm thời tắt sự kiện TextChanged để tránh code chạy lặp vô tận
                txtGiaBan.TextChanged -= txtGiaBan_TextChanged;

                // 4. Ép định dạng tiền tệ theo chuẩn Việt Nam (vi-VN)
                // Ký hiệu "{0:#,##0}" sẽ tự động nhóm 3 số lại và nhét dấu phân cách vào giữa
                txtGiaBan.Text = string.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:#,##0}", tien);

                // 5. Đưa con trỏ chuột nhấp nháy về lại cuối dòng để khách gõ tiếp
                txtGiaBan.SelectionStart = txtGiaBan.Text.Length;

                // 6. Bật sự kiện lên lại
                txtGiaBan.TextChanged += txtGiaBan_TextChanged;
            }
        }


    }

}
