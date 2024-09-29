using ChatApp.Core.Repositories;

namespace ChatServer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; private set; }
        public IMessageRepository Messages { get; private set; }
        public IGroupChatRepository GroupChats { get; private set; }
        public IGroupMemberRepository GroupMembers { get; private set; }
        public IOfflineMessageRepository OfflineMessages { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Messages = new MessageRepository(_context);
            GroupChats = new GroupChatRepository(_context);
            GroupMembers = new GroupMemberRepository(_context);
            OfflineMessages = new OfflineMessageRepository(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
