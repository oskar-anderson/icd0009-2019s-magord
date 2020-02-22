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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.AppUser).Include(o => o.Drink).Include(o => o.Food).Include(o => o.Ingredient).Include(o => o.OrderType).Include(o => o.Person).Include(o => o.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Drink)
                .Include(o => o.Food)
                .Include(o => o.Ingredient)
                .Include(o => o.OrderType)
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id");
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id");
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Id", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Id", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Id", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Drink)
                .Include(o => o.Food)
                .Include(o => o.Ingredient)
                .Include(o => o.OrderType)
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
