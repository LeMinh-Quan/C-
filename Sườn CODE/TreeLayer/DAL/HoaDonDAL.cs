using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HoaDonDAL
    {

        DataProvider db = new DataProvider(); // Class kết nối CSDL của bạn

        // Hàm lập hóa đơn trả về Mã Hóa Đơn (int)
        public int LapHoaDonBanHang(string sdtKhach, string tenKhach, string diachi, string email, int maNV, string hinhThucTT, decimal tongTien, string ghiChu, DataTable dtDanhSachMay)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // TẠO BIẾN CHỐT THỜI GIAN NGAY TẠI ĐÂY ĐỂ DÙNG CHUNG CHO CẢ HÓA ĐƠN & BẢO HÀNH
                    DateTime mienThoiGianChot = DateTime.Now;

                    // =========================================================
                    // BƯỚC 1: XỬ LÝ KHÁCH HÀNG (THÊM MỚI NẾU LÀ KHÁCH VÃNG LAI)
                    // =========================================================
                    string sqlCheckKH = "SELECT COUNT(*) FROM KHACH_HANG WHERE SDT = @sdt";
                    SqlCommand cmdCheckKH = new SqlCommand(sqlCheckKH, conn, trans);
                    cmdCheckKH.Parameters.AddWithValue("@sdt", sdtKhach);

                    if ((int)cmdCheckKH.ExecuteScalar() == 0)
                    {
                        string sqlInsertKH = "INSERT INTO KHACH_HANG(SDT, HoTen, DiaChi, Email) VALUES(@sdt, @ten, @diachi, @email)";
                        SqlCommand cmdKH = new SqlCommand(sqlInsertKH, conn, trans);
                        cmdKH.Parameters.AddWithValue("@sdt", sdtKhach);
                        cmdKH.Parameters.AddWithValue("@ten", tenKhach);
                        cmdKH.Parameters.AddWithValue("@diachi", diachi);
                        cmdKH.Parameters.AddWithValue("@email", email);
                        cmdKH.ExecuteNonQuery();
                    }

                    // =========================================================
                    // BƯỚC 2: TẠO HÓA ĐƠN TỔNG & LẤY MÃ HÓA ĐƠN
                    // =========================================================
                    string sqlHD = @"
                INSERT INTO HOA_DON(SDT_Khach, MaNV, NgayLap, HinhThucThanhToan, TongTien, TrangThai, GhiChu) 
                OUTPUT INSERTED.MaHD 
                VALUES(@sdt, @manv, @ngaylap, @httt, @tongTien, N'Hoàn thành', @ghiChu)"; // Dùng @ngaylap thay vì GETDATE()

                    SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                    cmdHD.Parameters.AddWithValue("@sdt", sdtKhach);
                    cmdHD.Parameters.AddWithValue("@manv", maNV);
                    cmdHD.Parameters.AddWithValue("@ngaylap", mienThoiGianChot); // Truyền mốc thời gian vào
                    cmdHD.Parameters.AddWithValue("@httt", hinhThucTT);
                    cmdHD.Parameters.AddWithValue("@tongTien", tongTien);
                    cmdHD.Parameters.AddWithValue("@ghiChu", ghiChu ?? "");

                    int maHDMoi = (int)cmdHD.ExecuteScalar();

                    // =========================================================
                    // BƯỚC 3: LƯU CHI TIẾT & XUẤT KHO KÍCH HOẠT BẢO HÀNH
                    // =========================================================
                    foreach (DataRow row in dtDanhSachMay.Rows)
                    {
                        string maModel = row["MaModel"].ToString();
                        string maSerial = row["MaSerial"].ToString();
                        decimal donGia = Convert.ToDecimal(row["DonGiaBan"]);

                        // 3.1 Thêm chi tiết hóa đơn
                        string sqlCTHD = @"
                    INSERT INTO CHI_TIET_HOA_DON(MaHD, MaModel, MaSerial, SoLuong, DonGiaBan) 
                    VALUES(@mahd, @mamodel, @maserial, 1, @dongia)";
                        SqlCommand cmdCTHD = new SqlCommand(sqlCTHD, conn, trans);
                        cmdCTHD.Parameters.AddWithValue("@mahd", maHDMoi);
                        cmdCTHD.Parameters.AddWithValue("@mamodel", maModel);
                        cmdCTHD.Parameters.AddWithValue("@maserial", maSerial);
                        cmdCTHD.Parameters.AddWithValue("@dongia", donGia);
                        cmdCTHD.ExecuteNonQuery();

                        // 3.2 Xuất kho và Kích hoạt bảo hành (Dùng chung @ngaylap)
                        string sqlXuatKho = @"
                    UPDATE CHI_TIET_SAN_PHAM 
                    SET TrangThai = N'Đã bán',
                        NgayHetHanBaoHanh = DATEADD(MONTH, (SELECT ThoiGianBaoHanh FROM SAN_PHAM WHERE MaModel = @mamodel), @ngaylap)
                    WHERE MaSerial = @maserial";
                        SqlCommand cmdXuatKho = new SqlCommand(sqlXuatKho, conn, trans);
                        cmdXuatKho.Parameters.AddWithValue("@mamodel", maModel);
                        cmdXuatKho.Parameters.AddWithValue("@maserial", maSerial);
                        cmdXuatKho.Parameters.AddWithValue("@ngaylap", mienThoiGianChot); // Truyền chung mốc thời gian của hóa đơn
                        cmdXuatKho.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return maHDMoi;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi CSDL khi lập hóa đơn: " + ex.Message);
                }
            }
        }

        public DataTable LayDanhSachHoaDon()
        {
            // Dùng LEFT JOIN để lôi được Tên Nhân Viên và Tên Khách Hàng lên cho đẹp
            string sql = @"
                        SELECT 
                            HD.MaHD, 
                            HD.NgayLap, 
                            NV.HoTen AS TenNV, 
                            ISNULL(KH.HoTen, N'Khách lẻ') AS TenKH, 
                            HD.SDT_Khach AS SDT,
                            HD.TongTien, 
                            HD.HinhThucThanhToan, 
                            HD.TrangThai, 
                            HD.GhiChu
                        FROM HOA_DON HD
                        LEFT JOIN NHAN_VIEN NV ON HD.MaNV = NV.MaNV
                        LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT
                        ORDER BY HD.NgayLap DESC"; // Xếp hóa đơn mới nhất lên đầu
            return db.ExecuteQuery(sql);
        }

        //########################################################################################
        //---------------------------------- NHÂN VIÊN ---------------------------------------------
        //########################################################################################
        // 1. Hàm lấy danh sách tổng hợp tất cả hóa đơn
        public DataTable LayDanhSachHoaDonDeBaoHanh()
        {
            string sql = @"
                        SELECT 
                            HD.MaHD, 
                            CT.MaModel,
                            CT.MaSerial, 
                            SP.TenSanPham, 
                            HD.NgayLap, 
                            NV.HoTen AS TenNV, 
                            ISNULL(KH.HoTen, N'Khách lẻ') AS TenKH, 
                            HD.SDT_Khach AS SDT,
                            CT.DonGiaBan,
                            HD.HinhThucThanhToan,
                            CTSP.NgayHetHanBaoHanh,
                            HD.GhiChu,
                            CTSP.TrangThai
                        FROM HOA_DON HD
                        JOIN CHI_TIET_HOA_DON CT ON HD.MaHD = CT.MaHD
                        JOIN CHI_TIET_SAN_PHAM CTSP ON CTSP.MaSerial = CT.MaSerial
                        JOIN SAN_PHAM SP ON CT.MaModel = SP.MaModel
                        LEFT JOIN NHAN_VIEN NV ON HD.MaNV = NV.MaNV
                        LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT
        
                        -- MỚI: Sắp xếp thông minh bằng CASE WHEN
                        ORDER BY 
                            CASE 
                                WHEN CTSP.TrangThai IN (N'Đang bảo hành', N'Hàng lỗi') THEN 1
                                ELSE 0                                       
                            END ASC,
                            HD.NgayLap DESC"; 

    return db.ExecuteQuery(sql);


        }
        // 2. Hàm lấy chi tiết các máy đã bán trong 1 hóa đơn cụ thể
        public DataTable LayChiTietHoaDon(int maHD)
        {
            string sql = @"
                        SELECT 
                            SP.TenSanPham AS Ten, 
                            CT.MaSerial AS Seri, 
                            CT.DonGiaBan AS Gia
                        FROM CHI_TIET_HOA_DON CT
                        JOIN SAN_PHAM SP ON CT.MaModel = SP.MaModel
                        WHERE CT.MaHD = @mahd";
            SqlParameter[] pars = { new SqlParameter("@mahd", maHD) };
            return db.ExecuteQuery(sql, pars);
        }
    }
}
