using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ContactsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContacts()
        {
            var contactDTO = await _uow.Contacts.DTOAllAsync();
            
            return Ok(contactDTO);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContact(Guid id)
        {
            var contact = await _uow.Contacts.DTOFirstOrDefaultAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(Guid id, ContactEditDTO contactEditDTO)
        {
            if (id != contactEditDTO.Id)
            {
                return BadRequest();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(contactEditDTO.Id);
            
            if (contact == null)
            {
                return BadRequest();
            }

            contact.Name = contactEditDTO.Name;
            
            _uow.Contacts.Update(contact);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Contacts.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(ContactCreateDTO contactCreateDTO)
        {
            var contact = new Contact
            {
                Id = contactCreateDTO.Id,
                Name = contactCreateDTO.Name,
            };
            
            _uow.Contacts.Add(contact);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(Guid id)
        {
            var contact = await _uow.Contacts.FirstOrDefaultAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _uow.Contacts.Remove(contact);
            await _uow.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
