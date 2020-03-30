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
using PublicApi.DTO.v1.FoodTypeDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FoodTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/FoodTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTypeDTO>>> GetFoodTypes()
        {
            var foodTypeDTOs = await _uow.FoodTypes.DTOAllAsync();
            
            return Ok(foodTypeDTOs);
        }

        // GET: api/FoodTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodTypeDTO>> GetFoodType(Guid id)
        {
            var foodType = await _uow.FoodTypes.DTOFirstOrDefaultAsync(id);

            if (foodType == null)
            {
                return NotFound();
            }

            return Ok(foodType);
        }

        // PUT: api/FoodTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodType(Guid id, FoodTypeEditDTO foodTypeEditDTO)
        {
            if (id != foodTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var foodType = await _uow.FoodTypes.FirstOrDefaultAsync(foodTypeEditDTO.Id);
            if (foodType == null)
            {
                return BadRequest();
            }
            
            foodType.Name = foodTypeEditDTO.Name;

            _uow.FoodTypes.Update(foodType);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.FoodTypes.ExistsAsync(id))
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
        public async Task<ActionResult<FoodType>> PostFoodType(FoodTypeCreateDTO foodTypeCreateDTO)
        {
            var foodType = new FoodType
            {
                Id = foodTypeCreateDTO.Id,
                Name = foodTypeCreateDTO.Name,
            };
            
            _uow.FoodTypes.Add(foodType);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetFoodType", new { id = foodType.Id }, foodType);
        }

        // DELETE: api/FoodTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodType>> DeleteFoodType(Guid id)
        {
            var foodType = await _uow.FoodTypes.FirstOrDefaultAsync(id);
            if (foodType == null)
            {
                return NotFound();
            }

            _uow.FoodTypes.Remove(foodType);
            await _uow.SaveChangesAsync();

            return Ok(foodType);
        }
    }
}
