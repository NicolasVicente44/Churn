using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Churn.Data;
using Churn.Models;

namespace Churn.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Cart);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            // Convert the PaymentMethods enum values into a list of SelectListItem objects
            var paymentMethods = Enum.GetValues(typeof(PaymentMethods))
                                     .Cast<PaymentMethods>()
                                     .Select(m => new SelectListItem
                                     {
                                         Text = m.ToString(),
                                         Value = m.ToString()
                                     })
                                     .ToList();

            ViewData["PaymentMethods"] = paymentMethods;
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CartId,Total,ShippingAddress,PaymentReceived,PaymentMethod")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the SelectList for the drop-down if the form is not valid
            ViewData["PaymentMethods"] = Enum.GetValues(typeof(PaymentMethods))
                                             .Cast<PaymentMethods>()
                                             .Select(m => new SelectListItem
                                             {
                                                 Text = m.ToString(),
                                                 Value = m.ToString(),
                                                 Selected = (m == order.PaymentMethod)
                                             })
                                             .ToList();

            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", order.CartId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["PaymentMethods"] = Enum.GetValues(typeof(PaymentMethods))
                                             .Cast<PaymentMethods>()
                                             .Select(m => new SelectListItem
                                             {
                                                 Text = m.ToString(),
                                                 Value = m.ToString(),
                                                 Selected = (m == order.PaymentMethod)
                                             })
                                             .ToList();

            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", order.CartId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CartId,Total,ShippingAddress,PaymentReceived,PaymentMethod")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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

            // Repopulate the SelectList for the drop-down if the form is not valid
            ViewData["PaymentMethods"] = Enum.GetValues(typeof(PaymentMethods))
                                             .Cast<PaymentMethods>()
                                             .Select(m => new SelectListItem
                                             {
                                                 Text = m.ToString(),
                                                 Value = m.ToString(),
                                                 Selected = (m == order.PaymentMethod)
                                             })
                                             .ToList();

            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", order.CartId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
