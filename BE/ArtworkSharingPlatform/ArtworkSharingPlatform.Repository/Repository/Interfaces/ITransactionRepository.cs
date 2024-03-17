using ArtworkSharingPlatform.Domain.Entities.Transactions;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransaction();
        Task<Transaction> GetTransactionById(int id);
    }
}