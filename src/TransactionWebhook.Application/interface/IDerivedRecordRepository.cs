using System;
using System.Collections.Generic;
using System.Text;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Application
{
    public interface IDerivedRecordRepository
    {
        Task AddAsync(DerivedRecord record);

        Task<DerivedRecord?> GetByTransactionIdAsync(Guid transactionId);
    }
}
