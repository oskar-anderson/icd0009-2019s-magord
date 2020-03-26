using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class FoodTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FoodTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FoodTypes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.FoodTypes.AllAsync());
        }

        // GET: FoodTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _uow.FoodTypes.FindAsync(id);
            
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
                //foodType.Id = Guid.NewGuid();
                _uow.FoodTypes.Add(foodType);
                await _uow.SaveChangesAsync();
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

            var foodType = await _uow.FoodTypes.FindAsync(id);
            
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
                _uow.FoodTypes.Update(foodType);
                await _uow.SaveChangesAsync();
                
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

            var foodType = await _uow.FoodTypes.FindAsync(id);
            
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
            var foodType = _uow.FoodTypes.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
