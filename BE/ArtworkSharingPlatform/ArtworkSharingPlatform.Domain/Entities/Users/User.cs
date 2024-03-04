using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using Microsoft.AspNetCore.Identity;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class User : IdentityUser<int>
{
    public string? Name { get; set; }
    public int PackageId { get; set; }
    public byte Status { get; set; }
    public int RemainingCredit { get; set; }
    public ICollection<Artwork>? Artworks { get; set; }
    public ICollection<PreOrder>? PreOrders { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    public ICollection<ConfigManager>? ConfigManagers { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}