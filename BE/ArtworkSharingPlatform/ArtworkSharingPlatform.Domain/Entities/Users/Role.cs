using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Role:BaseEntity
{
    private string? _roleName;
    public User? User;

    public string? RoleName
    {
        get => _roleName;
        set => _roleName = value;
    }
}