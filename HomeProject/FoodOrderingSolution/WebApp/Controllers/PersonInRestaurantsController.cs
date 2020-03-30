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
        public IActionResult Create()
        {
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
