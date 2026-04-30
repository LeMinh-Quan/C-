-- =============================================
-- TẠO DATABASE
-- =============================================
CREATE DATABASE TechZone_DB;
GO

USE TechZone_DB;
GO


-- =============================================
-- 1. NHÂN VIÊN
-- =============================================
CREATE TABLE NHAN_VIEN (
    MaNV INT IDENTITY(1000,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    Email VARCHAR(50) NOT NULL UNIQUE,
    CCCD VARCHAR(20) NOT NULL UNIQUE,
    SDT VARCHAR(20) NOT NULL,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(100),
    NgayVaoLam DATE DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) CHECK(TrangThai IN (N'Đang làm việc',N'Đã nghỉ')),
    VaiTro NVARCHAR(20) CHECK (VaiTro IN (N'Quản lý', N'Nhân viên')),
    HinhAnh NVARCHAR(MAX)
);
GO

-- =============================================
-- 2. HÃNG SẢN XUẤT
-- =============================================
CREATE TABLE HANG_SAN_XUAT (
    MaHang INT IDENTITY(1,1) PRIMARY KEY,
    TenHang NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- =============================================
-- 3. SẢN PHẨM
-- =============================================
CREATE TABLE SAN_PHAM (
    MaModel VARCHAR(50) PRIMARY KEY,
    MaHang INT NOT NULL,
    TenSanPham NVARCHAR(255) NOT NULL,
    PhanLoai NVARCHAR(100) NOT NULL,
    CPU NVARCHAR(100) NOT NULL,
    RAM NVARCHAR(100) NOT NULL,
    OCung NVARCHAR(100) NOT NULL,
    VGA NVARCHAR(100) NOT NULL,
    ManHinh NVARCHAR(100) NOT NULL,
    GiaBan DECIMAL(18,2) NOT NULL CHECK (GiaBan >= 0),
    ThoiGianBaoHanh INT NOT NULL CHECK (ThoiGianBaoHanh >= 0),
    HinhAnh NVARCHAR(MAX),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Đang kinh doanh',N'Ngừng bán')),
    MoTa NVARCHAR(MAX)
);
GO

-- =============================================
-- 4. CHI TIẾT SẢN PHẨM (SERIAL)
-- =============================================
CREATE TABLE CHI_TIET_SAN_PHAM (
    MaSerial VARCHAR(100) primary key,
    MaModel VARCHAR(50) NOT NULL,
    MaPO INT NOT NULL,
    NgayNhapKho DATETIME DEFAULT GETDATE(),
    NgayHetHanBaoHanh DATETIME,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Trong kho', N'Đã bán', N'Đang bảo hành', N'Hàng lỗi', N'Bảo hành hoàn tất')),
	
);
GO

-- =============================================
-- 5. NHÀ CUNG CẤP
-- =============================================
CREATE TABLE NHA_CUNG_CAP (
    MaNCC INT IDENTITY(1,1) PRIMARY KEY,
    TenNCC NVARCHAR(255) NOT NULL,
    SDT VARCHAR(20),
    DiaChi NVARCHAR(255)
);
GO

-- =============================================
-- 6. ĐƠN ĐẶT HÀNG
-- =============================================
CREATE TABLE DON_DAT_HANG (
    MaPO INT IDENTITY(1,1) PRIMARY KEY,
    MaNCC INT NOT NULL,
    MaNV INT NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayDuKienGiao DATE,
    TongTien DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Chờ giao', N'Giao thiếu', N'Hoàn tất')),
    GhiChu NVARCHAR(MAX)
);
GO

-- =============================================
-- 7. CHI TIẾT ĐƠN ĐẶT HÀNG
-- =============================================
CREATE TABLE CHI_TIET_DDH (
    MaPO INT,
    MaModel VARCHAR(50),
    SoLuongDat INT NOT NULL CHECK (SoLuongDat > 0),
    SoLuongThucNhan INT DEFAULT 0,
    DonGiaNhap DECIMAL(18,2) NOT NULL CHECK (DonGiaNhap >= 0),
	TongTien DECIMAL(18,2) DEFAULT 0,
    PRIMARY KEY (MaPO, MaModel)
);
GO

-- =============================================
-- 8. KHÁCH HÀNG
-- =============================================
CREATE TABLE KHACH_HANG (
    SDT VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    Email VARCHAR(100)
);
GO

-- =============================================
-- 9. HÓA ĐƠN
-- =============================================
CREATE TABLE HOA_DON (
    MaHD INT IDENTITY(11000,1) PRIMARY KEY,
    SDT_Khach VARCHAR(20) NULL,
    MaNV INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    HinhThucThanhToan NVARCHAR(50) CHECK (HinhThucThanhToan IN (N'Tiền mặt',N'Chuyển khoản',N'Quẹt thẻ')),
    TongTien DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(50) DEFAULT N'Hoàn thành',
    GhiChu NVARCHAR(MAX)
);
GO

-- =============================================
-- 10. CHI TIẾT HÓA ĐƠN
-- =============================================
CREATE TABLE CHI_TIET_HOA_DON (
    MaHD INT NOT NULL,
    MaModel VARCHAR(50),
    MaSerial VARCHAR(100),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGiaBan DECIMAL(18,2) NOT NULL,
	--PRIMARY KEY( MaHD, MaModel, Maserial)
);
GO

CREATE TABLE PHIEU_BAO_HANH (
    MaPhieuBH INT IDENTITY(200,1) PRIMARY KEY,
    MaSerial VARCHAR(100) NOT NULL,
    MaNV INT NOT NULL, -- Nhân viên lễ tân tiếp nhận
    NgayTiepNhan DATETIME DEFAULT GETDATE(),
    NgayTra DATE NULL,
    TinhTrangLoi NVARCHAR(MAX) NULL,
    TinhTrangVatLy NVARCHAR(MAX) NULL,
    
    -- MỚI: Nhân viên chọn 1 trong 2 lúc lập phiếu
    PhuongAnDeXuat NVARCHAR(50) CHECK(PhuongAnDeXuat IN (N'Xử lý tại chỗ', N'Gửi trả NCC')),
    
    -- MỚI: Cập nhật lại chuỗi trạng thái cho khớp luồng
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ( N'Chờ gửi NCC', N'Đang ở NCC', N'Đã về kho', N'Hoàn tất')),
    GhiChu NVARCHAR(MAX)
);

