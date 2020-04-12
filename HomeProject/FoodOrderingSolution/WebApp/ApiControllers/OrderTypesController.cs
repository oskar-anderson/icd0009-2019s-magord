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
using PublicApi.DTO.v1.OrderTypeDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public OrderTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/OrderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTypeDTO>>> GetOrderTypes()
        {
            var orderTypeDTOs = await _uow.OrderTypes.DTOAllAsync(User.UserGuidId());
            
            return Ok(orderTypeDTOs);
        }

        // GET: api/OrderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTypeDTO>> GetOrderType(Guid id)
        {
            var orderType = await _uow.OrderTypes.DTOFirstOrDefaultAsync(id, User.UserGuidId());

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
        public async Task<IActionResult> PutOrderType(Guid id, OrderTypeEditDTO orderTypeEditDTO)
        {
            if (id != orderTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var orderType = await _uow.OrderTypes.FirstOrDefaultAsync(orderTypeEditDTO.Id, User.UserGuidId());
            if (orderType == null)
            {
                return BadRequest();
            }
            
            orderType.Name = orderTypeEditDTO.Name;
            orderType.Comment = orderTypeEditDTO.Comment;
            
            _uow.OrderTypes.Update(orderType);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.OrderTypes.ExistsAsync(id, User.UserGuidId()))
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
        public async Task<ActionResult<OrderType>> PostOrderType(OrderTypeCreateDTO orderTypeCreateDTO)
        {
            var orderType = new OrderType
            {
                Id = orderTypeCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                Name = orderTypeCreateDTO.Name,
                Comment = orderTypeCreateDTO.Comment
            };
            
            _uow.OrderTypes.Add(orderType);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOrderType", new { id = orderType.Id }, orderType);
        }

        // DELETE: api/OrderTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderType>> DeleteOrderType(Guid id)
        {
            var orderType = await _uow.OrderTypes.FirstOrDefaultAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }

            _uow.OrderTypes.Remove(orderType);
            await _uow.SaveChangesAsync();

            return Ok(orderType);
        }
    }
}
