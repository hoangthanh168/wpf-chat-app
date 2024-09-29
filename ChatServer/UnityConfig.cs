using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using ChatServer.Repositories;
using ChatServer.Services;
using Unity;

namespace ChatServer
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<AppDbContext, AppDbContext>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IGroupChatService, GroupChatService>();
            container.RegisterType<IGroupMemberService, GroupMemberService>();
            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IOfflineMessageService, OfflineMessageService>();


            return container;
        }
    }
}
