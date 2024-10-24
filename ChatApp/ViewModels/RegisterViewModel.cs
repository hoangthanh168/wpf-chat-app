using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.Utils;
using ChatApp.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChatApp.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSession _userSession;
        private readonly UserService _userService;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _errorMessage;

        [Required(ErrorMessage = "Tên người dùng là bắt buộc.")]
        [MinLength(3, ErrorMessage = "Tên người dùng phải có ít nhất 3 ký tự.")]
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu không khớp.")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public DelegateCommand RegisterCommand { get; }
        public DelegateCommand ShowLoginCommand { get; }

        public RegisterViewModel(IServiceProvider serviceProvider, UserSession userSession)
        {
            _serviceProvider = serviceProvider;
            _userSession = userSession;
            _userService = _serviceProvider.GetRequiredService<UserService>();

            RegisterCommand = new DelegateCommand(ExecuteRegister, CanExecuteRegister);
            ShowLoginCommand = new DelegateCommand(ExecuteShowLogin);
        }

        private bool CanExecuteRegister()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);
            ErrorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
            return isValid;
        }

        private void ExecuteRegister()
        {
            var existingUser = _userService.GetUserByUsername(Username);
            if (existingUser != null)
            {
                ErrorMessage = "Tên người dùng đã tồn tại. Vui lòng chọn một tên người dùng khác.";
                return;
            }

            string encryptedPassword = Security.Encrypt(Password);

            var newUser = new User
            {
                Username = Username,
                PasswordHash = encryptedPassword,
                CreatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };

            _userSession.CurrentUser = newUser;

            _userService.CreateUser(newUser);

            var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
            shellViewModel.ShowMainView();
        }

        private void ExecuteShowLogin()
        {
            var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
            shellViewModel.ShowLoginView();
        }
    }
}
