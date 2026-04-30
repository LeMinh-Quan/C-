using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonDatHangDTO
    {
        int maNCC, maPO, maNV;
        string  trangThai, ghiChu;
        DateTime ngayTao, ngayDuKienGiao;
        decimal tongTien;

        public DonDatHangDTO(int maPO, int maNV,int maNCC, string trangThai, string ghiChu, DateTime ngayTao,  decimal tongTien)
        {
            this.MaPO = maPO;
            this.MaNV = maNV;
            this.MaNCC = maNCC;
            this.TrangThai = trangThai;
            this.GhiChu = ghiChu;
            this.NgayTao = ngayTao;
            this.TongTien = tongTien;
        }

         public DonDatHangDTO() { }

        public int MaPO { get => maPO; set => maPO = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public int MaNCC { get => maNCC; set => maNCC = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }
    }
}
