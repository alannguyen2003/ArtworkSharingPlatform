using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces;

public interface ICommissionStatusRepository
{
    CommissionStatus GetById(int id);
}