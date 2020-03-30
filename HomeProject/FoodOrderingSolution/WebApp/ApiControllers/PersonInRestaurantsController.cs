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
using PublicApi.DTO.v1.PersonInRestaurantDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInRestaurantsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PersonInRestaurantsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PersonInRestaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonInRestaurantDTO>>> GetPersonInRestaurants()
        {
            var personInRestaurantDTOs = await _uow.PersonsInRestaurants.DTOAllAsync();
            
            return Ok(personInRestaurantDTOs);
        }

        // GET: api/PersonInRestaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonInRestaurantDTO>> GetPersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _uow.PersonsInRestaurants.DTOFirstOrDefaultAsync(id);

            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return Ok(personInRestaurant);
        }

        // PUT: api/PersonInRestaurants/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonInRestaurant(Guid id, PersonInRestaurantEditDTO personInRestaurantEditDTO)
        {
            if (id != personInRestaurantEditDTO.Id)
            {
                return BadRequest();
            }

            var personInRestaurant = await _uow.PersonsInRestaurants.FirstOrDefaultAsync(personInRestaurantEditDTO.Id);
            if (personInRestaurant == null)
            {
                return BadRequest();
            }

            personInRestaurant.From = personInRestaurantEditDTO.From;
            personInRestaurant.To = personInRestaurantEditDTO.To;
            personInRestaurant.Role = personInRestaurantEditDTO.Role;

            _uow.PersonsInRestaurants.Update(personInRestaurant);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.PersonsInRestaurants.ExistsAsync(id))
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

        // POST: api/PersonInRestaurants
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonInRestaurant>> PostPersonInRestaurant(PersonInRestaurantCreateDTO personInRestaurantCreateDTO)
        {
            var personInRestaurant = new PersonInRestaurant
            {
                Id = personInRestaurantCreateDTO.Id,
                From = personInRestaurantCreateDTO.From,
                To = personInRestaurantCreateDTO.To,
                Role = personInRestaurantCreateDTO.Role
            };
            
            _uow.PersonsInRestaurants.Add(personInRestaurant);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPersonInRestaurant", new { id = personInRestaurant.Id }, personInRestaurant);
        }

        // DELETE: api/PersonInRestaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonInRestaurant>> DeletePersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _uow.PersonsInRestaurants.FirstOrDefaultAsync(id);
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            _uow.PersonsInRestaurants.Remove(personInRestaurant);
            await _uow.SaveChangesAsync();

            return Ok(personInRestaurant);
        }
    }
}
