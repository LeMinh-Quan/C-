using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class NhapKhoDAL
    {
        private DataProvider db = new DataProvider();

        // 1. LẤY DANH SÁCH PO CẦN NHẬP (Chỉ lấy Chờ giao và Giao thiếu)
        public DataTable LayDanhSachPO_NhapKho()
        {
            string sql = @"SELECT MaPO, NgayTao, TrangThai 
                           FROM DON_DAT_HANG 
                           WHERE TrangThai IN (N'Chờ giao', N'Giao thiếu') 
                           ORDER BY MaPO DESC";
            return db.ExecuteQuery(sql);
        }

        // 2. LẤY CHI TIẾT SẢN PHẨM CẦN NHẬP TRONG PO
        public DataTable LayChiTietSP_NhapKho(int maPO)
        {
            // Lấy ra (Số lượng đặt - Số lượng đã nhận) = Số lượng CÒN LẠI cần nhập đợt này
            string sql = @"SELECT ct.MaModel, sp.TenSanPham, 
                                  (ct.SoLuongDat - ct.SoLuongThucNhan) AS SoLuongCanNhap 
                           FROM CHI_TIET_DDH ct
                           JOIN SAN_PHAM sp ON ct.MaModel = sp.MaModel
                           WHERE ct.MaPO = @mapo AND (ct.SoLuongDat - ct.SoLuongThucNhan) > 0";

            SqlParameter[] pars = { new SqlParameter("@mapo", maPO) };
            return db.ExecuteQuery(sql, pars);
        }

        // 3. XỬ LÝ LƯU KHO (TRANSACTION KHÉP KÍN)
        public bool LuuPhieuNhapKho(int maPO, string ghiChu, DataTable dtSerials)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // A. Lưu từng Serial và Tăng số lượng thực nhận lên 1
                        string sqlInsertSerial = "INSERT INTO CHI_TIET_SAN_PHAM (MaSerial, MaModel, MaPO, NgayNhapKho, TrangThai) VALUES (@serial, @model, @mapo, GETDATE(), N'Trong kho')";
                        string sqlUpdateThucNhan = "UPDATE CHI_TIET_DDH SET SoLuongThucNhan = SoLuongThucNhan + 1 WHERE MaPO = @mapo AND MaModel = @model";

                        foreach (DataRow row in dtSerials.Rows)
                        {
                            // Lưu Serial
                            SqlCommand cmdCT = new SqlCommand(sqlInsertSerial, conn, trans);
                            cmdCT.Parameters.AddWithValue("@serial", row["MaSerial"]);
                            cmdCT.Parameters.AddWithValue("@model", row["MaModel"]);
                            cmdCT.Parameters.AddWithValue("@mapo", maPO);
                            cmdCT.ExecuteNonQuery();

                            // Cộng dồn số lượng đã nhận vào bảng ChiTiet_DDH
                            SqlCommand cmdCtyQty = new SqlCommand(sqlUpdateThucNhan, conn, trans);
                            cmdCtyQty.Parameters.AddWithValue("@mapo", maPO);
                            cmdCtyQty.Parameters.AddWithValue("@model", row["MaModel"]);
                            cmdCtyQty.ExecuteNonQuery();
                        }

                        // B. Kiểm tra xem PO này đã nhập đủ TOÀN BỘ sản phẩm chưa
                        string sqlCheckHoanTat = "SELECT SUM(SoLuongDat - SoLuongThucNhan) FROM CHI_TIET_DDH WHERE MaPO = @mapo";
                        SqlCommand cmdCheck = new SqlCommand(sqlCheckHoanTat, conn, trans);
                        cmdCheck.Parameters.AddWithValue("@mapo", maPO);
                        int soLuongThieu = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        string trangThaiMoi = (soLuongThieu == 0) ? "Hoàn tất" : "Giao thiếu";

                        // C. Cập nhật lại Trạng thái PO
                        string sqlUpdatePO = "UPDATE DON_DAT_HANG SET TrangThai = @tt, NgayDuKienGiao = GETDATE(), GhiChu = @gc WHERE MaPO = @mapo";
                        SqlCommand cmdPO = new SqlCommand(sqlUpdatePO, conn, trans);
                        cmdPO.Parameters.AddWithValue("@tt", trangThaiMoi);
                        cmdPO.Parameters.AddWithValue("@gc", ghiChu ?? (object)DBNull.Value);
                        cmdPO.Parameters.AddWithValue("@mapo", maPO);
                        cmdPO.ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        trans.Rollback();
                        // Lỗi 2627 hoặc 2601 là mã lỗi "Trùng khóa chính" của SQL Server
                        if (ex.Number == 2627 || ex.Number == 2601)
                        {
                            throw new Exception("Có mã Serial bị trùng lặp với dữ liệu đã có trong kho! Vui lòng kiểm tra lại.");
                        }
                        throw new Exception("Lỗi Database: " + ex.Message);
                    }
                }
            }
        }
        // Kiểm tra xem Serial đã tồn tại trong CSDL chưa
        public bool KiemTraSerialTonTai(string maSerial)
        {
            string sql = "SELECT COUNT(*) FROM CHI_TIET_SAN_PHAM WHERE MaSerial = @serial";
            SqlParameter[] pars = { new SqlParameter("@serial", maSerial) };

            int count = Convert.ToInt32(db.ExecuteScalar(sql, pars));
            return count > 0; // Nếu > 0 tức là đã tồn tại
        }

    }
}
