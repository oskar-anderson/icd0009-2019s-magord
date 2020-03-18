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
    public class OrderTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrderTypeRepository _orderTypeRepository;

        public OrderTypesController(AppDbContext context)
        {
            _context = context;
            _orderTypeRepository = new OrderTypeRepository(_context);
        }

        // GET: OrderTypes
        public async Task<IActionResult> Index()
        {
            return View(await _orderTypeRepository.AllAsync());
        }

        // GET: OrderTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _orderTypeRepository.FindAsync(id);
            
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // GET: OrderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                //orderType.Id = Guid.NewGuid();
                _orderTypeRepository.Add(orderType);
                await _orderTypeRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderType);
        }

        // GET: OrderTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _orderTypeRepository.FindAsync(id);
            
            if (orderType == null)
            {
                return NotFound();
            }
            return View(orderType);
        }

        // POST: OrderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderType orderType)
        {
            if (id != orderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _orderTypeRepository.Update(orderType);
                await _orderTypeRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(orderType);
        }

        // GET: OrderTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _orderTypeRepository.FindAsync(id);
            
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // POST: OrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderType = _orderTypeRepository.Remove(id);
            await _orderTypeRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
