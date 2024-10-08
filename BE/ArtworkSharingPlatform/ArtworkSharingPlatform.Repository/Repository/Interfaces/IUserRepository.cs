﻿using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        User GetByEmail(string email);
        User GetById(int id);
        IQueryable<User> GetAll();
        Task CreateUserAdmin(User user);
        Task UpdateUserAdmin(User user);
        Task DeleteUserAdmin(User user);
        Task DeleteUserAdminByEmail(string email);
        Task UpdateUserDetail(User user);
        Task ChangeAvatar(UserImage image);
        Task<UserImage> GetUserCurrentAvatar(int userId);

    }
}