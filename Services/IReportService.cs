using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface IReportService
    {
        Task<List<InventoryReportViewModel>> GetInventoryReportAsync();

        Task<List<AccountStatementViewModel>> GetAccountStatementAsync(int accountId);
    }
}