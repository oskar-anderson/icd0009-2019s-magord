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
    public class FoodTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly FoodTypeMapper _mapper = new FoodTypeMapper();

        public FoodTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.FoodType>>> GetFoodTypes()
        {
            return Ok((await _bll.FoodTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/FoodTypes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.FoodType>> GetFoodType(Guid id)
        {
            var foodType = await _bll.FoodTypes.FirstOrDefaultAsync(id);
            
            if (foodType == null)
            {
                return NotFound(new {message = "FoodType not found"});
            }

            return Ok(_mapper.Map(foodType));
        }

        // PUT: api/FoodTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodType(Guid id, V1DTO.FoodType foodType)
        {
            foodType.AppUserId = User.UserId();
            
            if (id != foodType.Id)
            {
                return BadRequest();
            }

            await _bll.FoodTypes.UpdateAsync(_mapper.Map(foodType), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<V1DTO.FoodType>> PostFoodType(V1DTO.FoodType foodType)
        {
            foodType.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(foodType);
            _bll.FoodTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            foodType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetFoodType",
                new { id = foodType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                foodType);
        }

        // DELETE: api/FoodTypes/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.FoodType>> DeleteFoodType(Guid id)
        {
            var foodType = await _bll.FoodTypes.FirstOrDefaultAsync(id, User.UserId());
            if (foodType == null)
            {
                return NotFound(new {message = "FoodType not found"});
            }

            await _bll.FoodTypes.RemoveAsync(foodType);
            await _bll.SaveChangesAsync();

            return Ok(foodType);
        }
    }
}
