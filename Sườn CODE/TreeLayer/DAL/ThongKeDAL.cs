using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThongKeDAL
    {

        private DataProvider db = new DataProvider();

        //thống kê hàng tồn kho
        public DataTable LayThongKeTonKhoTongHop()
        {
            // Chúng ta lấy thông tin từ bảng SAN_PHAM và JOIN với bảng CHI_TIET_SAN_PHAM để đếm
            string sql = @"SELECT sp.MaModel, sp.TenSanPham, sp.PhanLoai, 
                          COUNT(ct.MaSerial) AS SoLuongTon
                   FROM SAN_PHAM sp
                   LEFT JOIN CHI_TIET_SAN_PHAM ct ON sp.MaModel = ct.MaModel
                   WHERE ct.TrangThai = N'Trong kho'
                   GROUP BY sp.MaModel, sp.TenSanPham, sp.PhanLoai
                   ORDER BY SoLuongTon DESC";

            return db.ExecuteQuery(sql);
        }

        // Lấy danh sách hóa đơn theo khoảng thời gian
        public DataTable LayDanhSachHoaDonTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"SELECT hd.MaHD, hd.NgayLap, nv.HoTen AS TenNhanVien, 
                                  hd.SDT_Khach, kh.HoTen AS TenKhachHang, 
                                  hd.HinhThucThanhToan, hd.TongTien, hd.TrangThai
                           FROM HOA_DON hd
                           JOIN NHAN_VIEN nv ON hd.MaNV = nv.MaNV
                           LEFT JOIN KHACH_HANG kh ON hd.SDT_Khach = kh.SDT
                           WHERE CAST(hd.NgayLap AS DATE) >= @TuNgay 
                             AND CAST(hd.NgayLap AS DATE) <= @DenNgay
                           ORDER BY hd.NgayLap DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };

            return db.ExecuteQuery(sql, pars);
        }
        // hiệu xuất ( xử lý phần bán hàng)

        // Lấy TẤT CẢ hóa đơn (Lúc mới mở Form)
        public DataTable LayTatCaHoaDon()
        {
            string sql = @"SELECT hd.MaHD, hd.NgayLap, nv.HoTen AS TenNhanVien, 
                          hd.SDT_Khach, kh.HoTen AS TenKhachHang, 
                          hd.HinhThucThanhToan, hd.TongTien, hd.TrangThai
                   FROM HOA_DON hd
                   JOIN NHAN_VIEN nv ON hd.MaNV = nv.MaNV
                   LEFT JOIN KHACH_HANG kh ON hd.SDT_Khach = kh.SDT
                   ORDER BY hd.NgayLap DESC";

            return db.ExecuteQuery(sql);
        }
        // 1. Lấy thông tin chung của Hóa Đơn và Khách Hàng
        public DataTable LayThongTinChungHoaDon(int maHD)
        {
            string sql = @"SELECT hd.MaHD, hd.NgayLap, nv.HoTen AS TenNhanVien, 
                                  hd.HinhThucThanhToan, hd.TrangThai, hd.TongTien,
                                  kh.HoTen AS TenKhachHang, hd.SDT_Khach 
                           FROM HOA_DON hd
                           JOIN NHAN_VIEN nv ON hd.MaNV = nv.MaNV
                           LEFT JOIN KHACH_HANG kh ON hd.SDT_Khach = kh.SDT
                           WHERE hd.MaHD = @maHD";

            SqlParameter[] pars = { new SqlParameter("@maHD", maHD) };
            return db.ExecuteQuery(sql, pars);
        }

        // 2. Lấy danh sách sản phẩm (Serial) chi tiết trong Hóa Đơn
        public DataTable LayDanhSachChiTietHoaDon(int maHD)
        {
            string sql = @"SELECT ct.MaSerial, sp.TenSanPham, sp.PhanLoai, sp.GiaBan
                           FROM CHI_TIET_HOA_DON ct
                           JOIN CHI_TIET_SAN_PHAM ctsp ON ct.MaSerial = ctsp.MaSerial
                           JOIN SAN_PHAM sp ON ctsp.MaModel = sp.MaModel
                           WHERE ct.MaHD = @maHD";

            SqlParameter[] pars = { new SqlParameter("@maHD", maHD) };
            return db.ExecuteQuery(sql, pars);
        }
        //###############################################################################
        //----------------------- TABPAGE 3----------------------------------------------
        //###############################################################################

        // 1. Lấy danh sách Sản phẩm đổ vào ComboBox
        public DataTable LayDanhSachSanPhamChoCombo()
        {
            string sql = "SELECT MaModel, TenSanPham FROM SAN_PHAM";
            return db.ExecuteQuery(sql);
        }

        // 2. Lấy dữ liệu Doanh thu & Chi phí theo Khoảng thời gian
        public DataTable LayDuLieuDoanhThuLoiNhuan(DateTime tuNgay, DateTime denNgay)
        {
            // Tính sẵn Doanh Thu (Giá bán * SL) và Chi Phí (Giá nhập * SL) ngay từ SQL
            string sql = @"
                        SELECT 
                            hd.NgayLap, 
                            ct.MaModel, 
                            (ct.SoLuong * ct.DonGiaBan) AS TienDoanhThu, 
                            (ct.SoLuong * ISNULL(ctd.DonGiaNhap, 0)) AS TienChiPhi
                        FROM HOA_DON hd
                        JOIN CHI_TIET_HOA_DON ct ON hd.MaHD = ct.MaHD
                        LEFT JOIN CHI_TIET_SAN_PHAM ctsp ON ct.MaSerial = ctsp.MaSerial
                        LEFT JOIN CHI_TIET_DDH ctd 
                            ON ctsp.MaPO = ctd.MaPO 
                            AND ctsp.MaModel = ctd.MaModel   -- sửa chỗ này
                        WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                        AND hd.TrangThai = N'Hoàn thành'";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };

            return db.ExecuteQuery(sql, pars);
        }

        // 1. Lấy Doanh thu theo từng Tháng (Kết hợp cả Năm để không bị trùng tháng của năm khác)
        public DataTable LayDoanhThuTheoThang()
        {
            string sql = @"SELECT 
                        YEAR(hd.NgayLap) AS Nam, 
                        MONTH(hd.NgayLap) AS Thang, 
                        SUM(ct.SoLuong * ct.DonGiaBan) AS DoanhThu
                   FROM HOA_DON hd
                   JOIN CHI_TIET_HOA_DON ct ON hd.MaHD = ct.MaHD
                   WHERE hd.TrangThai = N'Hoàn thành'
                   GROUP BY YEAR(hd.NgayLap), MONTH(hd.NgayLap)
                   ORDER BY Nam ASC, Thang ASC"; // Sắp xếp theo thời gian tăng dần
            return db.ExecuteQuery(sql);
        }

        // 2. Lấy Doanh thu theo từng Năm
        public DataTable LayDoanhThuTheoNam()
        {
            string sql = @"SELECT 
                        YEAR(hd.NgayLap) AS Nam, 
                        SUM(ct.SoLuong * ct.DonGiaBan) AS DoanhThu
                   FROM HOA_DON hd
                   JOIN CHI_TIET_HOA_DON ct ON hd.MaHD = ct.MaHD
                   WHERE hd.TrangThai = N'Hoàn thành'
                   GROUP BY YEAR(hd.NgayLap)
                   ORDER BY Nam ASC";
            return db.ExecuteQuery(sql);
        }
        // Đổi tên hàm cho đúng ý nghĩa: Lấy doanh thu tổng hợp (tất cả sản phẩm)
        public DataTable LayThongKeDoanhThuTongHop(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                            SELECT 
                                SP.MaModel,
                                SP.TenSanPham,
                                SUM(CT.SoLuong * CT.DonGiaBan) AS TongDoanhThu,
                                SUM(CT.SoLuong * ISNULL(CTDDH.DonGiaNhap, 0)) AS TongChiPhi,
                                SUM(CT.SoLuong * CT.DonGiaBan) - SUM(CT.SoLuong * ISNULL(CTDDH.DonGiaNhap, 0)) AS TongLoiNhuan
                            FROM SAN_PHAM SP
                            JOIN CHI_TIET_HOA_DON CT ON SP.MaModel = CT.MaModel
                            JOIN HOA_DON HD ON CT.MaHD = HD.MaHD
                            LEFT JOIN CHI_TIET_SAN_PHAM CTSP ON CT.MaSerial = CTSP.MaSerial
                            LEFT JOIN CHI_TIET_DDH CTDDH ON CTSP.MaPO = CTDDH.MaPO AND CTSP.MaModel = CTDDH.MaModel
        
                            -- Chỉ còn giữ lại điều kiện lọc theo Ngày và Trạng thái
                            WHERE CAST(HD.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                              AND HD.TrangThai = N'Hoàn thành'
                            GROUP BY SP.MaModel, SP.TenSanPham";

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
