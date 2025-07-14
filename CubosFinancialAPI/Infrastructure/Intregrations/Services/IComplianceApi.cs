using CubosFinancialAPI.Infrastructure.Intregrations.Models.Requests;
using CubosFinancialAPI.Infrastructure.Intregrations.Models.Responses;
using Refit;

namespace CubosFinancialApi.Infrastructure.Intregrations.Clientes;

public interface IComplianceApi
{
    [Post("/auth/code")]
    Task<AuthCodeResponse> GetAuthCode(Authentication Authentication);

    [Post("/auth/token")]
    Task<TokenResponse> GetToken(AuthenticationCode AuthenticationCode);

    [Post("/cpf/validate")]
    Task<ValidationResponse> ValidateCpfAsync(DocumentNumber DocumentNumber, [Authorize("Bearer")] string token);

    [Post("/cnpj/validate")]
    Task<ValidationResponse> ValidateCnpjAsync(DocumentNumber DocumentNumber, [Authorize("Bearer")] string token);
}
