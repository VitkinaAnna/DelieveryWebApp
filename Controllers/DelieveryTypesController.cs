using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DelieveryWebApplication;

namespace DelieveryWebApplication.Controllers
{
    public class DelieveryTypesController : Controller
    {
        private readonly delieveryContext _context;

        public DelieveryTypesController(delieveryContext context)
        {
            _context = context;
        }

        // GET: DelieveryTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DelieveryTypes.ToListAsync());
        }

        // GET: DelieveryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delieveryType = await _context.DelieveryTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (delieveryType == null)
            {
                return NotFound();
            }

            return View(delieveryType);
        }

        // GET: DelieveryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DelieveryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,Name")] DelieveryType delieveryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delieveryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delieveryType);
        }

        // GET: DelieveryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delieveryType = await _context.DelieveryTypes.FindAsync(id);
            if (delieveryType == null)
            {
                return NotFound();
            }
            return View(delieveryType);
        }

        // POST: DelieveryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,Name")] DelieveryType delieveryType)
        {
            if (id != delieveryType.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delieveryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelieveryTypeExists(delieveryType.TypeId))
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
            return View(delieveryType);
        }

        // GET: DelieveryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delieveryType = await _context.DelieveryTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (delieveryType == null)
            {
                return NotFound();
            }

            return View(delieveryType);
        }

        // POST: DelieveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delieveryType = await _context.DelieveryTypes.FindAsync(id);
            _context.DelieveryTypes.Remove(delieveryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelieveryTypeExists(int id)
        {
            return _context.DelieveryTypes.Any(e => e.TypeId == id);
        }
    }
}
