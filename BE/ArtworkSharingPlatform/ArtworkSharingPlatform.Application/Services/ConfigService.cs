using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepository _configRepository;
        private readonly IMapper _mapper;
        public ConfigService(IConfigRepository configRepository, IMapper mapper)
        {
            _configRepository = configRepository;
            _mapper = mapper;
        }

        public async Task<List<ConfigManagerAdminDTO>> GetAll()
        {
            var config = await _configRepository.GetAll();
            var list = config.AsQueryable();
            return list.ProjectTo<ConfigManagerAdminDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<ConfigManagerAdminDTO> GetConfigById(int id)
        {
            var config = await _configRepository.GetConfigById(id);
            return _mapper.Map<ConfigManagerAdminDTO>(config);
        }

        public async Task UpdateConfig(ConfigManager config)
        {
            await _configRepository.UpdateConfig(config);
        }
    }
}
