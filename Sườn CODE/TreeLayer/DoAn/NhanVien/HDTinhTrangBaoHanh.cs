using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using Microsoft.Reporting.WinForms;

namespace DoAn.NhanVien
{
    public partial class HDTinhTrangBaoHanh : Form
    {
        private int _maPhieuBH;
        ReportHoaDonBUS rp = new ReportHoaDonBUS();

        public HDTinhTrangBaoHanh(int maPhieu)
        {
            InitializeComponent();
            _maPhieuBH = maPhieu;
        }

        private void HDTinhTrangBaoHang_Load(object sender, EventArgs e)
        {

            try
            {
                // 1. Lên BUS/DAL lấy dữ liệu
                DataTable dtPhieu = rp.InPhieuBaoHanh(_maPhieuBH);

                if (dtPhieu != null && dtPhieu.Rows.Count > 0)
                {
                    // 2. Chỉ định file thiết kế RDLC
                    reportViewer1.LocalReport.ReportEmbeddedResource = "DoAn.Report.PhieuBHMay.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    // 3. Đổ dữ liệu vào
                    ReportDataSource rds = new ReportDataSource("PhieuTinhTrangMay", dtPhieu);
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phiếu in: " + ex.Message);
            }
        }
    }
}
