using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    /// ContactTypes Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ContactTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ContactTypeMapper _mapper = new ContactTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Contact type-s
        /// </summary>
        /// <returns>List of ContactTypes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ContactType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ContactType>>> GetContactTypes()
        {
            return Ok((await _bll.ContactTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Contact type
        /// </summary>
        /// <param name="id">ContactType Id</param>
        /// <returns>ContactType object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ContactType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.ContactType>> GetContactType(Guid id)
        {
            var contactType = await _bll.ContactTypes.FirstOrDefaultAsync(id);

            if (contactType == null)
            {
                return NotFound(new {message = "Contact type not found"});
            }

            return Ok(_mapper.Map(contactType));

        }

        /// <summary>
        /// Update the Contact type
        /// </summary>
        /// <param name="id">ContactType id</param>
        /// <param name="contactType">ContactType object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutContactType(Guid id, V1DTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest(new {message = "The id and contactType.id do not match!"});
            }

            await _bll.ContactTypes.UpdateAsync(_mapper.Map(contactType));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        /// <summary>
        /// Create a new Contact type
        /// </summary>
        /// <param name="contactType">ContactType object to create</param>
        /// <returns>Created ContactType object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ContactType))]
        public async Task<ActionResult<V1DTO.ContactType>> PostContactType(V1DTO.ContactType contactType)
        {
            var bllEntity = _mapper.Map(contactType);
            _bll.ContactTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            contactType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetContactType",
                new {id = contactType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                contactType);
        }

        /// <summary>
        /// Delete a Contact type
        /// </summary>
        /// <param name="id">ContactType Id</param>
        /// <returns>Deleted ContactType object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ContactType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.ContactType>> DeleteContactType(Guid id)
        {
            var contactType = await _bll.ContactTypes.FirstOrDefaultAsync(id);
            if (contactType == null)
            {
                return NotFound(new {message = "Contact type not found!"});
            }

            await _bll.ContactTypes.RemoveAsync(contactType);
            await _bll.SaveChangesAsync();

            return Ok(contactType);
        }
    }
}
