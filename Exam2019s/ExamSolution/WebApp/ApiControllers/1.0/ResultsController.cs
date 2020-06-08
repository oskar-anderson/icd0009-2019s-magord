using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResultsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ResultsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.Result>>> GetResults()
        {
            return Ok(await _uow.Results.GetAllAsync(User.UserId()));
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Result>> GetResult(Guid id)
        {
            var result = await _uow.Results.FirstOrDefaultAsync(id, User.UserId());

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Results/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(Guid id, DAL.App.DTO.Result result)
        {
            result.AppUserId = User.UserId();
            
            if (id != result.Id)
            {
                return BadRequest();
            }

            await _uow.Results.UpdateAsync(result, User.UserId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Results
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(DAL.App.DTO.Result result)
        {
            result.AppUserId = User.UserId();
            
            _uow.Results.Add(result);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetResult",
                new {id = result.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteResult(Guid id)
        {
            var result = await _uow.Results.FirstOrDefaultAsync(id, User.UserId());
            
            if (result == null)
            {
                return NotFound();
            }

            await _uow.Results.RemoveAsync(result);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }
    }
}