using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class NhaCungCapDAL
    {

        private DataProvider db = new DataProvider();

        public DataTable LayDanhSachNCC()
        {
            string query = "SELECT * FROM NHA_CUNG_CAP";
            return db.ExecuteQuery(query);
        }

        public bool ThemNhaCungCap(NhaCungCapDTO ncc)
        {
            string query = "INSERT INTO NHA_CUNG_CAP (TenNCC, SDT, DiaChi) VALUES (@ten, @sdt, @diachi)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@ten", ncc.TenNCC),
            new SqlParameter("@sdt", ncc.Sdt ),     
            new SqlParameter("@diachi", ncc.DiaChi)
            };

            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }



    }
}
