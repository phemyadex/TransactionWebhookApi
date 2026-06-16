using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionWebhook.Domain.Entities
{

    public class Transaction
    {
        public Guid Id { get; set; }

        public string ExternalTransactionId { get; set; } = "";

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
