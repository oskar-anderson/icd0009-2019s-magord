using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.IngredientDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public IngredientsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredients()
        {
            var ingredientDTOs = await _uow.Ingredients.DTOAllAsync();
            
            return Ok(ingredientDTOs);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(Guid id)
        {
            var ingredient = await _uow.Ingredients.DTOFirstOrDefaultAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(Guid id, IngredientEditDTO ingredientEditDTO)
        {
            if (id != ingredientEditDTO.Id)
            {
                return BadRequest();
            }

            var ingredient = await _uow.Ingredients.FirstOrDefaultAsync(ingredientEditDTO.Id);
            if (ingredient == null)
            {
                return BadRequest();
            }
            
            ingredient.Name = ingredientEditDTO.Name;
            ingredient.Amount = ingredientEditDTO.Amount;
            
            _uow.Ingredients.Update(ingredient);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Ingredients.ExistsAsync(id))
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

        // POST: api/Ingredients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(IngredientCreateDTO ingredientCreateDTO)
        {
            var ingredient = new Ingredient
            {
                Id = ingredientCreateDTO.Id,
                Amount = ingredientCreateDTO.Amount,
                Name = ingredientCreateDTO.Name,
            };
            
            _uow.Ingredients.Add(ingredient);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> DeleteIngredient(Guid id)
        {
            var ingredient = await _uow.Ingredients.FirstOrDefaultAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _uow.Ingredients.Remove(ingredient);
            await _uow.SaveChangesAsync();

            return Ok(ingredient);
        }
    }
}
