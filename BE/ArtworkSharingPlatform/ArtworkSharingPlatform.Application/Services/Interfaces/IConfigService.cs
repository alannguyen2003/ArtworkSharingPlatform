using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.ConfigManager;
using ArtworkSharingPlatform.Domain.Entities.Configs;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IConfigService
    {
        Task<List<ConfigManagerAdminDTO>> GetAll();
        Task<ConfigManagerAdminDTO> GetConfigById(int id);
        ConfigManagerServiceResponse Create(NewConfigManagerRequest newConfigManagerRequest);
    }
}