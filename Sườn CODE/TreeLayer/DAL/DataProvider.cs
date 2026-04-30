using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataProvider
    {
        private SqlConnection conn;

        // Chuỗi kết nối của bạn
        private string connectionString =
            @"Data Source=LAPTOPCUALINH\MAY1;Initial Catalog=TechZone_DB2;Integrated Security=True";

        public DataProvider()
        {
            conn = new SqlConnection(connectionString);
        }

        // Mở kết nối
        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        // Đóng kết nối
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public SqlConnection GetConnection()
        {
            // KHÔNG dùng return conn; nữa
            // Mà hãy tạo mới và nhét chuỗi kết nối vào mỗi khi được gọi
            return new SqlConnection(connectionString);
        }

        /* ==========================================================
         * 1. CÁC HÀM SELECT (TRẢ VỀ DATATABLE)
         * ========================================================== */

        // 1.1 SELECT Không tham số (Dành cho truy vấn tĩnh như: SELECT * FROM NhanVien)
        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            finally
            {
                Close();
            }
            return dt;
        }

        // 1.2 SELECT Có tham số (Dành cho Tìm kiếm, Đăng nhập... - BẢO MẬT CAO)
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            finally
            {
                Close();
            }
            return dt;
        }

        /* ==========================================================
         * 2. CÁC HÀM INSERT, UPDATE, DELETE (TRẢ VỀ SỐ DÒNG BỊ ẢNH HƯỞNG)
         * ========================================================== */

        // 2.1 Thao tác Không tham số
        public int ExecuteNonQuery(string query)
        {
            int rows = 0;
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                rows = cmd.ExecuteNonQuery();
            }
            finally
            {
                Close();
            }
            return rows;
        }

        // 2.2 Thao tác Có tham số (Khuyên dùng cho mọi thao tác Thêm/Sửa/Xóa để tránh lỗi dấu nháy đơn)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            int rows = 0;
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                rows = cmd.ExecuteNonQuery();
            }
            finally
            {
                Close();
            }
            return rows;
        }

        /* ==========================================================
         * 3. CÁC HÀM TRẢ VỀ 1 GIÁ TRỊ DUY NHẤT (COUNT, SUM, MAX, MIN...)
         * ========================================================== */

        // 3.1 Không tham số
        public object ExecuteScalar(string query)
        {
            object result;
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                result = cmd.ExecuteScalar();
            }
            finally
            {
                Close();
            }
            return result;
        }

        // 3.2 Có tham số
        public object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            object result;
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                result = cmd.ExecuteScalar();
            }
            finally
            {
                Close();
            }
            return result;
        }
    }
}