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
    public class SanPhamDAL
    {
        private DataProvider db = new DataProvider();
        public bool ThemSanPham(SanPhamDTO sp)
        {
            string query = @"INSERT INTO SAN_PHAM 
        (MaModel, MaHang, TenSanPham, PhanLoai, CPU, RAM, OCung, VGA, ManHinh, GiaBan, ThoiGianBaoHanh, HinhAnh, TrangThai, MoTa) 
        VALUES 
        (@ma, @mahang, @ten, @loai, @cpu, @ram, @ocung, @vga, @manhinh, @giaban, @baohanh, @hinhanh, @trangthai, @mota)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ma", sp.MaModel),
                new SqlParameter("@mahang", sp.MaHang),
                new SqlParameter("@ten", sp.TenSanPham),
                new SqlParameter("@loai", sp.PhanLoai ?? ""),
                new SqlParameter("@cpu", sp.Cpu ?? ""),
                new SqlParameter("@ram", sp.Ram ?? ""),
                new SqlParameter("@ocung", sp.OCung ?? ""),
                new SqlParameter("@vga", sp.Vga ?? ""),
                new SqlParameter("@manhinh", sp.ManHinh ?? ""),
                new SqlParameter("@giaban", sp.GiaBan),
                new SqlParameter("@baohanh", sp.ThoiGianBaoHanh),
                new SqlParameter("@hinhanh", sp.HinhAnh ?? ""),
                new SqlParameter("@trangthai", sp.TrangThai ?? ""),
                new SqlParameter("@mota", sp.MoTa ?? "")
            };

            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
        public DataTable LayDanhSachSanPham()
        {
            // Sử dụng LEFT JOIN kết hợp điều kiện TrangThai để lấy đúng số lượng tồn kho
            // Những sản phẩm hết hàng (không có serial nào 'Trong kho') sẽ đếm ra 0
            string query = @"
                SELECT 
                    sp.*, 
                    h.TenHang, 
                    COUNT(ct.MaSerial) AS TongSoLuong
                FROM SAN_PHAM sp
                INNER JOIN HANG_SAN_XUAT h ON sp.MaHang = h.MaHang
                LEFT JOIN CHI_TIET_SAN_PHAM ct ON sp.MaModel = ct.MaModel AND ct.TrangThai = N'Trong kho'
                GROUP BY 
                    sp.MaModel, sp.MaHang, sp.TenSanPham, sp.PhanLoai, 
                    sp.CPU, sp.RAM, sp.OCung, sp.VGA, sp.ManHinh, 
                    sp.GiaBan, sp.ThoiGianBaoHanh, sp.HinhAnh, 
                    sp.TrangThai, sp.MoTa, h.TenHang";

            return db.ExecuteQuery(query);
        }
        public bool CapNhatSanPham(SanPhamDTO sp)
        {
            string query = @"UPDATE SAN_PHAM SET 
                        MaHang = @mahang, 
                        TenSanPham = @ten, 
                        PhanLoai = @loai, 
                        CPU = @cpu, 
                        RAM = @ram, 
                        OCung = @ocung, 
                        VGA = @vga, 
                        ManHinh = @manhinh,
                        GiaBan = @giaban, 
                        ThoiGianBaoHanh = @baohanh, 
                        HinhAnh = @hinhanh, 
                        TrangThai = @trangthai, 
                        MoTa = @mota 
                        WHERE MaModel = @ma"; // Sửa dựa trên Mã Model

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ma", sp.MaModel),
        new SqlParameter("@mahang", sp.MaHang),
        new SqlParameter("@ten", sp.TenSanPham),
        new SqlParameter("@loai", sp.PhanLoai ?? ""),
        new SqlParameter("@cpu", sp.Cpu ?? ""),
        new SqlParameter("@ram", sp.Ram ?? ""),
        new SqlParameter("@ocung", sp.OCung ?? ""),
        new SqlParameter("@vga", sp.Vga ?? ""),
        new SqlParameter("@manhinh", sp.ManHinh ?? ""),
        new SqlParameter("@giaban", sp.GiaBan),
        new SqlParameter("@baohanh", sp.ThoiGianBaoHanh),
        new SqlParameter("@hinhanh", sp.HinhAnh ?? ""),
        new SqlParameter("@trangthai", sp.TrangThai ?? ""),
        new SqlParameter("@mota", sp.MoTa ?? "")
            };

            int result = db.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public DataTable LayMaVaTenSanPham()
        {
            // Mẹo UX: Ghép Mã Model và Tên lại để nhân viên dễ nhìn (VD: "MB01 - Macbook Pro")
            string query = "SELECT MaModel, (MaModel + ' - ' + TenSanPham) AS TenHienThi FROM SAN_PHAM WHERE TrangThai = N'Đang kinh doanh'";
            return db.ExecuteQuery(query);
        }


        //-------------- LẬP HÓA ĐƠN NHÂN VIÊN---------------
        // Hàm dò tìm thông tin máy qua mã Serial
        public DataTable TimMayTrongKhoTheoSerial(string serial)
        {
            // JOIN 2 bảng để lấy vừa lấy được Serial cụ thể, vừa lấy được Tên và Giá của Model đó
            string sql = @"
                SELECT ct.MaSerial, sp.MaModel, sp.TenSanPham, sp.GiaBan 
                FROM CHI_TIET_SAN_PHAM ct 
                JOIN SAN_PHAM sp ON ct.MaModel = sp.MaModel 
                WHERE ct.MaSerial = @serial AND ct.TrangThai = N'Trong kho'";

            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@serial", serial);

            return db.ExecuteQuery(sql, pars);
        }


    }
}
