using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CTSanPhamDTO
    {
        string maSerial, maModel, trangThai;
        int maPO;
        DateTime ngayhetHangBH;

        public CTSanPhamDTO(string maSerial, string maModel, string trangThai, int maPO, DateTime ngayhetHangBH)
        {
            this.MaSerial = maSerial;
            this.MaModel = maModel;
            this.TrangThai = trangThai;
            this.MaPO = maPO;
            this.NgayhetHangBH = ngayhetHangBH;
        }

        CTSanPhamDTO() { }

        public string MaSerial { get => maSerial; set => maSerial = value; }
        public string MaModel { get => maModel; set => maModel = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public int MaPO { get => maPO; set => maPO = value; }
        public DateTime NgayhetHangBH { get => ngayhetHangBH; set => ngayhetHangBH = value; }
    }
}
