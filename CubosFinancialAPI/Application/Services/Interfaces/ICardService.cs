using CubosFinancialAPI.DTO.Responses;

namespace CubosFinancialAPI.Application.Services.Interfaces;

public interface ICardService
{
    Task<List<CartaoResponseDto>> GetAllCardsByPeopleAsync(string? userIdGuid, int page, int size);
}
