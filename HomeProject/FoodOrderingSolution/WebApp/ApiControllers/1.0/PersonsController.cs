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
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Persons Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PersonMapper _mapper = new PersonMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Person-s
        /// </summary>
        /// <returns>List of Persons</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Person>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Person>>> GetPersons()
        {
            return Ok((await _bll.Persons.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Person
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns>Person object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Person>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id);

            if (person == null)
            {
                return NotFound(new {message = "Person not found"});
            }

            return Ok(_mapper.Map(person));
        }

        /// <summary>
        /// Update the Person
        /// </summary>
        /// <param name="id">Person id</param>
        /// <param name="person">Person object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPerson(Guid id, V1DTO.Person person)
        {
            person.AppUserId = User.UserId();
            
            if (id != person.Id)
            {
                return BadRequest(new {message = "The id and person.id do not match!"});
            }

            await _bll.Persons.UpdateAsync(_mapper.Map(person), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Person 
        /// </summary>
        /// <param name="person">Person object to create</param>
        /// <returns>Created Person object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Person))]
        public async Task<ActionResult<V1DTO.Person>> PostPerson(V1DTO.Person person)
        {
            person.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(person);
            _bll.Persons.Add(bllEntity);
            await _bll.SaveChangesAsync();
            person.Id = bllEntity.Id;
            
            return CreatedAtAction("GetPerson",
                new { id = person.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                person);
        }

        /// <summary>
        /// Delete an Person
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns>Deleted Person object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Person>> DeletePerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserId());
            if (person == null)
            {
                return NotFound(new {message = "Person not found"});
            }

            await _bll.Persons.RemoveAsync(person);
            await _bll.SaveChangesAsync();

            return Ok(person);
        }
    }
}
