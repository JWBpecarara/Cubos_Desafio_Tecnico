using CubosFinancialAPI.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CubosFinancialAPI.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class CardsController(IAccountRepository accountRepository, ICardRepository cardRepository) : ControllerBase
{
    private readonly ICardRepository _cardRepository = cardRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllCardsByPeople([FromQuery] int items = 10, [FromQuery] int Page = 1)
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault()?.Value, out var userId))
            return Unauthorized("Token inválido.");

        if (items <= 0) items = 10;
        if (Page <= 0) Page = 1;

        var skip = (Page - 1) * items;

        List<CartaoResponseDto> cardsList = await _cardRepository.GetAllCardsByPeopleAsync(userId, skip, items);

        var result = new
        {
            cards = cardsList,
            pagination = new
            {
                itemsPerPage = items,
                currentPage = Page
            }
        };

        return Ok(result);
    }
}
