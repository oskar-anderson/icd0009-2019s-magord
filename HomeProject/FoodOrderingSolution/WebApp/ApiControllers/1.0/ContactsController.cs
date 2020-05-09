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
    /// Contacts Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ContactsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ContactMapper _mapper = new ContactMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get a list of all the Contact-s
        /// </summary>
        /// <returns>List of Contacts</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Contact>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Contact>>> GetContacts()
        {
            return Ok((await _bll.Contacts.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Contact
        /// </summary>
        /// <param name="id">Contact Id</param>
        /// <returns>Contact object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Contact>> GetContact(Guid id)
        {
            var contact = await _bll.Contacts.FirstOrDefaultAsync(id);
            
            if (contact == null)
            {
                return NotFound(new {message = "Contact not found"});
            }

            return Ok(_mapper.Map(contact));
        }

        /// <summary>
        /// Update the Contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <param name="contact">Contact object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutContact(Guid id, V1DTO.Contact contact)
        {
            contact.AppUserId = User.UserId();
            
            if (id != contact.Id)
            {
                return BadRequest(new {message = "The id and contact.id do not match!"});
            }

            await _bll.Contacts.UpdateAsync(_mapper.Map(contact), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Contact 
        /// </summary>
        /// <param name="contact">Contact object to create</param>
        /// <returns>Created Contact object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Contact))]
        public async Task<ActionResult<V1DTO.Contact>> PostContact(V1DTO.Contact contact)
        {
            contact.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(contact);
            _bll.Contacts.Add(bllEntity);
            await _bll.SaveChangesAsync();
            contact.Id = bllEntity.Id;
            
            return CreatedAtAction("GetContact",
                new { id = contact.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                contact);
        }

        /// <summary>
        /// Delete a Contact
        /// </summary>
        /// <param name="id">Contact Id</param>
        /// <returns>Deleted Contact object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Contact>> DeleteContact(Guid id)
        {
            var contact = await _bll.Contacts.FirstOrDefaultAsync(id, User.UserId());
            if (contact == null)
            {
                return NotFound(new {message = "Contact not found"});
            }

            await _bll.Contacts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
