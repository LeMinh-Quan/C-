using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BUS; // Đừng quên thư viện BUS của bạn
using Microsoft.Reporting.WinForms;

namespace DoAn.QuanLy
{
    public partial class HoaDonNH : Form
    {
        private int _maPO; // Biến lưu giữ mã đơn hàng
        ReportHoaDonBUS rpBus = new ReportHoaDonBUS();

        // ==============================================================
        // 1. KHỞI TẠO FORM (BẮT BUỘC NHẬN MÃ ĐƠN HÀNG)
        // ==============================================================
        public HoaDonNH(int maPO)
        {
            InitializeComponent();
            this._maPO = maPO;
        }

        private void HoaDonNH_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Gọi hàm lấy dữ liệu từ DB dựa vào Mã PO
                DataTable dtHoaDon = rpBus.InHoaDonNhapHang(_maPO);

                if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
                {
                    // 1. Quét toàn bộ các file đang được nhúng trong Project
                    reportViewer1.LocalReport.ReportEmbeddedResource = "DoAn.Report.hoaDonNhapHang.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    // 3. Truyền dữ liệu vào Report (Lưu ý: "DataSet1" là tên Dataset trong RDLC)
                    ReportDataSource rds = new ReportDataSource("NhapHang", dtHoaDon);
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