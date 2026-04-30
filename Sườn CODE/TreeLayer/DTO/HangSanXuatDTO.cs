using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HangSanXuatDTO
    {
        int maHang;
        string tenHang;

        public HangSanXuatDTO() { }
        public HangSanXuatDTO(int maHang, string tenHang)
        {
            this.MaHang = maHang;
            this.TenHang = tenHang;
        }

        public int MaHang { get => maHang; set => maHang = value; }
        public string TenHang { get => tenHang; set => tenHang = value; }
    }
}
