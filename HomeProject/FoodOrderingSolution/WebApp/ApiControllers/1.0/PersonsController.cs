using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PersonMapper _mapper = new PersonMapper();
        
        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Persons
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.Person>>> GetPersons()
        {
            return Ok((await _bll.Persons.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.Person>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id);

            if (person == null)
            {
                return NotFound(new {message = "Person not found"});
            }

            return Ok(_mapper.Map(person));
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, V1DTO.Person person)
        {
            person.AppUserId = User.UserId();
            
            if (id != person.Id)
            {
                return BadRequest();
            }

            await _bll.Persons.UpdateAsync(_mapper.Map(person), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // DELETE: api/Persons/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
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
