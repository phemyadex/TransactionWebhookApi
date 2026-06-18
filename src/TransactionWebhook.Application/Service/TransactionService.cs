using System;
using System.Collections.Generic;
using System.Text;
using TransactionWebhook.Application.DTO;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Application.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactions;
        private readonly IDerivedRecordRepository _derived;

        public TransactionService(
            ITransactionRepository transactions,
            IDerivedRecordRepository derived)
        {
            _transactions = transactions;
            _derived = derived;
        }

        public async Task ProcessAsync(TransactionRequest request)
        {
            if (IsInvalidString(request.TransactionId))
                throw new ArgumentException("Valid Transaction ID is required.");

            if (IsInvalidString(request.Currency))
                throw new ArgumentException("Valid Currency is required.");

            if (request.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            if (await _transactions.ExistsAsync(request.TransactionId))
                throw new InvalidOperationException(
                    $"Transaction ID '{request.TransactionId}' already exists.");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                ExternalTransactionId = request.TransactionId,
                Amount = request.Amount,
                Currency = request.Currency,
                CreatedAt = DateTime.UtcNow
            };

            await _transactions.AddAsync(transaction);

            var fee = request.Amount * 0.02m;

            var derivedRecord = new DerivedRecord
            {
                Id = Guid.NewGuid(),
                TransactionId = transaction.Id,
                Fee = fee,
                NetAmount = request.Amount - fee
            };

            await _derived.AddAsync(derivedRecord);

            await _transactions.SaveChangesAsync();
        }

        private static bool IsInvalidString(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ||
                   value.Trim().Equals("string", StringComparison.OrdinalIgnoreCase);
        }
    }
}
