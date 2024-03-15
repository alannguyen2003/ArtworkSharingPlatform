using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            return await _transactionRepository.GetAllTransaction();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }
    }
}
