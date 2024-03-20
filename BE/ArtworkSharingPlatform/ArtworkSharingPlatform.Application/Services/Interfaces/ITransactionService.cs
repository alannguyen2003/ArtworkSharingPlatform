using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ClosedXML.Excel;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransaction();
        Task<Transaction> GetTransactionById(int id);
        Task AddTransaction(TransactionDTO transactionDTO);
        Task<XLWorkbook> ExportTransaction(int id);
    }
}