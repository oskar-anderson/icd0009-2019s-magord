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

    public class OrderTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderTypeMapper _mapper = new OrderTypeMapper();
        
        public OrderTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderType>>> GetOrderTypes()
        {
            return Ok((await _bll.OrderTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/OrderTypes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.OrderType>> GetOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id);

            if (orderType == null)
            {
                return NotFound(new {message = "OrderType not found"});
            }

            return Ok(_mapper.Map(orderType));
        }

        // PUT: api/OrderTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderType(Guid id, V1DTO.OrderType orderType)
        {
            orderType.AppUserId = User.UserId();
            
            if (id != orderType.Id)
            {
                return BadRequest();
            }

            await _bll.OrderTypes.UpdateAsync(_mapper.Map(orderType), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<V1DTO.OrderType>> PostOrderType(V1DTO.OrderType orderType)
        {
            orderType.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(orderType);
            _bll.OrderTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            orderType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetOrderType",
                new { id = orderType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                orderType);
        }

        // DELETE: api/OrderTypes/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.OrderType>> DeleteOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id, User.UserId());
            if (orderType == null)
            {
                return NotFound(new {message = "OrderType not found"});
            }

            await _bll.OrderTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(orderType);
        }
    }
}
