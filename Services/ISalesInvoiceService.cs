using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface ISalesInvoiceService
    {
        Task<List<SalesInvoice>> GetAllAsync();
        Task<SalesInvoice?> GetByIdAsync(int id);
        Task AddAsync(SalesInvoice invoice);
        Task UpdateAsync(SalesInvoice invoice);
        Task DeleteAsync(int id);
    }
}