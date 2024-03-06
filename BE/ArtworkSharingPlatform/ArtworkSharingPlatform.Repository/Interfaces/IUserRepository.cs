using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface IUserRepository
{
    User GetById(int id);
}