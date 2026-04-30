using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CTDonDHDTTO
    {
        int maPO, soLuongDat, soLuongThucNhan;
        string maModel;
        decimal donGiaNhap, tongTien;

        public CTDonDHDTTO() { }

        public CTDonDHDTTO(int maPO, int soLuongDat, int soLuongThucNhan, string maModel, decimal donGiaNhap, decimal tongTien)
        {
            this.MaPO = maPO;
            this.SoLuongDat = soLuongDat;
            this.SoLuongThucNhan = soLuongThucNhan;
            this.MaModel = maModel;
            this.DonGiaNhap = donGiaNhap;
            this.TongTien = tongTien;
        }

        public int MaPO { get => maPO; set => maPO = value; }
        public int SoLuongDat { get => soLuongDat; set => soLuongDat = value; }
        public int SoLuongThucNhan { get => soLuongThucNhan; set => soLuongThucNhan = value; }
        public string MaModel { get => maModel; set => maModel = value; }
        public decimal DonGiaNhap { get => donGiaNhap; set => donGiaNhap = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }
    }
}
