using ArtworkSharingPlatform.Domain.Entities.Transactions;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class Manager : User
{
    public User? User;
    public ICollection<Transaction>? Transactions;
}