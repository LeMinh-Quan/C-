using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using DoAn.HeThong;


namespace DoAn.QuanLy
{
    public partial class BaoCaoThongKe : Form
    {
        private ThongKeBUS bcBUS =new ThongKeBUS();
        private DataTable dtDoanhThuHienTai;
        public BaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void BaoCaoThongKe_Load(object sender, EventArgs e)
        {

            //----------------------- TABPAGE 1----------------------------------------------
            LoadThongKeTonKho();
            // Tự động nạp toàn bộ danh sách khi mở Form
            drvHieuXuat.DataSource = bcBUS.LayTatCaHoaDon();
            //----------------------- TABPAGE 2----------------------------------------------
            HienThiHieuSuat();
            TinhTongHieuSuat();
            //----------------------- TABPAGE 3----------------------------------------------
            LoadComboBoxSanPham();
            ThietLapNgayMacDinh();
            VeBieuDoTheoThang();
            VeBieuDoTheoNam();

        }
        //###############################################################################################
        //---------------------------- TABPAGE1 THỐNG KÊ TỒN KHO-----------------------------------------
        //###############################################################################################

        private void LoadThongKeTonKho()
        {
            DataTable dt = bcBUS.LayThongKeTonKhoTongHop();
            drvDanhSachTonKhoSP.DataSource = dt;

            // Định dạng tiêu đề cột
            drvDanhSachTonKhoSP.Columns["MaModel"].HeaderText = "Mã Model";
            drvDanhSachTonKhoSP.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            drvDanhSachTonKhoSP.Columns["PhanLoai"].HeaderText = "Phân Loại";
            drvDanhSachTonKhoSP.Columns["SoLuongTon"].HeaderText = "Số Lượng Tồn"; // Đây là cột đếm

            // Căn giữa cột số lượng cho dễ nhìn
            drvDanhSachTonKhoSP.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            drvDanhSachTonKhoSP.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            drvDanhSachTonKhoSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            drvDanhSachTonKhoSP.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
            drvDanhSachTonKhoSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            drvDanhSachTonKhoSP.EnableHeadersVisualStyles = false;
            drvDanhSachTonKhoSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            drvDanhSachTonKhoSP.SelectionMode = DataGridViewSelectionMode.CellSelect;
            drvDanhSachTonKhoSP.AllowUserToAddRows = false;
            drvDanhSachTonKhoSP.ReadOnly = true;
        }

        private void drvDanhSachTonKhoSP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột đang vẽ có phải là cột "SoLuongTon" không
            if (drvDanhSachTonKhoSP.Columns[e.ColumnIndex].Name == "SoLuongTon" && e.Value != null)
            {
                // Chuyển giá trị ô về kiểu số để so sánh
                int slTon = Convert.ToInt32(e.Value);

                // Nếu số lượng bé hơn 5
                if (slTon < 5)
                {
                    // Tô đỏ cả dòng để gây chú ý mạnh
                    drvDanhSachTonKhoSP.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Firebrick; // Màu đỏ đậm
                    //drvDanhSachTonKhoSP.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;     // Chữ trắng cho dễ đọc

                    // Nếu bạn chỉ muốn tô màu riêng ô Số lượng thì dùng:
                    // e.CellStyle.BackColor = Color.Red;
                }
                else
                {
                    // Reset về mặc định nếu số lượng >= 5 (phòng trường hợp dữ liệu thay đổi)
                    drvDanhSachTonKhoSP.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    drvDanhSachTonKhoSP.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnTimKiemModelTonKho_Click(object sender, EventArgs e)
        {
            if (txtTimKiemModelTonKho.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã Model để tìm");
                return;
            }
            string tuKhoa = txtTimKiemModelTonKho.Text.Trim();
            DataTable dt = (DataTable)drvDanhSachTonKhoSP.DataSource;
            if( txtTimKiemModelTonKho.Text.Length == 0)
            {
                dt.DefaultView.RowFilter = "";
                ckSoLuongThap.Enabled = true;

            }
            else
            {
                dt.DefaultView.RowFilter = $"Convert(MaModel, 'System.String') LIKE '%{tuKhoa}%'";
                ckSoLuongThap.Enabled = false;
            }


        }
        private void btnRestModelTonKho_Click(object sender, EventArgs e)
        {
            txtTimKiemModelTonKho.Text = "";
            ckSoLuongThap.Enabled=true;
            LoadThongKeTonKho();
        }

        private void ckSoLuongThap_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)drvDanhSachTonKhoSP.DataSource;
            if (dt == null) return;

            if(ckSoLuongThap.Checked)
            {
                dt.DefaultView.RowFilter = "SoLuongTon <= 5";
                btnTimKiemModelTonKho.Enabled =false;
            }
            else
            {
                dt.DefaultView.RowFilter = "";
                btnTimKiemModelTonKho.Enabled = true;
            }
        }

