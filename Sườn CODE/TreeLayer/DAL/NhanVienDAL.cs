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
    public class NhanVienDAL
    {
        private DataProvider db = new DataProvider();

        public NhanVienDTO Login(string username, string password)
        {
            // Giả sử bảng của bạn tên là NhanVien, có cột TaiKhoan và MatKhau
            string query = "SELECT * FROM NHAN_VIEN WHERE Email = @user AND CCCD = @pass";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username),
                new SqlParameter("@pass", password)
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            // Nếu có dữ liệu trả về nghĩa là đăng nhập đúng
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                NhanVienDTO nv = new NhanVienDTO()
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen= row["HoTen"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    Cccd = row["CCCD"].ToString(),
                    Email = row["Email"].ToString(),
                    Sdt = row["SDT"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    HinhAnh = row["HinhAnh"].ToString(),
                    VaiTro= row["VaiTro"].ToString(),
                    NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                    NgayVaoLam = Convert.ToDateTime(row["NgayVaoLam"])

                };
                return nv; // Trả về thông tin nhân viên
            }
            return null; // Đăng nhập thất bại
        }

        public DataTable danhSachNhanVien()
        {
            string query = "SELECT * FROM NHAN_VIEN";
            DataTable dt = db.ExecuteQuery(query);
            return dt;

        }
        public bool ThemNhanVien(NhanVienDTO nv)
        {
            // Câu lệnh SQL (Nhớ kiểm tra lại tên các cột cho khớp với CSDL của bạn)
            string query = @"INSERT INTO NHAN_VIEN 
                    (HoTen, CCCD, Email, SDT, DiaChi, NgaySinh, NgayVaoLam, VaiTro, GioiTinh, TrangThai, HinhAnh) 
                    VALUES 
                    (@ten, @cccd, @email, @sdt, @diachi, @ngaysinh, @ngayvaolam, @vaitro, @gioitinh, @trangthai, @hinhanh)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ten", nv.HoTen),
                new SqlParameter("@cccd", nv.Cccd),
                new SqlParameter("@email", nv.Email),
                new SqlParameter("@sdt", nv.Sdt),
                new SqlParameter("@diachi", nv.DiaChi),
                new SqlParameter("@ngaysinh", nv.NgaySinh),
                new SqlParameter("@ngayvaolam", nv.NgayVaoLam),
                new SqlParameter("@vaitro", nv.VaiTro),
                new SqlParameter("@gioitinh", nv.GioiTinh),
                new SqlParameter("@trangthai", nv.TrangThai),
                // Nếu chưa làm phần ảnh, tạm thời truyền chuỗi rỗng
                new SqlParameter("@hinhanh", nv.HinhAnh ?? "")
            };

            // Thực thi lệnh. Nếu số dòng ảnh hưởng > 0 nghĩa là thêm thành công.
            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }



        public bool CapNhatNhanVien(NhanVienDTO nv)
        {
            string query = @"UPDATE NHAN_VIEN 
                     SET HoTen = @ten, CCCD = @cccd, Email = @email, 
                         SDT = @sdt, DiaChi = @diachi, NgaySinh = @ngaysinh, 
                         NgayVaoLam = @ngayvaolam, VaiTro = @vaitro, 
                         GioiTinh = @gioitinh, TrangThai = @trangthai, HinhAnh = @hinhanh
                     WHERE MaNV = @ma"; // QUAN TRỌNG NHẤT LÀ DÒNG NÀY

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ma", nv.MaNV), // Mã NV để tìm đúng người
        new SqlParameter("@ten", nv.HoTen),
        new SqlParameter("@cccd", nv.Cccd),
        new SqlParameter("@email", nv.Email),
        new SqlParameter("@sdt", nv.Sdt),
        new SqlParameter("@diachi", nv.DiaChi),
        new SqlParameter("@ngaysinh", nv.NgaySinh),
        new SqlParameter("@ngayvaolam", nv.NgayVaoLam),
        new SqlParameter("@vaitro", nv.VaiTro),
        new SqlParameter("@gioitinh", nv.GioiTinh),
        new SqlParameter("@trangthai", nv.TrangThai),
        new SqlParameter("@hinhanh", nv.HinhAnh ?? "")
            };

            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

    }
}
