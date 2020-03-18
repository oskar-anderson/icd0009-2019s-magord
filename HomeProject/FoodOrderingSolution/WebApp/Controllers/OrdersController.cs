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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(AppDbContext context)
        {
            _context = context;
            _orderRepository = new OrderRepository(_context);
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _orderRepository.AllAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.FindAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
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
                //order.Id = Guid.NewGuid();
                _orderRepository.Add(order);
                await _orderRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.FindAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }
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
                _orderRepository.Update(order);
                await _orderRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.FindAsync(id);
            
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
            var order = _orderRepository.Remove(id);
            await _orderRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
