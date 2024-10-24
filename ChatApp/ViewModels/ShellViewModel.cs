using ChatApp.Mvvm;
using ChatApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private object _currentView;
        private readonly MainViewModel _mainViewModel;
        private readonly IServiceProvider _serviceProvider;
        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterViewModel _registerViewModel;
        private readonly ClientSocket _clientSocket;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ShellViewModel(IServiceProvider serviceProvider,
                              LoginViewModel loginViewModel,
                              MainViewModel mainViewModel,
                              RegisterViewModel registerViewModel,
                              ClientSocket clientSocket)
        {
            this._mainViewModel = mainViewModel;
            this._serviceProvider = serviceProvider;
            this._loginViewModel = loginViewModel;
            this._registerViewModel = registerViewModel;
            this._clientSocket = clientSocket;

            CurrentView = _serviceProvider.GetRequiredService<LoginView>();
        }

        public void ShowMainView()
        {
            var mainView = _serviceProvider.GetRequiredService<MainView>();
            CurrentView = mainView;
        }

        public void ShowLoginView()
        {
            var loginView = _serviceProvider.GetRequiredService<LoginView>();
            CurrentView = loginView;
        }

        public void ShowRegisterView()
        {
            var registerView = _serviceProvider.GetRequiredService<RegisterView>();
            CurrentView = registerView;
        }
    }
}
