using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TownsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TownMapper _mapper = new TownMapper();

        public TownsController(IAppBLL bll)
        {
            _bll = bll; 
        }

        // GET: api/Towns
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.Town>>> GetTowns()
        {
            return Ok((await _bll.Towns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.Town>> GetTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id);
            
            if (town == null)
            {
                return NotFound(new {message = "Town not found"});
            }

            return Ok(_mapper.Map(town));
        }

        // PUT: api/Towns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(Guid id, V1DTO.Town town)
        {
            town.AppUserId = User.UserId();
            
            if (id != town.Id)
            {
                return BadRequest();
            }

            await _bll.Towns.UpdateAsync(_mapper.Map(town), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Towns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.Town>> PostTown(V1DTO.Town town)
        {
            town.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(town);
            _bll.Towns.Add(bllEntity);
            await _bll.SaveChangesAsync();
            town.Id = bllEntity.Id;
            
            return CreatedAtAction("GetTown",
                new { id = town.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                town);
        }

        // DELETE: api/Towns/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Town>> DeleteTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id, User.UserId());
            if (town == null)
            {
                return NotFound(new {message = "Town not found"});
            }

            await _bll.Towns.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(town);
        }
    }
}
