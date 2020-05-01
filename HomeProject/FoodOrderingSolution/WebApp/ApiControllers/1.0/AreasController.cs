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
    public class AreasController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AreaMapper _mapper = new AreaMapper();

        public AreasController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.Area>>> GetAreas()
        {
            return Ok((await _bll.Areas.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.Area>> GetArea(Guid id)
        {
            var area = await _bll.Areas.FirstOrDefaultAsync(id);
            
            if (area == null)
            {
                return NotFound(new {message = "Area not found"});
            }

            return Ok(_mapper.Map(area));
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutArea(Guid id, V1DTO.Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            await _bll.Areas.UpdateAsync(_mapper.Map(area));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Areas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.Area>> PostArea(V1DTO.Area area)
        {
            var bllEntity = _mapper.Map(area);
            _bll.Areas.Add(bllEntity);
            await _bll.SaveChangesAsync();
            area.Id = bllEntity.Id;
            
            return CreatedAtAction("GetArea",
                new { id = area.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.Area>> DeleteArea(Guid id)
        {
            var area = await _bll.Areas.FirstOrDefaultAsync(id);
            if (area == null)
            {
                return NotFound(new {message = "Area not found"});
            }

            await _bll.Areas.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(area);
        }
    }
}
