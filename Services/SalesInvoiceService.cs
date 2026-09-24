using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public SalesInvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesInvoice>> GetAllAsync()
        {
            return await _context.SalesInvoices
                .Include(x => x.Account)
                .Include(x => x.Details)
                    .ThenInclude(x => x.Item)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<SalesInvoice?> GetByIdAsync(int id)
        {
            return await _context.SalesInvoices
                .Include(x => x.Account)
                .Include(x => x.Details)
                    .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.SalesInvoiceId == id);
        }

        public async Task AddAsync(SalesInvoice invoice)
        {
            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesInvoice invoice)
        {
            var oldInvoice = await _context.SalesInvoices
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.SalesInvoiceId == invoice.SalesInvoiceId);

            if (oldInvoice == null)
                return;

            oldInvoice.InvoiceNumber = invoice.InvoiceNumber;
            oldInvoice.InvoiceDate = invoice.InvoiceDate;
            oldInvoice.PaymentType = invoice.PaymentType;
            oldInvoice.AccountId = invoice.AccountId;

            _context.SalesInvoiceDetails.RemoveRange(oldInvoice.Details);

            foreach (var detail in invoice.Details)
            {
                oldInvoice.Details.Add(new SalesInvoiceDetail
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
            var invoice = await _context.SalesInvoices
                .FirstOrDefaultAsync(x => x.SalesInvoiceId == id);

            if (invoice != null)
            {
                _context.SalesInvoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}