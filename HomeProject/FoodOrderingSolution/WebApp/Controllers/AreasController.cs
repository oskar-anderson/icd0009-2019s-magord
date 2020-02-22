using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class AreasController : Controller
    {
        private readonly AppDbContext _context;

        public AreasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Areas.Include(a => a.Town);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.Town)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Id");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TownId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Area area)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Id", area.TownId);
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Id", area.TownId);
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,TownId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
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
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Id", area.TownId);
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.Town)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var area = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(string id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
