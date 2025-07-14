using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository;

public class TransactionRepository(ConnectionContext context) : ITransactionRepository
{
    private readonly ConnectionContext _context = context;

    public TransactionResponseDto Add(Transaction Transaction)
    {
        _context.Transactions.Add(Transaction);
        _context.SaveChanges();

        return new TransactionResponseDto
        {
            Id = Transaction.Id,
            Value = Transaction.Value,
            Description = Transaction.Description,
            CreatedAt = Transaction.CreatedAt,
            UpdatedAt = Transaction.UpdatedAt
        };
    }

    public void AddInternalTransfe(InternalTransfer InternalTransfer)
    {
        _context.InternalTransfers.Add(InternalTransfer);
        _context.SaveChanges();
    }

    public async Task<decimal> GetBalanceByAccountIdAsync(Guid accountId)
    {
        return await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .SumAsync(t => t.Value);
    }
    public async Task<List<TransactionResponseDto>> GetAlltransactionsByAccountAsync(Guid accountId, int skip, int itemsPerPage, string type)
    {
        var transactions = await _context.Transactions
            .Where(c => c.AccountId == accountId && c.Type.ToString().Contains(type))
            .OrderByDescending(c => c.CreatedAt)
            .Skip(skip)
            .Take(itemsPerPage)
            .ToListAsync();

        return transactions.Select(a => new TransactionResponseDto
        {
            Id = a.Id,
            Value = a.Value,
            Description = a.Description,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        }).ToList();
    }

    public async Task<InternalTransfer?> GetInternalTransactionByAccountIdAsync(Guid accountId)
    {
        var resultado = await (from t in _context.InternalTransfers
                               join tOrigin in _context.Transactions
                                   on t.OriginTransactionId equals tOrigin.Id
                               join tDestination in _context.Transactions
                                   on t.DestinationTransactionId equals tDestination.Id
                               where t.OriginTransactionId == accountId || t.DestinationTransactionId == accountId
                               select new InternalTransfer
                               {
                                   Id = t.Id,
                                   OriginTransactionId = t.OriginTransactionId,
                                   OriginValue = tOrigin.Value,
                                   DestinationTransactionId = t.DestinationTransactionId,
                                   DestinationValue = tDestination.Value,
                                   DestinationAccountId = tDestination.AccountId,
                                   OriginAccountId = tOrigin.AccountId
                               }).FirstOrDefaultAsync();

        return resultado;

    }
}
