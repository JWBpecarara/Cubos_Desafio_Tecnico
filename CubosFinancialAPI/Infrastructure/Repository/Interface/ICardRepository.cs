using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface
{
    public interface ICardRepository
    {
        CartaoResponseDto Add (Card card);
        Task<List<CartaoResponseDto>> GetAllCardsByAccount(Guid accountId);
        Task<bool> PhysicalCardExists(Guid accountId);

    }
}
