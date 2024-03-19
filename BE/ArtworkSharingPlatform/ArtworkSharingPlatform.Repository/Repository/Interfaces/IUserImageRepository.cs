using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces;

public interface IUserImageRepository
{
    public UserImage GetByUserId(int id);
}