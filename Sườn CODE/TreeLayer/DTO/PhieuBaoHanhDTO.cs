using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuBaoHanhDTO
    {
        string maSerial, tinhTrangLoi, loaiBaoHanh, phuongAnXuLy, trangThai;
        int maNV, maPhieuBH;
        DateTime ngayTiepNhan, ngayHenTra;

        public PhieuBaoHanhDTO() { }
        public PhieuBaoHanhDTO(int maPhieuBH, string maSerial, int maNV, DateTime ngayTiepNhan, DateTime ngayHenTra, string tinhTrangLoi, string loaiBaoHanh, string phuongAnXuLy, string trangThai)
        {
            this.MaPhieuBH = maPhieuBH;
            this.MaSerial = maSerial;
            this.MaNV = maNV;
            this.NgayTiepNhan = ngayTiepNhan;
            this.NgayHenTra = ngayHenTra;
            this.TinhTrangLoi = tinhTrangLoi;
            this.LoaiBaoHanh = loaiBaoHanh;
            this.PhuongAnXuLy = phuongAnXuLy;
            this.TrangThai = trangThai;
        }

        public int MaPhieuBH { get => maPhieuBH; set => maPhieuBH = value; }
        public string MaSerial { get => maSerial; set => maSerial = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public DateTime NgayTiepNhan { get => ngayTiepNhan; set => ngayTiepNhan = value; }
        public DateTime NgayHenTra { get => ngayHenTra; set => ngayHenTra = value; }
        public string TinhTrangLoi { get => tinhTrangLoi; set => tinhTrangLoi = value; }
        public string LoaiBaoHanh { get => loaiBaoHanh; set => loaiBaoHanh = value; }
        public string PhuongAnXuLy { get => phuongAnXuLy; set => phuongAnXuLy = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
    }
}
