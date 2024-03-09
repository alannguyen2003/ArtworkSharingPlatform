using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;   
        }

        public List<UserInfoDTO> GetAll()
        {
            var users = _userRepository.GetAll();
            return users.ProjectTo<UserInfoDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public UserInfoDTO GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserInfoDTO>(user);
        }

        public UserInfoAudienceDTO GetUserDetail(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserInfoAudienceDTO>(user);
        }

        public async Task CreateUserAdmin(User user)
        {
            await _userRepository.CreateUserAdmin(user);
        }

        public async Task UpdateUserAdmin(User user)
        {
            await _userRepository.UpdateUserAdmin(user);
        }

        public async Task DeleteUserAdmin(User user)
        {
            await _userRepository.DeleteUserAdmin(user);
        }

        public async Task UpdateUserDetail(User user)
        {
            await _userRepository.UpdateUserDetail(user);
        }
    }
}
