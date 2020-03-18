using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class DrinksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDrinkRepository _drinkRepository;

        public DrinksController(AppDbContext context)
        {
            _context = context;
            _drinkRepository = new DrinkRepository(_context);
        }

        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            return View(await _drinkRepository.AllAsync());
        }

        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _drinkRepository.FindAsync();
            
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Size,Name,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                //drink.Id = Guid.NewGuid();
                _drinkRepository.Add(drink);
                await _drinkRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        // GET: Drinks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _drinkRepository.FindAsync(id);
            
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Size,Name,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Drink drink)
        {
            if (id != drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _drinkRepository.Update(drink);
                await _drinkRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _drinkRepository.FindAsync(id);
            
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drink = _drinkRepository.Remove(id);
            await _drinkRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
