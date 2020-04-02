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
    public class PricesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PricesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var prices = await _uow.Prices.AllAsync();
            return View(prices);
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value);

            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CampaignId"] = new SelectList(await _uow.Campaigns.AllAsync(), nameof(Campaign.Id), nameof(Campaign.Name));
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name));
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name));
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number));
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Price price)
        {
            if (ModelState.IsValid)
            {
                //price.Id = Guid.NewGuid();
                _uow.Prices.Add(price);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(await _uow.Campaigns.AllAsync(), nameof(Campaign.Id), nameof(Campaign.Name), price.CampaignId);
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), price.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name),price.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), price.IngredientId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), price.OrderId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value);

            if (price == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(await _uow.Campaigns.AllAsync(), nameof(Campaign.Id), nameof(Campaign.Name), price.CampaignId);
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), price.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name),price.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), price.IngredientId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), price.OrderId);
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Prices.Update(price);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Prices.ExistsAsync(price.Id))
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
            ViewData["CampaignId"] = new SelectList(await _uow.Campaigns.AllAsync(), nameof(Campaign.Id), nameof(Campaign.Name), price.CampaignId);
            ViewData["DrinkId"] = new SelectList(await _uow.Drinks.AllAsync(), nameof(Drink.Id), nameof(Drink.Name), price.DrinkId);
            ViewData["FoodId"] = new SelectList(await _uow.Foods.AllAsync(), nameof(Food.Id), nameof(Food.Name),price.FoodId);
            ViewData["IngredientId"] = new SelectList(await _uow.Ingredients.AllAsync(), nameof(Ingredient.Id), nameof(Ingredient.Name), price.IngredientId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), price.OrderId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value);

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
            await _uow.Prices.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
