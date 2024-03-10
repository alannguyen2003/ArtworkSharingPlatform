using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface IUserRoleRepository
{
    public List<string> GetRolesByUserId(int id);
}