        //###############################################################################################
        //---------------------------- TABPAGE2 THỐNG KÊ HÓA ĐƠN ----------------------------------------
        //###############################################################################################

        private void HienThiHieuSuat()
        {
            if (drvHieuXuat.Columns.Count > 0)
            {
                drvHieuXuat.Columns["MaHD"].HeaderText = "Mã HĐ";
                drvHieuXuat.Columns["NgayLap"].HeaderText = "Ngày Lập";
                drvHieuXuat.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                drvHieuXuat.Columns["TenNhanVien"].HeaderText = "Nhân Viên Bán";
                drvHieuXuat.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                drvHieuXuat.Columns["HinhThucThanhToan"].HeaderText = "Thanh Toán";

                drvHieuXuat.Columns["TongTien"].HeaderText = "Tổng Tiền";
                drvHieuXuat.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                drvHieuXuat.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                drvHieuXuat.Columns["TrangThai"].HeaderText = "Trạng Thái";
                drvHieuXuat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                drvHieuXuat.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                drvHieuXuat.ColumnHeadersHeight = 30; // chỉnh chiều cao (px)
                drvHieuXuat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                drvHieuXuat.EnableHeadersVisualStyles = false;
                drvHieuXuat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                drvHieuXuat.SelectionMode = DataGridViewSelectionMode.CellSelect;
                drvHieuXuat.AllowUserToAddRows = false;
                drvHieuXuat.ReadOnly = true;
            }
        }

        // HÀM 2: CHUYÊN TÍNH TOÁN DOANH THU & GÁN LÊN LABEL
        private void TinhTongHieuSuat()
        {
            DataTable dt = (DataTable)drvHieuXuat.DataSource ;
            if (dt == null) return;

            // Sử dụng DefaultView để chỉ tính toán trên các dòng đã được lọc (Filter)
            DataView dv = dt.DefaultView;

            // 1. Đếm số lượng đơn hàng hiện có trong danh sách đã lọc
            int tongDonHang = dv.Count;

            // 2. Tính tổng doanh thu
            decimal tongDoanhThu = 0;
            foreach (DataRowView rowView in dv)
            {
                DataRow row = rowView.Row;
                if (row["TongTien"] != DBNull.Value)
                {
                    tongDoanhThu += Convert.ToDecimal(row["TongTien"]);
                }
            }

            // 3. Hiển thị lên các Label của TechZone
            labTongDonHX.Text = tongDonHang.ToString();
            labDoanhThuHX.Text = tongDoanhThu.ToString("N0") + " VNĐ";
        }

        private void btnTimKiemHX_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dateNgayBatDauHX.Value.Date;
                DateTime denNgay = dateNgayketThucHX.Value.Date;

                // 1. Lấy dữ liệu theo ngày
                DataTable dtLoc = bcBUS.LayDanhSachHoaDonTheoNgay(tuNgay, denNgay);

                // 2. Gán vào lưới (Nếu không có, lưới sẽ tự động trống)
                drvHieuXuat.DataSource = dtLoc;

                // 3. Tự động định dạng và tính toán (Gọi hàm định dạng mới sửa)
                HienThiHieuSuat();
                TinhTongHieuSuat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dateNgayBatDauHX.Value = DateTime.Now;
            dateNgayketThucHX.Value = DateTime.Now;

            // Nếu bạn muốn mặc định là lấy từ đầu tháng đến hiện tại thì dùng:
            // dateNgayBatDauHX.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // 2. Lấy lại TOÀN BỘ dữ liệu từ database (Xóa bỏ điều kiện lọc ngày)
            drvHieuXuat.DataSource = bcBUS.LayTatCaHoaDon();

            // 3. Gọi lại 2 hàm quen thuộc để trang trí và tính toán
            HienThiHieuSuat(); // Tên hàm bạn đã đổi ở bước trước
            TinhTongHieuSuat();     // Tính lại tổng doanh thu của TẤT CẢ hóa đơn
        }

