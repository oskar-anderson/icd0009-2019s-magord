using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInRestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonInRestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonInRestaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonInRestaurant>>> GetPersonInRestaurants()
        {
            return await _context.PersonInRestaurants.ToListAsync();
        }

        // GET: api/PersonInRestaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonInRestaurant>> GetPersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _context.PersonInRestaurants.FindAsync(id);

            if (personInRestaurant == null)
            {
                return NotFound();
            }

            return personInRestaurant;
        }

        // PUT: api/PersonInRestaurants/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonInRestaurant(Guid id, PersonInRestaurant personInRestaurant)
        {
            if (id != personInRestaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(personInRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonInRestaurantExists(id))
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
        public async Task<ActionResult<PersonInRestaurant>> PostPersonInRestaurant(PersonInRestaurant personInRestaurant)
        {
            _context.PersonInRestaurants.Add(personInRestaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonInRestaurant", new { id = personInRestaurant.Id }, personInRestaurant);
        }

        // DELETE: api/PersonInRestaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonInRestaurant>> DeletePersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _context.PersonInRestaurants.FindAsync(id);
            if (personInRestaurant == null)
            {
                return NotFound();
            }

            _context.PersonInRestaurants.Remove(personInRestaurant);
            await _context.SaveChangesAsync();

            return personInRestaurant;
        }

        private bool PersonInRestaurantExists(Guid id)
        {
            return _context.PersonInRestaurants.Any(e => e.Id == id);
        }
    }
}
