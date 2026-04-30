using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVienDTO
    {
        int maNV;
        DateTime ngaySinh, ngayVaoLam;
        string hoTen, email, cccd, sdt, gioiTinh, diaChi, trangThai, vaiTro, hinhAnh;

        public NhanVienDTO() { }
        public NhanVienDTO(int maNV, DateTime ngaySinh, DateTime ngayVaoLam, string hoTen, string email, string cccd, string sdt, string gioiTinh, string diaChi, string trangThai, string vaiTro, string hinhAnh)
        {
            this.MaNV = maNV;
            this.NgaySinh = ngaySinh;
            this.NgayVaoLam = ngayVaoLam;
            this.HoTen = hoTen;
            this.Email = email;
            this.Cccd = cccd;
            this.Sdt = sdt;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.TrangThai = trangThai;
            this.VaiTro = vaiTro;
            this.HinhAnh = hinhAnh;
        }

        public int MaNV { get => maNV; set => maNV = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public DateTime NgayVaoLam { get => ngayVaoLam; set => ngayVaoLam = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Email { get => email; set => email = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string VaiTro { get => vaiTro; set => vaiTro = value; }
        public string HinhAnh { get => hinhAnh; set => hinhAnh = value; }
    }
}
