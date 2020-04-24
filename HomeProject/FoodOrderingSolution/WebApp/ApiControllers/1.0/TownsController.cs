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
using PublicApi.DTO.v1;
using Town = PublicApi.DTO.v1.Town;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TownsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TownsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Towns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Town>>> GetTowns()
        {
            var towns = (await _bll.Towns.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Town()
                {
                    Id = bllEntity.Id,
                    Name = bllEntity.Name,
                    AreaCount = bllEntity.AreaCount,
                });

            return Ok(towns);
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Town>> GetTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id, User.UserGuidId());
            
            if (town == null)
            {
                return NotFound();
            }

            return Ok(town);
        }

        // PUT: api/Towns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(Guid id, TownEdit townEditDTO)
        {
            if (id != townEditDTO.Id)
            {
                return BadRequest();
            }

            var town = await _bll.Towns.FirstOrDefaultAsync(townEditDTO.Id, User.UserGuidId());
            if (town == null)
            {
                return BadRequest();
            }
            
            town.Name = townEditDTO.Name;
            
            _bll.Towns.Update(town);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Towns.ExistsAsync(town.Id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Towns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Town>> PostTown(TownCreate townCreateDTO)
        {
            var town = new BLL.App.DTO.Town()
            {
                AppUserId = User.UserGuidId(),
                Name = townCreateDTO.Name
            };
            
            _bll.Towns.Add(town);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTown", new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Town>> DeleteTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id, User.UserGuidId());
            if (town == null)
            {
                return NotFound();
            }

            _bll.Towns.Remove(town);
            await _bll.SaveChangesAsync();

            return Ok(town);
        }
    }
}
