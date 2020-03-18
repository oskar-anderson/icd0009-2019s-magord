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
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
            _paymentRepository = new PaymentRepository(_context);
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(await _paymentRepository.AllAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.FindAsync(id);
            
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,TimeMade,PersonId,BillId,PaymentTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                //payment.Id = Guid.NewGuid();
                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.FindAsync(id);
            
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,TimeMade,PersonId,BillId,PaymentTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _paymentRepository.Update(payment);
                await _paymentRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.FindAsync(id);
            
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var payment = _paymentRepository.Remove(id);
            await _paymentRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
