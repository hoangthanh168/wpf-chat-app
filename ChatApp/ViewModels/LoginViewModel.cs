using ChatApp.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IServiceProvider _serviceProvider;
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

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin()
        {
            if (Username == "user" && Password == "password")
            {
                var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
                shellViewModel.ShowMainView();
            }
            else
            {
                ErrorMessage = "Invalid username or password. Please try again.";
            }
        }
    }

}
