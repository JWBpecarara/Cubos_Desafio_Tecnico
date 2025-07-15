using CubosFinancialAPI.Application.Services.Interfaces;
using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;

namespace CubosFinancialAPI.Application.Services;

public class AuthService(IPeopleRepository peopleRepository, ICriptografiaHelper criptografiaHelper, ITokenProvider tokenProvider) : IAuthService
{
    private readonly IPeopleRepository _peopleRepository = peopleRepository;
    private readonly ICriptografiaHelper _criptografiaHelper = criptografiaHelper;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<string?> LoginAsync(LoginRequestDto dto)
    {
        dto.Password = _criptografiaHelper.ENCODE_HMAC_SHA256_base64(dto.Password);

        People? people = await _peopleRepository.LoginAsync(dto);

        if (people is null) return null;

        return _tokenProvider.Create(people);
    }
}
