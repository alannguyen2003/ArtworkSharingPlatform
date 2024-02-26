using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Orders;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Artist : User
{
    private int _creditRemaining;
    public User? User;
    public ICollection<Artwork>? Artwork;
    public ICollection<PreOrder>? PreOrders;
    public int CreditRemaining
    {
        get => _creditRemaining;
        set => _creditRemaining = value;
    }
}