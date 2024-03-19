using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Repository.Repository;

public class CommissionRequestRepository : ICommissionRequestRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public CommissionRequestRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CommissionRequest>> GetAllCommission()
    {
        List<CommissionRequest> commission = null;
        try
        {
            commission = _dbContext.CommissionRequests
                .Include(c => c.Receiver)
                .Include(c => c.Sender)
                .Include(c => c.CommissionStatus)
                .Include(c => c.Genre)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return commission;
    }

    public void Insert(CommissionRequest commissionRequest)
    {
        _dbContext.CommissionRequests.Add(commissionRequest);
        _dbContext.SaveChanges();
    }

    public CommissionRequest GetById(int id)
    {
        return _dbContext.CommissionRequests
                .Include(c => c.Receiver)
                .Include(c => c.Sender)
                .Include(c => c.CommissionStatus)
                .Include(c => c.Genre)
                .FirstOrDefault();
    }

    public IEnumerable<CommissionRequest> GetAllBySenderId(int senderId)
    {
        return _dbContext.CommissionRequests.Where(commissionRequest => commissionRequest.SenderId == senderId);
    }

    public IEnumerable<CommissionRequest> GetAllByReceiverId(int receiverId)
    {
        return _dbContext.CommissionRequests.Where(commissionRequest => commissionRequest.ReceiverId == receiverId);
    }

    public void Update(CommissionRequest commissionRequest)
    {
        _dbContext.Update(commissionRequest);
        _dbContext.SaveChanges();
    }
}