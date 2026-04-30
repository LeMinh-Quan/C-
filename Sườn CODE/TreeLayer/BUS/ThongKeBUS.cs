using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class ThongKeBUS
    {
        ThongKeDAL bcDAL = new ThongKeDAL();
        public DataTable LayThongKeTonKhoTongHop()
        {
            return bcDAL.LayThongKeTonKhoTongHop();
        }
        public DataTable LayDanhSachHoaDonTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay.Date > denNgay.Date)
            {
                throw new Exception("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
            }
            return bcDAL.LayDanhSachHoaDonTheoNgay(tuNgay, denNgay);
        }
        public DataTable LayTatCaHoaDon()
        {
            return bcDAL.LayTatCaHoaDon();
        }
        public DataTable LayThongTinChungHoaDon(int maHD)
        {
            return bcDAL.LayThongTinChungHoaDon(maHD);
        }

        public DataTable LayDanhSachChiTietHoaDon(int maHD)
        {
            return bcDAL.LayDanhSachChiTietHoaDon(maHD);
        }
        //###############################################################################
        //----------------------- TABPAGE 3----------------------------------------------
        //###############################################################################
        public DataTable LayDanhSachSanPhamChoCombo()
        {
            return bcDAL.LayDanhSachSanPhamChoCombo();
        }

        public DataTable LayDuLieuDoanhThuLoiNhuan(DateTime tuNgay, DateTime denNgay)
        {
            return bcDAL.LayDuLieuDoanhThuLoiNhuan(tuNgay, denNgay);
        }
        public DataTable LayDoanhThuTheoThang()
        {
            return bcDAL.LayDoanhThuTheoThang();
        }

        public DataTable LayDoanhThuTheoNam()
        {
            return bcDAL.LayDoanhThuTheoNam();
        }

        public DataTable LayThongKeDoanhThuTongHop(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay > denNgay)
            {
                throw new Exception("Lỗi nghiệp vụ: Ngày bắt đầu không thể lớn hơn ngày kết thúc.");
            }
            return bcDAL.LayThongKeDoanhThuTongHop(tuNgay, denNgay);
        }

    }
}
