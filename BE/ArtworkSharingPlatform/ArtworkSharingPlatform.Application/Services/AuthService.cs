using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;
		private readonly IMapper _mapper;

		public AuthService(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration config,
            IMapper mapper)
        {
            _userManager = userManager;
			_userRepository = userRepository;
			_config = config;
			_mapper = mapper;
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
            if (await IsPhoneExistAsync(user.PhoneNumber))
            {
                throw new Exception("Phone is exist");
            }
            var result = await _userManager.CreateAsync(user, registerBody.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Audience");
            }
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
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,loginDTO.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName,loginDTO.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };
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
            var user = await _userRepository.GetAll().Where(x => x.Email == email).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            if (user != null && !string.IsNullOrEmpty(tokenString))
            {
                var result = _mapper.Map<UserDTO>(user);
                result.Token = tokenString;
                return result;
            }
            return null;
		}

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
                if (user == null)
                {
                    throw new Exception("User cannot be found");
                }
                if (resetPasswordDTO.Password != resetPasswordDTO.ConfirmPassword)
                {
                    throw new Exception("Confirm password must match with password");
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Code, resetPasswordDTO.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Password reset failed: " + result.Errors.FirstOrDefault()?.Description);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user != null;
        }
    }
}
