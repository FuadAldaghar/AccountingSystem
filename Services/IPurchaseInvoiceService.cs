using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface IPurchaseInvoiceService
    {
        Task<List<PurchaseInvoice>> GetAllAsync();
        Task<PurchaseInvoice?> GetByIdAsync(int id);
        Task AddAsync(PurchaseInvoice invoice);
        Task UpdateAsync(PurchaseInvoice invoice);
        Task DeleteAsync(int id);
    }
}