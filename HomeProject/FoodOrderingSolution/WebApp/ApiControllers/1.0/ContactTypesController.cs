using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ContactTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ContactTypeMapper _mapper = new ContactTypeMapper();


        public ContactTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ContactTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ContactType>>> GetContactTypes()
        {
            return Ok((await _bll.ContactTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.ContactType>> GetContactType(Guid id)
        {
            var contactType = await _bll.ContactTypes.FirstOrDefaultAsync(id);

            if (contactType == null)
            {
                return NotFound(new {message = "ContactType not found"});
            }

            return Ok(_mapper.Map(contactType));

        }

        // PUT: api/ContactTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutContactType(Guid id, V1DTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest(new {message = "id and gpsLocationType.id do not match"});
            }

            await _bll.ContactTypes.UpdateAsync(_mapper.Map(contactType));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ContactTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.ContactType>> DeleteContactType(Guid id)
        {
            var contactType = await _bll.ContactTypes.FirstOrDefaultAsync(id);
            if (contactType == null)
            {
                return NotFound(new {message = "ContactType not found!"});
            }

            await _bll.ContactTypes.RemoveAsync(contactType);
            await _bll.SaveChangesAsync();

            return Ok(contactType);
        }
    }
}