CREATE TABLE PHIEU_GUI_NCC (
    MaPhieuGui INT IDENTITY(1,1) PRIMARY KEY,
    MaNCC INT NOT NULL, -- Gửi cho hãng nào (Asus, Dell...)
    MaNVLap INT NOT NULL, -- Admin lập phiếu
    NgayGui DATETIME DEFAULT GETDATE(),
    NgayVe DATE NULL,
    
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Đang gửi NCC', N'NCC đã trả hàng')),
    GhiChu NVARCHAR(MAX),
);

CREATE TABLE CT_PHIEU_GUI_NCC (
    MaPhieuGui INT,
    MaPhieuBH INT NOT NULL, -- Link với Phiếu bảo hành của khách
    MaSerial VARCHAR(100) NOT NULL,
    -- Kết quả hãng báo về (Hãng đổi cho cái khác, hãng sửa, hay hãng từ chối)
    KetQuaTuNCC NVARCHAR(MAX) NULL, 
    -- Nếu hãng đòi thu tiền sửa (do lỗi người dùng), lưu vào đây để về thu lại của khách
    ChiPhiTuNCC DECIMAL(18,2) DEFAULT 0, 
    PRIMARY KEY (MaPhieuGui, MaPhieuBH),
);




-- =============================================
-- 🔗 FOREIGN KEY (ĐÃ TỐI ƯU)
-- =============================================

ALTER TABLE SAN_PHAM
ADD CONSTRAINT FK_SanPham_HangSX
FOREIGN KEY (MaHang) REFERENCES HANG_SAN_XUAT(MaHang)
ON UPDATE CASCADE;

ALTER TABLE DON_DAT_HANG
ADD CONSTRAINT FK_PO_NCC FOREIGN KEY (MaNCC)
REFERENCES NHA_CUNG_CAP(MaNCC);

ALTER TABLE DON_DAT_HANG
ADD CONSTRAINT FK_PO_NV FOREIGN KEY (MaNV)
REFERENCES NHAN_VIEN(MaNV);

