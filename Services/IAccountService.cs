using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        //Task DeleteAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}