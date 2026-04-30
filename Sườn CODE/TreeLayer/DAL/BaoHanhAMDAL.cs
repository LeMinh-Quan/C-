using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaoHanhAMDAL
    {
        private DataProvider db = new DataProvider();
        //########################################################################################
        //---------------------------------- ADMIN ---------------------------------------------
        //########################################################################################

        public DataTable LayDanhSachBaoHanhTongHop()
        {
            // Lấy thông tin từ Phiếu BH, nối sang Chi tiết SP để lấy Mã Model, rồi nối sang Sản phẩm để lấy Tên máy
            string sql = @"
                        SELECT 
                            pbh.MaPhieuBH,
                            pbh.MaSerial,
                            sp.TenSanPham,
                            pbh.TinhTrangLoi,
                            pbh.PhuongAnDeXuat,
                            pbh.TrangThai,
                            pbh.NgayTiepNhan
                        FROM PHIEU_BAO_HANH pbh
                        JOIN CHI_TIET_SAN_PHAM ctsp ON pbh.MaSerial = ctsp.MaSerial
                        JOIN SAN_PHAM sp ON ctsp.MaModel = sp.MaModel
                        ORDER BY pbh.NgayTiepNhan DESC"; // Sắp xếp phiếu mới nhất lên đầu

            // Giả sử bạn có class Database/SqlHelper chứa hàm ExecuteQuery
            return db.ExecuteQuery(sql);
        }

        // 2. Lấy danh sách Nhà cung cấp
        public DataTable LayDanhSachNhaCungCap()
        {
            string sql = "SELECT MaNCC, TenNCC FROM NHA_CUNG_CAP";
            return db.ExecuteQuery(sql);
        }

        // 3. LƯU PHIẾU GỬI (DÙNG TRANSACTION)
        public bool LuuPhieuGuiNCC(int maNCC, int maAdmin, DateTime ngayGui, string ghiChu, int[] mangMaPhieuBH)
        {
            using (SqlConnection conn = db.GetConnection()) // Gọi GetConnection từ DataProvider
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Tạo phiếu gửi tổng
                    string sqlInsertPhieuGui = @"
                INSERT INTO PHIEU_GUI_NCC (MaNCC, MaNVLap, NgayGui, TrangThai, GhiChu) 
                OUTPUT INSERTED.MaPhieuGui
                VALUES (@MaNCC, @MaNVLap, @NgayGui, N'Đang gửi NCC', @GhiChu)";

                    SqlCommand cmdPhieuGui = new SqlCommand(sqlInsertPhieuGui, conn, transaction);
                    cmdPhieuGui.Parameters.AddRange(new SqlParameter[] {
                new SqlParameter("@MaNCC", maNCC),
                new SqlParameter("@MaNVLap", maAdmin),
                new SqlParameter("@NgayGui", ngayGui),
                new SqlParameter("@GhiChu",  string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu)
            });

                    int maPhieuGuiMoi = (int)cmdPhieuGui.ExecuteScalar();

                    // 2. Vòng lặp lưu chi tiết VÀ cập nhật trạng thái
                    for (int i = 0; i < mangMaPhieuBH.Length; i++)
                    {
                        int maPhieuBH = mangMaPhieuBH[i];

                        // A. Lấy Serial
                        string sqlGetSerial = "SELECT MaSerial FROM PHIEU_BAO_HANH WHERE MaPhieuBH = @MaPhieuBH";
                        SqlCommand cmdGetSerial = new SqlCommand(sqlGetSerial, conn, transaction);
                        cmdGetSerial.Parameters.Add(new SqlParameter("@MaPhieuBH", maPhieuBH));
                        string maSerial = cmdGetSerial.ExecuteScalar().ToString();

                        // B. Insert vào bảng Chi tiết phiếu gửi
                        string sqlInsertCT = "INSERT INTO CT_PHIEU_GUI_NCC (MaPhieuGui, MaPhieuBH, MaSerial) VALUES (@MaPhieuGui, @MaPhieuBH, @MaSerial)";
                        SqlCommand cmdInsertCT = new SqlCommand(sqlInsertCT, conn, transaction);
                        cmdInsertCT.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@MaPhieuGui", maPhieuGuiMoi),
                    new SqlParameter("@MaPhieuBH", maPhieuBH),
                    new SqlParameter("@MaSerial", maSerial)
                });
                        cmdInsertCT.ExecuteNonQuery();

                        // C. MỚI THÊM: Cập nhật trạng thái của PHIEU_BAO_HANH thành "Đang ở NCC"
                        string sqlUpdatePBH = "UPDATE PHIEU_BAO_HANH SET TrangThai = N'Đang ở NCC' WHERE MaPhieuBH = @MaPhieuBH";
                        SqlCommand cmdUpdatePBH = new SqlCommand(sqlUpdatePBH, conn, transaction);
                        cmdUpdatePBH.Parameters.Add(new SqlParameter("@MaPhieuBH", maPhieuBH));
                        cmdUpdatePBH.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi khi lưu phiếu gửi: " + ex.Message);
                }
            }
        }
        // LẤY DANH SÁCH PHIẾU GỬI NCC
        public DataTable LayDanhSachPhieuGuiNCC()
        {
            // Lấy thông tin phiếu gửi và JOIN với bảng NHA_CUNG_CAP để lấy tên Hãng
            // Sửa lại câu SQL trong DAL thành thế này:
            string sql = @"
                            SELECT 
                                pg.MaPhieuGui, 
                                ncc.TenNCC, 
                                nv.HoTen AS TenAdmin, -- ĐÂY! Join để lấy Họ Tên thay vì Mã
                                pg.NgayGui, 
                                pg.TrangThai, 
                                pg.GhiChu
                            FROM PHIEU_GUI_NCC pg
                            JOIN NHA_CUNG_CAP ncc ON pg.MaNCC = ncc.MaNCC
                            JOIN NHAN_VIEN nv ON pg.MaNVLap = nv.MaNV -- Nối với bảng Nhân viên
                            ORDER BY pg.NgayGui DESC";

            return db.ExecuteQuery(sql);
        }
        public DataTable LayChiTietPhieuGuiNCC(int maPhieuGui)
        {
            // Lấy từ bảng Chi tiết, nối sang bảng Sản phẩm để lấy Tên và Lỗi
            string sql = @"
                        SELECT 
                            ct.MaPhieuBH AS Ma, 
                            ct.MaSerial AS Seri, 
                            sp.TenSanPham AS Ten, 
                            pbh.TinhTrangLoi AS Loi
                        FROM CT_PHIEU_GUI_NCC ct
                        JOIN PHIEU_BAO_HANH pbh ON ct.MaPhieuBH = pbh.MaPhieuBH
                        JOIN CHI_TIET_SAN_PHAM ctsp ON pbh.MaSerial = ctsp.MaSerial
                        JOIN SAN_PHAM sp ON ctsp.MaModel = sp.MaModel
                        WHERE ct.MaPhieuGui = @ma";

            SqlParameter[] pars = { new SqlParameter("@ma", maPhieuGui) };
            return db.ExecuteQuery(sql, pars);
        }
        // Hàm cập nhật 1 Lô hàng (1 Phiếu Gửi + N Phiếu Bảo Hành bên trong)
        public bool CapNhatToanBoLohangVeKho(int maPhieuGui)
        {
            using (SqlConnection conn = db.GetConnection()) // Giả sử db là DataProvider
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. CẬP NHẬT PHIẾU GỬI TỔNG (Bảng Cha)
                    string sqlPG = @"UPDATE PHIEU_GUI_NCC 
                             SET TrangThai = N'NCC đã trả hàng', NgayVe = GETDATE() 
                             WHERE MaPhieuGui = @maPhieuGui";
                    SqlCommand cmdPG = new SqlCommand(sqlPG, conn, trans);
                    cmdPG.Parameters.AddWithValue("@maPhieuGui", maPhieuGui);
                    cmdPG.ExecuteNonQuery();

                    // 2. CẬP NHẬT TOÀN BỘ MÁY BÊN TRONG (Bảng Con)
                    // Lấy tất cả MaPhieuBH nằm trong cái MaPhieuGui này để chuyển thành 'Đã về kho'
                    string sqlBH = @"UPDATE PHIEU_BAO_HANH 
                             SET TrangThai = N'Đã về kho' 
                             WHERE MaPhieuBH IN (SELECT MaPhieuBH FROM CT_PHIEU_GUI_NCC WHERE MaPhieuGui = @maPhieuGui)";
                    SqlCommand cmdBH = new SqlCommand(sqlBH, conn, trans);
                    cmdBH.Parameters.AddWithValue("@maPhieuGui", maPhieuGui);
                    cmdBH.ExecuteNonQuery();

                    // 3. Cập nhật kết quả chung vào bảng Chi Tiết (Tuỳ chọn)
                    string sqlCT = @"UPDATE CT_PHIEU_GUI_NCC 
                             SET KetQuaTuNCC = N'Hãng đã xử lý xong', ChiPhiTuNCC = 0 
                             WHERE MaPhieuGui = @maPhieuGui";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                    cmdCT.Parameters.AddWithValue("@maPhieuGui", maPhieuGui);
                    cmdCT.ExecuteNonQuery();

                    trans.Commit(); // Hoàn tất 3 lệnh cùng lúc
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi SQL khi nhận lô hàng: " + ex.Message);
                }
            }
        }

        public int LayMaPhieuGuiMoiNhat(int maNV)
        {
            // SỬA Ở ĐÂY: Đổi WHERE MaNV thành WHERE MaNVLap
            string sql = "SELECT MAX(MaPhieuGui) FROM PHIEU_GUI_NCC WHERE MaNVLap = @MaNV";

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        //########################################################################################
        //---------------------------------- NHÂN VIÊN ---------------------------------------------
        //########################################################################################



        public int LapPhieuBaoHanh(string maSerial, int maNV, string loi, string vatLy, string phuongAn, string trangThaiPBH, string ghiChu)
        {
            using (SqlConnection conn = db.GetConnection()) // Mở kết nối
            {
                conn.Open();
                // Bắt đầu phiên giao dịch
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // =========================================================
                    // BƯỚC 1: LƯU PHIẾU BẢO HÀNH & LẤY MÃ PHIẾU
                    // =========================================================
                    string sqlInsert = @"
                INSERT INTO PHIEU_BAO_HANH (MaSerial, MaNV, NgayTiepNhan, TinhTrangLoi, TinhTrangVatLy, PhuongAnDeXuat, TrangThai, GhiChu)
                OUTPUT INSERTED.MaPhieuBH
                VALUES (@serial, @manv, GETDATE(), @loi, @vatly, @phuongan, @trangthai, @ghichu)";

                    SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, trans);
                    cmdInsert.Parameters.AddWithValue("@serial", maSerial);
                    cmdInsert.Parameters.AddWithValue("@manv", maNV);
                    cmdInsert.Parameters.AddWithValue("@loi", loi);
                    cmdInsert.Parameters.AddWithValue("@vatly", vatLy);
                    cmdInsert.Parameters.AddWithValue("@phuongan", phuongAn);
                    cmdInsert.Parameters.AddWithValue("@trangthai", trangThaiPBH);
                    cmdInsert.Parameters.AddWithValue("@ghichu", ghiChu ?? "");

                    // Lấy mã phiếu vừa tạo
                    int maPhieuMoi = (int)cmdInsert.ExecuteScalar();

                    // =========================================================
                    // BƯỚC 2: CẬP NHẬT TRẠNG THÁI CỦA MÁY TRONG KHO (ĐÃ SỬA)
                    // =========================================================
                    // - Nếu Xử lý tại chỗ -> Khách mang máy về luôn -> Trạng thái máy về lại "Đã bán"
                    // - Nếu Gửi trả NCC -> Máy lưu lại kho cửa hàng -> Trạng thái máy là "Đang bảo hành"
                    string trangThaiMay = (phuongAn == "Xử lý tại chỗ") ? "Bảo hành hoàn tất" : "Đang bảo hành";

                    string sqlUpdate = "UPDATE CHI_TIET_SAN_PHAM SET TrangThai = @trangthaiCTSP WHERE MaSerial = @serial";

                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@trangthaiCTSP", trangThaiMay);
                    cmdUpdate.Parameters.AddWithValue("@serial", maSerial);

                    // Thực thi lệnh Update
                    cmdUpdate.ExecuteNonQuery();

                    // =========================================================
                    // Nếu cả 2 lệnh đều chạy mượt mà -> Lưu vĩnh viễn (Commit)
                    trans.Commit();
                    return maPhieuMoi;
                }
                catch (Exception ex)
                {
                    // Lỗi đứt gánh giữa chừng -> Hủy bỏ toàn bộ (Rollback)
                    trans.Rollback();
                    throw new Exception("Lỗi hệ thống khi lưu phiếu bảo hành: " + ex.Message);
                }
            }
        }
        public DataTable LayDanhSachBaoHanh()
        {
            string sql = @"
                        SELECT 
                            PBH.MaPhieuBH, 
                            PBH.MaSerial, 
                            SP.TenSanPham, 
                            PBH.NgayTiepNhan, 
                            PBH.NgayTra, 
                            NV.HoTen AS TenNV, 
                            PBH.TinhTrangLoi, 
                            PBH.PhuongAnDeXuat, 
                            PBH.TrangThai, 
                            PBH.GhiChu
                        FROM PHIEU_BAO_HANH PBH
                        JOIN CHI_TIET_SAN_PHAM CTSP ON PBH.MaSerial = CTSP.MaSerial
                        JOIN SAN_PHAM SP ON CTSP.MaModel = SP.MaModel
                        JOIN NHAN_VIEN NV ON PBH.MaNV = NV.MaNV
                        ORDER BY PBH.NgayTiepNhan DESC"; // Sắp xếp phiếu mới nhất lên trên cùng

            return db.ExecuteQuery(sql);
        }



        public DataTable ChiTietPhieuBH(int maPhieuBH)
        {
            // Sử dụng cách cộng chuỗi trực tiếp maPhieuBH để đơn giản hóa
            // nhưng vẫn dùng LEFT JOIN để lấy đầy đủ thông tin khách hàng
            string sql = @"
                SELECT TOP 1
                    PBH.MaPhieuBH, 
                    PBH.NgayTiepNhan, 
                    NV.HoTen AS TenNhanVien,
                    KH.HoTen AS KhachHang, 
                    HD.SDT_Khach AS SDT,
                    SP.TenSanPham, 
                    PBH.MaSerial,
                    PBH.TinhTrangLoi,
                    PBH.TinhTrangVatLy,
                    ISNULL(CTNCC.KetQuaTuNCC, N'Chưa có thông tin / Xử lý tại chỗ') AS KetQua,
                    ISNULL(CTNCC.ChiPhiTuNCC, 0) AS ChiPhi
                FROM PHIEU_BAO_HANH PBH
                JOIN NHAN_VIEN NV ON PBH.MaNV = NV.MaNV
                JOIN CHI_TIET_SAN_PHAM CTSP ON PBH.MaSerial = CTSP.MaSerial
                JOIN SAN_PHAM SP ON CTSP.MaModel = SP.MaModel
                
                LEFT JOIN CHI_TIET_HOA_DON CTHD ON CTHD.MaSerial = PBH.MaSerial
                LEFT JOIN HOA_DON HD ON HD.MaHD = CTHD.MaHD
                LEFT JOIN KHACH_HANG KH ON HD.SDT_Khach = KH.SDT
                LEFT JOIN CT_PHIEU_GUI_NCC CTNCC ON CTNCC.MaPhieuBH = PBH.MaPhieuBH

                WHERE PBH.MaPhieuBH = " + maPhieuBH + @"
                ORDER BY HD.NgayLap DESC";

            // Trả về đúng kiểu DataTable bằng hàm quen thuộc của bạn
            return db.ExecuteQuery(sql);
        }


        public bool XacNhanTraMayChoKhach(int maPhieu, string ghiChuTra, string serial, string ketQua, decimal chiPhi)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. CẬP NHẬT PHIẾU BẢO HÀNH (Trạng thái -> Hoàn tất)
                    string sqlPhieu = @"
                UPDATE PHIEU_BAO_HANH 
                SET TrangThai = N'Hoàn tất', 
                    NgayTra = GETDATE(), 
                    GhiChu = ISNULL(GhiChu, '') + @ghiChu 
                WHERE MaPhieuBH = @maPhieu";

                    SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, conn, trans);
                    string ghiChuThem = string.IsNullOrEmpty(ghiChuTra) ? "" : " | Khách nhận máy: " + ghiChuTra;
                    cmdPhieu.Parameters.AddWithValue("@ghiChu", ghiChuThem);
                    cmdPhieu.Parameters.AddWithValue("@maPhieu", maPhieu);
                    cmdPhieu.ExecuteNonQuery();

                    // 2. CẬP NHẬT KHO (Trạng thái máy -> Bảo hành hoàn tất)
                    string sqlKho = "UPDATE CHI_TIET_SAN_PHAM SET TrangThai = N'Bảo hành hoàn tất' WHERE MaSerial = @serial";
                    SqlCommand cmdKho = new SqlCommand(sqlKho, conn, trans);
                    cmdKho.Parameters.AddWithValue("@serial", serial);
                    cmdKho.ExecuteNonQuery();

                    // 3. CẬP NHẬT KẾT QUẢ VÀ CHI PHÍ TỪ NCC (Đây là phần bạn đang thiếu)
                    // Vì MaPhieuBH là duy nhất trong CT_PHIEU_GUI_NCC, ta dùng nó làm điều kiện
                    string sqlNCC = @"
                UPDATE CT_PHIEU_GUI_NCC 
                SET KetQuaTuNCC = @ketQua, 
                    ChiPhiTuNCC = @chiPhi 
                WHERE MaPhieuBH = @maPhieu";

                    SqlCommand cmdNCC = new SqlCommand(sqlNCC, conn, trans);
                    cmdNCC.Parameters.AddWithValue("@ketQua", string.IsNullOrEmpty(ketQua) ? (object)DBNull.Value : ketQua);
                    cmdNCC.Parameters.AddWithValue("@chiPhi", chiPhi);
                    cmdNCC.Parameters.AddWithValue("@maPhieu", maPhieu);
                    cmdNCC.ExecuteNonQuery();

                    // Lưu vĩnh viễn tất cả thay đổi
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi hệ thống khi xác nhận trả máy: " + ex.Message);
                }
            }
        }
    } 
}
