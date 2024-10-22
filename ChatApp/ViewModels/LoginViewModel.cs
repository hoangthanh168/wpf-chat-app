using ChatApp.Core.Services;
using ChatApp.Core.Utils;
using ChatApp.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserService _userService; // Injected UserService
        private string _username;
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ShowRegisterCommand { get; }

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userService = _serviceProvider.GetRequiredService<UserService>();

            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            ShowRegisterCommand = new DelegateCommand(ExecuteShowRegister);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin()
        {
            var user = _userService.Authenticate(Username, Security.Encrypt(Password));

            if (user != null)
            {
                var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
                shellViewModel.ShowMainView();
            }
            else
            {
                // Show an error message if login fails
                ErrorMessage = "Invalid username or password. Please try again.";
            }
        }

        private void ExecuteShowRegister()
        {
            var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
            shellViewModel.ShowRegisterView();
        }
    }
}
