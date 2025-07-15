using CubosFinancialAPI.Application.Services.Interfaces;
using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Enum;
using CubosFinancialAPI.Infrastructure.Repository.Interface;

namespace CubosFinancialAPI.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionResponseDto?> CreateTransactionAsync(Guid accountId, CreateTransactionRequestDto request)
    {
        var tipo = request.Value < 0 ? TransactionType.Debit : TransactionType.Credit;

        if (tipo == TransactionType.Debit)
        {
            var balance = await _transactionRepository.GetBalanceByAccountIdAsync(accountId);
            if (balance + request.Value < 0) return null;// valor é negativo
        }

        var Transaction = new Model.Transaction
        {
            AccountId = accountId,
            Type = tipo,
            Value = request.Value,
            Description = request.Description
        };

        return _transactionRepository.Add(Transaction);
    }
}
