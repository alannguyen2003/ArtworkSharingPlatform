using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public UserRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetById(int id)
    {
        return _dbContext?.Users?.Where(user => user.Id == id).FirstOrDefault()!;
    }
}