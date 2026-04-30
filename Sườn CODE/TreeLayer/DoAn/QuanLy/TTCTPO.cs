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

namespace DoAn.QuanLy
{
    public partial class TTCTPO : Form
    {
        private int maPO_HienTai;
        private DonDatHangBUS poBUS = new DonDatHangBUS();
        public TTCTPO(int maPO)
        {
            this.maPO_HienTai = maPO;
            this.Text = "Chi Tiết Đơn Đặt Hàng - PO" + maPO;
            InitializeComponent();
        }

        private void TTCTPO_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
            LoadThongTinChung();
        }
        private void LoadThongTinChung()
        {
            try
            {
                DataTable dt = poBUS.LayThongTinChung(maPO_HienTai);

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];

                    lblMaPO.Text = "PO" + r["MaPO"].ToString();
                    lblNhaCungCap.Text = r["TenNCC"].ToString();
                    lblNhanVien.Text = r["TenNV"].ToString();
                    lblGhiChu.Text = r["GhiChu"].ToString();
                    lblNgayTao.Text = Convert.ToDateTime(r["NgayTao"]).ToString("dd/MM/yyyy HH:mm");

                    if (r["NgayDuKienGiao"] != DBNull.Value)
                        lblNgayGiao.Text = Convert.ToDateTime(r["NgayDuKienGiao"]).ToString("dd/MM/yyyy");
                    else
                        lblNgayGiao.Text = "Chưa giao";

                    string trangThai = r["TrangThai"].ToString();
                    lblTrangThai.Text = trangThai;
                    if (trangThai == "Hoàn tất") lblTrangThai.ForeColor = Color.SeaGreen;
                    else if (trangThai == "Giao thiếu") lblTrangThai.ForeColor = Color.Firebrick;
                    else lblTrangThai.ForeColor = Color.DarkOrange;

                    decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                    lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin chung: " + ex.Message);
            }
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                DataTable dtSP = poBUS.LayChiTiet(maPO_HienTai);
                dgvChiTietSP.DataSource = dtSP;

                dgvChiTietSP.Columns["MaModel"].HeaderText = "Mã Model";
                dgvChiTietSP.Columns["TenSanPham"].HeaderText = "Tên Dòng Laptop";

                dgvChiTietSP.Columns["SoLuongDat"].HeaderText = "SL Yêu Cầu";
                dgvChiTietSP.Columns["SoLuongDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvChiTietSP.Columns["DonGiaNhap"].HeaderText = "Đơn Giá (VNĐ)";
                dgvChiTietSP.Columns["DonGiaNhap"].DefaultCellStyle.Format = "N0";
                dgvChiTietSP.Columns["DonGiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvChiTietSP.Columns["TongTien"].HeaderText = "Thành Tiền (VNĐ)";
                dgvChiTietSP.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvChiTietSP.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
