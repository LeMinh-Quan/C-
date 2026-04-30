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
    public class NhaCungCapBUS
    {
        NhaCungCapDAL nccDAL = new NhaCungCapDAL();
        public DataTable LayToanBoNCC()
        {
            return nccDAL.LayDanhSachNCC();
        }
        public bool ThemNCCMoi(NhaCungCapDTO ncc)
        {
            return nccDAL.ThemNhaCungCap(ncc);
        }

    }
}
