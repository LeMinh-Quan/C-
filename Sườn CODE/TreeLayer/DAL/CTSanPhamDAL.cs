using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CTSanPhamDAL
    {
        private DataProvider db = new DataProvider();
        public DataTable LayDanhSachSerial()
        {
            // Câu lệnh SQL lấy chi tiết máy dựa theo MaModel
            string query = @"SELECT * 
                     FROM CHI_TIET_SAN_PHAM ";

            return db.ExecuteQuery(query); // Giả sử db là class kết nối của bạn
        }
    }
}
