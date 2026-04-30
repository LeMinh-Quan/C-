using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BUS;
using Microsoft.Reporting.WinForms;

namespace DoAn.QuanLy
{
    public partial class HoaDonBaoHanh : Form
    {
        private int _maPhieuGui;
        ReportHoaDonBUS rpBus = new ReportHoaDonBUS();
        public HoaDonBaoHanh(int maPhieu)
        {
            InitializeComponent();
            this._maPhieuGui = maPhieu;

        }

        private void HoaDonBaoHanh_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Gọi hàm lấy dữ liệu từ DB dựa vào Mã PO
                DataTable dtHoaDon = rpBus.InHoaDonGuiNhaCC(_maPhieuGui);

                if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
                {
                    // 1. Quét toàn bộ các file đang được nhúng trong Project
                    reportViewer1.LocalReport.ReportEmbeddedResource = "DoAn.Report.HoaDonGuiNHCC.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    // 3. Truyền dữ liệu vào Report (Lưu ý: "DataSet1" là tên Dataset trong RDLC)
                    ReportDataSource rds = new ReportDataSource("HDGuiNCC", dtHoaDon);
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    // 4. Hiển thị
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho hóa đơn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phiếu in: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}