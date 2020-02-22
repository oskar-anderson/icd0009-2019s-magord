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
    public class PersonInRestaurantsController : Controller
    {
        private readonly AppDbContext _context;

        public PersonInRestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonInRestaurants
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonInRestaurants.Include(p => p.Person).Include(p => p.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonInRestaurants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _context.PersonInRestaurants
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: PersonInRestaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,To,Role,PersonId,RestaurantId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PersonInRestaurant personInRestaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personInRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _context.PersonInRestaurants.FindAsync(id);
            if (personInRestaurant == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // POST: PersonInRestaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("From,To,Role,PersonId,RestaurantId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PersonInRestaurant personInRestaurant)
        {
            if (id != personInRestaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personInRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonInRestaurantExists(personInRestaurant.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _context.PersonInRestaurants
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return View(personInRestaurant);
        }

        // POST: PersonInRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var personInRestaurant = await _context.PersonInRestaurants.FindAsync(id);
            _context.PersonInRestaurants.Remove(personInRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonInRestaurantExists(string id)
        {
            return _context.PersonInRestaurants.Any(e => e.Id == id);
        }
    }
}
