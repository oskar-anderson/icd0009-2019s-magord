using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChoicesController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ChoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Choices
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.Choice>>> GetChoices()
        {
            return Ok(await _uow.Choices.GetAllAsync());
        }

        // GET: api/Choices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Choice>> GetChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);

            if (choice == null)
            {
                return NotFound();
            }

            return Ok(choice);
        }

        // PUT: api/Choices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> PutChoice(Guid id, DAL.App.DTO.Choice choice)
        {
            if (id != choice.Id)
            {
                return BadRequest();
            }

            await _uow.Choices.UpdateAsync(choice);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Choices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Choice>> PostChoice(DAL.App.DTO.Choice choice)
        {
            _uow.Choices.Add(choice);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetChoice",
                new {id = choice.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                choice);
        }

        // DELETE: api/Choices/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Choice>> DeleteChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);

            if (choice == null)
            {
                return NotFound();
            }

            await _uow.Choices.RemoveAsync(choice);
            await _uow.SaveChangesAsync();

            return Ok(choice);
        }
    }
}