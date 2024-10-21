# ChatApp

## Mô tả
ChatApp là một phần mềm chat được phát triển theo yêu cầu môn học Lập Trình Mạng tại Đại Học DLU, phần mềm hỗ trợ giao tiếp trực tiếp và nhóm, với tính năng lưu trữ tin nhắn offline. Tất cả các thành phần giao tiếp với nhau cục bộ trên máy tính của người dùng mà không cần một backend riêng biệt.

## Tính năng
- **Đăng ký & Đăng nhập:** Người dùng có thể tạo tài khoản và đăng nhập vào hệ thống.
- **Tin nhắn trực tiếp:** Gửi và nhận tin nhắn giữa các người dùng.
- **Nhóm chat:** Tạo, quản lý và tham gia các nhóm chat.
- **Tin nhắn offline:** Lưu trữ tin nhắn khi người nhận không trực tuyến.
- **Giao tiếp cục bộ:** Tất cả các chức năng giao tiếp và xử lý dữ liệu diễn ra cục bộ trên máy tính người dùng.

## Công nghệ sử dụng
- .NET Framework, Entity Framework
- WPF (Windows Presentation Foundation)
- Unity
- Git
- QL Server

## Cấu trúc dự án
```
ChatApp/
├── ChatApp/              # Project Presentation Layer (WPF)
    ├── App.config
    ├── App.xaml
    ├── App.xaml.cs
    ├── ChatApp.csproj
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
    ├── packages.config
    ├── Converters
    ├── Mvvm
    │   ├── BindableBase.cs
    │   └── DelegateCommand.cs
    ├── Navigation
    │   ├── FrameAnimator.cs
    │   └── NavigationServiceEx.cs
    ├── Properties
    │   ├── AssemblyInfo.cs
    │   ├── Resources.Designer.cs
    │   ├── Resources.resx
    │   ├── Settings.Designer.cs
    │   └── Settings.settings
    ├── ViewModels
    │   ├── ChatItemViewModel.cs
    │   ├── ChatViewModel.cs
    │   ├── MenuItem.cs
    │   ├── MessageItemViewModel.cs
    │   ├── SettingsViewModel.cs
    │   └── ShellViewModel.cs
    └── Views
        ├── ChatItemControl.xaml
        ├── ChatItemControl.xaml.cs
        ├── ChatPage.xaml
        ├── ChatPage.xaml.cs
        ├── LoginPage.xaml
        ├── LoginPage.xaml.cs
        ├── MessageItemControl.xaml
        ├── MessageItemControl.xaml.cs
        ├── SettingsPage.xaml
        └── SettingsPage.xaml.cs
├── ChatApp.Core/           # Core Layer (Models, Repositories, Services)
    ├── ChatApp.Core.csproj
    ├── Models
    │   └── Models.cs
    ├── Properties
    │   └── AssemblyInfo.cs
    ├── Repositories
    │   ├── IGenericRepository.cs
    │   ├── IGroupChatRepository.cs
    │   ├── IGroupMemberRepository.cs
    │   ├── IMessageRepository.cs
    │   ├── IOfflineMessageRepository.cs
    │   ├── IUnitOfWork.cs
    │   └── IUserRepository.cs
    ├── Services
    │   ├── GroupChatService.cs
    │   ├── GroupMemberService.cs
    │   ├── IGroupChatService.cs
    │   ├── IGroupMemberService.cs
    │   ├── IMessageService.cs
    │   ├── IOfflineMessageService.cs
    │   ├── IUserService.cs
    │   ├── MessageService.cs
    │   ├── OfflineMessageService.cs
    │   └── UserService.cs
    └── rp
        ├── IGenericRepository.cs
        ├── IGroupChatRepository.cs
        ├── IGroupMemberRepository.cs
        ├── IMessageRepository.cs
        ├── IOfflineMessageRepository.cs
        ├── IUnitOfWork.cs
        └── IUserRepository.cs
        
└── ChatServer/             # Data Access Layer và Server Logic (cục bộ)
    ├── App.config
    ├── App.xaml
    ├── App.xaml.cs
    ├── AppDbContext.cs
    ├── ChatServer.csproj
    ├── ChatServer.csproj.user
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
    ├── packages.config
    ├── UnityConfig.cs
    ├── Migrations
    │   ├── 202409291523308_InitialCreate.cs
    │   ├── 202409291523308_InitialCreate.Designer.cs
    │   ├── 202409291523308_InitialCreate.resx
    │   └── Configuration.cs
    ├── Properties
    │   ├── AssemblyInfo.cs
    │   ├── Resources.Designer.cs
    │   ├── Resources.resx
    │   ├── Settings.Designer.cs
    │   └── Settings.settings
    ├── Repositories
    │   ├── GenericRepository.cs
    │   ├── GroupChatRepository.cs
    │   ├── GroupMemberRepository.cs
    │   ├── MessageRepository.cs
    │   ├── OfflineMessageRepository.cs
    │   ├── UnitOfWork.cs
    │   └── UserRepository.cs
    └── Views
        ├── ChatItemControl.xaml
        ├── ChatItemControl.xaml.cs
        ├── ChatPage.xaml
        ├── ChatPage.xaml.cs
        ├── LoginPage.xaml
        ├── LoginPage.xaml.cs
        ├── MessageItemControl.xaml
        ├── MessageItemControl.xaml.cs
        ├── SettingsPage.xaml
        └── SettingsPage.xaml.cs

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
