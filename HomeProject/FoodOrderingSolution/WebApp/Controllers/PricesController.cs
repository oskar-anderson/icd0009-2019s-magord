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
    public class PricesController : Controller
    {
        private readonly AppDbContext _context;

        public PricesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Prices.Include(p => p.Campaign).Include(p => p.Drink).Include(p => p.Food).Include(p => p.Ingredient).Include(p => p.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Campaign)
                .Include(p => p.Drink)
                .Include(p => p.Food)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name");
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name");
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus");
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
                price.Id = Guid.NewGuid();
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", price.CampaignId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", price.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", price.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", price.IngredientId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", price.OrderId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", price.CampaignId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", price.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", price.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", price.IngredientId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", price.OrderId);
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
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", price.CampaignId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", price.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", price.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", price.IngredientId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", price.OrderId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Campaign)
                .Include(p => p.Drink)
                .Include(p => p.Food)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var price = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(Guid id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }
    }
}
