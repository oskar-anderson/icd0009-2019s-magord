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
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public BillsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var bills = await _uow.Bills.AllAsync(User.UserGuidId());
            return View(bills);
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number));
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName));
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                _uow.Bills.Add(bill);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), bill.OrderId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), bill.PersonId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), bill.OrderId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), bill.PersonId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Bill bill)
        {
            bill.AppUserId = User.UserGuidId();
            
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Bills.Update(bill);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _uow.Bills.ExistsAsync(bill.Id, User.UserGuidId()))
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
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Number), bill.OrderId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), bill.PersonId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Bills.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
