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
            private readonly UserService _userService;
            private string _username;
            private string _password;
            private string _confirmPassword;
            private string _errorMessage;

            [Required(ErrorMessage = "Username is required.")]
            [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
            public string Username
            {
                get => _username;
                set
                {
                    SetProperty(ref _username, value);
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }

            [Required(ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string Password
            {
                get => _password;
                set
                {
                    SetProperty(ref _password, value);
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }

            [Required(ErrorMessage = "Please confirm your password.")]
            [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
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

            public RegisterViewModel(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                _userService = _serviceProvider.GetRequiredService<UserService>();

                RegisterCommand = new DelegateCommand(ExecuteRegister, CanExecuteRegister);
                ShowLoginCommand = new DelegateCommand(ExecuteShowLogin);
            }

            private bool CanExecuteRegister()
            {
                // Perform validation for each field before allowing the command to be executed
                var validationContext = new ValidationContext(this);
                var validationResults = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);
                ErrorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                return isValid;
            }

            private void ExecuteRegister()
            {
                // Check if the username already exists
                var existingUser = _userService.GetUserByUsername(Username);
                if (existingUser != null)
                {
                    ErrorMessage = "Username already exists. Please choose a different username.";
                    return;
                }

                // Encrypt the password before saving
                string encryptedPassword = Security.Encrypt(Password);

                // Create a new User object with the encrypted password
                var newUser = new User
                {
                    Username = Username,
                    PasswordHash = encryptedPassword,  // Storing the encrypted password
                    CreatedAt = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow
                };

                // Save the new user using the UserService
                _userService.CreateUser(newUser);

                // Navigate to the main view after successful registration
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
