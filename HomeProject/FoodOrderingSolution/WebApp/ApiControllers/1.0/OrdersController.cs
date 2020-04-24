using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.OrderDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orderDTOs = await _uow.Orders.DTOAllAsync(User.UserGuidId());

            return Ok(orderDTOs);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var order = await _uow.Orders.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, OrderEditDTO orderEditDTO)
        {
            if (id != orderEditDTO.Id)
            {
                return BadRequest();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(orderEditDTO.Id, User.UserGuidId());
            if (order == null)
            {
                return BadRequest();
            }

            order.OrderStatus = orderEditDTO.OrderStatus;
            order.Number = orderEditDTO.Number;

            _uow.Orders.Update(order);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Orders.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderCreateDTO orderCreateDTO)
        {
            var order = new Order
            {
                Id = orderCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                OrderStatus = orderCreateDTO.OrderStatus,
                Number = orderCreateDTO.Number,
                TimeCreated = orderCreateDTO.TimeCreated
            };

            _uow.Orders.Add(order);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _uow.Orders.FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            _uow.Orders.Remove(order);
            await _uow.SaveChangesAsync();

            return Ok(order);
        }
    }
}
