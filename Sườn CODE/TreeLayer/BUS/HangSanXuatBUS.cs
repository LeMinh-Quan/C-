using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class HangSanXuatBUS
    {
        HangSanXuatDAL hangDAL = new HangSanXuatDAL();

        public DataTable LayToanBoHang()
        {
            return hangDAL.LayDanhSachHang();
        }
        public bool ThemHangMoi(string tenHang)
        {
            return hangDAL.ThemHangSanXuat(tenHang);
        }
        public bool KiemTraTenHangTonTai(string tenHang)
        {
            return hangDAL.KiemTraTenHangTonTai(tenHang);
        }


    }
}
