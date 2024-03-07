using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<IdentityResult> Register(RegisterDTO registerBody)
        {
            var user = new User
            {
                UserName = registerBody.Email,
                Email = registerBody.Email,
                Name = registerBody.Name,
                PhoneNumber = registerBody.PhoneNumber,
                Status = 1
            };
            var result = await _userManager.CreateAsync(user, registerBody.Password);
            return result;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || user.Status == 0)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        }

        public async Task<string> GenerateTokenString(LoginDTO loginDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,loginDTO.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName,loginDTO.Email),
            };

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60),
                Issuer = _config.GetSection("Jwt:Issuer").Value,
                Audience = _config.GetSection("Jwt:Audience").Value,
                SigningCredentials = signingCred};

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityToken);
            return tokenHandler.WriteToken(token);
        }

		public async Task<UserDTO> GetUserDTO(string email, string tokenString)
		{
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && !string.IsNullOrEmpty(tokenString))
            {
                return new UserDTO
                {
                    Email = user.Email,
                    Token = tokenString,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name
                };
            }
            return null;
		}

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
