using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HangSanXuatDAL
    {
        private DataProvider db = new DataProvider();
        public DataTable LayDanhSachHang()
        {
            string query = "SELECT * FROM HANG_SAN_XUAT";
            return db.ExecuteQuery(query); // Giả sử 'db' là lớp kết nối của bạn
        }

        public bool ThemHangSanXuat(string tenHang)
        {
            // Câu lệnh SQL thêm dữ liệu (MaHang tự tăng nên không cần chèn)
            string query = "INSERT INTO HANG_SAN_XUAT (TenHang) VALUES (@tenhang)";

            // Truyền tham số để chống lỗi gõ nháy đơn (') và chống Hack SQL Injection
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@tenhang", tenHang)
            };

            // Thực thi lệnh và kiểm tra xem có dòng nào được thêm không
            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }


        public bool KiemTraTenHangTonTai(string tenHang)
        {
            // Lệnh đếm số lượng bản ghi có Tên Hãng trùng với chữ người dùng nhập
            string query = "SELECT COUNT(*) FROM HANG_SAN_XUAT WHERE TenHang = @tenhang";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@tenhang", tenHang)
            };

            // Dùng ExecuteScalar để lấy về 1 giá trị duy nhất (kết quả của hàm COUNT)
            // Lưu ý: db là class DataProvider của bạn. Nếu bạn chưa có hàm ExecuteScalar thì báo mình nhé!
            int soLuong = Convert.ToInt32(db.ExecuteScalar(query, parameters));

            // Nếu soLuong > 0 tức là đã có ít nhất 1 hãng bị trùng tên
            return soLuong > 0;
        }
    }
}
