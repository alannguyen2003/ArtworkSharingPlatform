using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ArtworkSharingPlatformDbContext _context;

        public MessageRepository(ArtworkSharingPlatformDbContext context)
        {
            _context = context;
        }
        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<Connection> GetConnection(string id)
        {
            return await _context.Connections.FindAsync(id);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public IQueryable<Message> GetMessageAsQueryable()
        {
            return _context.Messages.AsQueryable();
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public void RemoveMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
