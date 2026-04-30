using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class HoaDonBUS
    {

        HoaDonDAL hdDAL = new HoaDonDAL();

        public int LapHoaDonBanHang(string sdtKhach, string tenKhach, string diachi, string email, int maNV, string hinhThucTT, decimal tongTien, string ghiChu, DataTable dtDanhSachMay)
        {
            // 2. Cho phép đẩy xuống DAL xử lý
            return hdDAL.LapHoaDonBanHang(sdtKhach, tenKhach,diachi,email, maNV, hinhThucTT, tongTien, ghiChu, dtDanhSachMay);
        }

        public DataTable LayDanhSachHoaDon()
        {
            return hdDAL.LayDanhSachHoaDon();
        }   

        //########################################################################################
        //---------------------------------- NHÂN VIÊN ---------------------------------------------
        //########################################################################################
        public DataTable LayDanhSachHoaDonCT()
        {
            return hdDAL.LayDanhSachHoaDonDeBaoHanh();
        }

        public DataTable LayChiTietHoaDon(int maHD)
        {
            return hdDAL.LayChiTietHoaDon(maHD);
        }


    }
}
