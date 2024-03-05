using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Register(RegisterDTO registerBody)
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
            return result.Succeeded;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null || user.Status == 0)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        }
    }
}
