using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenString(LoginDTO loginDTO);
        Task<bool> Login(LoginDTO loginDTO);
        Task<UserDTO> GetUserDTO(string email, string tokenString);
        Task<IdentityResult> Register(RegisterDTO registerBody);
    }
}