using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;

namespace CubosFinancialAPI.Application.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDto?> CreateTransactionAsync(Guid accountId, CreateTransactionRequestDto request);
}
