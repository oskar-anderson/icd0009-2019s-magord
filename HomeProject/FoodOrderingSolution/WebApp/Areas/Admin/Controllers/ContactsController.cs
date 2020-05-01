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
    public class ContactsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ContactsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _uow.Contacts.AllAsync();
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.AllAsync(), nameof(ContactType.Id), nameof(ContactType.Name));
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName));
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                //contact.Id = Guid.NewGuid();
                _uow.Contacts.Add(contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.AllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), contact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);
            
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.AllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), contact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), contact.PersonId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Contacts.Update(contact);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Contacts.ExistsAsync(contact.Id))
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
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.AllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), contact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstName), contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);
            
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Contacts.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
