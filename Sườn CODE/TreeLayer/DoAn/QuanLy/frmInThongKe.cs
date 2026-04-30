using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace DoAn.QuanLy
{
    public partial class frmInThongKe : Form
    {
        private DataTable _dtThongKe;
        private DateTime _tuNgay;
        private DateTime _denNgay;
        public frmInThongKe(DataTable dt, DateTime tuNgay, DateTime denNgay)
        {
            InitializeComponent();
            _dtThongKe = dt;
            _tuNgay = tuNgay;
            _denNgay = denNgay;
        }

        private void frmInThongKe_Load(object sender, EventArgs e)
        {

            try
            {
                // 1. Xóa hết dữ liệu cũ trong ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();

                // 2. Truyền 2 Parameter "Từ ngày" và "Đến ngày" vào 2 biểu thức <<Expr>> trên RDLC
                // Lưu ý: "paTuNgay" và "paDenNgay" phải GÕ CHÍNH XÁC tên Parameter bạn tạo trong RDLC
                ReportParameter[] p = new ReportParameter[2];
                p[0] = new ReportParameter("paTuNgay", _tuNgay.ToString("dd/MM/yyyy"));
                p[1] = new ReportParameter("paDenNgay", _denNgay.ToString("dd/MM/yyyy"));
                reportViewer1.LocalReport.SetParameters(p);

                // 3. Đổ DataTable vào Bảng trong RDLC
                // Lưu ý: "DataSetThongKe" phải GÕ CHÍNH XÁC tên DataSet bạn đã đặt trong file RDLC
                ReportDataSource rds = new ReportDataSource("DataSetThongKe", _dtThongKe);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // 4. Làm mới và hiển thị báo cáo
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bản in: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
