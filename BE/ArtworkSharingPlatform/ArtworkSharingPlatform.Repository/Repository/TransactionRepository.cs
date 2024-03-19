using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ArtworkSharingPlatformDbContext _dbContext;
        public TransactionRepository(ArtworkSharingPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            List<Transaction> transactions = null;
            try
            {
                transactions = await _dbContext.Transactions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            Transaction transaction = null;
            try
            {
                transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return transaction;
        }
        public async Task AddTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                await _dbContext.Transactions.AddAsync(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
