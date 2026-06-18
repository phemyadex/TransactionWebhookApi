using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<DerivedRecord> DerivedRecords => Set<DerivedRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasIndex(x => x.ExternalTransactionId)
                .IsUnique();
        }
    }
}
