// IUnitOfWork.cs
using System;

namespace ChatApp.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        IGroupChatRepository GroupChats { get; }
        IGroupMemberRepository GroupMembers { get; }
        IOfflineMessageRepository OfflineMessages { get; }
        
        int Complete();
    }
}
