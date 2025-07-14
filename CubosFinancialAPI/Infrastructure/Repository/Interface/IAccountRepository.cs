using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface;

public interface IAccountRepository
{
    AccountResponseDto Add(Account Account);
    Task<bool> AccountExistsAsync(string AccountNumber);
    Task<List<AccountResponseDto>> GetAllAccountByPeolpeAsync(Guid userId);
}
