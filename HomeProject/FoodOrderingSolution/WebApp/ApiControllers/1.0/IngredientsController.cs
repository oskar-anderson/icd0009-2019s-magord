using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Ingredients Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class IngredientsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IngredientMapper _mapper = new IngredientMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public IngredientsController(IAppBLL bll)
        {
            _bll = bll; 
        }

        /// <summary>
        /// Get a list of all the Ingredient-s
        /// </summary>
        /// <returns>List of Ingredients</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Ingredient>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Ingredient>>> GetIngredients()
        {
            return Ok((await _bll.Ingredients.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Ingredient
        /// </summary>
        /// <param name="id">Ingredient Id</param>
        /// <returns>Ingredient object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Ingredient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Ingredient>> GetIngredient(Guid id)
        {
            var ingredient = await _bll.Ingredients.FirstOrDefaultAsync(id);
            
            if (ingredient == null)
            {
                return NotFound(new {message = "Ingredient not found"});
            }

            return Ok(_mapper.Map(ingredient));
        }

        /// <summary>
        /// Update the Ingredient
        /// </summary>
        /// <param name="id">Ingredient id</param>
        /// <param name="ingredient">Ingredient object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutIngredient(Guid id, V1DTO.Ingredient ingredient)
        {
            ingredient.AppUserId = User.UserId();
            
            if (id != ingredient.Id)
            {
                return BadRequest(new {message = "The id and ingredient.id do not match!"});
            }

            await _bll.Ingredients.UpdateAsync(_mapper.Map(ingredient), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Ingredient 
        /// </summary>
        /// <param name="ingredient">Ingredient object to create</param>
        /// <returns>Created Ingredient object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Ingredient))]
        public async Task<ActionResult<V1DTO.Ingredient>> PostIngredient(V1DTO.Ingredient ingredient)
        {
            ingredient.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(ingredient);
            _bll.Ingredients.Add(bllEntity);
            await _bll.SaveChangesAsync();
            ingredient.Id = bllEntity.Id;
            
            return CreatedAtAction("GetIngredient",
                new { id = ingredient.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                ingredient);
        }

        /// <summary>
        /// Delete an Ingredient
        /// </summary>
        /// <param name="id">Ingredient Id</param>
        /// <returns>Deleted Ingredient object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Ingredient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Ingredient>> DeleteIngredient(Guid id)
        {
            var ingredient = await _bll.Ingredients.FirstOrDefaultAsync(id, User.UserId());
            if (ingredient == null)
            {
                return NotFound(new {message = "Ingredient not found"});
            }

            await _bll.Ingredients.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(ingredient);
        }
    }
}