ALTER TABLE CHI_TIET_DDH
ADD CONSTRAINT FK_CTDDH_PO FOREIGN KEY (MaPO)
REFERENCES DON_DAT_HANG(MaPO)
ON DELETE CASCADE;

ALTER TABLE CHI_TIET_DDH
ADD CONSTRAINT FK_CTDDH_SP FOREIGN KEY (MaModel)
REFERENCES SAN_PHAM(MaModel);

ALTER TABLE CHI_TIET_SAN_PHAM
ADD CONSTRAINT FK_SERIAL_SP FOREIGN KEY (MaModel)
REFERENCES SAN_PHAM(MaModel);

ALTER TABLE CHI_TIET_SAN_PHAM
ADD CONSTRAINT FK_SERIAL_PO FOREIGN KEY (MaPO)
REFERENCES DON_DAT_HANG(MaPO)
ON DELETE CASCADE;

ALTER TABLE HOA_DON
ADD CONSTRAINT FK_HD_KH FOREIGN KEY (SDT_Khach)
REFERENCES KHACH_HANG(SDT)
ON DELETE SET NULL;

ALTER TABLE HOA_DON
ADD CONSTRAINT FK_HD_NV FOREIGN KEY (MaNV)
REFERENCES NHAN_VIEN(MaNV);

ALTER TABLE CHI_TIET_HOA_DON
ADD CONSTRAINT FK_CTHD_HD FOREIGN KEY (MaHD)
REFERENCES HOA_DON(MaHD)
ON DELETE CASCADE;

ALTER TABLE CHI_TIET_HOA_DON
ADD CONSTRAINT FK_CTHD_SP FOREIGN KEY (MaModel)
REFERENCES SAN_PHAM(MaModel);

ALTER TABLE CHI_TIET_HOA_DON
ADD CONSTRAINT FK_CTHD_SERIAL FOREIGN KEY (MaSerial)
REFERENCES CHI_TIET_SAN_PHAM(MaSerial)
ON DELETE SET NULL;

ALTER TABLE PHIEU_BAO_HANH
ADD CONSTRAINT FK_BH_SERIAL FOREIGN KEY (MaSerial)
REFERENCES CHI_TIET_SAN_PHAM(MaSerial);

ALTER TABLE PHIEU_BAO_HANH
ADD CONSTRAINT FK_BH_NV FOREIGN KEY (MaNV)
REFERENCES NHAN_VIEN(MaNV);


ALTER TABLE PHIEU_GUI_NCC
ADD CONSTRAINT FK_PHIEUGUI_NCC
FOREIGN KEY (MaNCC) REFERENCES NHA_CUNG_CAP(MaNCC);

ALTER TABLE PHIEU_GUI_NCC
ADD CONSTRAINT FK_PHIEUGUI_NV
FOREIGN KEY (MaNVLap) REFERENCES NHAN_VIEN(MaNV);


ALTER TABLE CT_PHIEU_GUI_NCC
ADD CONSTRAINT FK_CTPHIEUGUI_PHIEUGUINCC
FOREIGN KEY (MaPhieuGui) REFERENCES PHIEU_GUI_NCC(MaPhieuGui) ON DELETE CASCADE;

ALTER TABLE CT_PHIEU_GUI_NCC
ADD CONSTRAINT FK_CTPHIEUGUI_PHIEUBH
FOREIGN KEY (MaPhieuBH) REFERENCES PHIEU_BAO_HANH(MaPhieuBH);



-- =============================================
-- ⚡ INDEX (TĂNG TỐC)
-- =============================================
CREATE INDEX IDX_SP_MaHang ON SAN_PHAM(MaHang);
CREATE INDEX IDX_HD_MaNV ON HOA_DON(MaNV);
CREATE INDEX IDX_CTHD_MaHD ON CHI_TIET_HOA_DON(MaHD);
CREATE INDEX IDX_SERIAL_Model ON CHI_TIET_SAN_PHAM(MaModel);



INSERT INTO HANG_SAN_XUAT (TenHang) VALUES
(N'Dell'), (N'HP'), (N'Lenovo'), (N'ASUS'), (N'Acer');

