using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface ICommissionImagesRepository
{
    void Insert(CommissionImage image);
    void DeleteAllByCommissionRequestId(int id);
}