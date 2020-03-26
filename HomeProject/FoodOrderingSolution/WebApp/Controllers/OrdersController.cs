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
        public async Task<IActionResult> Details(Guid? id)
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
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name");
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Name", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Name", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", order.RestaurantId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Order order)
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
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", order.DrinkId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name", order.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Name", order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", order.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
