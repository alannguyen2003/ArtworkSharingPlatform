using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface IUserRepository
{
    User GetById(int id);
    IQueryable<User> GetAll();
    Task CreateUserAdmin(User user);
    Task UpdateUserAdmin(User user);
    Task DeleteUserAdmin(User user);
    Task UpdateUserDetail(User user);
}