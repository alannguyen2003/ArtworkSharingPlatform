using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
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

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var code = await _userService.ForgotPassword(email);
            var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            return Ok(token);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var resetPassword = new ResetPasswordDTO
            {
                Email = resetPasswordDTO.Email,
                Password = resetPasswordDTO.Password,
                ConfirmPassword = resetPasswordDTO.ConfirmPassword,
                Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDTO.Code))
            };
            await _userService.ResetPassword(resetPassword);
            return Ok("Reset password successfully. Return to login");
        }
    }
}
