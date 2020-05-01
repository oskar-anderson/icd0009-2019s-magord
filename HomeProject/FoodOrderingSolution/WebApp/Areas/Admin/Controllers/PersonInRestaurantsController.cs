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
    public class PersonInRestaurantsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonInRestaurantsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PersonInRestaurants
        public async Task<IActionResult> Index()
        {
            var personInRestaurants = await _uow.PersonsInRestaurants.AllAsync();
            return View(personInRestaurants);
        }

        // GET: PersonInRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _uow.PersonsInRestaurants.FirstOrDefaultAsync(id.Value);

            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName));
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name));
            return View();
        }

        // POST: PersonInRestaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonInRestaurant personInRestaurant)
        {
            if (ModelState.IsValid)
            {
                //personInRestaurant.Id = Guid.NewGuid();
                _uow.PersonsInRestaurants.Add(personInRestaurant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _uow.PersonsInRestaurants.FirstOrDefaultAsync(id.Value);

            if (personInRestaurant == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // POST: PersonInRestaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonInRestaurant personInRestaurant)
        {
            if (id != personInRestaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PersonsInRestaurants.Update(personInRestaurant);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.PersonsInRestaurants.ExistsAsync(personInRestaurant.Id))
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
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), personInRestaurant.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), personInRestaurant.RestaurantId);
            return View(personInRestaurant);
        }

        // GET: PersonInRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRestaurant = await _uow.PersonsInRestaurants.FirstOrDefaultAsync(id.Value);

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
            await _uow.PersonsInRestaurants.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
