using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Email = User.GetEmail();
            var messages = await _messageService.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(new PaginationHeader(
                messages.CurrentPage,
                messages.PageSize,
                messages.TotalCount,
                messages.TotalPage));
            return Ok(messages);
        }
        [HttpGet("artistMessages")]
        public async Task<ActionResult<List<MessageDTO>>> GetMessageBoxForArtist()
        {
            var messages = await _messageService.GetMessageBoxForArtist(User.GetEmail());
            
            return Ok(messages);
        }

    }
}
