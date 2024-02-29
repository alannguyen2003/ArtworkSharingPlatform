using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Transactions;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int PackageId { get; set; }
    public byte Status { get; set; }
    public int RemainingCredit { get; set; }
    public ICollection<Artwork>? Artworks { get; set; }
    public ICollection<PreOrder>? PreOrders { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public Role? Role { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    public ICollection<ConfigManager>? ConfigManagers { get; set; }

    public ICollection<Like>? Likes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
}