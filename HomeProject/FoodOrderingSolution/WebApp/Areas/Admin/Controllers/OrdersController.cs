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
    public class OrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _uow.Orders.AllAsync(User.UserGuidId());
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name));
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name));
            ViewData["OrderTypeId"] = new SelectList(await _uow.OrderTypes.AllAsync(), nameof(OrderType.Id), nameof(OrderType.Name));
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName));
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name));
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            order.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                //order.Id = Guid.NewGuid();
                _uow.Orders.Add(order);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), order.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), order.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(await _uow.OrderTypes.AllAsync(), nameof(OrderType.Id), nameof(OrderType.Name), order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), order.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (order == null)
            {
                return NotFound();
            }
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), order.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), order.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(await _uow.OrderTypes.AllAsync(), nameof(OrderType.Id), nameof(OrderType.Name), order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), order.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), order.RestaurantId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            
            order.AppUserId = User.UserGuidId();


            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Orders.Update(order);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.Orders.ExistsAsync(order.Id, User.UserGuidId()))
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
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), order.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name), order.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), order.IngredientId);
            ViewData["OrderTypeId"] = new SelectList(await _uow.OrderTypes.AllAsync(), nameof(OrderType.Id), nameof(OrderType.Name), order.OrderTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), order.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
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
            await _uow.Orders.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
