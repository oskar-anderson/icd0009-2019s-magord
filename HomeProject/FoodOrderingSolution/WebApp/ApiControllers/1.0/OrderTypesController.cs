using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using OrderType = PublicApi.DTO.v1.OrderType;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class OrderTypesController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        public OrderTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes()
        {
            var orderTypes = (await _bll.OrderTypes.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new OrderType()
                {
                    Id = bllEntity.Id,
                    Name = bllEntity.Name,
                    Comment = bllEntity.Comment,
                });
            
            return Ok(orderTypes);
        }

        // GET: api/OrderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderType>> GetOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id, User.UserGuidId());

            if (orderType == null)
            {
                return NotFound();
            }

            return Ok(orderType);
        }

        // PUT: api/OrderTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderType(Guid id, OrderTypeEdit orderTypeEditDTO)
        {
            if (id != orderTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(orderTypeEditDTO.Id, User.UserGuidId());
            if (orderType == null)
            {
                return BadRequest();
            }
            
            orderType.Name = orderTypeEditDTO.Name;
            orderType.Comment = orderTypeEditDTO.Comment;
            
            _bll.OrderTypes.Update(orderType);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.OrderTypes.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/OrderTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderType>> PostOrderType(OrderTypeCreate orderTypeCreateDTO)
        {
            var orderType = new BLL.App.DTO.OrderType
            {
                AppUserId = User.UserGuidId(),
                Name = orderTypeCreateDTO.Name,
                Comment = orderTypeCreateDTO.Comment
            };
            
            _bll.OrderTypes.Add(orderType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrderType", new { id = orderType.Id }, orderType);
        }

        // DELETE: api/OrderTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderType>> DeleteOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (orderType == null)
            {
                return NotFound();
            }

            _bll.OrderTypes.Remove(orderType);
            await _bll.SaveChangesAsync();

            return Ok(orderType);
        }
    }
}
