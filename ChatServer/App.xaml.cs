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
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddChatAppServices();
            services.AddSingleton<ServerSocket>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
