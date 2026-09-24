using AccountingSystem.Data;
using AccountingSystem.Models;
using AccountingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Controllers
{
    public class PurchaseInvoicesController : Controller
    {
        private readonly IPurchaseInvoiceService _invoiceService;
        private readonly ApplicationDbContext _context;

        public PurchaseInvoicesController(
            IPurchaseInvoiceService invoiceService,
            ApplicationDbContext context)
        {
            _invoiceService = invoiceService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllAsync();

            return View(invoices);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadData();

            return View(new PurchaseInvoice
            {
                InvoiceDate = DateTime.Now,
                Details = new List<PurchaseInvoiceDetail>
                {
                    new PurchaseInvoiceDetail()
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseInvoice invoice)
        {
            invoice.Details = invoice.Details?
                .Where(x => x.ItemId > 0 && x.Quantity > 0)
                .ToList() ?? new List<PurchaseInvoiceDetail>();

            if (!invoice.Details.Any())
            {
                ModelState.AddModelError("", "يجب إضافة صنف واحد على الأقل.");
            }

            if (!ModelState.IsValid)
            {
                await LoadData();
                return View(invoice);
            }

            await _invoiceService.AddAsync(invoice);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);

            if (invoice == null)
                return NotFound();

            await LoadData();

            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PurchaseInvoice invoice)
        {
            if (id != invoice.PurchaseInvoiceId)
                return BadRequest();

            invoice.Details = invoice.Details?
                .Where(x => x.ItemId > 0 && x.Quantity > 0)
                .ToList() ?? new List<PurchaseInvoiceDetail>();

            if (!invoice.Details.Any())
            {
                ModelState.AddModelError("", "يجب إضافة صنف واحد على الأقل.");
            }

            if (!ModelState.IsValid)
            {
                await LoadData();
                return View(invoice);
            }

            await _invoiceService.UpdateAsync(invoice);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _invoiceService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadData()
        {
            ViewBag.Accounts = await _context.Accounts
                .OrderBy(x => x.AccountName)
                .ToListAsync();

            ViewBag.Items = await _context.Items
                .OrderBy(x => x.ItemName)
                .ToListAsync();
        }
    }
}