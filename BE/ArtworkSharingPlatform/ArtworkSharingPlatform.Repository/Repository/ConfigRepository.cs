using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly ArtworkSharingPlatformDbContext _dbContext;
        public ConfigRepository(ArtworkSharingPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ConfigManager>> GetAll()
        {
            List<ConfigManager> configs = null;
            try
            {
                configs = await _dbContext.ConfigManagers.Include(c => c.Administrator).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return configs;
        }

        public async Task<ConfigManager> GetConfigById(int id)
        {
            ConfigManager config = null;
            try
            {
                config = await _dbContext.ConfigManagers.Include(c => c.Administrator).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return config;
        }

        public async Task<ConfigManager> GetLastestConfig()
        {
            ConfigManager config = null;
            try
            {
                config = await _dbContext.ConfigManagers
                                          .Include(c => c.Administrator) // Include any related data if necessary
                                          .OrderByDescending(c => c.ConfigDate) // Order by ConfigDate descending
                                          .FirstOrDefaultAsync(); // Get the first record, which is the latest
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return config;
        }


        public async Task UpdateConfig(ConfigManager config)
        {
            try
            {
                var configManager = await GetConfigById(config.Id);
                if (configManager != null)
                {
                    configManager.Id = config.Id;
                    configManager.IsServicePackageConfig = config.IsServicePackageConfig;
                    configManager.IsGeneralConfig = config.IsGeneralConfig;
                    configManager.IsPagingConfig = config.IsPagingConfig;
                    configManager.PackageConfigs = config.PackageConfigs;
                    configManager.LogoUrl = config.LogoUrl;
                    configManager.MyPhoneNumber = config.MyPhoneNumber;
                    configManager.Address = config.Address;
                    configManager.TotalItemPerPage = config.TotalItemPerPage;
                    configManager.RowSize = config.RowSize;
                    configManager.IsAdvertisementConfig = config.IsAdvertisementConfig;
                    configManager.CompanyName = config.CompanyName;
                    configManager.CompanyPhoneNumber = config.CompanyPhoneNumber;
                    configManager.CompanyEmail = config.CompanyEmail;

                    _dbContext.Entry<ConfigManager>(config).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Insert(ConfigManager configManager)
        {
            await _dbContext.ConfigManagers.AddAsync(configManager);
            _dbContext.SaveChanges();
        }
    }
}
