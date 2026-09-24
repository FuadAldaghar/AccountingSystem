using AccountingSystem.Models;

namespace AccountingSystem.Services
{
    public interface IPaymentVoucherService
    {
        Task<List<PaymentVoucher>> GetAllAsync();
        Task<PaymentVoucher?> GetByIdAsync(int id);
        Task AddAsync(PaymentVoucher voucher);
        Task UpdateAsync(PaymentVoucher voucher);
        Task DeleteAsync(int id);
    }
}