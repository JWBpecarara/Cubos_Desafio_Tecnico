using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure.Repository;

public class PeopleRepository(ConnectionContext context) : IPeopleRepository
{
    private readonly ConnectionContext _context = context;

    public PostPeopleResponseDto Add(People People)
    {
        _context.Peoples.Add(People);
        _context.SaveChanges();

        return new PostPeopleResponseDto
        {
            Id = People.Id,
            Name = People.Name,
            Document = People.Document,
            CreatedAt = People.CreatedAt,
            UpdatedAt = People.UpdatedAt
        };
    }

    public async Task<People?> LoginAsync(LoginRequestDto People)
    {
        return await _context.Peoples.
            FirstOrDefaultAsync(u => u.Document == People.Document && u.Password == People.Password);
    }
}
