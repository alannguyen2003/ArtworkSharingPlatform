using ArtworkSharingPlatform.DataTransferLayer;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDTO loginDTO);
        Task<bool> Register(RegisterDTO registerBody);
    }
}