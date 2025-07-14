using CubosFinancialApi.Infrastructure.Intregrations.Clientes;
using CubosFinancialAPI.Infrastructure.Intregrations.Models.Requests;
using System.Text.RegularExpressions;

namespace CubosFinancialAPI.Infrastructure.Intregrations.Services;

public interface IComplianceService
{
    Task<bool> ValidateCpfOrCnpjAsync(string documento);
}

public class ComplianceService(IComplianceApi complianceApi) : IComplianceService
{
    private readonly IComplianceApi _complianceApi = complianceApi;

    public async Task<bool> ValidateCpfOrCnpjAsync(string documento)
    {
         // Obter o código de autorização
         var authCodeResult = await _complianceApi.GetAuthCode(new Authentication());
         var authCode = authCodeResult.Data.AuthCode;

         // Obter o token usando o código de autorização
         var tokenResult = await _complianceApi.GetToken(new AuthenticationCode { AuthCode = authCode });
         var accessToken = tokenResult.Data.AccessToken;

         // Sanitizar o documento (remover caracteres não numéricos)
         var documentoSanitizado = Regex.Replace(documento, @"\D", "");

         var documentRequest = new DocumentNumber { Document = documentoSanitizado };

         if (documentoSanitizado.Length == 11)
         {
             var cpfValidation = await _complianceApi.ValidateCpfAsync(documentRequest, accessToken); 
             return cpfValidation.Success;
         }
         if (documentoSanitizado.Length == 14)
         {
             var cnpjValidation = await _complianceApi.ValidateCnpjAsync(documentRequest, accessToken);
             return cnpjValidation.Success;
         }

         throw new ArgumentException("O documento informado não é um CPF ou CNPJ válido.");
    }
}
