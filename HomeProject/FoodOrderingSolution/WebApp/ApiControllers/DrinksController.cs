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
    public class DrinksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrinksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrinks()
        {
            return await _context.Drinks.ToListAsync();
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(Guid id)
        {
            var drink = await _context.Drinks.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(Guid id, Drink drink)
        {
            if (id != drink.Id)
            {
                return BadRequest();
            }

            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/Drinks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drink>> DeleteDrink(Guid id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return drink;
        }

        private bool DrinkExists(Guid id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}
