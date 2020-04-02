using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class FoodsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FoodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var foods = await _uow.Foods.AllAsync();
            return View(foods);
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.Foods.FirstOrDefaultAsync(id.Value);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FoodTypeId"] = new SelectList(await _uow.FoodTypes.AllAsync(), nameof(FoodType.Id), nameof(FoodType.Name));
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            if (ModelState.IsValid)
            {
                //food.Id = Guid.NewGuid();
                _uow.Foods.Add(food);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodTypeId"] = new SelectList(await _uow.FoodTypes.AllAsync(), nameof(FoodType.Id), nameof(FoodType.Name), food.FoodTypeId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.Foods.FirstOrDefaultAsync(id.Value);

            if (food == null)
            {
                return NotFound();
            }
            ViewData["FoodTypeId"] = new SelectList(await _uow.FoodTypes.AllAsync(), nameof(FoodType.Id), nameof(FoodType.Name), food.FoodTypeId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Foods.Update(food);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Contacts.ExistsAsync(food.Id))
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
            ViewData["FoodTypeId"] = new SelectList(await _uow.FoodTypes.AllAsync(), nameof(FoodType.Id), nameof(FoodType.Name), food.FoodTypeId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.Foods.FirstOrDefaultAsync(id.Value);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Foods.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
