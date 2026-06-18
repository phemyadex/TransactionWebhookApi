using System;
using System.Collections.Generic;
using System.Text;
using TransactionWebhook.Application.DTO;

namespace TransactionWebhook.Application.Service
{
    public interface ITransactionService
    {
        Task ProcessAsync(TransactionRequest request);
    }
}
