using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReportHoaDonDAL
    {
        private DataProvider db = new DataProvider();

        //------------ NHẬP HÀNG ---------------------
        public DataTable LayDuLieuInHoaDonNhap(int maPO)
        {
            string sql = @"
                        SELECT 
                            PO.MaPO, 
                            PO.NgayTao, 
                            PO.TongTien,
                            NV.HoTen AS TenNhanVien, 
                            NCC.TenNCC AS TenNhaCungCap, 
                            SP.TenSanPham, 
                            CT.SoLuongDat, 
                            CT.DonGiaNhap, 
                            (CT.SoLuongDat * CT.DonGiaNhap) AS ThanhTien
                        FROM DON_DAT_HANG PO
                        JOIN NHAN_VIEN NV ON PO.MaNV = NV.MaNV
                        JOIN NHA_CUNG_CAP NCC ON PO.MaNCC = NCC.MaNCC
                        JOIN CHI_TIET_DDH CT ON PO.MaPO = CT.MaPO
                        JOIN SAN_PHAM SP ON CT.MaModel = SP.MaModel
                        WHERE PO.MaPO = @MaPO";

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPO", maPO);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // -------------- hóa đơn gửi nhà cung cấp------------------
        public DataTable LayDuLieuInPhieuGuiBaoHanh(int maPhieuGui)
        {
            string sql = @"
                    SELECT 
                        PG.MaPhieuGui AS MaPhieu,
                        GETDATE() AS NgayGui,
                        PG.GhiChu AS [GhiChu],
                        NCC.TenNCC AS NCC,
                        NV.HoTen AS NhanVien,
                        CT.MaPhieuBH AS MaBH, 
                        CT.MaSerial AS Serial,  
                        SP.TenSanPham AS TenSp,
                        BH.TinhTrangLoi AS TinhTrang 
                    FROM PHIEU_GUI_NCC PG
                    JOIN NHA_CUNG_CAP NCC ON PG.MaNCC = NCC.MaNCC
                    JOIN NHAN_VIEN NV ON PG.MaNVLap = NV.MaNV
                    JOIN CT_PHIEU_GUI_NCC CT ON PG.MaPhieuGui = CT.MaPhieuGui
                    JOIN PHIEU_BAO_HANH BH ON CT.MaPhieuBH = BH.MaPhieuBH
                    JOIN CHI_TIET_SAN_PHAM CTSP ON CT.MaSerial = CTSP.MaSerial
                    JOIN SAN_PHAM SP ON CTSP.MaModel = SP.MaModel
                    WHERE PG.MaPhieuGui = @MaPhieuGui";
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieuGui", maPhieuGui);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // --------------- hóa đơn bán hàng
        public DataTable LayDuLieuInHoaDonBanHang(int maHD)
        {
            // Bám sát CSDL: Lấy SoLuong, DonGiaBan và tính toán ThanhTien ngay trong SQL
            string sql = @"
                            SELECT 
                                HD.MaHD,
                                HD.NgayLap,
                                NV.HoTen AS TenNhanVien,
                                ISNULL(KH.HoTen, N'Khách vãng lai') AS TenKhachHang,
                                ISNULL(KH.SDT, '') AS SDT_Khach,
                                SP.TenSanPham,
                                CT.MaSerial,
                                CT.SoLuong,
                                CT.DonGiaBan AS DonGia,
                                (CT.SoLuong * CT.DonGiaBan) AS ThanhTien, -- Tính thành tiền cho từng dòng
                                HD.TongTien,
                                HD.GhiChu
                            FROM HOA_DON HD
                            JOIN NHAN_VIEN NV ON HD.MaNV = NV.MaNV
                            LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT 
                            JOIN CHI_TIET_HOA_DON CT ON HD.MaHD = CT.MaHD
                            JOIN SAN_PHAM SP ON CT.MaModel = SP.MaModel
                            WHERE HD.MaHD = @MaHD";

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        // phiếu bảo hành
        public DataTable LayDuLieuInPhieuBaoHanh(int maPhieuBH)
        {
            string sql = @"
                    SELECT TOP 1
                        PBH.MaPhieuBH,
                        PBH.NgayTiepNhan,
                        NV.HoTen AS TenNhanVien,
                        ISNULL(KH.HoTen, N'Khách vãng lai') AS TenKhachHang,
            
                        -- Che 5 số giữa của SĐT giống Hóa Đơn Bán Hàng
                        CASE 
                            WHEN LEN(KH.SDT) >= 10 THEN LEFT(KH.SDT, 3) + '*****' + RIGHT(KH.SDT, 2)
                            ELSE ISNULL(KH.SDT, '') 
                        END AS SDT_Khach,
            
                        SP.TenSanPham,
                        CTSP.NgayHetHanBaoHanh,
                        CTSP.MaModel,
                        PBH.MaSerial,
                        PBH.TinhTrangLoi,
                        PBH.TinhTrangVatLy,
                        PBH.PhuongAnDeXuat,
                        PBH.GhiChu
                    FROM PHIEU_BAO_HANH PBH
                    JOIN NHAN_VIEN NV ON PBH.MaNV = NV.MaNV
                    JOIN CHI_TIET_SAN_PHAM CTSP ON PBH.MaSerial = CTSP.MaSerial
                    JOIN SAN_PHAM SP ON CTSP.MaModel = SP.MaModel
                    -- Bắt cầu qua Hóa Đơn để lấy thông tin Khách Hàng đã mua máy này
                    LEFT JOIN CHI_TIET_HOA_DON CTHD ON CTSP.MaSerial = CTHD.MaSerial
                    LEFT JOIN HOA_DON HD ON CTHD.MaHD = HD.MaHD
                    LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT
                    WHERE PBH.MaPhieuBH = @MaPhieuBH
                    ORDER BY HD.NgayLap DESC"; 

            using (SqlConnection conn = db.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaPhieuBH", maPhieuBH);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
        }


        public DataTable LayDuLieuInPhieuTraBaoHanh(int maPhieuBH)
        {
            string sql = @"
                        SELECT TOP 1
                            PBH.MaPhieuBH,
                            GETDATE() AS NgayTra, -- Ngày in phiếu là ngày trả máy
                            NV.HoTen AS TenNhanVien,
                            ISNULL(KH.HoTen, N'Khách vãng lai') AS TenKhachHang,
            
                            -- Che 5 số giữa SĐT, nếu không có thì để chữ 'Không có'
                            CASE 
                                WHEN LEN(KH.SDT) >= 10 THEN LEFT(KH.SDT, 3) + '*****' + RIGHT(KH.SDT, 2)
                                ELSE ISNULL(KH.SDT, N'Không có') 
                            END AS SDT,
            
                            SP.TenSanPham,
                            PBH.MaSerial AS MaMay,
                            PBH.TinhTrangLoi,
                            PBH.TinhTrangVatLy, -- Lấy thêm cột này cho mẫu mới
           
                            CTNCC.KetQuaTuNCC AS KetQuaTuHang,
                            ISNULL(CTNCC.ChiPhiTuNCC, 0) AS ChiPhi
            
                        FROM PHIEU_BAO_HANH PBH
                        JOIN NHAN_VIEN NV ON PBH.MaNV = NV.MaNV
                        JOIN CHI_TIET_SAN_PHAM CTSP ON PBH.MaSerial = CTSP.MaSerial
                        JOIN SAN_PHAM SP ON CTSP.MaModel = SP.MaModel
                        LEFT JOIN CT_PHIEU_GUI_NCC CTNCC ON PBH.MaPhieuBH = CTNCC.MaPhieuBH
                        LEFT JOIN CHI_TIET_HOA_DON CTHD ON CTSP.MaSerial = CTHD.MaSerial
                        LEFT JOIN HOA_DON HD ON CTHD.MaHD = HD.MaHD
                        LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT
                        WHERE PBH.MaPhieuBH = @MaPhieuBH
                        ORDER BY HD.NgayLap DESC";

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieuBH", maPhieuBH);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
