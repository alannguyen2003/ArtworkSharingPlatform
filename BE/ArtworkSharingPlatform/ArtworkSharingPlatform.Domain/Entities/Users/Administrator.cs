using ArtworkSharingPlatform.Domain.Entities.Configs;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Administrator : User
{
    public ICollection<ConfigManager>? ConfigManagers;
}