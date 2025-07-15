using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using CubosFinancialAPI.Model;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using CubosFinancialAPI.Enum;
using CubosFinancialAPI.Application.Services.Interfaces;

namespace CubosFinancialAPI.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class AccountsController(ITransactionService transactionService, IAccountRepository accountRepository, ICardRepository cardRepository, ITransactionRepository transactionRepository) : ControllerBase
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    [HttpPost]
    public async Task<IActionResult> CreateAccount(AccountRequestDto request)
    {
        // Checagem de Token valido
        if (!Guid.TryParse(User.Claims.FirstOrDefault()?.Value, out var userId))
            return Unauthorized("Token inválido.");

        // Checagem de duplicidade
        if (await _accountRepository.AccountExistsAsync(request.Account))
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


        List<AccountResponseDto> result = await _accountRepository.GetAllAccountByPeolpeAsync(userId);

        return Ok(result);
    }

    [HttpPost("{accountId}/cards")]
    public async Task<IActionResult> CreateCards(Guid accountId, [FromBody] CreateCartaoRequestDto request)
    {
        // Verificar se já existe um cartão físico para essa conta
        if (request.Type == CardType.Physical)
        {
            bool hasPhysicalCard = await _cardRepository.PhysicalCardExistsAsync(accountId);
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

        CartaoResponseDto result = _cardRepository.Add(card);

        return Ok(result);
    }

    [HttpGet("{accountId}/cards")]
    public async Task<IActionResult> GetAllCardsByAccount(Guid accountId)
    {
        List<CartaoResponseDto> result = await _cardRepository.GetAllCardsByAccountAsync(accountId);

        return Ok(result);
    }

    [HttpPost("{accountId}/cards/transactions")]
    public async Task<IActionResult> CreateTransaction(Guid accountId, [FromBody] CreateTransactionRequestDto request)
    {
        TransactionResponseDto? result = await _transactionService.CreateTransactionAsync(accountId, request);

        if (result is null) return BadRequest("Saldo insuficiente para a transação.");

        return Ok(result);
    }

    [HttpPost("{accountId}/cards/transactions/internal")]
    public async Task<IActionResult> CreateTransactionInternal(Guid accountId, [FromBody] InternalTransactionRequestDto request)
    {

        var balance = await _transactionRepository.GetBalanceByAccountIdAsync(accountId);

        if (balance + request.Value < 0) // valor é negativo
        {
            return BadRequest("Saldo insuficiente para a transação.");
        }


        var Destination = new Model.Transaction
        {
            AccountId = request.ReceiverAccountId,
            Value = request.Value,
            Type = TransactionType.Credit,
            Description = request.Description
        };
        TransactionResponseDto resultDestination = _transactionRepository.Add(Destination);

        var Origin = new Model.Transaction
        {
            AccountId = accountId,
            Value = (request.Value * -1),
            Type = TransactionType.Debit,
            Description = request.Description
        };
        TransactionResponseDto resultOrigin = _transactionRepository.Add(Origin);

        var Internal = new Model.InternalTransfer
        {
            DestinationTransactionId = resultDestination.Id,
            OriginTransactionId = resultOrigin.Id
        };

        _transactionRepository.AddInternalTransfe(Internal);

        return Ok(resultDestination);
    }

    [HttpGet("{accountId}/balance")]
    public async Task<IActionResult> GetBalance(Guid accountId)
    {
        decimal balance = await _transactionRepository.GetBalanceByAccountIdAsync(accountId);
        return Ok(new { balance = Math.Round(balance, 2) });
    }

    [HttpPost("{accountId}/cards/{transactionId}/revert")]
    public async Task<IActionResult> revertInternalTransaction(Guid accountId, Guid transactionId)
    {

        var InternalTransaction = await _transactionRepository.GetInternalTransactionByAccountIdAsync(transactionId);

        if (InternalTransaction == null) // valor é negativo          
            return BadRequest("TransactionId Invalido");

        var balance = await _transactionRepository.GetBalanceByAccountIdAsync(InternalTransaction.DestinationAccountId);

        if (balance + (InternalTransaction.OriginValue * -1) < 0) // valor é negativo
            return BadRequest("Saldo insuficiente para a transação.");



        var Destination = new Model.Transaction
        {
            AccountId = InternalTransaction.DestinationAccountId,
            Value = (InternalTransaction.DestinationValue * -1),
            Type = TransactionType.Debit,
            Description = "Estorno de cobrança indevida."
        };
        TransactionResponseDto resultDestination = _transactionRepository.Add(Destination);

        var Origin = new Model.Transaction
        {
            AccountId = InternalTransaction.OriginAccountId,
            Value = (InternalTransaction.OriginValue * -1),
            Type = TransactionType.Credit,
            Description = "Estorno de cobrança indevida."
        };
        TransactionResponseDto resultOrigin = _transactionRepository.Add(Origin);


        return Ok(resultDestination);
    }

    [HttpGet("{accountId}/transactions")]
    public async Task<IActionResult> GetAlltransactionsByAccount(Guid accountId, [FromQuery] int items = 10, [FromQuery] int Page = 1, [FromQuery] string type = "")
    {
        if (items <= 0) items = 10;
        if (Page <= 0) Page = 1;

        var skip = (Page - 1) * items;

        List<TransactionResponseDto> Transaction = await _transactionRepository.GetAlltransactionsByAccountAsync(accountId, skip, items, type);

        var result = new
        {
            transactions = Transaction,
            pagination = new
            {
                itemsPerPage = items,
                currentPage = Page
            }
        };

        return Ok(result);
    }
}
