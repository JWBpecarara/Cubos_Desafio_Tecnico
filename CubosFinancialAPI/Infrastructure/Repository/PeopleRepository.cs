using CubosFinancialAPI.DTO.Requests;
using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ConnectionContext _context;

        public PeopleRepository(ConnectionContext context)
        {
            _context = context;
        }

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

        public async Task<People?> Login(LoginRequestDto People)
        {
            return await _context.Peoples.
                FirstOrDefaultAsync(u => u.Document == People.Document && u.Password == People.Password); 
        }
    }
}
