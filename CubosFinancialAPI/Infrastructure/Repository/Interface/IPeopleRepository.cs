using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Infrastructure.Repository.Interface;

public interface IPeopleRepository
{
    PostPeopleResponseDto Add(People People);
    Task<People?> LoginAsync(LoginRequestDto People);
}
