using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Orders;

public class PreOrder : BaseEntity
{
    private int _buyerId;
    private int _artistId;
    private DateTime _estimateDate;
    private decimal _totalPrice;
    public Artwork? Artwork;
    public Audience? Buyer;
    public Artist? Artist;

    public int BuyerId
    {
        get => _buyerId;
        set => _buyerId = value;
    }

    public int ArtistId
    {
        get => _artistId;
        set => _artistId = value;
    }

    public DateTime EstimateDate
    {
        get => _estimateDate;
        set => _estimateDate = value;
    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set => _totalPrice = value;
    }
}