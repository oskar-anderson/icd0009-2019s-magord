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
using Extensions;

namespace WebApp.Controllers
{
    public class AreasController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AreasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            var areas = await _uow.Areas.AllAsync();
            return View(areas);
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _uow.Areas.FirstOrDefaultAsync(id.Value);
            
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TownId"] = new SelectList(await _uow.Towns.AllAsync(), nameof(Town.Id), nameof(Town.Name));
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Area area)
        {
            if (ModelState.IsValid)
            {
                _uow.Areas.Add(area);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TownId"] = new SelectList(await _uow.Towns.AllAsync(), nameof(Town.Id), nameof(Town.Name), area.TownId);
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _uow.Areas.FirstOrDefaultAsync(id.Value);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["TownId"] = new SelectList(await _uow.Towns.AllAsync(), nameof(Town.Id), nameof(Town.Name), area.TownId);
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Areas.Update(area);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Areas.ExistsAsync(area.Id))
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
            ViewData["TownId"] = new SelectList(await _uow.Towns.AllAsync(), nameof(Town.Id), nameof(Town.Name), area.TownId);
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _uow.Areas.FirstOrDefaultAsync(id.Value);
            
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Areas.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
