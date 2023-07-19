using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Churn.Data;
using Churn.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Churn.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return _context.Accounts != null ?
                      View(await _context.Accounts.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.Accounts' is null.");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountType,MinimumBalance,InterestRate,MaximumWithdrawalLimit,Id,Name,Description")] Account account, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    account.ImageFileName = await UploadPhoto(imageFile);
                }

                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountType,MinimumBalance,InterestRate,MaximumWithdrawalLimit,Id,Name,Description")] Account account, IFormFile imageFile)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        // Delete the previous image file if it exists
                        if (!string.IsNullOrEmpty(account.ImageFileName))
                        {
                            string previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "accounts", account.ImageFileName);
                            if (System.IO.File.Exists(previousImagePath))
                            {
                                System.IO.File.Delete(previousImagePath);
                            }
                        }

                        account.ImageFileName = await UploadPhoto(imageFile);
                    }

                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Accounts' is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                // Delete the associated image file if it exists
                if (!string.IsNullOrEmpty(account.ImageFileName))
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "accounts", account.ImageFileName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<string> UploadPhoto(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "accounts", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                return fileName;
            }

            return null;
        }
    }
}
