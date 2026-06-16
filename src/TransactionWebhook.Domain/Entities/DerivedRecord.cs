using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionWebhook.Domain.Entities
{
    public class DerivedRecord
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        public decimal Fee { get; set; }

        public decimal NetAmount { get; set; }
    }
}
