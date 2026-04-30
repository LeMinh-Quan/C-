using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DonDatHangDAL
    {
        private DataProvider db = new DataProvider();

        public int LuuDonHangMoi(int maNCC, int maNV, decimal tongTien, DataTable dtChiTiet)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. LƯU BẢNG CHA VÀ TÓM LẤY MÃ TỰ TĂNG
                        string sqlPO = @"INSERT INTO DON_DAT_HANG (MaNCC, MaNV, TongTien, TrangThai) 
                                         OUTPUT INSERTED.MaPO 
                                         VALUES (@mancc, @manv, @tong, N'Chờ giao')";

                        SqlCommand cmdPO = new SqlCommand(sqlPO, conn, trans);
                        cmdPO.Parameters.AddWithValue("@mancc", maNCC);
                        cmdPO.Parameters.AddWithValue("@manv", maNV);
                        cmdPO.Parameters.AddWithValue("@tong", tongTien);

                        int maPOMoi = (int)cmdPO.ExecuteScalar(); // Lấy mã số vừa tạo

                        // 2. LẶP QUA GIỎ HÀNG LƯU BẢNG CON
                        string sqlCT = @"INSERT INTO CHI_TIET_DDH (MaPO, MaModel, SoLuongDat, DonGiaNhap, TongTien) 
                                         VALUES (@mapo, @mamodel, @soluong, @dongia, @tongtien)";

                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                            cmdCT.Parameters.AddWithValue("@mapo", maPOMoi);
                            cmdCT.Parameters.AddWithValue("@mamodel", row["MaModel"]);
                            cmdCT.Parameters.AddWithValue("@soluong", row["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@dongia", row["DonGiaNhap"]);
                            cmdCT.Parameters.AddWithValue("@tongtien", row["ThanhTien"]); // Lấy cột ThanhTien từ C#

                            cmdCT.ExecuteNonQuery();
                        }

                        // 3. CHỐT LƯU
                        trans.Commit();
                        return maPOMoi; // Trả về mã đơn hàng cho Tầng BUS
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Lỗi chi tiết từ CSDL: " + ex.Message);
                    }
                }
            }
        }



        // 1. Lấy thông tin chung: Mã, Ngày, NCC (Tên, Đia chỉ, SĐT), Nhân viên
        public DataTable LayThongTinChungDonHang(int maPO)
        {
            // 🔥 ĐÃ THÊM: po.TrangThai vào dòng SELECT đầu tiên
            string sql = @"SELECT po.MaPO, po.NgayTao, po.NgayDuKienGiao, po.TongTien, po.TrangThai, po.GhiChu,
                          ncc.TenNCC, ncc.DiaChi, ncc.SDT, 
                          nv.HoTen  AS TenNV
                   FROM DON_DAT_HANG po
                   JOIN NHA_CUNG_CAP ncc ON po.MaNCC = ncc.MaNCC
                   JOIN NHAN_VIEN nv ON po.MaNV = nv.MaNV
                   WHERE po.MaPO = @mapo";

            SqlParameter[] pars = { new SqlParameter("@mapo", maPO) };
            return db.ExecuteQuery(sql, pars);
        }

        // 2. Lấy danh sách sản phẩm trong đơn
        public DataTable LayChiTietDonHang(int maPO)
        {
            string sql = @"SELECT ct.MaModel, sp.TenSanPham, ct.SoLuongDat, 
                          ct.DonGiaNhap, ct.TongTien
                   FROM CHI_TIET_DDH ct
                   JOIN SAN_PHAM sp ON ct.MaModel = sp.MaModel
                   WHERE ct.MaPO = @mapo";
            SqlParameter[] pars = { new SqlParameter("@mapo", maPO) };
            return db.ExecuteQuery(sql, pars);
        }

        // Thêm hàm này vào class DonDatHangDAL
        public DataTable LayDanhSachDonHang()
        {
            string sql = @"SELECT po.MaPO, po.NgayTao, po.NgayDuKienGiao, 
                          ncc.TenNCC, nv.HoTen AS TenNV, po.TongTien, po.TrangThai
                   FROM DON_DAT_HANG po
                   JOIN NHA_CUNG_CAP ncc ON po.MaNCC = ncc.MaNCC
                   JOIN NHAN_VIEN nv ON po.MaNV = nv.MaNV
                   ORDER BY po.MaPO DESC";

            return db.ExecuteQuery(sql);
        }



    }
}
