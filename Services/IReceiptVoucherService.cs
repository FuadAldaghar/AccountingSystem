using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface IReceiptVoucherService
    {
        Task<List<ReceiptVoucher>> GetAllAsync();
        Task<ReceiptVoucher?> GetByIdAsync(int id);
        Task AddAsync(ReceiptVoucher voucher);
        Task UpdateAsync(ReceiptVoucher voucher);
        Task DeleteAsync(int id);
    }
}