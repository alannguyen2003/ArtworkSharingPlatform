using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
