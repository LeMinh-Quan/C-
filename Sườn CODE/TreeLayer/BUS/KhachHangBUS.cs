using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class KhachHangBUS
    {
        KhachHangDAL khDAL = new KhachHangDAL();

        public DataTable LayKhachHangTheoSDT(string sdt)
        {
            return khDAL.LayKhachHangTheoSDT(sdt);
        }
    }
}
