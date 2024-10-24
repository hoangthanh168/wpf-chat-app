using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.Utils;
using ChatApp.Mvvm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ChatApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSession _userSession;
        private readonly ClientSocket _clientSocket;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
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
        public Action<User> OnLoginSuccess { get; set; }
        public LoginViewModel(IServiceProvider serviceProvider, UserSession userSession, ClientSocket clientSocket, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _userSession = userSession;
            _clientSocket = clientSocket;
            _userService = _serviceProvider.GetRequiredService<UserService>();
            _configuration = configuration;

            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            ShowRegisterCommand = new DelegateCommand(ExecuteShowRegister);
            LoadCredentials();
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin()
        {
            var encryptedPassword = Security.Encrypt(Password);
            var user = _userService.Authenticate(Username, encryptedPassword);

            if (user != null)
            {
                _userSession.CurrentUser = user;



                SaveCredentials();

                var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
                shellViewModel.ShowMainView();
                OnLoginSuccess?.Invoke(user);
            }
            else
            {
                ErrorMessage = "Invalid username or password. Please try again.";
            }
        }

        private void ExecuteShowRegister()
        {
            var shellViewModel = _serviceProvider.GetRequiredService<ShellViewModel>();
            shellViewModel.ShowRegisterView();
        }

        private void LoadCredentials()
        {
            var filePath = "appsettings.json";

            if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(filePath)))
            {
                CreateDefaultAppSettings(filePath);
            }

            var json = File.ReadAllText(filePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            if (jsonObj["UserCredentials"] == null)
            {
                jsonObj["UserCredentials"] = new Newtonsoft.Json.Linq.JObject();
                jsonObj["UserCredentials"]["Username"] = string.Empty;
                jsonObj["UserCredentials"]["Password"] = string.Empty;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }

            Username = jsonObj["UserCredentials"]["Username"];
            var password = _configuration["UserCredentials:Password"];

            if (!string.IsNullOrEmpty(password as string))
            {
                Password = Security.Decrypt(password);
            }
        }

        private void CreateDefaultAppSettings(string filePath)
        {
            dynamic defaultSettings = new
            {
                UserCredentials = new
                {
                    Username = string.Empty,
                    Password = string.Empty
                }
            };

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(defaultSettings, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }

        private void SaveCredentials()
        {
            var filePath = "appsettings.json";
            var json = File.ReadAllText(filePath);

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            if (jsonObj["UserCredentials"] == null)
            {
                jsonObj["UserCredentials"] = new Newtonsoft.Json.Linq.JObject();
            }

            jsonObj["UserCredentials"]["Username"] = Username;
            jsonObj["UserCredentials"]["Password"] = Security.Encrypt(Password);

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }
    }
}
