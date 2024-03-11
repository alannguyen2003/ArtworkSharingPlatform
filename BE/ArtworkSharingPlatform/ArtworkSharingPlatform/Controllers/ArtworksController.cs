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
			var currentUserId = User.GetUserId();
			//var currentUserId = 7;
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

		[HttpGet("{id}")]
		public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
		{
			var artwork = await _artworkService.GetArtworkAsync(id);
			if(artwork == null)
			{
				return NotFound();
			}
			return Ok(artwork);
		}

        [HttpPost("like")]
        public async Task<IActionResult> UserLike([FromBody] ArtworkLikeDTO like)
        {
            like.UserId = User.GetUserId();
            await _artworkService.UserLike(like);
            return Ok(new { message = "Artwork liked successfully." });
        }

        [HttpPost("rating")]
        public async Task<IActionResult> UserRating([FromBody] ArtworkRatingDTO rating)
        {
            await _artworkService.UserRating(rating);
            return Ok(new { message = "Rating submitted successfully." });
        }

        [HttpPost("follow")]
        public async Task<IActionResult> UserFollow([FromBody] UserFollowDTO follow)
        {
            await _artworkService.UserFollow(follow);
            return Ok(new { message = "User followed successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> AddArtwork([FromBody] ArtworkDTO artwork)
        {
            await _artworkService.AddArtwork(artwork);
            return CreatedAtAction(nameof(GetArtwork), new { id = artwork.Id }, new { message = "Artwork added successfully.", artwork });
        }

        [HttpDelete("{artworkId}")]
        public async Task<IActionResult> DeleteArtwork(int artworkId)
        {
            await _artworkService.DeleteArtwork(artworkId);
            return Ok(new { message = "Artwork deleted successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArtwork([FromBody] ArtworkDTO artwork)
        {
            await _artworkService.UpdateArtwork(artwork);
            return Ok(new { message = "Artwork updated successfully." });
        }

        [HttpGet("likes")]
        public async Task<ActionResult<IEnumerable<ArtworkLikeToShowDTO>>> GetArtworksLike()
        {
            var artworksLikes = await _artworkService.GetArtworksLike(User.GetUserId());
            return Ok(artworksLikes);
        }

    }
}
