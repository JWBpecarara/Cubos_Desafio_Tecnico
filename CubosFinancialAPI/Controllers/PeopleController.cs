using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Intregrations.Services;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CubosFinancialAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ComplianceService _complianceService;
        private readonly CriptografiaHelper _criptografiaHelper;
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(ComplianceService complianceService, CriptografiaHelper criptografiaHelper, IPeopleRepository PeopleRepository)
        {
            _complianceService = complianceService;
            _criptografiaHelper = criptografiaHelper;
            _peopleRepository = PeopleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostPeopleRequesteDto dto)
        {
            var isValid = await _complianceService.ValidateCpfOrCnpjAsync(dto.Document);

            if(isValid == false)
                return BadRequest("O documento informado não é um CPF ou CNPJ válido");

            var People = new Model.People
            {
                Name = dto.Name,
                Document = Regex.Replace(dto.Document, @"\D", ""),
                Password = _criptografiaHelper.ENCODE_HMAC_SHA256_base64(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            PostPeopleResponseDto result = _peopleRepository.Add(People);

            return Ok(result);
        }
    }
}
