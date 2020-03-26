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
    public class BillsController : Controller
    {
        private readonly AppDbContext _context;

        public BillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bills.Include(b => b.AppUser).Include(b => b.Order).Include(b => b.Person);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.AppUser)
                .Include(b => b.Order)
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeIssued,Number,Sum,OrderId,AppUserId,PersonId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.Id = Guid.NewGuid();
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", bill.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", bill.OrderId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", bill.PersonId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", bill.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", bill.OrderId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", bill.PersonId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TimeIssued,Number,Sum,OrderId,AppUserId,PersonId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", bill.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderStatus", bill.OrderId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", bill.PersonId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.AppUser)
                .Include(b => b.Order)
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var bill = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(Guid id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