        private void btnCTDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn dòng nào trên DataGridView chưa
                if (drvHieuXuat.CurrentRow == null || drvHieuXuat.CurrentRow.IsNewRow)
                {
                    MessageBox.Show("Vui lòng chọn một Hóa đơn trên lưới để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Lấy Mã Hóa Đơn từ cột "MaHD" của dòng đang được chọn
                // Lưu ý: Tên cột "MaHD" phải khớp chính xác với tên cột bạn đã đặt trong hàm DinhDangLuoiHieuSuat
                int maHD = Convert.ToInt32(drvHieuXuat.CurrentRow.Cells["MaHD"].Value);

                // 3. Khởi tạo và mở Form Chi Tiết Hóa Đơn, truyền Mã HĐ qua
                TTCTHoaDon frmChiTiet = new TTCTHoaDon(maHD);

                // Dùng ShowDialog() thay vì Show() để bắt người dùng xem xong phải Đóng form chi tiết 
                // thì mới được thao tác tiếp trên form Thống kê.
                frmChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết hóa đơn: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //###############################################################################
        //----------------------- TABPAGE 3----------------------------------------------
        //###############################################################################

        // 2. HÀM ĐỔ DỮ LIỆU COMBOBOX
        private void LoadComboBoxSanPham()
        {
            DataTable dtSP = bcBUS.LayDanhSachSanPhamChoCombo();

            // Chèn thêm dòng "--- Tất cả ---" vào dòng đầu tiên (Index 0)
            DataRow rowAll = dtSP.NewRow();
            rowAll["MaModel"] = "ALL";
            rowAll["TenSanPham"] = "--- Tất cả sản phẩm ---";
            dtSP.Rows.InsertAt(rowAll, 0);

            cbbSanPham.DataSource = dtSP;
            cbbSanPham.DisplayMember = "TenSanPham";
            cbbSanPham.ValueMember = "MaModel";
        }

        // Hàm này thay thế hoàn toàn cho nút Lọc cũ
        private void TaiDuLieuTuSQL()
        {
            try
            {
                DateTime tuNgay = datebatDauDT.Value.Date;
                DateTime denNgay = dateKetThucDT.Value.Date;

                // Tải dữ liệu từ SQL và lưu vào biến toàn cục
                dtDoanhThuHienTai = bcBUS.LayDuLieuDoanhThuLoiNhuan(tuNgay, denNgay);

                // Sau khi có dữ liệu mới, lập tức gọi hàm tính toán
                TinhToanVaHienThiDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. HÀM TÍNH TOÁN (Đã cập nhật chuẩn xác theo cấu trúc CSDL TechZone)
        private void TinhToanVaHienThiDoanhThu()
        {
            // Kiểm tra an toàn: Nếu chưa có dữ liệu hoặc chưa load xong combobox thì ngưng
            if (dtDoanhThuHienTai == null || cbbSanPham.SelectedValue == null) return;

            // Xử lý an toàn khi lấy Mã Model từ ComboBox (Tránh lỗi lúc Form vừa load)
            string maModelChon = "";
            if (cbbSanPham.SelectedValue is string)
            {
                maModelChon = cbbSanPham.SelectedValue.ToString();
            }
            else if (cbbSanPham.SelectedValue is DataRowView)
            {
                maModelChon = ((DataRowView)cbbSanPham.SelectedValue)["MaModel"].ToString();
            }
            else return; // Nếu không lấy được thì thoát

            // Sử dụng DataView để lọc dữ liệu trực tiếp trong bộ nhớ cực nhanh
            DataView dv = dtDoanhThuHienTai.DefaultView;

            // LỌC THEO COMBOBOX
            if (maModelChon == "ALL")
            {
                dv.RowFilter = ""; // Nếu chọn "Tất cả" -> Xóa bộ lọc
            }
            else
            {
                dv.RowFilter = $"MaModel = '{maModelChon}'"; // Lọc đúng cái Model khách chọn
            }

            // TÍNH TOÁN CỘNG DỒN
            decimal tongDoanhThu = 0;
            decimal tongChiPhi = 0;

            // Vòng lặp này chỉ chạy qua những dòng đã được lọc
            foreach (DataRowView rowView in dv)
            {
                DataRow row = rowView.Row;

                // Gọi đúng tên 2 cột mà chúng ta đã tính sẵn phép nhân bên SQL (Tầng DAL)
                if (row["TienDoanhThu"] != DBNull.Value)
                {
                    tongDoanhThu += Convert.ToDecimal(row["TienDoanhThu"]);
                }

                if (row["TienChiPhi"] != DBNull.Value)
                {
                    tongChiPhi += Convert.ToDecimal(row["TienChiPhi"]);
                }
            }

            // Tính lợi nhuận cuối cùng
            decimal tongLoiNhuan = tongDoanhThu - tongChiPhi;

            // HIỂN THỊ LÊN GIAO DIỆN
            lblTongDT.Text = tongDoanhThu.ToString("N0") + " VNĐ";
            lblTongChiPhi.Text = tongChiPhi.ToString("N0") + " VNĐ";
            lblTongLoiNhuan.Text = tongLoiNhuan.ToString("N0") + " VNĐ";

            // Trang trí thêm: Đổi màu cảnh báo nếu bán lỗ (Lợi nhuận âm)
            if (tongLoiNhuan < 0)
            {
                lblTongLoiNhuan.ForeColor = Color.Firebrick; // Đỏ đậm báo động
            }
            else
            {
                lblTongLoiNhuan.ForeColor = Color.ForestGreen; // Xanh lá mát mắt
            }
        }
        private void ThietLapNgayMacDinh()
        {
            // 1. Lấy thời gian hiện tại của hệ thống máy tính
            DateTime homNay = DateTime.Now;

            // 2. Tạo ra ngày mùng 1 của tháng và năm hiện tại
            // Cú pháp: new DateTime(Năm, Tháng, Ngày)
            DateTime ngayDauThang = new DateTime(homNay.Year, homNay.Month, 1);

            // 3. Gán lên giao diện
            datebatDauDT.Value = ngayDauThang; // Từ ngày: 01/...
            dateKetThucDT.Value = homNay;      // Đến ngày: Hôm nay

            dateNgayBatDauHX.Value = ngayDauThang;
            dateNgayketThucHX.Value = homNay;
        }

        private void datebatDauDT_ValueChanged(object sender, EventArgs e)
        {
            TaiDuLieuTuSQL();
        }

        private void dateKetThucDT_ValueChanged(object sender, EventArgs e)
        {
            TaiDuLieuTuSQL();
        }

        private void cbbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhToanVaHienThiDoanhThu();
        }
        // Hàm 1: Vẽ biểu đồ theo THÁNG
        private void VeBieuDoTheoThang()
        {
            DataTable dtThang = bcBUS.LayDoanhThuTheoThang();
            if (dtThang == null || dtThang.Rows.Count == 0) return;

            // 1. Xóa dữ liệu cũ trên biểu đồ (nếu có)
            chartTheoThang.Datasets.Clear();

            // 2. Tạo một bộ dữ liệu dạng Cột (Bar Chart)
            var dataset = new Guna.Charts.WinForms.GunaBarDataset();
            dataset.Label = "Doanh thu (VNĐ)";

            // 3. Đổ dữ liệu từ DataTable vào biểu đồ
            foreach (DataRow row in dtThang.Rows)
            {
                // Tạo nhãn trục X (Ví dụ: "T4/2026")
                string nhanThangNam = $"Tháng {row["Thang"]}/{row["Nam"]}";

                // Lấy số tiền trục Y
                double doanhThu = Convert.ToDouble(row["DoanhThu"]);

                // Thêm điểm dữ liệu vào dataset
                dataset.DataPoints.Add(nhanThangNam, doanhThu);
            }

            // 4. Đẩy dataset vào Chart và cập nhật giao diện
            chartTheoThang.Datasets.Add(dataset);
            chartTheoThang.Update();
        }

        // Hàm 2: Vẽ biểu đồ theo NĂM
        private void VeBieuDoTheoNam()
        {
            DataTable dtNam = bcBUS.LayDoanhThuTheoNam();
            if (dtNam == null || dtNam.Rows.Count == 0) return;

            // 1. Xóa dữ liệu cũ
            chartTheoNam.Datasets.Clear();

            // 2. Tạo bộ dữ liệu dạng Cột
            var dataset = new Guna.Charts.WinForms.GunaBarDataset();
            dataset.Label = "Doanh thu (VNĐ)";

            // 3. Đổ dữ liệu
            foreach (DataRow row in dtNam.Rows)
            {
                string nhanNam = $"Năm {row["Nam"]}"; // Trục X
                double doanhThu = Convert.ToDouble(row["DoanhThu"]); // Trục Y

                dataset.DataPoints.Add(nhanNam, doanhThu);
            }

            // 4. Cập nhật biểu đồ
            chartTheoNam.Datasets.Add(dataset);
            chartTheoNam.Update();
        }

        private void btnBaoCaoDoanhThu_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = datebatDauDT.Value;
                DateTime denNgay = dateKetThucDT.Value;

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi hàm TỔNG HỢP (Chỉ cần truyền 2 ngày)
                DataTable dtThongKe = bcBUS.LayThongKeDoanhThuTongHop(tuNgay, denNgay);

                if (dtThongKe != null && dtThongKe.Rows.Count > 0)
                {
                    frmInThongKe frmIn = new frmInThongKe(dtThongKe, tuNgay, denNgay);
                    frmIn.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu phát sinh trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
