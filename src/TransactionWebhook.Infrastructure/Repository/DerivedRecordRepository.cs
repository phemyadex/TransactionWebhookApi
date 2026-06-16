using Microsoft.EntityFrameworkCore;
using TransactionWebhook.Application;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Infrastructure.Repository;

public class DerivedRecordRepository : IDerivedRecordRepository
{
    private readonly AppDbContext _dbContext;

    public DerivedRecordRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(DerivedRecord record)
    {
        await _dbContext.DerivedRecords.AddAsync(record);
    }

    public async Task<DerivedRecord?> GetByTransactionIdAsync(Guid transactionId)
    {
        return await _dbContext.DerivedRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TransactionId == transactionId);
    }
}