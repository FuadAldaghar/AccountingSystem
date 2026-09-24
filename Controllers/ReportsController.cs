using AccountingSystem.Data;
using AccountingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ApplicationDbContext _context;

        public ReportsController(
            IReportService reportService,
            ApplicationDbContext context)
        {
            _reportService = reportService;
            _context = context;
        }

        public async Task<IActionResult> Inventory()
        {
            var report = await _reportService.GetInventoryReportAsync();

            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> AccountStatement(int? accountId)
        {
            ViewBag.Accounts = await _context.Accounts
                .OrderBy(x => x.AccountName)
                .ToListAsync();

            if (!accountId.HasValue || accountId.Value <= 0)
            {
                ViewBag.SelectedAccountId = 0;

                return View(new List<AccountingSystem.Models.AccountStatementViewModel>());
            }

            var report = await _reportService
                .GetAccountStatementAsync(accountId.Value);

            ViewBag.SelectedAccountId = accountId.Value;

            return View(report);
        }
    }
}