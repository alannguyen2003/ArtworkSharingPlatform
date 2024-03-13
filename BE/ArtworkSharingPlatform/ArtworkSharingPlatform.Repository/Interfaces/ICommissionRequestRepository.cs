using ArtworkSharingPlatform.Domain.Entities.Commissions;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface ICommissionRequestRepository
{
    void Insert(CommissionRequest commissionRequest);
    CommissionRequest GetById(int id);
    IEnumerable<CommissionRequest> GetAllBySenderId(int senderId);
    IEnumerable<CommissionRequest> GetAllByReceiverId(int senderId);
    void Update(CommissionRequest commissionRequest);
    Task<List<CommissionRequest>> GetAllCommission();
}