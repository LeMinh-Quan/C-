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
    public partial class QuanLyNhanVien : Form
    {
        NhanVienBUS nv = new NhanVienBUS();
        private int maNVDangNhap;
        public QuanLyNhanVien( int maNV)
        {
            InitializeComponent();
            this.maNVDangNhap = maNV;
            dateNgayLV.Value= DateTime.Now;
            dateNgaySinh.Value= DateTime.Now;
            cbbChucVu.SelectedIndex = 0;
            btnCapNhatNV.Enabled = false;
            drvDanhSachNV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachNV.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachNV.EnableHeadersVisualStyles = false;
            drvDanhSachNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhSachNV.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvDanhSachNV.AllowUserToAddRows = false;
            drvDanhSachNV.ReadOnly = true;

        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            danhSachNV();
            demSoLuongNV();

        }

        //#######################################--TABPAGE 1 DANH SÁCH NHÂN VIÊN--##################################
        
        //----------------------------Load bảng Nhan_vien lên datagridview----------------------------------
        private void danhSachNV()
        {
            drvDanhSachNV.DataSource = nv.LayDanhSachNhanVien();


            //---đổi tên cột
            drvDanhSachNV.Columns["MaNV"].HeaderText = "Mã NV";
            drvDanhSachNV.Columns["HoTen"].HeaderText = "Họ Tên";
            drvDanhSachNV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            drvDanhSachNV.Columns["GioiTinh"].HeaderText = "Giới Tính";
            drvDanhSachNV.Columns["TrangThai"].HeaderText = "Trạng Thái";
            drvDanhSachNV.Columns["NgayVaoLam"].HeaderText = "Ngày Làm Việc";
            drvDanhSachNV.Columns["VaiTro"].HeaderText = "Vai Trò";

            drvDanhSachNV.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            drvDanhSachNV.Columns["NgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            drvDanhSachNV.Columns["GioiTinh"].Width = 50;
            drvDanhSachNV.Columns["MaNV"].Width = 50;
            //--Ẩn cột
            drvDanhSachNV.Columns[3].Visible = false;
            drvDanhSachNV.Columns[4].Visible = false;
            drvDanhSachNV.Columns[5].Visible = false;
            drvDanhSachNV.Columns[7].Visible = false;
            drvDanhSachNV.Columns[11].Visible = false;
        }

        //------------------------------xử lý trạng thái == đã nghĩ thì màu đỏ dòng-----------------------------------
        private void drvDanhSachNV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Duyệt qua từng dòng trong DataGridView
            foreach (DataGridViewRow row in drvDanhSachNV.Rows)
            {
                // Kiểm tra xem ô Trạng Thái có dữ liệu không để tránh lỗi văng app
                if (row.Cells["TrangThai"].Value != null)
                {
                    string trangThai = row.Cells["TrangThai"].Value.ToString();

                    // Nếu trạng thái là "Đã nghỉ" (Bạn nhớ gõ đúng chính tả chữ hoa chữ thường giống trong Database nhé)
                    if (trangThai == "Đã nghỉ")
                    {
                        // Đổi màu nền của cả dòng thành màu Đỏ
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128);

                      
                    }
                    else
                    {
                        // (Tùy chọn) Khôi phục màu mặc định cho các nhân viên bình thường
                        // Để phòng trường hợp dữ liệu được load lại
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }


        //-----------------------------ĐẾM SỐ LƯỢNG NHÂN VIÊN-------------------------------------------
        private void demSoLuongNV()
        {
            //-----đếm số lượng trong data
            DataTable dt = (DataTable)drvDanhSachNV.DataSource;

            int soDangLam = dt.Select("TrangThai = 'Đang làm việc'").Length;
            int soDaNghi = dt.Select("TrangThai = 'Đã nghỉ'").Length;

            lblSLLamViec.Text = soDangLam.ToString();
            lblSLDaNghi.Text = soDaNghi.ToString();
        }

        //------------------------------Hiện tất cả danh sách( radTatca)---------------------------------
        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (radTatCa.Checked)
            {
                danhSachNV();
            }
        }
        //-------------------------------------LÀM MỚI---------------------------------------------------
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMSNV.Text = "";
            radTatCa.Checked = true;
        }
        //-------------------------------------TÌM KIẾM THEO MSNV-----------------------------------------
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập MSNV để tìm");
                return;
            }
            radTatCa.Checked = false;
            string tuKhoa = txtMSNV.Text.Trim();
            DataTable dt = (DataTable)drvDanhSachNV.DataSource;
            try
            {

                dt.DefaultView.RowFilter = $"Convert(MaNV, 'System.String') LIKE '%{tuKhoa}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lọc dữ liệu: " + ex.Message);

            }
        }

        //------------------------------------------------CHỌN 1 DÒNG BẤM NUT CẬP NHẬT-------------------------------------
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnCapNhatNV.Enabled = true;
            // 1. KIỂM TRA XEM NGƯỜI DÙNG ĐÃ CHỌN DÒNG NÀO TRÊN LƯỚI CHƯA
            if (drvDanhSachNV.CurrentRow == null || drvDanhSachNV.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trong danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // =================================================================
            // 🚀 XỬ LÝ MỚI: KIỂM TRA QUYỀN TRUY CẬP (VAI TRÒ)
            // =================================================================
            DataGridViewRow row = drvDanhSachNV.CurrentRow;
            string vaiTro = row.Cells["VaiTro"].Value?.ToString();
            int maNVQL = Convert.ToInt32(row.Cells["MaNV"].Value);

            // Nếu là Quản lý thì báo lỗi và đuổi ra ngoài ngay lập tức
            if (vaiTro == "Quản lý" && maNVQL != this.maNVDangNhap)
            {
                MessageBox.Show("Bạn không có quyền chỉnh sửa hồ sơ của cấp Quản lý!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Chốt chặn tại đây, các dòng code phía dưới sẽ không được chạy
            }
            // =================================================================



            // Lấy nguyên cái dòng (row) mà người dùng đang click chuột vào

            try
            {
                // =================================================================
                // 2. GÁN MÃ NHÂN VIÊN VÀO BIẾN TOÀN CỤC (Cực kỳ quan trọng)
                maNVDangChon = maNVQL;

                // 3. CHUYỂN SANG TAB "THÊM/CẬP NHẬT"
                // Giả sử TabControl của bạn tên là tabControl1 và Tab Thêm/Cập nhật tên là tabPage2
                guna2TabControl1.SelectedTab = tabPage2;

                // 4. ĐỔ DỮ LIỆU TỪ DÒNG ĐƯỢC CHỌN LÊN CÁC TEXTBOX, COMBOBOX...

                // TextBox (Thêm dấu ? để phòng trường hợp dữ liệu bị Null thì không bị văng app)
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtSoDienThoai.Text = row.Cells["SDT"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();

                // ComboBox Chức vụ
                cbbChucVu.Text = row.Cells["VaiTro"].Value?.ToString();

                // DateTimePicker (Chuyển chuỗi về ngày tháng)
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                    dateNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                if (row.Cells["NgayVaoLam"].Value != DBNull.Value)
                    dateNgayLV.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);

                // RadioButton Giới tính
                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString();
                if (gioiTinh == "Nam")
                    radNam.Checked = true;
                else
                    radNu.Checked = true;

                // RadioButton Trạng thái
                string trangThai = row.Cells["TrangThai"].Value?.ToString();
                if (trangThai == "Đang làm việc")
                    radDangLV.Checked = true;
                else
                    radDangLV.Checked = false;

                // 5. XỬ LÝ LẤY LẠI HÌNH ẢNH CŨ
                // Lấy lại đường dẫn ảnh gán vào biến toàn cục đã tạo ở bài trước
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
                    pitHinh.Image = null; // Chưa có ảnh
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //#########################################--TABPAGE2 THÊM/CẬP NHẬT--####################################


        //---------------------------------------------------THÊM NHÂN VIÊN---------------------------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA DỮ LIỆU ĐẦU VÀO (Validation cơ bản)
            if (txtDiaChi.Text == "" || txtEmail.Text == "" || txtHoTen.Text == "" || cbbChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if( txtCCCD.Text.Length < 12)
            {
                MessageBox.Show("CCCD không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txtSoDienThoai.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // (Giả sử bạn đặt tên control là dtpNgaySinh)
            DateTime ngaySinh = dateNgaySinh.Value.Date;

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
                dateNgaySinh.Focus();
                return; // Dừng lại, không cho lưu
            }
            try
            {
                // 2. LẤY DỮ LIỆU TỪ FORM VÀ ĐÓNG GÓI VÀO DTO
                NhanVienDTO nvMoi = new NhanVienDTO();

                // TextBox
                nvMoi.HoTen = txtHoTen.Text.Trim();
                nvMoi.Cccd = txtCCCD.Text.Trim();
                nvMoi.Email = txtEmail.Text.Trim();
                nvMoi.Sdt = txtSoDienThoai.Text.Trim();
                nvMoi.DiaChi = txtDiaChi.Text.Trim();

                // DateTimePicker (Chỉ lấy Ngày, bỏ Giờ)
                nvMoi.NgaySinh = dateNgaySinh.Value.Date;
                nvMoi.NgayVaoLam = dateNgayLV.Value.Date;

                // ComboBox (Lấy chữ đang hiển thị)
                nvMoi.VaiTro = cbbChucVu.Text;

                // RadioButton (Dùng toán tử 3 ngôi cho ngắn gọn)
                nvMoi.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                nvMoi.TrangThai = radDangLV.Checked ? "Đang làm việc" : "Đã nghỉ";

                nvMoi.HinhAnh = tenHinhAnh;

                // 3. GỌI BUS ĐỂ LƯU VÀO DATABASE
                if (nv.ThemNhanVienMoi(nvMoi))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Tải lại lưới dữ liệu bên tab DANH SÁCH để cập nhật người mới
                    lamMoi();
                    danhSachNV();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //----------------------------------------------XỬ LÝ THÊM ẢNH------------------------------------------
        public string tenHinhAnh = "";
        public string imagePath = Path.Combine(Application.StartupPath, "Images");
        private void btnChonAnh_Click(object sender, EventArgs e)
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
  
        //------------------------------------------LÀM MỚI Ô NHẬP LIỆU-----------------------------------
        private void lamMoi()
        {
            btnThem.Enabled = true;
            btnCapNhatNV.Enabled = false;
            txtSoDienThoai.Text = "";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
            cbbChucVu.SelectedIndex = 0;
            dateNgayLV.Value = DateTime.Now;
            dateNgayLV.Value = DateTime.Now;
            radNam.Checked = true;
            radDangLV.Checked = true;
            pitHinh.Image = null;
            tenHinhAnh = "";
        }
        //-------------------------------------------CẬP NHẬT NHÂN VIÊN------------------------------------
        private int maNVDangChon = -1;

        private void btnCapNhatNV_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA XEM ĐÃ CHỌN NHÂN VIÊN ĐỂ CẬP NHẬT CHƯA
            if (maNVDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên bên danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. KIỂM TRA DỮ LIỆU ĐẦU VÀO
            if (txtDiaChi.Text=="" || txtEmail.Text=="" || txtHoTen.Text =="" || cbbChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtSoDienThoai.Text.Length < 10)
            {
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
            }

            if (txtCCCD.Text.Length < 12)
            {
                {
                    MessageBox.Show("Số CCCD không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
            }

            try
            {
                // 3. ĐÓNG GÓI DỮ LIỆU TỪ FORM VÀO DTO
                NhanVienDTO nvSua = new NhanVienDTO();

                // Cực kỳ quan trọng: Gán Mã NV đang chọn vào DTO để SQL biết cập nhật ai
                nvSua.MaNV = maNVDangChon;

                nvSua.HoTen = txtHoTen.Text.Trim();
                nvSua.Cccd = txtCCCD.Text.Trim();
                nvSua.Email = txtEmail.Text.Trim();
                nvSua.Sdt = txtSoDienThoai.Text.Trim();
                nvSua.DiaChi = txtDiaChi.Text.Trim();
                nvSua.NgaySinh = dateNgaySinh.Value.Date;
                nvSua.NgayVaoLam = dateNgayLV.Value.Date;
                nvSua.VaiTro = cbbChucVu.Text;
                nvSua.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                nvSua.TrangThai = radDangLV.Checked ? "Đang làm việc" : "Đã nghỉ";
                nvSua.HinhAnh = tenHinhAnh;

                // 4. GỌI BUS ĐỂ UPDATE
                if (nv.CapNhatThongTin(nvSua))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    danhSachNV(); // Load lại lưới để thấy sự thay đổi
                    lamMoi(); // Dọn dẹp form

                    // Trả biến nhớ về lại trạng thái ban đầu
                    maNVDangChon = -1;
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

        
        private void btnlamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Thì từ chối, không cho ký tự đó hiện lên TextBox
                e.Handled = true;
            }
        }

        private void txtMSNV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
