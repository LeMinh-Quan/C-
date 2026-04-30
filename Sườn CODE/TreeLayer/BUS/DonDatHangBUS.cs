using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    
    public class DonDatHangBUS
    {
        private DonDatHangDAL poDAL = new DonDatHangDAL();

        // 2. Viết hàm nhận dữ liệu từ Giao diện (GUI) và truyền xuống DAL

        public int LuuDonDatHang(int maNCC, int maNV, decimal tongTien, DataTable dtChiTiet)
        {
            if (maNCC <= 0)
                throw new Exception("Mã Nhà cung cấp không hợp lệ!");

            if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
                throw new Exception("Đơn hàng đang trống. Vui lòng thêm sản phẩm!");

            if (tongTien <= 0)
                throw new Exception("Tổng tiền đơn hàng không hợp lệ!");

            // Trả thẳng kết quả (Mã PO) từ DAL lên
            return poDAL.LuuDonHangMoi(maNCC, maNV, tongTien, dtChiTiet);
        }

        public DataTable LayThongTinChung(int maPO)
        {
            return poDAL.LayThongTinChungDonHang(maPO);
        }

        public DataTable LayChiTiet(int maPO)
        {
            return poDAL.LayChiTietDonHang(maPO);
        }

        // Thêm hàm này vào class DonDatHangBUS
        public DataTable LayDanhSachDonHang()
        {
            return poDAL.LayDanhSachDonHang();
        }

    }
}
