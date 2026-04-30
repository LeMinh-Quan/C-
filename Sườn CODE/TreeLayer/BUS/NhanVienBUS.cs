using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAL nvDAL = new NhanVienDAL();

        // Đổi kiểu trả về từ DataTable thành NhanVienDTO
        public NhanVienDTO DangNhap(string username, string password)
        {
            return nvDAL.Login(username, password);
        }

        public DataTable LayDanhSachNhanVien()
        {
            // nvDAL là đối tượng NhanVienDAL bạn đã khởi tạo ở đầu class BUS
            return nvDAL.danhSachNhanVien();
        }

        public bool ThemNhanVienMoi(NhanVienDTO nv)
        {
            return nvDAL.ThemNhanVien(nv);
        }

        public bool CapNhatThongTin(NhanVienDTO nv)
        {
            return nvDAL.CapNhatNhanVien(nv);
        }
    }
}
