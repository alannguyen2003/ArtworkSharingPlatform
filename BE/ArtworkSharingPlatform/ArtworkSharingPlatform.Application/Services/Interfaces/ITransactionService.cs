using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Transactions;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransaction();
        Task<Transaction> GetTransactionById(int id);
        Task AddTransaction(TransactionDTO transactionDTO);
    }
}