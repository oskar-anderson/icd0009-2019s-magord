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
    public class IngredientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientsController(AppDbContext context)
        {
            _context = context;
            _ingredientRepository = new IngredientRepository(_context);
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            return View(await _ingredientRepository.AllAsync());
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientRepository.FindAsync(id);
            
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Amount,FoodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                //ingredient.Id = Guid.NewGuid();
                _ingredientRepository.Add(ingredient);
                await _ingredientRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientRepository.FindAsync(id);
            
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Amount,FoodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ingredientRepository.Update(ingredient);
                await _ingredientRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientRepository.FindAsync(id);
            
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ingredient = _ingredientRepository.Remove(id);
            await _ingredientRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
