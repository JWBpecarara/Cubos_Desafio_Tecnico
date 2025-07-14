using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Intregrations.Services;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly CriptografiaHelper _criptografiaHelper;
        private readonly TokenProvider _tokenProvider;


        public LoginController(CriptografiaHelper criptografiaHelper, TokenProvider tokenProvider,IPeopleRepository PeopleRepository)
        {
            _peopleRepository = PeopleRepository;
            _criptografiaHelper = criptografiaHelper;
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] LoginRequestDto dto)
        {
            dto.Password = _criptografiaHelper.ENCODE_HMAC_SHA256_base64(dto.Password);

            Model.People? people = await _peopleRepository.Login(dto);

            if (people == null)
                return Unauthorized("Credenciais inválidas.");

            var token = _tokenProvider.Create(people);

            return Ok(token);
        }
    }
}
