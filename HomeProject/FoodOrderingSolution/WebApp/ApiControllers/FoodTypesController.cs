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
    public class FoodTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodType>>> GetFoodTypes()
        {
            return await _context.FoodTypes.ToListAsync();
        }

        // GET: api/FoodTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodType>> GetFoodType(Guid id)
        {
            var foodType = await _context.FoodTypes.FindAsync(id);

            if (foodType == null)
            {
                return NotFound();
            }

            return foodType;
        }

        // PUT: api/FoodTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodType(Guid id, FoodType foodType)
        {
            if (id != foodType.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodTypeExists(id))
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

        // POST: api/FoodTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FoodType>> PostFoodType(FoodType foodType)
        {
            _context.FoodTypes.Add(foodType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodType", new { id = foodType.Id }, foodType);
        }

        // DELETE: api/FoodTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodType>> DeleteFoodType(Guid id)
        {
            var foodType = await _context.FoodTypes.FindAsync(id);
            if (foodType == null)
            {
                return NotFound();
            }

            _context.FoodTypes.Remove(foodType);
            await _context.SaveChangesAsync();

            return foodType;
        }

        private bool FoodTypeExists(Guid id)
        {
            return _context.FoodTypes.Any(e => e.Id == id);
        }
    }
}
