using System;
using ChatApp.Mvvm;
using ChatApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private object _currentView;
        private readonly MainViewModel _mainViewModel;
        private readonly IServiceProvider _serviceProvider;
        private readonly LoginViewModel _loginViewModel;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ShellViewModel(IServiceProvider serviceProvider, LoginViewModel loginViewModel, MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._serviceProvider = serviceProvider;
            this._loginViewModel = loginViewModel;

            CurrentView = _serviceProvider.GetRequiredService<LoginPage>();
        }

        public void ShowMainView()
        {
            var mainView = _serviceProvider.GetRequiredService<MainView>();
            CurrentView = mainView;
        }

        public void ShowLoginPage()
        {
            var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
            CurrentView = loginPage;
        }
    }
}
