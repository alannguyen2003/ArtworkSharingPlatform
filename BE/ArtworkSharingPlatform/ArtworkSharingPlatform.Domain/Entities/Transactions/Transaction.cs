using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Transactions;

public class Transaction : BaseEntity
{
    private string? _reportName;
    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime _createDate;
    private int _creatorId;
    public ICollection<PackageBilling>? PackageBillings;
    public User? User;
    public Manager? TransactionCreator;

    public string? ReportName
    {
        get => _reportName;
        set => _reportName = value;
    }

    public DateTime StartDate
    {
        get => _startDate;
        set => _startDate = value;
    }

    public DateTime EndDate
    {
        get => _endDate;
        set => _endDate = value;
    }

    public DateTime CreateDate
    {
        get => _createDate;
        set => _createDate = value;
    }

    public int CreatorId
    {
        get => _creatorId;
        set => _creatorId = value;
    }
}