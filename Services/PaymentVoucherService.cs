using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class PaymentVoucherService : IPaymentVoucherService
    {
        private readonly ApplicationDbContext _context;

        public PaymentVoucherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentVoucher>> GetAllAsync()
        {
            return await _context.PaymentVouchers
                .Include(v => v.Account)
                .OrderByDescending(v => v.VoucherDate)
                .ToListAsync();
        }

        public async Task<PaymentVoucher?> GetByIdAsync(int id)
        {
            return await _context.PaymentVouchers
                .Include(v => v.Account)
                .FirstOrDefaultAsync(v => v.PaymentVoucherId == id);
        }

        public async Task AddAsync(PaymentVoucher voucher)
        {
            _context.PaymentVouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentVoucher voucher)
        {
            _context.PaymentVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var voucher = await GetByIdAsync(id);

            if (voucher != null)
            {
                _context.PaymentVouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }
        }
    }
}