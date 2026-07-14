# 💻 Laptop Management System - Hệ Thống Quản Lý Bán Laptop

Một ứng dụng Desktop được phát triển bằng **C#** và **.NET Framework** để quản lý, bán và theo dõi laptop một cách hiệu quả. Hệ thống được thiết kế cho các cửa hàng bán lẻ laptop với giao diện người dùng thân thiện và cơ sở dữ liệu mạnh mẽ.

---

## 🚀 Công Nghệ Sử Dụng (Tech Stack)

![CSharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![WinForms](https://img.shields.io/badge/WinForms-0078D4?style=for-the-badge&logo=windows&logoColor=white)

**Yêu Cầu:**
- **Visual Studio** 2017 trở lên
- **.NET Framework** 4.7.2+
- **SQL Server** 2019 hoặc Express
- **Windows** 7 trở lên

---

## ✨ Tính Năng Nổi Bật

### 📦 Quản Lý Sản Phẩm (Product Management)

- ✅ **Thêm/Sửa/Xóa Sản Phẩm**
  - Nhập thông tin chi tiết laptop (CPU, RAM, SSD, GPU, v.v.)
  - Quản lý giá bán và giá vốn
  - Theo dõi số lượng tồn kho
  - Phân loại theo thương hiệu, dòng sản phẩm

- ✅ **Tìm Kiếm & Lọc Sản Phẩm**
  - Tìm kiếm theo tên, mã sản phẩm
  - Lọc theo loại, thương hiệu, giá
  - Sắp xếp theo các tiêu chí khác nhau

- ✅ **Quản Lý Thương Hiệu & Danh Mục**
  - Thêm/sửa danh mục sản phẩm
  - Quản lý thương hiệu (Brand)
  - Phân loại sản phẩm rõ ràng

### 👥 Quản Lý Khách Hàng (Customer Management)

- ✅ **Lưu Trữ Thông Tin Khách Hàng**
  - Tên, số điện thoại, địa chỉ
  - Email, ghi chú khách hàng
  - Lịch sử mua hàng

- ✅ **Tìm Kiếm Khách Hàng**
  - Tìm kiếm theo tên, số điện thoại
  - Xem lịch sử giao dịch
  - Quản lý thông tin liên lạc

- ✅ **Phân Loại Khách Hàng**
  - Khách hàng thường xuyên
  - Khách hàng mới
  - Khách hàng VIP

### 💳 Quản Lý Đơn Hàng (Order Management)

- ✅ **Tạo Đơn Hàng**
  - Thêm sản phẩm vào đơn
  - Tính toán giá tự động (bao gồm VAT, giảm giá)
  - Chọn phương thức thanh toán

- ✅ **Theo Dõi Đơn Hàng**
  - Xem trạng thái đơn hàng
  - Lịch sử thanh toán
  - In hóa đơn/chứng từ

- ✅ **Quản Lý Thanh Toán**
  - Các phương thức thanh toán khác nhau
  - Xử lý hoàn tiền
  - Báo cáo doanh thu

### 📊 Báo Cáo & Thống Kê (Reports & Analytics)

- ✅ **Báo Cáo Doanh Thu**
  - Doanh thu theo ngày/tháng/năm
  - Lợi nhuận theo sản phẩm
  - So sánh doanh thu các kỳ

- ✅ **Báo Cáo Kho**
  - Sản phẩm tồn kho
  - Sản phẩm bán chạy
  - Cảnh báo sản phẩm sắp hết

- ✅ **Báo Cáo Khách Hàng**
  - Khách hàng mua nhiều nhất
  - Lịch sử mua hàng từng khách
  - Phân tích khách hàng

### 🔧 Quản Lý Hệ Thống (System Management)

- ✅ **Quản Lý Người Dùng**
  - Tài khoản đăng nhập
  - Phân quyền người dùng (Admin, Staff, Viewer)
  - Nhật ký hoạt động

- ✅ **Cấu Hình Hệ Thống**
  - Cài đặt công ty
  - Cấu hình in hóa đơn
  - Quản lý cơ sở dữ liệu

- ✅ **Sao Lưu & Khôi Phục**
  - Sao lưu dữ liệu định kỳ
  - Khôi phục từ bản sao lưu

---

## 📂 Cấu Trúc Dự Án

```
C#-Laptop-Management/
├── Sườn CODE/
│   └── TreeLayer/                      # 📁 Cấu trúc 3 tầng (3-Layer Architecture)
│       ├── Presentation Layer/         # Lớp giao diện (WinForms UI)
│       │   ├── MainForm.cs             # Form chính
│       │   ├── ProductForm.cs          # Form quản lý sản phẩm
│       │   ├── CustomerForm.cs         # Form quản lý khách hàng
│       │   ├── OrderForm.cs            # Form quản lý đơn hàng
│       │   ├── ReportForm.cs           # Form báo cáo
│       │   └── LoginForm.cs            # Form đăng nhập
│       │
│       ├── Business Logic Layer/       # Lớp xử lý logic (Business Rules)
│       │   ├── ProductService.cs       # Service quản lý sản phẩm
│       │   ├── CustomerService.cs      # Service quản lý khách hàng
│       │   ├── OrderService.cs         # Service quản lý đơn hàng
│       │   ├── ReportService.cs        # Service báo cáo
│       │   └── AuthenticationService.cs # Service xác thực
│       │
│       └── Data Access Layer/          # Lớp truy cập dữ liệu (Database)
│           ├── ProductDAO.cs           # DAO sản phẩm
│           ├── CustomerDAO.cs          # DAO khách hàng
│           ├── OrderDAO.cs             # DAO đơn hàng
│           ├── UserDAO.cs              # DAO người dùng
│           └── DatabaseConnection.cs   # Kết nối cơ sở dữ liệu
│
├── database/
│   ├── LaptopManagement.sql            # Script tạo database
│   ├── InsertSampleData.sql            # Dữ liệu mẫu
│   └── StoredProcedures.sql            # Stored Procedures
│
├── App.config                          # File cấu hình ứng dụng
├── Properties/
│   └── Settings.settings               # Cài đặt ứng dụng
│
├── README.md                           # File hướng dẫn (file này)
└── Documentation/                      # Tài liệu dự án
    ├── UserGuide.md                    # Hướng dẫn người dùng
    ├── DeveloperGuide.md               # Hướng dẫn nhà phát triển
    └── DatabaseSchema.md               # Sơ đồ cơ sở dữ liệu

```

---

## 🛠️ Yêu Cầu & Cài Đặt

### Yêu Cầu Hệ Thống

- **Windows OS**: Windows 7, 8, 10, 11
- **.NET Framework**: 4.7.2 hoặc cao hơn
- **Visual Studio**: 2017 Community Edition hoặc cao hơn
- **SQL Server**: 2019 Express hoặc cao hơn (hoặc LocalDB)
- **RAM**: 4GB (tối thiểu)
- **Dung lượng**: 500MB

### Bước Cài Đặt

#### 1️⃣ Clone Repository

```bash
git clone https://github.com/LeMinh-Quan/C-.git
cd C-
```

#### 2️⃣ Chuẩn Bị Cơ Sở Dữ Liệu

**Tùy chọn A - Sử dụng SQL Server:**

1. Mở **SQL Server Management Studio**
2. Chạy script từ file `database/LaptopManagement.sql`
3. Chạy script `database/InsertSampleData.sql` để nhập dữ liệu mẫu

**Tùy chọn B - Sử dụng LocalDB:**

```bash
# Tạo database bằng LocalDB
sqlcmd -S (localdb)\mssqllocaldb -i database/LaptopManagement.sql
sqlcmd -S (localdb)\mssqllocaldb -i database/InsertSampleData.sql
```

#### 3️⃣ Cấu Hình Kết Nối Database

Mở file `App.config` và cập nhật connection string:

```xml
<connectionStrings>
    <add name="LaptopDB" 
         connectionString="Server=(localdb)\mssqllocaldb;Database=LaptopManagement;Integrated Security=true;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Hoặc nếu dùng SQL Server thông thường:

```xml
<connectionStrings>
    <add name="LaptopDB" 
         connectionString="Server=YourServerName;Database=LaptopManagement;User Id=sa;Password=YourPassword;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

#### 4️⃣ Mở Project trong Visual Studio

1. Mở **Visual Studio**
2. File → Open → Project/Solution
3. Chọn file `.sln` trong thư mục `C-`
4. Nhấn **Build Solution** (Ctrl+Shift+B)

#### 5️⃣ Chạy Ứng Dụng

- Nhấn **F5** hoặc **Debug** → **Start Debugging**
- Hoặc nhấn nút **Start** trong Visual Studio

#### 6️⃣ Đăng Nhập

Sử dụng tài khoản mặc định:

| Email | Mật Khẩu | Vai Trò |
|-------|----------|--------|
| admin@laptop.com | admin123 | Admin |
| staff@laptop.com | staff123 | Nhân viên |

---

## 📖 Hướng Dẫn Sử Dụng

### Cho Quản Trị Viên (Admin)

1. **Quản Lý Sản Phẩm**
   ```
   Menu → Sản Phẩm → Quản Lý Sản Phẩm
   - Thêm sản phẩm mới: Nhấn "Thêm" → Điền thông tin
   - Sửa sản phẩm: Chọn sản phẩm → Nhấn "Sửa"
   - Xóa sản phẩm: Chọn sản phẩm → Nhấn "Xóa"
   ```

2. **Quản Lý Khách Hàng**
   ```
   Menu → Khách Hàng → Quản Lý Khách Hàng
   - Thêm khách hàng: Nhấn "Thêm" → Điền thông tin
   - Sửa thông tin: Chọn khách → Nhấn "Sửa"
   - Xem lịch sử: Chọn khách → Xem "Lịch Sử Mua"
   ```

3. **Tạo Đơn Hàng**
   ```
   Menu → Đơn Hàng → Tạo Đơn Hàng Mới
   - Chọn khách hàng
   - Thêm sản phẩm vào đơn
   - Cập nhật số lượng
   - Chọn phương thức thanh toán
   - Nhấn "Lưu"
   ```

4. **Xem Báo Cáo**
   ```
   Menu → Báo Cáo
   - Chọn loại báo cáo (Doanh Thu, Kho, Khách Hàng)
   - Chọn kỳ báo cáo
   - Nhấn "Xuất Báo Cáo"
   ```

5. **Quản Lý Người Dùng**
   ```
   Menu → Cài Đặt → Quản Lý Người Dùng
   - Thêm tài khoản mới
   - Phân quyền theo vai trò
   - Kích hoạt/vô hiệu hóa tài khoản
   ```

### Cho Nhân Viên (Staff)

1. **Tạo Đơn Hàng**
   - Truy cập Menu → Đơn Hàng → Tạo Đơn
   - Chọn khách hàng (có thể thêm khách mới)
   - Chọn sản phẩm từ danh sách
   - Xác nhận thanh toán

2. **Tìm Kiếm Sản Phẩm**
   - Sử dụng thanh tìm kiếm
   - Lọc theo thương hiệu, giá
   - Xem chi tiết sản phẩm

3. **Xem Đơn Hàng**
   - Xem danh sách đơn đã tạo
   - Xem chi tiết từng đơn
   - In hóa đơn

---

## 🔐 Bảo Mật

- ✔️ **Xác Thực**: Đăng nhập bằng email & mật khẩu
- ✔️ **Phân Quyền**: Kiểm soát truy cập theo vai trò (Role-based)
- ✔️ **Mã Hóa Mật Khẩu**: Sử dụng hashing an toàn
- ✔️ **Nhật Ký Hoạt Động**: Ghi lại tất cả thao tác người dùng
- ✔️ **Xác Thực SQL**: Sử dụng Parameterized Queries chống SQL Injection
- ✔️ **Backup Định Kỳ**: Sao lưu dữ liệu tự động

---

## 📊 Kiến Trúc Ứng Dụng

### 3-Layer Architecture (Kiến Trúc 3 Tầng)

```
┌─────────────────────────────┐
│   Presentation Layer        │  ← WinForms UI (Giao Diện)
│  (MainForm, ProductForm...) │
└──────────────┬──────────────┘
               │
┌──────────────▼──────────────┐
│   Business Logic Layer      │  ← Business Rules (Xử Lý Logic)
│ (Services, Validations)     │
└──────────────┬──────────────┘
               │
┌──────────────▼──────────────┐
│   Data Access Layer         │  ← Database Operations (Truy Cập DB)
│  (DAO, SQL Queries)         │
└──────────────┬──────────────┘
               │
┌──────────────▼──────────────┐
│   SQL Server Database       │  ← Cơ Sở Dữ Liệu
└─────────────────────────────┘
```

**Lợi Ích:**
- ✅ Dễ bảo trì và mở rộng
- ✅ Tách biệt trách nhiệm (Separation of Concerns)
- ✅ Dễ kiểm thử (Unit Testing)
- ✅ Tái sử dụng code

---

## 🐛 Troubleshooting (Khắc Phục Sự Cố)

### ❌ Lỗi: "Connection string" không hợp lệ
**Giải pháp:**
- Kiểm tra file `App.config`
- Đảm bảo SQL Server đang chạy
- Thử kết nối trực tiếp bằng SSMS
- Kiểm tra tên database và permissions

### ❌ Lỗi: "Login failed for user"
**Giải pháp:**
- Kiểm tra SQL Server Authentication settings
- Đảm bảo user có quyền truy cập database
- Thử sử dụng Windows Authentication

### ❌ Lỗi: "Table or view not found"
**Giải pháp:**
- Kiểm tra script SQL đã chạy thành công
- Xác nhận database hiện tại
- Chạy lại script `LaptopManagement.sql`

### ❌ Ứng dụng không khởi động
**Giải pháp:**
- Kiểm tra .NET Framework version
- Rebuild Solution (Ctrl+Shift+B)
- Xóa bin/obj folders rồi rebuild
- Kiểm tra event logs của Windows

### ❌ Lỗi: "The following exception was thrown: ObjectDisposedException"
**Giải pháp:**
- Kiểm tra database connection đã đóng đúng cách
- Sử dụng `using` statement cho database connections
- Kiểm tra các thread đang chạy

---

## 📚 Tài Liệu & Tham Khảo

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET Framework Guide](https://docs.microsoft.com/en-us/dotnet/framework/)
- [SQL Server Documentation](https://docs.microsoft.com/en-us/sql/)
- [Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- [ADO.NET Documentation](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/)

---

## 💡 Tính Năng Sắp Thêm (Roadmap)

- [ ] Export báo cáo sang Excel/PDF
- [ ] Tích hợp in hóa đơn tự động
- [ ] Quản lý nhân viên (Payroll)
- [ ] Mobile app sync
- [ ] Real-time Dashboard
- [ ] Tích hợp thanh toán online
- [ ] Machine Learning cho dự báo bán hàng

---

## 📈 Hiệu Năng

- ✅ Xử lý nhanh với cơ sở dữ liệu SQL Server
- ✅ Giao diện responsive và mượt
- ✅ Hỗ trợ nhiều người dùng cùng lúc
- ✅ Xử lý dữ liệu lớn hiệu quả

---

## 👨‍💻 Tác Giả

**Lê Minh Quân** (LeMinh-Quan)

- 🔗 GitHub: [https://github.com/LeMinh-Quan](https://github.com/LeMinh-Quan)
- 📧 Email: [your-email@example.com]
---

## 📦 Backup & Resources

Backup đầy đủ của dự án có sẵn tại:

📁 **Google Drive**: [Link sao lưu Google Drive](https://drive.google.com/drive/folders/1MEJS2G9ZpROVKfPOQRi2luVdXbTFCb3J?usp=sharing)

(Bao gồm source code, database, documentation, và resources)

---


## 📋 Danh Sách Kiểm Tra (Checklist) Trước Khi Triển Khai

- [ ] Database được tạo thành công
- [ ] Connection string đã được cấu hình chính xác
- [ ] Tất cả stored procedures đã được tạo
- [ ] Dữ liệu mẫu đã được nhập
- [ ] Ứng dụng có thể khởi động mà không có lỗi
- [ ] Login/Logout hoạt động bình thường
- [ ] Quản lý sản phẩm đầy đủ (CRUD)
- [ ] Quản lý khách hàng đầy đủ (CRUD)
- [ ] Tạo đơn hàng hoạt động
- [ ] Báo cáo có thể xuất
- [ ] In hóa đơn hoạt động
- [ ] Sao lưu/khôi phục dữ liệu hoạt động
- [ ] Tất cả validations hoạt động
- [ ] Performance tốt với dữ liệu lớn
- [ ] Code đã comment rõ ràng
- [ ] Không có sensitive data trong code

---

## 🤝 Đóng Góp

Nếu bạn muốn cải thiện dự án:

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/NewFeature`)
3. Commit thay đổi (`git commit -m 'Add NewFeature'`)
4. Push lên branch (`git push origin feature/NewFeature`)
5. Mở Pull Request

---


## 🎯 Mục Tiêu Dự Án

Phát triển một hệ thống quản lý laptop chuyên nghiệp, an toàn và dễ sử dụng cho các cửa hàng bán lẻ:

✅ Quản lý sản phẩm hiệu quả  
✅ Theo dõi kho hàng chính xác  
✅ Xử lý bán hàng nhanh chóng  
✅ Báo cáo chi tiết và chính xác  
✅ Giao diện thân thiện với người dùng  
✅ Bảo mật dữ liệu tốt  

---

**🎉 Chúc bạn thành công với dự án! Nếu có thắc mắc, hãy tạo Issue hoặc liên hệ với tác giả.**

---

*Last Updated: 2026-07-14*  
*Version: 1.0.0*  
*Status: ✅ Stable Release*