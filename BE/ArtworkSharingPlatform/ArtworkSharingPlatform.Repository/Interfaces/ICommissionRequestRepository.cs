using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface ICommissionRequestRepository
{
    void InsertCommission(CommissionRequest commissionRequest);
}