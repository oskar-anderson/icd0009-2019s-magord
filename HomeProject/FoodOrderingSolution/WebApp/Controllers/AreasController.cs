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
    public class AreasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAreaRepository _areaRepository;

        public AreasController(AppDbContext context)
        {
            _context = context;
            _areaRepository = new AreaRepository(_context);
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            return View(await _areaRepository.AllAsync());
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _areaRepository.FindAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TownId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Area area)
        {
            if (ModelState.IsValid)
            {
                //area.Id = Guid.NewGuid();
                _areaRepository.Add(area);
                await _areaRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _areaRepository.FindAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,TownId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _areaRepository.Update(area);
                await _areaRepository.SaveChangesAsync();
                    
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _areaRepository.FindAsync(id);
            
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
            var area = _areaRepository.Remove(id);
            await _areaRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
    }
}
