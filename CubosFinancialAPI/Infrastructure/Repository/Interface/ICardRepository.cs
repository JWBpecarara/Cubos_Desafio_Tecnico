using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface;

public interface ICardRepository
{
    CartaoResponseDto Add(Card card);
    Task<List<CartaoResponseDto>> GetAllCardsByAccountAsync(Guid accountId);
    Task<List<CartaoResponseDto>> GetAllCardsByPeopleAsync(Guid accountId, int skip, int itemsPerPage);
    Task<bool> PhysicalCardExistsAsync(Guid accountId);
}
