using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface
{
    public interface IAccountRepository
    {
        AccountResponseDto Add(Account Account);
        Task<bool> AccountExists(string AccountNumber);
        Task<List<AccountResponseDto>> GetAllAccountByPeolpe(Guid userId);
    }
}
