using AccountingSystem.Data;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryReportViewModel>> GetInventoryReportAsync()
        {
            var purchasedQuantities = await _context.PurchaseInvoiceDetails
                .GroupBy(x => x.ItemId)
                .Select(g => new
                {
                    ItemId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToDictionaryAsync(x => x.ItemId, x => x.Quantity);

            var soldQuantities = await _context.SalesInvoiceDetails
                .GroupBy(x => x.ItemId)
                .Select(g => new
                {
                    ItemId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToDictionaryAsync(x => x.ItemId, x => x.Quantity);

            var items = await _context.Items
                .OrderBy(x => x.ItemNumber)
                .ToListAsync();

            var report = new List<InventoryReportViewModel>();

            foreach (var item in items)
            {
                purchasedQuantities.TryGetValue(
                    item.ItemId,
                    out decimal purchasedQuantity);

                soldQuantities.TryGetValue(
                    item.ItemId,
                    out decimal soldQuantity);

                report.Add(new InventoryReportViewModel
                {
                    ItemId = item.ItemId,
                    ItemNumber = item.ItemNumber,
                    ItemName = item.ItemName,
                    Unit = item.Unit,
                    PurchasedQuantity = purchasedQuantity,
                    SoldQuantity = soldQuantity
                });
            }

            return report;
        }



        public async Task<List<AccountStatementViewModel>> GetAccountStatementAsync(int accountId)
        {
            var statement = new List<AccountStatementViewModel>();

            var receiptVouchers = await _context.ReceiptVouchers
                .Where(x => x.AccountId == accountId)
                .ToListAsync();

            foreach (var voucher in receiptVouchers)
            {
                statement.Add(new AccountStatementViewModel
                {
                    Date = voucher.VoucherDate,
                    OperationType = "سند قبض",
                    ReferenceNumber = voucher.VoucherNumber,
                    Description = voucher.Notes ?? string.Empty,
                    Debit = 0,
                    Credit = voucher.Amount
                });
            }

            var paymentVouchers = await _context.PaymentVouchers
                .Where(x => x.AccountId == accountId)
                .ToListAsync();

            foreach (var voucher in paymentVouchers)
            {
                statement.Add(new AccountStatementViewModel
                {
                    Date = voucher.VoucherDate,
                    OperationType = "سند صرف",
                    ReferenceNumber = voucher.VoucherNumber,
                    Description = voucher.Notes ?? string.Empty,
                    Debit = voucher.Amount,
                    Credit = 0
                });
            }

            var purchaseInvoices = await _context.PurchaseInvoices
                .Where(x => x.AccountId == accountId)
                .Include(x => x.Details)
                .ToListAsync();

            foreach (var invoice in purchaseInvoices)
            {
                var total = invoice.Details
                    .Sum(x => x.Quantity * x.UnitPrice);

                statement.Add(new AccountStatementViewModel
                {
                    Date = invoice.InvoiceDate,
                    OperationType = "فاتورة شراء",
                    ReferenceNumber = invoice.InvoiceNumber,
                    Description = invoice.PaymentType,
                    Debit = 0,
                    Credit = total
                });
            }

            var salesInvoices = await _context.SalesInvoices
                .Where(x => x.AccountId == accountId)
                .Include(x => x.Details)
                .ToListAsync();

            foreach (var invoice in salesInvoices)
            {
                var total = invoice.Details
                    .Sum(x => x.Quantity * x.UnitPrice);

                statement.Add(new AccountStatementViewModel
                {
                    Date = invoice.InvoiceDate,
                    OperationType = "فاتورة بيع",
                    ReferenceNumber = invoice.InvoiceNumber,
                    Description = invoice.PaymentType,
                    Debit = total,
                    Credit = 0
                });
            }

            statement = statement
                .OrderBy(x => x.Date)
                .ThenBy(x => x.OperationType)
                .ToList();

            decimal balance = 0;

            foreach (var row in statement)
            {
                balance += row.Debit - row.Credit;
                row.Balance = balance;
            }

            return statement;
        }
    }
}