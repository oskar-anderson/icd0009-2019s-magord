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
    public class RestaurantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsController(AppDbContext context)
        {
            _context = context;
            _restaurantRepository = new RestaurantRepository(_context);
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _restaurantRepository.AllAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _restaurantRepository.FindAsync(id);
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,OpenedFrom,ClosedFrom,AreaId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                //restaurant.Id = Guid.NewGuid();
                _restaurantRepository.Add(restaurant);
                await _restaurantRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _restaurantRepository.FindAsync(id);
            
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Address,OpenedFrom,ClosedFrom,AreaId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _restaurantRepository.Update(restaurant);
                await _restaurantRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _restaurantRepository.FindAsync(id);
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restaurant = _restaurantRepository.Remove(id);
            await _restaurantRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
