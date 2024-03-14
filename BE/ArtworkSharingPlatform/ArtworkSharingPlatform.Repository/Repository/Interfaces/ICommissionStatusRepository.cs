using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface ICommissionStatusRepository
{
    CommissionStatus GetById(int id);
}