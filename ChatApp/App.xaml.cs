using ChatApp.Navigation;
using ChatApp.ViewModels;
using ChatApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ChatApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ShellViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginPage>();
            services.AddTransient<MainView>();

            services.AddTransient<NavigationServiceEx>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
