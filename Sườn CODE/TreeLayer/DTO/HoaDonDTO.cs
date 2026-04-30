using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDonDTO
    {
        int maHD, maNV;
        string sdtKhach, hinhThucThanhToan, trangThai, ghiChu;
        DateTime ngayLap;
        decimal tongTien;

        public HoaDonDTO(int maHD, int maNV, string sdtKhach, string hinhThucThanhToan, string trangThai, string ghiChu, DateTime ngayLap, decimal tongTien)
        {
            this.MaHD = maHD;
            this.MaNV = maNV;
            this.SdtKhach = sdtKhach;
            this.HinhThucThanhToan = hinhThucThanhToan;
            this.TrangThai = trangThai;
            this.GhiChu = ghiChu;
            this.NgayLap = ngayLap;
            this.TongTien = tongTien;
        }

        HoaDonDTO() { }

        public int MaHD { get => maHD; set => maHD = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public string SdtKhach { get => sdtKhach; set => sdtKhach = value; }
        public string HinhThucThanhToan { get => hinhThucThanhToan; set => hinhThucThanhToan = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public DateTime NgayLap { get => ngayLap; set => ngayLap = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }
    }
}
