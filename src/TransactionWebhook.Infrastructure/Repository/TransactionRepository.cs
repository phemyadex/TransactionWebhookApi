using System;
using System.Collections.Generic;
using System.Text;
using TransactionWebhook.Application;
using TransactionWebhook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TransactionWebhook.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _db.Transactions.AnyAsync(x => x.ExternalTransactionId == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _db.Transactions.AddAsync(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}
