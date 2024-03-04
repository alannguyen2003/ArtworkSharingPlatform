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
	public class ArtworksController : ControllerBase
	{
		private readonly IArtworkService _artworkService;

		public ArtworksController(IArtworkService artworkService)
        {
			_artworkService = artworkService;
		}

		[HttpGet]
		public async Task<ActionResult<PagedList<ArtworkDTO>>> GetArtworks([FromQuery] UserParams userParams) 
		{
			//var currentUserId = User.GetUserId();
			var currentUserId = 7;
			userParams.CurrentUserId = currentUserId;

			if(userParams.MinPrice > userParams.MaxPrice)
			{
				return BadRequest("Minimum price cannot exceed maximum price");
			}

			var artworks = await _artworkService.GetArtworksAsync(userParams);

			Response.AddPaginationHeader(new PaginationHeader(artworks.CurrentPage, 
																artworks.PageSize,
																artworks.TotalCount,
																artworks.TotalPage));
			return Ok(artworks);
		}
    }
}
