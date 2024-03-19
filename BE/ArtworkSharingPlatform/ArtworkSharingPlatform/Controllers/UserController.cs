using ArtworkSharingHost.CloudinaryService;
using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.User;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
		private readonly IImageService _imageService;
		private readonly IMapper _mapper;
        private readonly IPackageService _packageService;
        public UserController(
            IUserService userService, 
            IImageService imageService,
            IMapper mapper,
            IPackageService packageService)
        {
            _userService = userService;
			_imageService = imageService;
			_mapper = mapper;
            _packageService = packageService;
        }
        [HttpGet("artist/{email}")]
        [Authorize]
        public async Task<IActionResult> GetArtistProfile(string email)
        {
            var artistProfile = await _userService.GetArtistProfileByEmail(email);
            if (artistProfile == null)
            {
                return NotFound("Artist Not Found");
            }
            return Ok(artistProfile);
        }

        [HttpGet("/detail/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var userDto = _userService.GetUserDetail(id);
                if (userDto == null)
                {
                    return NotFound("User not found");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateDetail")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetail([FromBody]UserDetailUpdateDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                await _userService.UpdateUserDetail(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpGet("get-profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {

            var result = await _userService.GetUserProfile(User.GetEmail());
            return Ok(result);
        }
        [HttpPut("edit-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        
        {
            var user = _mapper.Map<User>(updateProfileDTO);
            await _userService.UpdateUserDetail(user);
            return Ok();
        }

		[HttpPut("change-avatar")]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar([FromBody] UserImageDTO userImageDTO)

		{
            if (string.IsNullOrEmpty(userImageDTO.PublicId)) return BadRequest("Change your image first in order to save");
            var currentAvatar = await _userService.GetCurrentUserAvatar(User.GetUserId());
            userImageDTO.UserId = User.GetUserId();
            await _userService.ChangeAvatar(userImageDTO);
            if (currentAvatar != null && !string.IsNullOrEmpty(currentAvatar.PublicId))
            {
                await _imageService.DeletePhotoAsync(currentAvatar.PublicId);
            }
			return Ok();
		}
        [HttpPut("buy-package/{packageId}")]
        [Authorize]
        public async Task<IActionResult> BuyPackage(int packageId)
        {
            int userId = User.GetUserId();
            var buyPackage = await _packageService.UserBuyPackage(userId, packageId);
            return Ok(buyPackage);
        }

        [HttpPost("get-with-email")]
        public async Task<IActionResult> GetUserWithEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            return Ok(user);
        }

    }
}
