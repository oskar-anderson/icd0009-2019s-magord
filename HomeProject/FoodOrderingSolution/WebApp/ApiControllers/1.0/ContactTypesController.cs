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
    public class ContactTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ContactTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ContactTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactTypeDTO>>> GetContactTypes()
        {
            var contactTypeDTOs = await _uow.ContactTypes.DTOAllAsync();
            
            return Ok(contactTypeDTOs);
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactTypeDTO>> GetContactType(Guid id)
        {
            var contactType = await _uow.ContactTypes.DTOFirstOrDefaultAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return Ok(contactType);
        }

        // PUT: api/ContactTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(Guid id, ContactTypeEditDTO contactTypeEditDTO)
        {
            if (id != contactTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(contactTypeEditDTO.Id);
            if (contactType == null)
            {
                return BadRequest();
            }

            contactType.Name = contactTypeEditDTO.Name;
            
            _uow.ContactTypes.Update(contactType);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.ContactTypes.ExistsAsync(id))
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

        // POST: api/ContactTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactTypeCreateDTO contactTypeCreateDTO)
        {
            var contactType = new ContactType
            {
                Id = contactTypeCreateDTO.Id,
                Name = contactTypeCreateDTO.Name,
            };
            
            _uow.ContactTypes.Add(contactType);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactType>> DeleteContactType(Guid id)
        {
            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _uow.ContactTypes.Remove(contactType);
            await _uow.SaveChangesAsync();

            return Ok(contactType);
        }
    }
}
