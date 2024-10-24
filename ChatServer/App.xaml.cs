using ChatApp.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ChatServer
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
            UserCreation.SeedFirstUser(_serviceProvider);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddChatAppServices();
            services.AddSingleton<ServerSocket>();
            services.AddSingleton<MainWindow>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }


    }
}
