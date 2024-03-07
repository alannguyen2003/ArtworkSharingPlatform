using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IMessageService
    {
        void AddGroup(Group group);
        void AddMessage(Message message);
        void RemoveMessage(Message message);
        Task<Connection> GetConnection(string id);
        Task<Message> GetMessage(int id);
        Task<Group> GetMessageGroup(string groupName);
        Task<IEnumerable<MessageDTO>> GetMessageThread(string currentEmail, string recipientEmail, int artworkId);
        Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);
        void RemoveConnection(Connection connection);
        Task<List<MessageDTO>> GetMessageBoxForArtist(string artistEmail);
        Task<bool> SaveChangesAsync();
    }
}
