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
    public class TownsController : Controller
    {
        private readonly AppDbContext _context;

        public TownsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Towns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Towns.ToListAsync());
        }

        // GET: Towns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // GET: Towns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Town town)
        {
            if (ModelState.IsValid)
            {
                town.Id = Guid.NewGuid();
                _context.Add(town);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(town);
        }

        // GET: Towns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }
            return View(town);
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Town town)
        {
            if (id != town.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(town);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TownExists(town.Id))
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
            return View(town);
        }

        // GET: Towns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var town = await _context.Towns.FindAsync(id);
            _context.Towns.Remove(town);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownExists(Guid id)
        {
            return _context.Towns.Any(e => e.Id == id);
        }
    }
}
