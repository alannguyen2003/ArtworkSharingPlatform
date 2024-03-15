using ArtworkSharingHost.CloudinaryService;
using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
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
            var currentUserId = User.GetUserId();
            //var currentUserId = 7;
            userParams.CurrentUserId = currentUserId;

            if (userParams.MinPrice > userParams.MaxPrice)
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
            if (artwork == null)
            {
                return NotFound();
            }
            return Ok(artwork);
        }

        [HttpPost]
        public async Task<IActionResult> AddArtwork([FromBody] ArtworkToAddDTO artwork)
        {
            artwork.CreatedDate = DateTime.UtcNow;
            artwork.OwnerId = User.GetUserId();
            var flag = artwork.ArtworkImages.Any(x => x.IsThumbnail == true);
            if (!flag)
            {
                artwork.ArtworkImages.First().IsThumbnail = true;
            }
            await _artworkService.AddArtwork(artwork);
            return Ok(artwork);
        }

        [HttpPost("like")]
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
        [HttpPut("sell/{artworkId}")]
        public async Task<IActionResult> ConfirmSell(int artworkId)
        {
            var isSuccess = await _artworkService.ConfirmSell(artworkId, User.GetUserId());
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("rating")]
        public async Task<IActionResult> UserRating([FromBody] ArtworkRatingDTO rating)
        {
            await _artworkService.UserRating(rating);
            return Ok(new { message = "Rating submitted successfully." });
        }

        [HttpPost("follow/{email}")]
        public async Task<IActionResult> UserFollow(string email)
        {
            await _artworkService.UserFollow(User.GetUserId(), email);
            return Ok(new { message = "User followed successfully." });
        }
        [HttpPost("comment")]
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
        public async Task<IActionResult> DeleteArtwork(int artworkId)
        {
            await _artworkService.DeleteArtwork(artworkId);
            return Ok(new { message = "Artwork deleted successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArtwork([FromBody] ArtworkUpdateDTO artwork)
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
        [HttpGet("search")]
        public async Task<ActionResult<IList<ArtworkDTO>>> ArtworkSearch(string search)
        {
            var results = await _artworkService.SearchArtworkByTitle(search);
            return Ok(results);
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ArtworkDTO>>> ArtworkFilter(int genreId)
        {
            var results = await _artworkService.SearchArtworkByGenre(genreId);
            return Ok(results);
        }

        [HttpGet("GetArtistArtwork")]
        public async Task<IActionResult> GetArtistArtwork()
        {
            var result = await _artworkService.GetArtistArtwork(User.GetUserId());
            return Ok(result);
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage([FromBody] ArtworkImageDTO artworkImage)
        {
            var result = await _artworkService.AddImageToArtwork(artworkImage);
            if (result == null) return BadRequest("Error while adding image");
            return Ok(result);
        }
        [HttpPut("set-thumbnail/{imageId}")]
        public async Task<IActionResult> SetThumbnail(int imageId)
        {
            var result = await _artworkService.SetThumbnail(imageId);
            if (!result) return BadRequest("Error while setting thumbnail image for artwork");
            return Ok();
        }
        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteArtworkImage([FromBody] ArtworkImageDTO imageDTO)
        {
            var publicId = imageDTO.PublicId;
            var result = await _artworkService.DeleteArtworkImage(imageDTO);
            if (!result) return BadRequest("Error while deleting image");
            await _imageService.DeletePhotoAsync(publicId);
            return Ok();
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportArtwork([FromBody] ReportDTO reportDTO)
        {
            reportDTO.ReporterId = User.GetUserId();
            await _artworkService.ReportArtwork(reportDTO);
            return Ok();
        }
    }
}    

