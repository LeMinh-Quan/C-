using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CTHoaDonDTO
    {
        int iD_CTHD, maHD, soLuong;
        string maModel, maSerial;
        decimal donGiaBan;

        CTHoaDonDTO() { }
        public CTHoaDonDTO(int iD_CTHD, int maHD, string maModel, string maSerial, int soLuong, decimal donGiaBan)
        {
            this.ID_CTHD = iD_CTHD;
            this.MaHD = maHD;
            this.MaModel = maModel;
            this.MaSerial = maSerial;
            this.SoLuong = soLuong;
            this.DonGiaBan = donGiaBan;
        }

        public int ID_CTHD { get => iD_CTHD; set => iD_CTHD = value; }
        public int MaHD { get => maHD; set => maHD = value; }
        public string MaModel { get => maModel; set => maModel = value; }
        public string MaSerial { get => maSerial; set => maSerial = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public decimal DonGiaBan { get => donGiaBan; set => donGiaBan = value; }
    }
}
