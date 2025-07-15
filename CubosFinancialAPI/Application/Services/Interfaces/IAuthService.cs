using CubosFinancialAPI.DTO.Requests;

namespace CubosFinancialAPI.Application.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginRequestDto people);
}
