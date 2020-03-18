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
    public class FoodsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFoodRepository _foodRepository;

        public FoodsController(AppDbContext context)
        {
            _context = context;
            _foodRepository = new FoodRepository(_context);
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            return View(await _foodRepository.AllAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _foodRepository.FindAsync();
            
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name,Amount,Size,FoodTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Food food)
        {
            if (ModelState.IsValid)
            {
                //food.Id = Guid.NewGuid();
                _foodRepository.Add(food);
                await _foodRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _foodRepository.FindAsync(id);
            
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,Name,Amount,Size,FoodTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _foodRepository.Update(food);
                await _foodRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _foodRepository.FindAsync(id);
            
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
            var food = _foodRepository.Remove(id);
            await _foodRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
