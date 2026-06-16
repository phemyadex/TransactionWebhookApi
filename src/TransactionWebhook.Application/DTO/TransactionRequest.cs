using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionWebhook.Application.DTO
{
    public record TransactionRequest(
    string TransactionId,
    decimal Amount,
    string Currency);
}
