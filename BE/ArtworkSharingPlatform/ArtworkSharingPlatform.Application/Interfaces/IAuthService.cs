using ArtworkSharingPlatform.DataTransferLayer;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginDTO loginDTO);
        Task<bool> Login(LoginDTO loginDTO);
        Task<bool> Register(RegisterDTO registerBody);
    }
}