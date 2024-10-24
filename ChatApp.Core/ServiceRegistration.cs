using ChatApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Core
{
    public static class ServiceRegistration
    {
        public static void AddChatAppServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<UserService>();
            services.AddScoped<MessageService>();
        }



    }
}
