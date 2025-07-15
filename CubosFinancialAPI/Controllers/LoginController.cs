using CubosFinancialAPI.Application.Services.Interfaces;
using CubosFinancialAPI.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CubosFinancialAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        string? tokenJwt = await _authService.LoginAsync(dto);

        if (tokenJwt is null) return Unauthorized("Credenciais inválidas.");

        return Ok(new { token = tokenJwt });
    }
}
