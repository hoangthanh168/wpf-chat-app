using ChatApp.Core;
using ChatApp.Navigation;
using ChatApp.ViewModels;
using ChatApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ChatApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            _configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
            UserCreation.SeedFirstUser(_serviceProvider);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);

            services.AddSingleton<ShellViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddTransient<RegisterViewModel>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<MainView>();
            services.AddTransient<ChatView>();
            services.AddSingleton<SettingsView>();
            services.AddTransient<RegisterView>();

            services.AddTransient<NavigationServiceEx>();
            services.AddSingleton<ClientSocket>();
            services.AddSingleton<UserSession>();
            services.AddChatAppServices();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }


    }
}
