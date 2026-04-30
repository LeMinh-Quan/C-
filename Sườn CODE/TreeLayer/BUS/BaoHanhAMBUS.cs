using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BaoHanhAMBUS
    {
        private BaoHanhAMDAL bhDAL = new BaoHanhAMDAL();
        //########################################################################################
        //---------------------------------- ADMIN ---------------------------------------------
        //########################################################################################

        // 1. Lấy toàn bộ danh sách để đổ lên DataGridView
        public DataTable LayDanhSachBaoHanhTH()
        {
            return bhDAL.LayDanhSachBaoHanhTongHop();
        }
        public DataTable LayDanhSachNhaCungCap()
        {
            return bhDAL.LayDanhSachNhaCungCap();
        }

        public bool TaoPhieuGuiNCC1(int maNCC, int maAdmin, DateTime ngayGui, string ghiChu, int[] mangMaPhieuBH)
        {
            // Nếu tất cả dữ liệu đều sạch và hợp lệ -> Đẩy xuống DAL thực hiện lưu (với Transaction)
            return bhDAL.LuuPhieuGuiNCC(maNCC, maAdmin, ngayGui, ghiChu, mangMaPhieuBH);
        }

        // Lấy danh sách phiếu gửi NCC đưa lên GUI
        public DataTable LayDanhSachPhieuGuiNCC()
        {
            return bhDAL.LayDanhSachPhieuGuiNCC();
        }
        public DataTable LayChiTietPhieuGuiNCC(int maPhieuGui)
        {
            return bhDAL.LayChiTietPhieuGuiNCC(maPhieuGui);

        }
        public bool CapNhatToanBoLohangVeKho(int maPhieuGui)
        {
            return bhDAL.CapNhatToanBoLohangVeKho(maPhieuGui);


        }

        public int LayMaPhieuGuiMoiNhat(int maNV)
        {
            // dalBaoHanh là tên biến DAL mà bạn đã khai báo sẵn trong file BUS này
            return bhDAL.LayMaPhieuGuiMoiNhat(maNV);
        }
        //########################################################################################
        //---------------------------------- NHÂN VIÊN---------------------------------------------
        //########################################################################################

        public int LapPhieuBaoHanh1(string maSerial, int maNV, string loi, string vatLy, string phuongAn, string trangThai, string ghiChu)
        {
            return bhDAL.LapPhieuBaoHanh(maSerial, maNV, loi, vatLy, phuongAn, trangThai, ghiChu);
        }

        public DataTable LayDanhSachBH()
        {
            return bhDAL.LayDanhSachBaoHanh();
        }



        public DataTable ChiTietPhieuBHTraKhach(int maPhieuBH)
        {
            return bhDAL.ChiTietPhieuBH(maPhieuBH);
        }

        public bool XacNhanTraMay(int maPhieu, string ghiChu, string serial, string ketQua, decimal chiPhi)
        {
            return bhDAL.XacNhanTraMayChoKhach(maPhieu, ghiChu, serial,ketQua,chiPhi);
        }

    }
}
