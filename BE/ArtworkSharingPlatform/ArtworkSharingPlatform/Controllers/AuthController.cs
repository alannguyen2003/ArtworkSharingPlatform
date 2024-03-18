using ArtworkSharingHost.EmailService;
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
		private readonly IEmailService _emailService;

		public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
			_emailService = emailService;
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
            try
            {
				if (await _emailService.ValidateEmail(registerDTO.Email))
				{
					return BadRequest("Your email is invalid");
				}
				var result = await _authService.Register(registerDTO);
				if (result.Succeeded)
				{
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ForgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
				var code = await _authService.ForgotPassword(email);
				var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				await _emailService.SendAsync("autoemail62@gmail.com", email, "Reset Password Confirmation", $"Please enter this code to reset your password: {token}");
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
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
            try
            {
				await _authService.ResetPassword(resetPassword);
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}
