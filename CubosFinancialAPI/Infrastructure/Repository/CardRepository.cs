using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Enum;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure.Repository;

public class CardRepository(ConnectionContext context) : ICardRepository
{
    private readonly ConnectionContext _context = context;

    public CartaoResponseDto Add(Card card)
    {
        _context.Cards.Add(card);
        _context.SaveChanges();

        return new CartaoResponseDto
        {
            Id = card.Id,
            Type = card.Type,
            Number = card.Number[^4..], // últimos 4 dígitos
            Cvv = card.Cvv,
            CreatedAt = card.CreatedAt,
            UpdatedAt = card.UpdatedAt
        };
    }

    public async Task<List<CartaoResponseDto>> GetAllCardsByAccountAsync(Guid accountId)
    {
        var cards = await _context.Cards
          .Where(a => a.AccountId == accountId)
          .ToListAsync(); 

        return cards.Select(a => new CartaoResponseDto
        {
            Id = a.Id,
            Type = a.Type,
            Number = a.Number[^4..], 
            Cvv = a.Cvv,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        }).ToList();
    }

    public async Task<List<CartaoResponseDto>> GetAllCardsByPeopleAsync(Guid peopleId, int skip, int itemsPerPage )
    {
        var cards = await _context.Cards
            .Where(c => c.Account.PeopleId == peopleId)
            .OrderByDescending(c => c.CreatedAt)
            .Skip(skip)
            .Take(itemsPerPage)
            .ToListAsync();

        return cards.Select(a => new CartaoResponseDto
        {
            Id = a.Id,
            Type = a.Type,
            Number = a.Number[^4..],
            Cvv = a.Cvv,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        }).ToList();
    }

    public async Task<bool> PhysicalCardExistsAsync(Guid accountId)
    {
        return await _context.Cards
            .AnyAsync(c => c.AccountId == accountId && c.Type == CardType.Physical);
    }
}
