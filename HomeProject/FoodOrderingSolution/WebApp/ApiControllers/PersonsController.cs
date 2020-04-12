using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PersonsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons()
        {
            var personDTOs = await _uow.Persons.DTOAllAsync(User.UserGuidId());
            
            return Ok(personDTOs);
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(Guid id)
        {
            var person = await _uow.Persons.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonEditDTO personEditDTO)
        {
            if (id != personEditDTO.Id)
            {
                return BadRequest();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(personEditDTO.Id, User.UserGuidId());
            if (person == null)
            {
                return BadRequest();
            }

            person.FirstName = personEditDTO.FirstName;
            person.LastName = personEditDTO.LastName;
            person.Sex = personEditDTO.Sex;
            person.DateOfBirth = personEditDTO.DateOfBirth;
            
            _uow.Persons.Update(person);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Persons.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Persons
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonCreateDTO personCreateDTO)
        {
            var person = new Person
            {
                Id = personCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                FirstName = personCreateDTO.FirstName,
                LastName = personCreateDTO.LastName,
                Sex = personCreateDTO.Sex,
                DateOfBirth = personCreateDTO.DateOfBirth,
            };
            
            _uow.Persons.Add(person);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _uow.Persons.FirstOrDefaultAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _uow.Persons.Remove(person);
            await _uow.SaveChangesAsync();

            return Ok(person);
        }
    }
}
