using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Role : BaseEntity
{
    public string? RoleName { get; set; }
    public ICollection<User>? Users { get; set; }
}