
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface;

public interface ITransactionRepository
{
    Task<decimal> GetBalanceByAccountIdAsync(Guid accountId);
    TransactionResponseDto Add(Transaction Transaction);
    void AddInternalTransfe(InternalTransfer InternalTransfer);
    Task<InternalTransfer?> GetInternalTransactionByAccountIdAsync(Guid accountId);
    Task<List<TransactionResponseDto>> GetAlltransactionsByAccountAsync(Guid accountId, int skip, int itemsPerPage, string type);
}
