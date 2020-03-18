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
    public class PersonInRestaurantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPersonInRestaurantRepository _personInRestaurantRepository;

        public PersonInRestaurantsController(AppDbContext context)
        {
            _context = context;
            _personInRestaurantRepository = new PersonInRestaurantRepository(_context);
        }

        // GET: PersonInRestaurants
        public async Task<IActionResult> Index()
        {
            return View(await _personInRestaurantRepository.AllAsync());
        }

        // GET: PersonInRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _personInRestaurantRepository.FindAsync(id);
            
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonInRestaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,To,Role,PersonId,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonInRestaurant personInRestaurant)
        {
            if (ModelState.IsValid)
            {
                //personInRestaurant.Id = Guid.NewGuid();
                _personInRestaurantRepository.Add(personInRestaurant);
                await _personInRestaurantRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _personInRestaurantRepository.FindAsync(id);
            
            if (personInRestaurant == null)
            {
                return NotFound();
            }
            return View(personInRestaurant);
        }

        // POST: PersonInRestaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,To,Role,PersonId,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonInRestaurant personInRestaurant)
        {
            if (id != personInRestaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _personInRestaurantRepository.Update(personInRestaurant);
                await _personInRestaurantRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _personInRestaurantRepository.FindAsync(id);
            
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return View(personInRestaurant);
        }

        // POST: PersonInRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personInRestaurant = _personInRestaurantRepository.Remove(id);
            await _personInRestaurantRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
