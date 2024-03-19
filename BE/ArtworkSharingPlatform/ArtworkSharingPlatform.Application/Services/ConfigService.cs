using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.ConfigManager;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Repository.Repository;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ArtworkSharingPlatform.Application.Services
{
	public class ConfigService : IConfigService
    {
        private readonly IConfigRepository _configRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private string _validationError;
        private NewConfigManagerRequest _newConfigManagerRequest;
        private ConfigManagerServiceResponseValidation _configManagerServiceResponseValidation;
        private ConfigManager _configManager;

        public ConfigService(IConfigRepository configRepository, IMapper mapper, 
            IUserRepository userRepository,IUserRoleRepository userRoleRepository)
        {
            _configRepository = configRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
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

        public ConfigManagerServiceResponse Create(NewConfigManagerRequest newConfigManagerRequest)
        {
            _newConfigManagerRequest = newConfigManagerRequest;
            if (!IsUserAnAdmin()) return NotAnAdminResult();
            if (!IsRequestValid()) 
                return InvalidRequestResult();
            MapRequestDTOToEntity();
            InsertConfigManagerEntityToDb();
            return SuccessConfigManagerInsertionResult();
        }

        private bool IsUserAnAdmin()
        {
            int adminIdTest = _newConfigManagerRequest.AdministratorId;
            List<string> roles = _userRoleRepository.GetRolesByUserId(adminIdTest);
            if (roles.Contains("Admin")) return true;
            return false;
        }
        
        private bool IsRequestValid()
        {
            var res = ConfigManagerServiceValidation.IsNewConfigManagerRequestValid(_newConfigManagerRequest);
            _validationError = res.Message;
            if (!res.Success) return false;
            return true;
        }
        private void MapRequestDTOToEntity()
        {
            _configManager = _mapper.Map<ConfigManager>(_newConfigManagerRequest);
            _configManager.ConfigDate = DateTime.Today;
        
        }
        private void InsertConfigManagerEntityToDb()
        {
            _configRepository.Insert(_configManager);
        }
        private ConfigManagerServiceResponse NotAnAdminResult()
        {
            return new ConfigManagerServiceResponse()
            {
                Result = ConfigManagerServiceResult.NOT_AN_ADMIN,
                StatusCode = ConfigManagerStatusCode.NOT_AN_ADMIN,
                Message = $"The user with id = {_newConfigManagerRequest.AdministratorId} " +
                          $"is not an Administrator. Please log in again as an Administrator."
            };
        }
        private ConfigManagerServiceResponse InvalidRequestResult()
        {
            return new ConfigManagerServiceResponse()
            {
                Result = ConfigManagerServiceResult.INVALID_REQUEST,
                StatusCode = ConfigManagerStatusCode.INVALID_REQUEST,
                Message = _validationError
            };
        }
        private ConfigManagerServiceResponse SuccessConfigManagerInsertionResult()
        {
            return new ConfigManagerServiceResponse()
            {
                Result = ConfigManagerServiceResult.SUCCESS,
                StatusCode = ConfigManagerStatusCode.SUCCESS,
                Message = $"Successfully inserted the new" +
                          $" ConfigManager into database for Administrator with Id = {_newConfigManagerRequest.AdministratorId}"
            };
        }

        public async Task UpdateConfig(ConfigManager config)
        {
            await _configRepository.UpdateConfig(config);
        }
    }
}
