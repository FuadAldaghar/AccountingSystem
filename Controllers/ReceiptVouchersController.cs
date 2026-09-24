using AccountingSystem.Data;
using AccountingSystem.Models;
using AccountingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Controllers
{
    public class ReceiptVouchersController : Controller
    {
        private readonly IReceiptVoucherService _voucherService;
        private readonly ApplicationDbContext _context;

        public ReceiptVouchersController(
            IReceiptVoucherService voucherService,
            ApplicationDbContext context)
        {
            _voucherService = voucherService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vouchers = await _voucherService.GetAllAsync();

            return View(vouchers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Accounts = await _context.Accounts
                .OrderBy(a => a.AccountName)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptVoucher voucher)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewBag.Accounts = await _context.Accounts
                    .OrderBy(a => a.AccountName)
                    .ToListAsync();

                return View(voucher);
            }

            await _voucherService.AddAsync(voucher);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var voucher = await _voucherService.GetByIdAsync(id);

            if (voucher == null)
                return NotFound();

            ViewBag.Accounts = await _context.Accounts
                .OrderBy(a => a.AccountName)
                .ToListAsync();

            return View(voucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ReceiptVoucher voucher)
        {
            if (id != voucher.ReceiptVoucherId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Accounts = await _context.Accounts
                    .OrderBy(a => a.AccountName)
                    .ToListAsync();

                return View(voucher);
            }

            await _voucherService.UpdateAsync(voucher);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _voucherService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}