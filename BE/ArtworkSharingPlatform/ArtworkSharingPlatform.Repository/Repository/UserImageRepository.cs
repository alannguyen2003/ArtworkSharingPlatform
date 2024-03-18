using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class UserImageRepository:IUserImageRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public UserImageRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public UserImage GetByUserId(int userId)
    {
        return _dbContext.UserImages.FirstOrDefault(userImage => userImage.UserId == userId)!;
    }
}