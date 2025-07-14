using CubosFinancialAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure;

public class ConnectionContext(DbContextOptions<ConnectionContext> options) : DbContext(options)
{
    public DbSet<People> Peoples { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<InternalTransfer> InternalTransfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>()
            .Property(c => c.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}
