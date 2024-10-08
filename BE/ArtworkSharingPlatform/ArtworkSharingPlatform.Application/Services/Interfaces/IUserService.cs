﻿using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IUserService
    {
        List<UserInfoDTO> GetAll();
        UserInfoDTO GetUserById(int id);
        UserInfoDTO GetUserByEmail(string email);
        Task<UserProfileDTO>GetArtistProfileByEmail(string email);
        UserInfoAudienceDTO GetUserDetail(int id);
        Task<UserProfileDTO> GetUserProfile(string email);
        Task CreateUserAdmin(User user);
        Task UpdateUserAdmin(User user);
        Task DeleteUserAdmin(User user);
        Task DeleteUserAdminByEmail(string email);
        Task UpdateUserDetail(User user);
        Task<UserImageDTO> GetCurrentUserAvatar(int userId);
        Task ChangeAvatar(UserImageDTO imageDto);
    }
}