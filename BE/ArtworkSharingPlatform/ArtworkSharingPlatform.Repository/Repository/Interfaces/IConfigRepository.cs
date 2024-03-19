using ArtworkSharingPlatform.Domain.Entities.Configs;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces
{
    public interface IConfigRepository
    {
        Task<List<ConfigManager>> GetAll();
        Task<ConfigManager> GetConfigById(int id);
        Task<ConfigManager> GetLastestConfig();
        Task UpdateConfig(ConfigManager config);
        Task Insert(ConfigManager configManager);
	}
}