using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.AreaDTOs;

namespace WebApp.ApiControllers._1._0
{
    
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AreasController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;

        public AreasController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> GetAreas()
        {
            var areaDTOs = await _bll.Areas.DTOAllAsync();
            
            return Ok(areaDTOs);

        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTO>> GetArea(Guid id)
        {
            var area = await _bll.Areas.DTOFirstOrDefaultAsync(id);
            
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

            var area = await _bll.Areas.FirstOrDefaultAsync(areaEditDTO.Id);
            if (area == null)
            {
                return BadRequest();
            }

            area.Name = areaEditDTO.Name;
            _bll.Areas.Update(area);
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Areas.ExistsAsync(id))
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
                TownId = areaCreateDTO.TownId
            };
            
            _bll.Areas.Add(area);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> DeleteArea(Guid id)
        {
            var area = await _bll.Areas.FirstOrDefaultAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }

            _bll.Areas.Remove(area);
            await _bll.SaveChangesAsync();

            return Ok(area);
        }
    }
}
