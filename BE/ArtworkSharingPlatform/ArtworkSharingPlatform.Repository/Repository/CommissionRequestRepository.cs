using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class CommissionRequestRepository:ICommissionRequestRepository
{
    private ArtworkSharingPlatformDbContext _dbContext;

    public CommissionRequestRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertCommission(CommissionRequest commissionRequest)
    {
        _dbContext.CommissionRequests.Add(commissionRequest);
        _dbContext.SaveChanges();
    }
}