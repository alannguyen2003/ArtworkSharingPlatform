using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Orders;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Artist : User
{
    public int CreditRemaining { get; set; }
}