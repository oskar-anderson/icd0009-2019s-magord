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
    public class PaymentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await _uow.Payments.AllAsync();
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BillId"] = new SelectList(await _uow.Bills.AllAsync(), nameof(Bill.Id), nameof(Bill.Sum));
            ViewData["PaymentTypeId"] = new SelectList(await _uow.PaymentTypes.AllAsync(), nameof(PaymentType.Id), nameof(PaymentType.Name));
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName));
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                //payment.Id = Guid.NewGuid();
                _uow.Payments.Add(payment);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillId"] = new SelectList(await _uow.Bills.AllAsync(), nameof(Bill.Id), nameof(Bill.Sum),payment.BillId);
            ViewData["PaymentTypeId"] = new SelectList(await _uow.PaymentTypes.AllAsync(), nameof(PaymentType.Id), nameof(PaymentType.Name), payment.PaymentTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), payment.PersonId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value);

            if (payment == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(await _uow.Bills.AllAsync(), nameof(Bill.Id), nameof(Bill.Sum),payment.BillId);
            ViewData["PaymentTypeId"] = new SelectList(await _uow.PaymentTypes.AllAsync(), nameof(PaymentType.Id), nameof(PaymentType.Name), payment.PaymentTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), payment.PersonId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Payments.Update(payment);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Contacts.ExistsAsync(payment.Id))
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
            ViewData["BillId"] = new SelectList(await _uow.Bills.AllAsync(), nameof(Bill.Id), nameof(Bill.Sum),payment.BillId);
            ViewData["PaymentTypeId"] = new SelectList(await _uow.PaymentTypes.AllAsync(), nameof(PaymentType.Id), nameof(PaymentType.Name), payment.PaymentTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), payment.PersonId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value);

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
            await _uow.Payments.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
