using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.User;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("artist/{email}")]
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
        [HttpPut("edit-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        
        {
            var user = _mapper.Map<User>(updateProfileDTO);
            await _userService.UpdateUserDetail(user);
            return Ok();
        }
    }
}
