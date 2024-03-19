using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Repository.Repository;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace ArtworkSharingHost.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IAuthService _authService;
        private readonly IArtworkService _artworkService;
        private readonly IMapper _mapper;
        private readonly IHubContext<PresenceHub> _presenceHub;

        public MessageHub(
            IMessageService messageService, 
            IAuthService authService,
            IArtworkService artworkService,
            IMapper mapper,
            IHubContext<PresenceHub> presenceHub
            )
        {
            _messageService = messageService;
            _authService = authService;
            _artworkService = artworkService;
            _mapper = mapper;
            _presenceHub = presenceHub;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"];
            var artwork = httpContext.Request.Query["artworkId"];
            Int32.TryParse(artwork, out int artworkId);
            var groupName = GetGroupName(Context.User.GetEmail(), otherUser, artworkId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await AddToGroup(groupName);

            var messages = await _messageService
                .GetMessageThread(Context.User.GetEmail(), otherUser, artworkId);
            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
        }

        public async Task SendMessage(CreateMessageDTO createMessageDto)
        {
            var senderUsername = Context.User.GetEmail();
            if (senderUsername == createMessageDto.RecipientEmail.ToLower()) throw new HubException("You cannot send messages to yourself");

            var sender = await _authService.GetUserByEmail(senderUsername);
            var recipient = await _authService.GetUserByEmail(createMessageDto.RecipientEmail);

            if (sender == null || recipient == null) throw new HubException("User not found");

            var message = new Message
            {
                //EF Core will automatically populate the SenderId and RecipientId fields 
                //But not for the SenderEmail and RecipientEmail
                Sender = sender,
                Recipient = recipient,
                SenderEmail = senderUsername,
                RecipientEmail = recipient.UserName,
                ArtworkId = createMessageDto.ArtworkId,
                Content = createMessageDto.Content
            };

            var groupName = GetGroupName(senderUsername, recipient.UserName, createMessageDto.ArtworkId);
            var group = await _messageService.GetMessageGroup(groupName);
            if (group.Connections.Any(x => x.Email == recipient.Email))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                var connections = await PresenceTracker.GetConnectionsForUser(recipient.Email);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new
                        {
                            email = sender.Email,
                            artworkId = createMessageDto.ArtworkId
                        });
                }
            }

            _messageService.AddMessage(message);
            if (await _messageService.SaveChangesAsync())
            {
                message.Artwork = _mapper.Map<Artwork>(await _artworkService.GetArtworkAsync(createMessageDto.ArtworkId));
                var messageDto = _mapper.Map<MessageDTO>(message);
				await Clients.Group(groupName).SendAsync("NewMessage", messageDto);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await RemoveFromMessageGroup();
            await base.OnDisconnectedAsync(exception);
        }


        private string GetGroupName(string caller, string other, int artworkId)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}-{artworkId}" : $"{other}-{caller}-{artworkId}";
        }

        private async Task AddToGroup(string groupName)
        {
            var group = await _messageService.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetEmail());

            if (group == null)
            {
                group = new Group(groupName);
                _messageService.AddGroup(group);
            }

            group.Connections.Add(connection);

            if (await _messageService.SaveChangesAsync()) return;
            throw new HubException("Failed to add message");

        }

        private async Task RemoveFromMessageGroup()
        {
            var connection = await _messageService.GetConnection(Context.ConnectionId);
            _messageService.RemoveConnection(connection);

            if (await _messageService.SaveChangesAsync()) return;
            throw new HubException("Failed to remove connection");
        }
    }
}
