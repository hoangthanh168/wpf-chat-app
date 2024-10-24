# ChatApp

## Mô tả
ChatApp là một phần mềm chat được phát triển theo yêu cầu môn học Lập Trình Mạng tại Đại Học Đà Lạt, phần mềm hỗ trợ giao tiếp trực tiếp và nhóm, với tính năng lưu trữ tin nhắn offline.

## Tính năng
- **Đăng ký & Đăng nhập:** Người dùng có thể tạo tài khoản và đăng nhập vào hệ thống.
- **Tin nhắn trực tiếp:** Gửi và nhận tin nhắn giữa các người dùng.
- **Nhóm chat:** Tạo, quản lý và tham gia các nhóm chat.
- **Tin nhắn offline:** Lưu trữ tin nhắn khi người nhận không trực tuyến.
- **Giao tiếp cục bộ:** Tất cả các chức năng giao tiếp và xử lý dữ liệu diễn ra cục bộ trên máy tính người dùng.

## Công nghệ sử dụng
- .NET Framework, Entity Framework
- Mô hình MVVM
- MahApps
- WPF (Windows Presentation Foundation)
- DependencyInjection
- Git
- SQL Server


## Cấu trúc dự án
```
ChatApp/
├── ChatApp/ 
├── ChatApp.Core/
└── ChatServer/
```

## Yêu cầu hệ thống
- **.NET Framework:** 4.8
- **Visual Studio:** 2019 hoặc mới hơn
- **SQL Server:** 2017 hoặc mới hơn

## Hướng dẫn cài đặt

### 1. Clone Repository
```bash
git clone https://github.com/yourusername/ChatApp.git
cd ChatApp
```

### 2. Mở Solution với Visual Studio
- Mở file `ChatApp.sln` bằng Visual Studio.

### 3. Cài đặt các Package cần thiết
Trong Visual Studio, mở **Package Manager Console** và chạy:
```powershell
Update-Package -reinstall
```
Hoặc sử dụng **NuGet Package Manager** để cài đặt các package thiếu.

### 4. Cấu hình chuỗi kết nối
Chỉnh sửa file `App.config` trong dự án `ChatServer` `ChatApp` `ChatApp.Core` để cấu hình chuỗi kết nối đến SQL Server.
```xml
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=YOUR_SERVER;Initial Catalog=ChatAppDB;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 5. Áp dụng Migrations và Tạo Database
Mở **Package Manager Console** chọn `ChatApp.Core` là Startup Project và trong **Package Manager Console** chọn .Core và chạy:
```powershell
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```
