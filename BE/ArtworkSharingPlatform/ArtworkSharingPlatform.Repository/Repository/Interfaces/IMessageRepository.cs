using ArtworkSharingPlatform.Domain.Entities.Messages;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces
{
    public interface IMessageRepository 
    {
        void AddGroup(Group group);
        void AddMessage(Message message);
        void RemoveMessage(Message message);
        Task<Connection> GetConnection(string id);
        Task<Message> GetMessage(int id);
        Task<Group> GetMessageGroup(string groupName);
        IQueryable<Message> GetMessageAsQueryable();
        void RemoveConnection(Connection connection);
        Task<bool> SaveChangesAsync();

    }
}
