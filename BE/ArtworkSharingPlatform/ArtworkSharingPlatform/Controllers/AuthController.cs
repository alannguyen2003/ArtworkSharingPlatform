using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Application.Services;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await _authService.Login(loginDTO))
            {
                var tokenString = await _authService.GenerateTokenString(loginDTO);
                var userDto = await _authService.GetUserDTO(loginDTO.Email, tokenString);
                return Ok(userDto);
            }
            return BadRequest("Invalid username or password");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.Register(registerDTO);

			if (result.Succeeded){
                var loginDto = new LoginDTO
                {
                    Email = registerDTO.Email,
                    Password = registerDTO.Password
                };
                var tokenString = await _authService.GenerateTokenString(loginDto);
                var userDto = await _authService.GetUserDTO(loginDto.Email, tokenString);
                return Ok(userDto);
            } 
            return BadRequest(result.Errors);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var code = await _authService.ForgotPassword(email);
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
            await _authService.ResetPassword(resetPassword);
            return Ok("Reset password successfully. Return to login");
        }
    }
}
