using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DelieveryWebApplication;
using Microsoft.AspNetCore.Authorization;


namespace DelieveryWebApplication.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class DelieveriesController : Controller
    {
        private readonly delieveryContext _context;

        public DelieveriesController(delieveryContext context)
        {
            _context = context;
        }

        // GET: Delieveries
        public async Task<IActionResult> Index()
        {
            var delieveryContext = _context.Delieveries.Include(d => d.Dish).Include(d => d.Order).Include(d=>d.Order.Client);
            return View(await delieveryContext.ToListAsync());
        }

        // GET: Delieveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delievery = await _context.Delieveries.Include(d => d.Order.Client)
                .Include(d => d.Dish)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DelieveryId == id);
            if (delievery == null)
            {
                return NotFound();
            }

            return View(delievery);
        }

        // GET: Delieveries/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Menus, "DishId", "Name");
            ViewData["OrderId"] = _context.Orders.Include(o => o.Client).Select(o => new SelectListItem(o.Client.Name, o.OrderId.ToString())).ToList();
            return View();
        }

        // POST: Delieveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // _context.Menus.Select(m=>new SelectListItem(m.Name, m.DishId.ToString())).ToList();
        //edit.OrderList = _context.Orders.Include(o => o.Client).Select(o => new SelectListItem(o.Client.Name, o.OrderId.ToString())).ToList();


        public async Task<IActionResult> Create([Bind("DishId,OrderId,Amount,DelieveryId")] Delievery delievery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delievery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Menus, "DishId", "Name", delievery.DishId);
            ViewData["OrderId"] = _context.Orders.Include(o => o.Client).Select(o => new SelectListItem(o.Client.Name, o.OrderId.ToString())).ToList();
            return View(delievery);
        }

        // GET: Delieveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delievery = await _context.Delieveries.FindAsync(id);
            if (delievery == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Menus, "DishId", "Name", delievery.DishId);
            ViewData["OrderId"] = _context.Orders.Include(o => o.Client).Select(o => new SelectListItem(o.Client.Name, o.OrderId.ToString())).ToList();
            return View(delievery);
        }

        // POST: Delieveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,OrderId,Amount,DelieveryId")] Delievery delievery)
        {
            if (id != delievery.DelieveryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delievery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelieveryExists(delievery.DelieveryId))
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
            ViewData["DishId"] = new SelectList(_context.Menus, "DishId", "Name", delievery.DishId);
            ViewData["OrderId"] = _context.Orders.Include(o => o.Client).Select(o => new SelectListItem(o.Client.Name, o.OrderId.ToString())).ToList();
            return View(delievery);
        }

        // GET: Delieveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delievery = await _context.Delieveries.Include(d => d.Order.Client)
                .Include(d => d.Dish)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DelieveryId == id);
            if (delievery == null)
            {
                return NotFound();
            }

            return View(delievery);
        }

        // POST: Delieveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delievery = await _context.Delieveries.FindAsync(id);
            _context.Delieveries.Remove(delievery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelieveryExists(int id)
        {
            return _context.Delieveries.Any(e => e.DelieveryId == id);
        }
    }
}
