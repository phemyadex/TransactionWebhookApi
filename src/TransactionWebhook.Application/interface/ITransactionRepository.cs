using System;
using System.Collections.Generic;
using System.Text;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Application
{
    public interface ITransactionRepository
    {
        Task<bool> ExistsAsync(string externalId);

        Task AddAsync(Transaction transaction);

        Task SaveChangesAsync();
    }
}
