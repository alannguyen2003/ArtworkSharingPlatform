namespace ArtworkSharingPlatform.Repository.Repository.Interfaces;

public interface IUserRoleRepository
{
    public List<string> GetRolesByUserId(int id);
}