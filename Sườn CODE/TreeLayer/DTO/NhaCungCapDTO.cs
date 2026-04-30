using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhaCungCapDTO
    {
        string  tenNCC, sdt, diaChi;
        int maNCC;
        public NhaCungCapDTO() { }
        public NhaCungCapDTO(int maNCC, string tenNCC, string sdt, string diaChi)
        {
            this.MaNCC = maNCC;
            this.TenNCC = tenNCC;
            this.Sdt = sdt;
            this.DiaChi = diaChi;
        }

        public int MaNCC { get => maNCC; set => maNCC = value; }
        public string TenNCC { get => tenNCC; set => tenNCC = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
