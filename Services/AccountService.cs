using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .OrderBy(a => a.AccountNumber)
                .ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await GetByIdAsync(id);

            if (account == null)
            {
                return false;
            }

            try
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        //public async Task DeleteAsync(int id)
        //{
        //    var account = await GetByIdAsync(id);

        //    if (account != null)
        //    {
        //        _context.Accounts.Remove(account);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}