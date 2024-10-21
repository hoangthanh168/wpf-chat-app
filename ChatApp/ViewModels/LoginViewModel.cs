using System;
using System.Windows.Input;
using ChatApp.Mvvm;

namespace ChatApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _name;
        private string _ipAddress;
        private bool _isLoginSuccessful;
        private bool _isAdmin;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        public bool IsLoginSuccessful
        {
            get => _isLoginSuccessful;
            set => SetProperty(ref _isLoginSuccessful, value);
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        public ICommand LoginCommand { get; }

        // Tài khoản admin mặc định và IP gốc
        private readonly string _adminName = "admin";
        private readonly string _adminIp = "127.0.0.1";

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(OnLogin, CanLogin);
        }

        private void OnLogin()
        {
            // Xác thực tài khoản admin
            if (Name == _adminName && IpAddress == _adminIp)
            {
                IsAdmin = true;
                IsLoginSuccessful = true;
            }
            // Xác thực người dùng thường
            else if (IsValidIpAddress(IpAddress) && !string.IsNullOrWhiteSpace(Name))
            {
                IsAdmin = false;
                IsLoginSuccessful = true;
            }
            else
            {
                // Đăng nhập thất bại
                IsLoginSuccessful = false;
            }
        }

        private bool IsValidIpAddress(string ipAddress)
        {
            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(IpAddress);
        }
    }
}
