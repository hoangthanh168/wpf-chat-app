# ChatApp

## Mô tả
ChatApp là một ứng dụng chat đa người dùng, hỗ trợ giao tiếp trực tiếp và nhóm, với tính năng lưu trữ tin nhắn offline. Ứng dụng được xây dựng dựa trên kiến trúc phân tầng, đảm bảo tính mở rộng và dễ bảo trì. Tất cả các thành phần giao tiếp với nhau cục bộ trên máy tính của người dùng mà không cần một backend riêng biệt.

## Tính năng
- **Đăng ký & Đăng nhập:** Người dùng có thể tạo tài khoản và đăng nhập vào hệ thống.
- **Tin nhắn trực tiếp:** Gửi và nhận tin nhắn giữa các người dùng.
- **Nhóm chat:** Tạo, quản lý và tham gia các nhóm chat.
- **Tin nhắn offline:** Lưu trữ tin nhắn khi người nhận không trực tuyến.
- **Giao tiếp cục bộ:** Tất cả các chức năng giao tiếp và xử lý dữ liệu diễn ra cục bộ trên máy tính người dùng.

## Công nghệ sử dụng
- **Backend:** .NET Framework, Entity Framework
- **Frontend:** WPF (Windows Presentation Foundation)
- **Dependency Injection:** Unity
- **Version Control:** Git
- **Quản lý cơ sở dữ liệu:** SQL Server

## Cấu trúc dự án
```
ChatApp/
├── ChatApp/                # Project Presentation Layer (WPF)
├── ChatApp.Core/           # Core Layer (Models, Repositories, Services)
└── ChatServer/             # Data Access Layer và Server Logic (cục bộ)
```

## Yêu cầu hệ thống
- **Hệ điều hành:** Windows 10 trở lên
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
Chỉnh sửa file `App.config` trong dự án `ChatServer` để cấu hình chuỗi kết nối đến SQL Server.
```xml
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=YOUR_SERVER;Initial Catalog=ChatAppDB;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 5. Áp dụng Migrations và Tạo Database
Mở **Package Manager Console** và chạy:
```powershell
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

### 6. Chạy Ứng dụng
- **ChatServer:** Chạy dự án `ChatServer` để khởi động dịch vụ xử lý dữ liệu.
- **ChatApp:** Chạy dự án `ChatApp` để mở giao diện người dùng.

## Sử dụng
1. **Đăng ký hoặc Đăng nhập:** Tạo tài khoản hoặc đăng nhập vào hệ thống.
2. **Tạo hoặc Tham gia Nhóm Chat:** Tạo nhóm mới hoặc tham gia các nhóm hiện có.
3. **Gửi Tin nhắn:** Gửi và nhận tin nhắn trực tiếp hoặc trong nhóm.
4. **Xem Tin nhắn Offline:** Xem các tin nhắn đã gửi khi bạn không trực tuyến.

## Cách đóng góp
1. **Fork Repository**
2. **Tạo Branch Mới**
    ```bash
    git checkout -b feature/your-feature
    ```
3. **Commit Thay đổi**
    ```bash
    git commit -m "Add some feature"
    ```
4. **Push Branch**
    ```bash
    git push origin feature/your-feature
    ```
5. **Mở Pull Request**

## License
[MIT](LICENSE)

## Liên hệ
- **Email:** your.email@example.com
- **LinkedIn:** [Your LinkedIn](https://www.linkedin.com/in/yourprofile/)
- **GitHub:** [yourusername](https://github.com/yourusername)

---

**Lưu ý:**
- Thay thế các thông tin như `yourusername`, `YOUR_SERVER`, `your.email@example.com`, và các liên kết liên hệ bằng thông tin thực tế của bạn.
- Cập nhật phần License nếu bạn sử dụng loại giấy phép khác ngoài MIT.
- Thêm các phần khác nếu dự án của bạn có thêm tính năng hoặc yêu cầu đặc biệt.