using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class User : IdentityUser<int>
{
    public string? Name { get; set; }
    public int PackageId { get; set; }
    public byte Status { get; set; }
    public string Description { get; set; }
    public string? FacebookLink{ get; set; }
    public string? TwitterLink{ get; set; }
    [JsonIgnore]
    public UserImage UserImage { get; set; }
    public int RemainingCredit { get; set; }
    public ICollection<Artwork>? Artworks { get; set; }
    public ICollection<PreOrder>? PreOrders { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    public ICollection<ConfigManager>? ConfigManagers { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    [JsonIgnore]
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Message>? MessageReceived;
    public ICollection<Message>? MessageSent;
    public ICollection<CommissionRequest>? CommissionSent { get; set; }
    public ICollection<CommissionRequest>? CommissionReceived{ get; set; }
    public List<Follow> FollowedUsers { get; set; }
    public List<Follow> IsFollowedByUsers { get; set; }
}