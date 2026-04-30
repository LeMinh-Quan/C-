using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class NhapKhoBUS
    {

        private NhapKhoDAL nkDAL = new NhapKhoDAL();

        // =======================================================
        // 1. LẤY DANH SÁCH CÁC ĐƠN HÀNG CẦN NHẬP (CÒN CHỜ/THIẾU)
        // =======================================================
        public DataTable LayDanhSachPO_NhapKho()
        {
            return nkDAL.LayDanhSachPO_NhapKho();
        }

        // =======================================================
        // 2. LẤY CHI TIẾT SẢN PHẨM CẦN NHẬP CỦA PO ĐÓ
        // =======================================================
        public DataTable LayChiTietSP_NhapKho(int maPO)
        {
            if (maPO <= 0) return null;
            return nkDAL.LayChiTietSP_NhapKho(maPO);
        }

        // =======================================================
        // 3. KIỂM TRA NGHIỆP VỤ & LƯU PHIẾU NHẬP
        // =======================================================
        public bool LuuPhieuNhapKho(int maPO, string ghiChu, DataTable dtSerials)
        {
            // --- CÁC BƯỚC KIỂM TRA LOGIC NGHIỆP VỤ (BUSINESS RULES) ---

            // 1. Mã PO phải hợp lệ
            if (maPO <= 0)
                throw new Exception("Mã Đơn hàng (PO) không hợp lệ!");

            // 2. Lưới dữ liệu (Giỏ hàng tạm) không được rỗng
            if (dtSerials == null || dtSerials.Rows.Count == 0)
                throw new Exception("Chưa có mã Serial nào được quét. Không thể lưu kho!");

            // 3. (Tùy chọn nâng cao): Kiểm tra xem có Serial nào bị trùng trong SQL chưa?
            // Nếu hệ thống thực tế, bạn nên viết thêm 1 hàm KiemTraSerialTonTai() ở DAL và gọi ở đây.
            // Ví dụ: 
            // foreach(DataRow row in dtSerials.Rows)
            // {
            //     if (nkDAL.KiemTraSerialTonTai(row["MaSerial"].ToString()))
            //         throw new Exception($"Mã Serial {row["MaSerial"]} đã tồn tại trong hệ thống!");
            // }

            // --- NẾU QUA ĐƯỢC HẾT CÁC ẢI -> ĐẨY XUỐNG DAL ĐỂ LƯU VÀO DB ---
            return nkDAL.LuuPhieuNhapKho(maPO, ghiChu, dtSerials);
        }

        public bool KiemTraSerialTonTai(string maSerial)
        {
            if (string.IsNullOrWhiteSpace(maSerial)) return false;
            return nkDAL.KiemTraSerialTonTai(maSerial);
        }

    }
}