INSERT INTO NHAN_VIEN
(HoTen, NgaySinh, Email, CCCD, SDT, GioiTinh, DiaChi, NgayVaoLam, TrangThai, VaiTro, HinhAnh)
VALUES

-- 1. Quản lý
(N'Nguyễn Văn A',
 '1995-05-20',
 'a',
 '1',
 '0901000001',
 N'Nam',
 N'TP HCM',
 '2023-01-10',
 N'Đang làm việc',
 N'Quản lý',
 NULL),

-- 2. Nhân viên
(N'Trần Thị B',
 '1998-03-15',
 't',
 '0',
 '0902000002',
 N'Nữ',
 N'Hà Nội',
 '2024-02-01',
 N'Đang làm việc',
 N'Nhân viên',
 NULL),

-- 3. Nhân viên
(N'Lê Văn C',
 '1997-08-10',
 'lvc@gmail.com',
 '012345678903',
 '0903000003',
 N'Nam',
 N'Đà Nẵng',
 '2024-03-12',
 N'Đang làm việc',
 N'Nhân viên',
 NULL),

-- 4. Nhân viên (đã nghỉ)
(N'Phạm Thị D',
 '1996-11-25',
 'ptd@gmail.com',
 '012345678904',
 '0904000004',
 N'Nữ',
 N'Cần Thơ',
 '2023-06-18',
 N'Đã nghỉ',
 N'Nhân viên',
 NULL),

-- 5. Nhân viên
(N'Hoàng Văn E',
 '1999-01-05',
 'hve@gmail.com',
 '012345678905',
 '0905000005',
 N'Nam',
 N'Hải Phòng',
 '2024-05-20',
 N'Đang làm việc',
 N'Nhân viên',
 NULL);

INSERT INTO SAN_PHAM 
(MaModel, MaHang, TenSanPham, PhanLoai, CPU, RAM, OCung, VGA, ManHinh, GiaBan, ThoiGianBaoHanh, TrangThai, MoTa)
VALUES

-- 1. Dell Inspiron 15 (500GB)
('DELL-I5-8G-500',
 1,
 N'Dell Inspiron 15 3520',
 N'Văn phòng',
 N'Intel Core i5-1235U',
 N'8GB DDR4',
 N'SSD 500GB',
 N'Intel Iris Xe',
 N'15.6 inch FHD',
 15000000,
 24,
 N'Đang kinh doanh',
 N'Laptop văn phòng phổ thông, phù hợp học tập và làm việc'),

-- 2. Dell Inspiron 15 (1TB)
('DELL-I5-8G-1T',
 1,
 N'Dell Inspiron 15 3520',
 N'Văn phòng',
 N'Intel Core i5-1235U',
 N'8GB DDR4',
 N'SSD 1TB',
 N'Intel Iris Xe',
 N'15.6 inch FHD',
 17000000,
 24,
 N'Đang kinh doanh',
 N'Phiên bản nâng cấp dung lượng lưu trữ 1TB'),

-- 3. HP Pavilion Gaming
('HP-R5-16G-512',
 2,
 N'HP Pavilion Gaming 15',
 N'Gaming',
 N'AMD Ryzen 5 5600H',
 N'16GB DDR4',
 N'SSD 512GB',
 N'RTX 3050 4GB',
 N'15.6 inch FHD 144Hz',
 22000000,
 24,
 N'Đang kinh doanh',
 N'Laptop gaming tầm trung, chơi game tốt'),

-- 4. ASUS Vivobook
('ASUS-I7-16G-512',
 4,
 N'ASUS Vivobook 15',
 N'Văn phòng',
 N'Intel Core i7-1255U',
 N'16GB DDR4',
 N'SSD 512GB',
 N'Intel Iris Xe',
 N'15.6 inch OLED',
 20000000,
 24,
 N'Đang kinh doanh',
 N'Màn hình OLED đẹp, hiệu năng mạnh cho văn phòng')


 select * from PHIEU_BAO_HANH
  select * from CT_PHIEU_GUI_NCC
   select * from KHACH_HANG
select * from KHACH_HANG
select * from CHI_TIET_SAN_PHAM
