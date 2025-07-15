using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Intregrations.Services;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CubosFinancialAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController(ComplianceService complianceService, ICriptografiaHelper criptografiaHelper, IPeopleRepository PeopleRepository) : ControllerBase
{
    private readonly ComplianceService _complianceService = complianceService;
    private readonly ICriptografiaHelper _criptografiaHelper = criptografiaHelper;
    private readonly IPeopleRepository _peopleRepository = PeopleRepository;

    [HttpPost]
    public async Task<IActionResult> Create(PostPeopleRequesteDto dto)
    {
        bool isValid = await _complianceService.ValidateCpfOrCnpjAsync(dto.Document);

        if (!isValid)
            return BadRequest("O documento informado não é um CPF ou CNPJ válido");

        People peple = new()
        {
            Name = dto.Name,
            Document = Regex.Replace(dto.Document, @"\D", ""),
            Password = _criptografiaHelper.ENCODE_HMAC_SHA256_base64(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        PostPeopleResponseDto result = _peopleRepository.Add(peple);

        return Ok(result);
    }
}
