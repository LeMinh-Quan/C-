using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhachHangDAL
    {
        DataProvider db = new DataProvider(); // Class kết nối CSDL của bạn

        public DataTable LayKhachHangTheoSDT(string sdt)
        {
            string sql = "SELECT * FROM KHACH_HANG WHERE SDT LIKE '%' + @sdt + '%'";

            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@sdt", sdt);

            return db.ExecuteQuery(sql, pars);
        }
    }
}
