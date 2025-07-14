using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CubosFinancialAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController(CriptografiaHelper criptografiaHelper, TokenProvider tokenProvider, IPeopleRepository PeopleRepository) : ControllerBase
{
    private readonly IPeopleRepository _peopleRepository = PeopleRepository;
    private readonly CriptografiaHelper _criptografiaHelper = criptografiaHelper;
    private readonly TokenProvider _tokenProvider = tokenProvider;

    [HttpPost]
    public async Task<IActionResult> Login ([FromBody] LoginRequestDto dto)
    {
        dto.Password = _criptografiaHelper.ENCODE_HMAC_SHA256_base64(dto.Password);

        Model.People? people = await _peopleRepository.LoginAsync(dto);

        if (people == null)
            return Unauthorized("Credenciais inválidas.");

        var tokenJwt = _tokenProvider.Create(people);

        return Ok(new { token = tokenJwt });
    }
}
