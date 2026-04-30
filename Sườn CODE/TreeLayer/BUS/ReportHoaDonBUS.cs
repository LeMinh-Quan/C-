using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    
    public class ReportHoaDonBUS
    {
        private ReportHoaDonDAL rp = new ReportHoaDonDAL(); 

        public DataTable InHoaDonNhapHang(int maPo)
        {
            return rp.LayDuLieuInHoaDonNhap(maPo);
        }


        public DataTable InHoaDonGuiNhaCC(int maPhieuGui)
        {
            return rp.LayDuLieuInPhieuGuiBaoHanh(maPhieuGui);
        }

        public DataTable InHoaDonBanHang(int maHD)
        {
            return rp.LayDuLieuInHoaDonBanHang(maHD);
        }

        public DataTable InPhieuBaoHanh(int maPhieu)
        {
            return rp.LayDuLieuInPhieuBaoHanh(maPhieu);
        }

        public DataTable InPhieuBaoHanhKQ(int maPhieu)
        {
            return rp.LayDuLieuInPhieuTraBaoHanh(maPhieu);
        }
    }
}
