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
    public class FoodTypesController : Controller
    {
        private readonly AppDbContext _context;

        public FoodTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FoodTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodTypes.ToListAsync());
        }

        // GET: FoodTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // GET: FoodTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] FoodType foodType)
        {
            if (ModelState.IsValid)
            {
                foodType.Id = Guid.NewGuid();
                _context.Add(foodType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodType);
        }

        // GET: FoodTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes.FindAsync(id);
            if (foodType == null)
            {
                return NotFound();
            }
            return View(foodType);
        }

        // POST: FoodTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] FoodType foodType)
        {
            if (id != foodType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodTypeExists(foodType.Id))
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
            return View(foodType);
        }

        // GET: FoodTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // POST: FoodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var foodType = await _context.FoodTypes.FindAsync(id);
            _context.FoodTypes.Remove(foodType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodTypeExists(Guid id)
        {
            return _context.FoodTypes.Any(e => e.Id == id);
        }
    }
}
