using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class CTSanPhamBUS
    {
        CTSanPhamDAL ctDAL = new CTSanPhamDAL();
        public DataTable LayDanhSachSerial()
        {
            // Gọi DAL thực thi
            return ctDAL.LayDanhSachSerial();
        }
    }
}
