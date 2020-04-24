using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.FoodDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FoodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodDTO>>> GetFoods()
        {
            var foodDTOs = await _uow.Foods.DTOAllAsync();
            
            return Ok(foodDTOs);
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDTO>> GetFood(Guid id)
        {
            var food = await _uow.Foods.DTOFirstOrDefaultAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(Guid id, FoodEditDTO foodEditDTO)
        {
            if (id != foodEditDTO.Id)
            {
                return BadRequest();
            }

            var food = await _uow.Foods.FirstOrDefaultAsync(foodEditDTO.Id);
            if (food == null)
            {
                return BadRequest();
            }

            food.Description = foodEditDTO.Description;
            food.Name = foodEditDTO.Name;
            food.Amount = foodEditDTO.Amount;
            food.Size = foodEditDTO.Size;
            
            _uow.Foods.Update(food);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Foods.ExistsAsync(id))
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

        // POST: api/Foods
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(FoodCreateDTO foodCreateDTO)
        {
            var food = new Food
            {
                Id = foodCreateDTO.Id,
                Name = foodCreateDTO.Name,
                Description  = foodCreateDTO.Description,
                Amount = foodCreateDTO.Amount,
                Size = foodCreateDTO.Size
            };
            
            _uow.Foods.Add(food);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood(Guid id)
        {
            var food = await _uow.Foods.FirstOrDefaultAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _uow.Foods.Remove(food);
            await _uow.SaveChangesAsync();

            return Ok(food);
        }
    }
}
