using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class SanPhamBUS
    {
        private SanPhamDAL spDAL = new SanPhamDAL();
        public bool ThemSanPhamMoi(SanPhamDTO sp)
        {
            return spDAL.ThemSanPham(sp);
        }

        public DataTable LayToanBoSanPham()
        {
            return spDAL.LayDanhSachSanPham();
        }

        public bool CapNhatThongTinSP(SanPhamDTO sp)
        {
            return spDAL.CapNhatSanPham(sp);
        }

        public DataTable LayDanhSachSPChoDonHang()
        {
            return spDAL.LayMaVaTenSanPham();
        }
        //-------------- LẬP HÓA ĐƠN NHÂN VIÊN---------------

        public DataTable TimMayTrongKhoTheoSerial(string serial)
        {
            return spDAL.TimMayTrongKhoTheoSerial(serial);
        }


    }
}
