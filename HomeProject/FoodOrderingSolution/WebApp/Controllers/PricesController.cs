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
    public class PricesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPriceRepository _priceRepository;

        public PricesController(AppDbContext context)
        {
            _context = context;
            _priceRepository = new PriceRepository(_context);
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            return View(await _priceRepository.AllAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _priceRepository.FindAsync(id);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,To,Value,IngredientId,FoodId,DrinkId,OrderId,CampaignId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Price price)
        {
            if (ModelState.IsValid)
            {
                //price.Id = Guid.NewGuid();
                _priceRepository.Add(price);
                await _priceRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _priceRepository.FindAsync(id);
            
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,To,Value,IngredientId,FoodId,DrinkId,OrderId,CampaignId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _priceRepository.Update(price);
                await _priceRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _priceRepository.FindAsync(id);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = _priceRepository.Remove(id);
            await _priceRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
