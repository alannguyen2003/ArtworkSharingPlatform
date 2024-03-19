using ArtworkSharingHost.CloudinaryService;
using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Artwork;
using ArtworkSharingPlatform.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        private readonly IImageService _imageService;

        public ArtworksController(IArtworkService artworkService, IImageService imageService)
        {
            _artworkService = artworkService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ArtworkDTO>>> GetArtworks([FromQuery] UserParams userParams)
        {
			string genreIds = Request.Query["genres"]; 
            if(!string.IsNullOrEmpty(genreIds))
            {
                try
                {
					int[] genreIdsArray = genreIds?.Split(',').Select(int.Parse).ToArray();
                    userParams.GenreIds = genreIdsArray;
				}
                catch (Exception ex)
                {
                    return BadRequest("Invalid genres");
                }
                
			}

            if (userParams.MinPrice > userParams.MaxPrice)
            {
                return BadRequest("Minimum price cannot exceed maximum price");
            }

            if (userParams.MinPrice < 0 || userParams.MaxPrice< 0)
            {
                return BadRequest("Minimum price or Maximum Price must not be below 0.");
            }

            var artworks = await _artworkService.GetArtworksAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(artworks.CurrentPage,
                                                                artworks.PageSize,
                                                                artworks.TotalCount,
                                                                artworks.TotalPage));
            return Ok(artworks);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
        {
            var artwork = await _artworkService.GetArtworkAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }
            return Ok(artwork);
        }
		[HttpGet("rating/{artworkId}")]
        [Authorize]
		public async Task<ActionResult<int>> GetArtworkRatingForUser(int artworkId)
		{
			var rating = await _artworkService.GetArtworkRatingForUser(User.GetUserId(), artworkId);
			return Ok(rating);
		}

		[HttpPost]
        [Authorize(Policy = "RequireArtistRole")]
        public async Task<IActionResult> AddArtwork([FromBody] ArtworkToAddDTO artwork)
        {
            try
            {
				artwork.CreatedDate = DateTime.UtcNow;
				artwork.OwnerId = User.GetUserId();
				artwork.Status = 1;
				var flag = artwork.ArtworkImages.Any(x => x.IsThumbnail == true);
				if (!flag)
				{
					artwork.ArtworkImages.First().IsThumbnail = true;
				}
				await _artworkService.AddArtwork(artwork);
				return Ok(artwork);
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("like")]
        [Authorize]
        public async Task<IActionResult> UserLike([FromBody] int artworkId)
        {
            var like = new ArtworkLikeDTO
            {
                UserId = User.GetUserId(),
                ArtworkId = artworkId
            };
            await _artworkService.UserLike(like);
            return Ok();
        }
        [HttpPut("sell/{artworkId}/{buyUserEmail}")]
        public async Task<IActionResult> ConfirmSell(int artworkId, string buyUserEmail)
        {
            var isSuccess = await _artworkService.ConfirmSell(artworkId, User.GetUserId(), buyUserEmail);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("follow/{email}")]
        [Authorize]
        public async Task<IActionResult> UserFollow(string email)
        {
            if(email == User.GetEmail()) return BadRequest("You cannot follow yourself");
            await _artworkService.UserFollow(User.GetUserId(), email);
            return Ok(new { message = "User followed successfully." });
        }
        [HttpGet("comment/{artworkId}")]
        [Authorize]
        public async Task<IActionResult> GetArtworkComments(int artworkId)
        {
            var comments = await _artworkService.GetArtworkComments(artworkId);
            return Ok(comments);
        }
		[HttpGet("comment-number/{artworkId}")]
        [Authorize]
		public async Task<IActionResult> GetArtworkCommentNumber(int artworkId)
		{
			var comments = await _artworkService.GetArtworkComments(artworkId);
            var result = 0;
            if(comments != null) result = comments.Count();
            return Ok(result);
		}
		[HttpPost("comment")]
        [Authorize]
        public async Task<IActionResult> UserComment([FromBody] string content, int artworkId)
        {
            var comment = new ArtworkCommentDTO
            {
                UserId = User.GetUserId(),
                Content = content,
                ArtworkId = artworkId
            };
            await _artworkService.ArtworkComment(comment);
            return Ok(new { message = "User comment successfully." });
        }

        [HttpDelete("{artworkId}")]
        [Authorize]
        public async Task<IActionResult> DeleteArtwork(int artworkId)
        {
            await _artworkService.DeleteArtwork(artworkId);
            return Ok(new { message = "Artwork deleted successfully." });
        }

        [HttpPut]
        [Authorize(Policy = "RequireArtistRole")]
        public async Task<IActionResult> UpdateArtwork([FromBody] ArtworkUpdateDTO artwork)
        {
            await _artworkService.UpdateArtwork(artwork);
            return Ok(new { message = "Artwork updated successfully." });
        }
        [HttpGet("GetArtistArtwork")]
        [Authorize(Policy = "RequireArtistRole")]
        public async Task<IActionResult> GetArtistArtwork()
        {
            var result = await _artworkService.GetArtistArtwork(User.GetUserId());
            return Ok(result);
        }
        [HttpPost("add-image")]
        [Authorize(Policy = "RequireArtistRole")]
        public async Task<IActionResult> AddImage([FromBody] ArtworkImageDTO artworkImage)
        {
            var result = await _artworkService.AddImageToArtwork(artworkImage);
            if (result == null) return BadRequest("Error while adding image");
            return Ok(result);
        }
        [HttpPut("set-thumbnail/{imageId}")]
		[Authorize(Policy = "RequireArtistRole")]
		public async Task<IActionResult> SetThumbnail(int imageId)
        {
            var result = await _artworkService.SetThumbnail(imageId);
            if (!result) return BadRequest("Error while setting thumbnail image for artwork");
            return Ok();
        }
        [HttpDelete("delete-image")]
		[Authorize(Policy = "RequireArtistRole")]
		public async Task<IActionResult> DeleteArtworkImage([FromBody] ArtworkImageDTO imageDTO)
        {
            var publicId = imageDTO.PublicId;
            var result = await _artworkService.DeleteArtworkImage(imageDTO);
            if (!result) return BadRequest("Error while deleting image");
            await _imageService.DeletePhotoAsync(publicId);
            return Ok();
        }

        [HttpPost("report")]
        [Authorize]
        public async Task<IActionResult> ReportArtwork([FromBody] ReportDTO reportDTO)
        {
            reportDTO.ReporterId = User.GetUserId();
            reportDTO.CreatedDate = DateTime.UtcNow;
            await _artworkService.ReportArtwork(reportDTO);
            return Ok();
        }

        //[HttpPut("active-artwork/{artworkId}")]
       // public async Task<IActionResult> ActiveArtwork(int artworkId)
       // {
        //    var userId = User.GetUserId();
        //    await _artworkService.ActiveArtworkStatus(artworkId, userId);
         //   return Ok();
        //}
    }
}    

