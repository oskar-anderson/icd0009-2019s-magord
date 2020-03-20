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
    public class TownsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TownsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Towns
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Towns.AllAsync());
        }

        // GET: Towns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _uow.Towns.FindAsync(id);
            
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // GET: Towns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Town town)
        {
            if (ModelState.IsValid)
            {
                //town.Id = Guid.NewGuid();
                _uow.Towns.Add(town);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(town);
        }

        // GET: Towns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _uow.Towns.FindAsync(id);
            
            if (town == null)
            {
                return NotFound();
            }
            return View(town);
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Town town)
        {
            if (id != town.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Towns.Update(town);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(town);
        }

        // GET: Towns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _uow.Towns.FindAsync(id);
            
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var town = _uow.Towns.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
