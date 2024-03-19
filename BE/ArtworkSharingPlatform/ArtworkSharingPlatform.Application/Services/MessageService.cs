using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
		private readonly IArtworkRepository _artworkRepository;
		private readonly IMapper _mapper;

        public MessageService(
            IMessageRepository messageRepository, 
            IArtworkRepository artworkRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
			_artworkRepository = artworkRepository;
			_mapper = mapper;
        }
        public void AddGroup(Group group)
        {
             _messageRepository.AddGroup(group);
        }

        public void AddMessage(Message message)
        {
            _messageRepository.AddMessage(message);
        }

        public async Task<Connection> GetConnection(string id)
        {
            return await _messageRepository.GetConnection(id);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _messageRepository.GetMessage(id);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _messageRepository.GetMessageGroup(groupName);
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _messageRepository.GetMessageAsQueryable();
            query = query.OrderByDescending(x => x.MessageSent).AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientEmail == messageParams.Email),
                "Outbox" => query.Where(u => u.SenderEmail == messageParams.Email),
                _ => query.Where(u => u.RecipientEmail == messageParams.Email && u.DateRead == null)
            };
            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);
            return await PagedList<MessageDTO>
                            .CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentEmail, string recipientEmail, int artworkId)
        {
            var query = _messageRepository.GetMessageAsQueryable();
            query = query.Where(
                m => m.SenderEmail == currentEmail &&
                m.RecipientEmail == recipientEmail &&
                m.ArtworkId == artworkId
                ||
                m.SenderEmail == recipientEmail &&
                m.RecipientEmail == currentEmail &&
                m.ArtworkId == artworkId
                )
                .OrderBy(m => m.MessageSent)
                .AsQueryable();
            var unreadMessages = query.Where(m => m.DateRead == null && m.RecipientEmail == currentEmail && m.ArtworkId == artworkId).ToList();

            if (unreadMessages.Count > 0)
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
                await _messageRepository.SaveChangesAsync();
            }
            return await query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void RemoveConnection(Connection connection)
        {
            _messageRepository.RemoveConnection(connection);
        }

        public void RemoveMessage(Message message)
        {
            _messageRepository.RemoveMessage(message);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _messageRepository.SaveChangesAsync();
        }

        public async Task<List<MessageDTO>> GetMessageBoxForArtist(string artistEmail)
        {
            var query = _messageRepository.GetMessageAsQueryable();
            query = query.OrderByDescending(x => x.MessageSent).AsQueryable();

            query = query.Where(x => x.RecipientEmail == artistEmail);
            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);
            List<MessageDTO> result = new List<MessageDTO>();
            foreach (var message in messages)
            {
                if(await _artworkRepository.CheckArtworkAvailability(message.Artwork.Id))
                {
					if (result.Count == 0)
					{
						result.Add(message);
					}
					else
					{
						var flag = false;
						foreach (var m in result)
						{
							if (!result.Any(x => x.SenderEmail == message.SenderEmail && x.Artwork.Id == message.Artwork.Id))
							{
								flag = true;
							}
						}
						if (flag)
						{
							result.Add(message);
						}
					}
				}
            }
            return result;
        }
        
    }
}
