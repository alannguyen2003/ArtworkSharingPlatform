using ArtworkSharingPlatform.Domain.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole>? UserRoles { get; set; }
}