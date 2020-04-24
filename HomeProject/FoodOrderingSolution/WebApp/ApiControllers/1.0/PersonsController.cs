using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Person = PublicApi.DTO.v1.Person;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        
        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var persons = (await _bll.Persons.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Person()
                {
                    Id = bllEntity.Id,
                    FirstName = bllEntity.FirstName,
                    LastName = bllEntity.LastName,
                    Sex = bllEntity.Sex,
                    DateOfBirth = bllEntity.DateOfBirth,
                });

            return Ok(persons);
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserGuidId());

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
        public async Task<IActionResult> PutPerson(Guid id, PersonEdit personEditDTO)
        {
            if (id != personEditDTO.Id)
            {
                return BadRequest();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(personEditDTO.Id, User.UserGuidId());
            if (person == null)
            {
                return BadRequest();
            }

            person.FirstName = personEditDTO.FirstName;
            person.LastName = personEditDTO.LastName;
            person.Sex = personEditDTO.Sex;
            person.DateOfBirth = personEditDTO.DateOfBirth;
            
            _bll.Persons.Update(person);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Persons.ExistsAsync(id, User.UserGuidId()))
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
        public async Task<ActionResult<Person>> PostPerson(PersonCreate personCreateDTO)
        {
            var person = new BLL.App.DTO.Person
            {
                AppUserId = User.UserGuidId(),
                FirstName = personCreateDTO.FirstName,
                LastName = personCreateDTO.LastName,
                Sex = personCreateDTO.Sex,
                DateOfBirth = personCreateDTO.DateOfBirth,
            };
            
            _bll.Persons.Add(person);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserGuidId());
            if (person == null)
            {
                return NotFound();
            }

            _bll.Persons.Remove(person);
            await _bll.SaveChangesAsync();

            return Ok(person);
        }
    }
}
