using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface ICommissionRequestRepository
{
    void Insert(CommissionRequest commissionRequest);
    CommissionRequest GetById(int id);
    void Update(CommissionRequest commissionRequest);
}