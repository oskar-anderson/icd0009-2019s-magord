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
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public AreasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> GetAreas()
        {
            var areaDTOs = await _uow.Areas.DTOAllAsync();
            
            return Ok(areaDTOs);

        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTO>> GetArea(Guid id)
        {
            var area = await _uow.Areas.DTOFirstOrDefaultAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }

            return Ok(area);
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(Guid id, AreaEditDTO areaEditDTO)
        {
            if (id != areaEditDTO.Id)
            {
                return BadRequest();
            }

            var area = await _uow.Areas.FirstOrDefaultAsync(areaEditDTO.Id);
            if (area == null)
            {
                return BadRequest();
            }

            area.Name = areaEditDTO.Name;
            _uow.Areas.Update(area);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Areas.ExistsAsync(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(AreaCreateDTO areaCreateDTO)
        {
            var area = new Area
            {
                Id = areaCreateDTO.Id,
                Name = areaCreateDTO.Name,
            };
            
            _uow.Areas.Add(area);
            await _uow.SaveChangesAsync();
            
            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> DeleteArea(Guid id)
        {
            var area = await _uow.Areas.FirstOrDefaultAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }

            _uow.Areas.Remove(area);
            await _uow.SaveChangesAsync();

            return Ok(area);
        }
    }
}
