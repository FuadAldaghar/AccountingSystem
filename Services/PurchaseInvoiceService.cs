using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseInvoice>> GetAllAsync()
        {
            return await _context.PurchaseInvoices
                .Include(x => x.Account)
                .Include(x => x.Details)
                    .ThenInclude(x => x.Item)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<PurchaseInvoice?> GetByIdAsync(int id)
        {
            return await _context.PurchaseInvoices
                .Include(x => x.Account)
                .Include(x => x.Details)
                    .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.PurchaseInvoiceId == id);
        }

        public async Task AddAsync(PurchaseInvoice invoice)
        {
            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseInvoice invoice)
        {
            var oldInvoice = await _context.PurchaseInvoices
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.PurchaseInvoiceId == invoice.PurchaseInvoiceId);

            if (oldInvoice == null)
                return;

            oldInvoice.InvoiceNumber = invoice.InvoiceNumber;
            oldInvoice.InvoiceDate = invoice.InvoiceDate;
            oldInvoice.PaymentType = invoice.PaymentType;
            oldInvoice.AccountId = invoice.AccountId;

            _context.PurchaseInvoiceDetails.RemoveRange(oldInvoice.Details);

            foreach (var detail in invoice.Details)
            {
                oldInvoice.Details.Add(new PurchaseInvoiceDetail
                {
                    ItemId = detail.ItemId,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.PurchaseInvoices
                .FirstOrDefaultAsync(x => x.PurchaseInvoiceId == id);

            if (invoice != null)
            {
                _context.PurchaseInvoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}