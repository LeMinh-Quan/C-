using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPhamDTO
    {
        string maModel, tenSanPham, phanLoai, cpu, ram, oCung, vga, manHinh, hinhAnh, trangThai, moTa;
        int maHang, thoiGianBaoHanh;
        decimal giaNhap, giaBan;

        public SanPhamDTO() { }
        public SanPhamDTO(string maModel, int maHang, string tenSanPham, string phanLoai, string cpu, string ram, string oCung, string vga, string manHinh,  decimal giaBan, int thoiGianBaoHanh, string hinhAnh, string trangThai, string moTa)
        {
            this.MaModel = maModel;
            this.MaHang = maHang;
            this.TenSanPham = tenSanPham;
            this.PhanLoai = phanLoai;
            this.Cpu = cpu;
            this.Ram = ram;
            this.OCung = oCung;
            this.Vga = vga;
            this.ManHinh = manHinh;
            this.GiaBan = giaBan;
            this.ThoiGianBaoHanh = thoiGianBaoHanh;
            this.HinhAnh = hinhAnh;
            this.TrangThai = trangThai;
            this.MoTa = moTa;
        }

        public string MaModel { get => maModel; set => maModel = value; }
        public int MaHang { get => maHang; set => maHang = value; }
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }
        public string Cpu { get => cpu; set => cpu = value; }
        public string Ram { get => ram; set => ram = value; }
        public string OCung { get => oCung; set => oCung = value; }
        public string Vga { get => vga; set => vga = value; }
        public string ManHinh { get => manHinh; set => manHinh = value; }
        public decimal GiaBan { get => giaBan; set => giaBan = value; }
        public int ThoiGianBaoHanh { get => thoiGianBaoHanh; set => thoiGianBaoHanh = value; }
        public string HinhAnh { get => hinhAnh; set => hinhAnh = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string MoTa { get => moTa; set => moTa = value; }
    }
}
