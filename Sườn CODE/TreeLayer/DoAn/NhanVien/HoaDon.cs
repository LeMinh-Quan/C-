using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using Microsoft.Reporting.WinForms;

namespace DoAn.NhanVien
{
    public partial class HoaDon : Form
    {
        private int _maHD;
        ReportHoaDonBUS rpBus = new ReportHoaDonBUS();
        public HoaDon(int maHD)
        {
            InitializeComponent();
            this._maHD = maHD;

        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Gọi BUS/DAL lấy dữ liệu
                DataTable dtHoaDon = rpBus.InHoaDonBanHang(_maHD);

                if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
                {
                    // 2. Ép đường dẫn file RDLC
                    reportViewer1.LocalReport.ReportEmbeddedResource = "DoAn.Report.HoaDonBanHang.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    // 3. Gắn dữ liệu ("HoaDonBanHang" là tên Dataset trong RDLC)
                    ReportDataSource rds = new ReportDataSource("HoaDonBanHang", dtHoaDon);
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }
    }
}
