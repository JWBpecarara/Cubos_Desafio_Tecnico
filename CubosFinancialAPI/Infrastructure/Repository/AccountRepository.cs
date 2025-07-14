using CubosFinancialAPI.DTO.Responses;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConnectionContext _context;

        public AccountRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<bool> AccountExists(string accountNumber)
        {
            return await _context.Accounts
                .AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public AccountResponseDto Add(Account Account)
        {
            _context.Accounts.Add(Account);
            _context.SaveChanges();

            return new AccountResponseDto
            {
                Id = Account.Id,
                Branch = Account.Branch,
                Account = Account.AccountNumber,
                CreatedAt = Account.CreatedAt,
                UpdatedAt = Account.UpdatedAt
            };
        }

        public async Task<List<AccountResponseDto>> GetAllAccountByPeolpe(Guid userId)
        {
            var accounts = await _context.Accounts
                .Where(a => a.PeopleId == userId)
                .ToListAsync();

            return accounts.Select(a => new AccountResponseDto
            {
                Id = a.Id,
                Branch = a.Branch,
                Account = a.AccountNumber,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            }).ToList();        
        }
    }
}
