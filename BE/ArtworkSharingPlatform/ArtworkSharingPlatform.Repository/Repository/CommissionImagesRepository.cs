using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class CommissionImagesRepository : ICommissionImagesRepository
{
    public ArtworkSharingPlatformDbContext _dbContext;

    public CommissionImagesRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(CommissionImage image)
    {
        _dbContext.CommissionImages.Add(image);
        _dbContext.SaveChanges();
    }

    public void DeleteAllByCommissionRequestId(int id)
    {
        _dbContext.CommissionImages
            .RemoveRange(_dbContext.CommissionImages
                .Where(commImg => commImg.CommissionRequestId == id));
        _dbContext.SaveChanges();
    }
}