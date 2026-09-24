using AccountingSystem.Models;
using AccountingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // عرض الحسابات
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAsync();

            return View(accounts);
        }

        // صفحة إضافة حساب
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // حفظ الحساب
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            await _accountService.AddAsync(account);

            TempData["Success"] = "تم إضافة الحساب بنجاح.";

            return RedirectToAction(nameof(Index));
        }

        // صفحة تعديل الحساب
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // حفظ التعديل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(account);
            }

            await _accountService.UpdateAsync(account);

            TempData["Success"] = "تم تعديل الحساب بنجاح.";

            return RedirectToAction(nameof(Index));
        }

        // حذف الحساب
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _accountService.DeleteAsync(id);

            if (deleted)
            {
                TempData["Success"] = "تم حذف الحساب بنجاح.";
            }
            else
            {
                TempData["Error"] = "لا يمكن حذف الحساب لأنه مرتبط بحركات أو فواتير موجودة في النظام.";
            }

            return RedirectToAction(nameof(Index));
        }



        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _accountService.DeleteAsync(id);

        //    TempData["Success"] = "تم حذف الحساب بنجاح.";

        //    return RedirectToAction(nameof(Index));
        //}
    }
}