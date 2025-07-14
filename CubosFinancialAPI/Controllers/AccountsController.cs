using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using CubosFinancialAPI.Model;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CubosFinancialAPI.Infrastructure.Repository;
using CubosFinancialAPI.Enum;

namespace CubosFinancialAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;




        public AccountsController(IAccountRepository accountRepository, ICardRepository cardRepository)
        {
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
        }

        [HttpPost]
        public async Task<IActionResult>  CreateAccount( AccountRequestDto request)
        {
            // Checagem de Token valido
            if (!Guid.TryParse(User.Claims.FirstOrDefault()?.Value, out var userId))
                return Unauthorized("Token inválido.");

            // Checagem de duplicidade
            if ( await _accountRepository.AccountExists(request.Account))
                return Conflict("Account number already exists.");

            var account = new Account
            {
                Branch = request.Branch,
                AccountNumber = request.Account,
                PeopleId = userId
            };

            AccountResponseDto result = _accountRepository.Add(account);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountByPeolpe()
        {
            if (!Guid.TryParse(User.Claims.FirstOrDefault()?.Value, out var userId))
                return Unauthorized("Token inválido.");


            List<AccountResponseDto> result = await _accountRepository.GetAllAccountByPeolpe(userId);

            return Ok(result);
        }

        [HttpPost("{accountId}/cards")]
        public async Task<IActionResult> CreateCards(Guid accountId, [FromBody] CreateCartaoRequestDto request)
        {


            // Verificar se já existe um cartão físico para essa conta
            if (request.Type == CardType.physical)
            {
                bool hasPhysicalCard = await _cardRepository.PhysicalCardExists(accountId);
                if (hasPhysicalCard)
                    return BadRequest("A conta já possui um cartão físico.");
            }

            var card = new Card
            {
                AccountId = accountId,
                Type = request.Type,
                Number = request.Number, 
                Cvv = request.Cvv,
            };

            CartaoResponseDto result =  _cardRepository.Add(card);

            return Ok(result);
        }

        [HttpGet("{accountId}/cards")]
        public async Task<IActionResult> GetAllCardsByAccount(Guid accountId)
        {

            List<CartaoResponseDto> result = await _cardRepository.GetAllCardsByAccount(accountId);

            return Ok(result);
        }
    }
}
