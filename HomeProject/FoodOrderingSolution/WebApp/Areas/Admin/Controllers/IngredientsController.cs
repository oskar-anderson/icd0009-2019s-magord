using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "Admin")]
    public class IngredientsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public IngredientsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            var ingredients = await _uow.Ingredients.AllAsync();
            return View(ingredients);
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.Ingredients.FirstOrDefaultAsync(id.Value);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name));
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                //ingredient.Id = Guid.NewGuid();
                _uow.Ingredients.Add(ingredient);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), ingredient.FoodId);
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.Ingredients.FirstOrDefaultAsync(id.Value);

            if (ingredient == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), ingredient.FoodId);
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Ingredients.Update(ingredient);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Ingredients.ExistsAsync(ingredient.Id))
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
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), ingredient.FoodId);
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.Ingredients.FirstOrDefaultAsync(id.Value);

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
            await _uow.Ingredients.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
