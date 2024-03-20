using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            return await _transactionRepository.GetAllTransaction();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }
        public async Task AddTransaction(TransactionDTO transactionDTO)
        {
            var transaction = _mapper.Map<Transaction>(transactionDTO);
            await _transactionRepository.AddTransaction(transaction);
        }

        public async Task<XLWorkbook> ExportTransaction(int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null) throw new NullReferenceException("Transaction not found");

            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Transaction Detail");
            worksheet.Cell(1, 1).Value = "Transaction ID";
            worksheet.Cell(1, 2).Value = transaction.Id;
            worksheet.Cell(2, 1).Value = "Report Name";
            worksheet.Cell(2, 2).Value = transaction.ReportName;
            worksheet.Cell(3, 1).Value = "Create Date";
            worksheet.Cell(3, 2).Value = transaction.CreateDate;
            worksheet.Cell(4, 1).Value = "Total Price";
            worksheet.Cell(4, 2).Value = transaction.TotalPrice;
            worksheet.Cell(5, 1).Value = "Sender ID";
            worksheet.Cell(5, 2).Value = transaction.SenderId;

            return workbook;
        }

        public async Task<XLWorkbook> ExportTransactionList()
        {
            var transactions = await _transactionRepository.GetAllTransaction();
            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Transactions");

            // Thêm tiêu đề cho các cột
            worksheet.Cell(1, 1).Value = "Transaction ID";
            worksheet.Cell(1, 2).Value = "Report Name";
            worksheet.Cell(1, 3).Value = "Create Date";
            worksheet.Cell(1, 4).Value = "Total Price";
            worksheet.Cell(1, 5).Value = "Sender ID";

            int currentRow = 2;
            foreach (var transaction in transactions)
            {
                worksheet.Cell(currentRow, 1).Value = transaction.Id;
                worksheet.Cell(currentRow, 2).Value = transaction.ReportName;
                worksheet.Cell(currentRow, 3).Value = transaction.CreateDate;
                worksheet.Cell(currentRow, 4).Value = transaction.TotalPrice;
                worksheet.Cell(currentRow, 5).Value = transaction.SenderId;
                currentRow++;
            }

            return workbook;
        }

    }
}
