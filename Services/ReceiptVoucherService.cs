using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class ReceiptVoucherService : IReceiptVoucherService
    {
        private readonly ApplicationDbContext _context;

        public ReceiptVoucherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReceiptVoucher>> GetAllAsync()
        {
            return await _context.ReceiptVouchers
                .Include(v => v.Account)
                .OrderByDescending(v => v.VoucherDate)
                .ToListAsync();
        }

        public async Task<ReceiptVoucher?> GetByIdAsync(int id)
        {
            return await _context.ReceiptVouchers
                .Include(v => v.Account)
                .FirstOrDefaultAsync(v => v.ReceiptVoucherId == id);
        }

        public async Task AddAsync(ReceiptVoucher voucher)
        {
            _context.ReceiptVouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReceiptVoucher voucher)
        {
            _context.ReceiptVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var voucher = await GetByIdAsync(id);

            if (voucher != null)
            {
                _context.ReceiptVouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }
        }
    }
}