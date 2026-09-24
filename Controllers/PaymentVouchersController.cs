using AccountingSystem.Data;
using AccountingSystem.Models;
using AccountingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Controllers
{
    public class PaymentVouchersController : Controller
    {
        private readonly IPaymentVoucherService _voucherService;
        private readonly ApplicationDbContext _context;

        public PaymentVouchersController(
            IPaymentVoucherService voucherService,
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
            await LoadAccounts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentVoucher voucher)
        {
            if (!ModelState.IsValid)
            {
                await LoadAccounts();
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

            await LoadAccounts();

            return View(voucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PaymentVoucher voucher)
        {
            if (id != voucher.PaymentVoucherId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await LoadAccounts();
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

        private async Task LoadAccounts()
        {
            ViewBag.Accounts = await _context.Accounts
                .OrderBy(a => a.AccountName)
                .ToListAsync();
        }
    }
}