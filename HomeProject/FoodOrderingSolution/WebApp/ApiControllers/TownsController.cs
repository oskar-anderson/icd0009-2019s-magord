using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TownsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public TownsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Towns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TownDTO>>> GetTowns()
        {
            var townDTOs = await _uow.Towns.DTOAllAsync(User.UserGuidId());

            return Ok(townDTOs);
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TownDTO>> GetTown(Guid id)
        {
            var town = await _uow.Towns.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            
            if (town == null)
            {
                return NotFound();
            }

            return Ok(town);
        }

        // PUT: api/Towns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(Guid id, TownEditDTO townEditDTO)
        {
            if (id != townEditDTO.Id)
            {
                return BadRequest();
            }

            var town = await _uow.Towns.FirstOrDefaultAsync(townEditDTO.Id, User.UserGuidId());
            if (town == null)
            {
                return BadRequest();
            }
            
            town.Name = townEditDTO.Name;
            
            _uow.Towns.Update(town);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Towns.ExistsAsync(town.Id, User.UserGuidId()))
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
        public async Task<ActionResult<Town>> PostTown(TownCreateDTO townCreateDTO)
        {
            var town = new Town()
            {
                Id = townCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                Name = townCreateDTO.Name
            };
            
            _uow.Towns.Add(town);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTown", new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Town>> DeleteTown(Guid id)
        {
            var town = await _uow.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }

            _uow.Towns.Remove(town);
            await _uow.SaveChangesAsync();

            return town;
        }
    }
}
