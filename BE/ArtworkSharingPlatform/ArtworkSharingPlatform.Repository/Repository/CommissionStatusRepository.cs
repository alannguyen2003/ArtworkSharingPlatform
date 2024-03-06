using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class CommissionStatusRepository : ICommissionStatusRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public CommissionStatusRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CommissionStatus GetById(int id)
    {
        return _dbContext?.CommissionStatus?
            .Where(commStatus => commStatus.Id == id)
            .FirstOrDefault()!;
    }
}