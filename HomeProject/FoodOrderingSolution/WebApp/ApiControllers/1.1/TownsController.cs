using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._1
{
    [ApiController]
    [ApiVersion( "1.1" )]
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
        public async Task<ActionResult<IEnumerable<TownDTO>>> GetTowns()
        {
            var townDTOs = await _bll.Towns.DTOAllAsync(User.UserGuidId());

            return Ok(townDTOs);
        }

        /// <summary>
        /// Find and return town from datasource
        /// </summary>
        /// <param name="id">town id - guid</param>
        /// <returns>Town object based on id</returns>
        /// <response code="200">The town was successfully retrieved.</response>
        /// <response code="404">The town does not exist.</response>
        // GET: api/Towns/5
        [ProducesResponseType( typeof( Town ), 200 )]	
        [ProducesResponseType( 404 )]
        [HttpGet("{id}")]
        public async Task<ActionResult<TownDTO>> GetTown(Guid id)
        {
            var town = await _bll.Towns.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            
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
        public async Task<ActionResult<Town>> PostTown(TownCreateDTO townCreateDTO)
        {
            var town = new Town()
            {
                Id = townCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                Name = townCreateDTO.Name
            };
            
            _bll.Towns.Add(town);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTown", new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
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
