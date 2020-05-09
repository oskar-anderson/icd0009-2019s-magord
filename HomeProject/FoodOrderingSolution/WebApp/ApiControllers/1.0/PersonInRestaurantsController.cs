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
    /// PersonInRestaurants Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PersonInRestaurantsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PersonInRestaurantMapper _mapper = new PersonInRestaurantMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PersonInRestaurantsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Persons in restaurant-s
        /// </summary>
        /// <returns>List of PersonsInRestaurants</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PersonInRestaurant>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PersonInRestaurant>>> GetPersonInRestaurants()
        {
            return Ok((await _bll.PersonInRestaurants.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Person in restaurant
        /// </summary>
        /// <param name="id">PersonInRestaurant Id</param>
        /// <returns>PersonInRestaurant object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PersonInRestaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.PersonInRestaurant>> GetPersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _bll.PersonInRestaurants.FirstOrDefaultAsync(id);
            
            if (personInRestaurant == null)
            {
                return NotFound(new {message = "Person in restaurant not found"});
            }

            return Ok(_mapper.Map(personInRestaurant));
        }

        /// <summary>
        /// Update the Person in restaurant
        /// </summary>
        /// <param name="id">PersonInRestaurant id</param>
        /// <param name="personInRestaurant">PersonInRestaurant object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonInRestaurant(Guid id, V1DTO.PersonInRestaurant personInRestaurant)
        {
            personInRestaurant.AppUserId = User.UserId();
            
            if (id != personInRestaurant.Id)
            {
                return BadRequest(new {message = "The id and personInRestaurant.id do not match!"});
            }

            await _bll.PersonInRestaurants.UpdateAsync(_mapper.Map(personInRestaurant), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Person in restaurant
        /// </summary>
        /// <param name="personInRestaurant">PersonInRestaurant object to create</param>
        /// <returns>Created PersonInRestaurant object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PersonInRestaurant))]
        public async Task<ActionResult<V1DTO.PersonInRestaurant>> PostPersonInRestaurant(V1DTO.PersonInRestaurant personInRestaurant)
        {
            personInRestaurant.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(personInRestaurant);
            _bll.PersonInRestaurants.Add(bllEntity);
            await _bll.SaveChangesAsync();
            personInRestaurant.Id = bllEntity.Id;
            
            return CreatedAtAction("GetPersonInRestaurant",
                new { id = personInRestaurant.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                personInRestaurant);
        }

        /// <summary>
        /// Delete an Person in restaurant
        /// </summary>
        /// <param name="id">PersonInRestaurant Id</param>
        /// <returns>Deleted PersonInRestaurant object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PersonInRestaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.PersonInRestaurant>> DeletePersonInRestaurant(Guid id)
        {
            var personInRestaurant = await _bll.PersonInRestaurants.FirstOrDefaultAsync(id, User.UserId());
            if (personInRestaurant == null)
            {
                return NotFound(new {message = "Person in restaurant not found"});
            }

            await _bll.PersonInRestaurants.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(personInRestaurant);
        }
    }
}
