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
    public class TownsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TownsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Towns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Town>>> GetTowns()
        {
            return await _context.Towns.ToListAsync();
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Town>> GetTown(Guid id)
        {
            var town = await _context.Towns.FindAsync(id);

            if (town == null)
            {
                return NotFound();
            }

            return town;
        }

        // PUT: api/Towns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(Guid id, Town town)
        {
            if (id != town.Id)
            {
                return BadRequest();
            }

            _context.Entry(town).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownExists(id))
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

        // POST: api/Towns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Town>> PostTown(Town town)
        {
            _context.Towns.Add(town);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTown", new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Town>> DeleteTown(Guid id)
        {
            var town = await _context.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }

            _context.Towns.Remove(town);
            await _context.SaveChangesAsync();

            return town;
        }

        private bool TownExists(Guid id)
        {
            return _context.Towns.Any(e => e.Id == id);
        }
    }
}
