using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    public class DrinksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkMapper _mapper = new DrinkMapper();

        public DrinksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Drinks
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.Drink>>> GetDrinks()
        {
            return Ok((await _bll.Drinks.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.Drink>> GetDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id);
            
            if (drink == null)
            {
                return NotFound(new {message = "Drink not found"});
            }

            return Ok(_mapper.Map(drink));
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(Guid id, V1DTO.Drink drink)
        {
            drink.AppUserId = User.UserId();
            
            if (id != drink.Id)
            {
                return BadRequest();
            }

            await _bll.Drinks.UpdateAsync(_mapper.Map(drink), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Drinks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.Drink>> PostDrink(V1DTO.Drink drink)
        {
            drink.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(drink);
            _bll.Drinks.Add(bllEntity);
            await _bll.SaveChangesAsync();
            drink.Id = bllEntity.Id;
            
            return CreatedAtAction("GetDrink",
                new { id = drink.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.Drink>> DeleteDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id, User.UserId());
            if (drink == null)
            {
                return NotFound( new {message = "Drink not found"});
            }

            await _bll.Drinks.RemoveAsync(drink);
            await _bll.SaveChangesAsync();

            return Ok(drink);
        }
    }
}
