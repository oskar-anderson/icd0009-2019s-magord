using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes()
        {
            return await _context.OrderTypes.ToListAsync();
        }

        // GET: api/OrderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderType>> GetOrderType(Guid id)
        {
            var orderType = await _context.OrderTypes.FindAsync(id);

            if (orderType == null)
            {
                return NotFound();
            }

            return orderType;
        }

        // PUT: api/OrderTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderType(Guid id, OrderType orderType)
        {
            if (id != orderType.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(id))
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
        public async Task<ActionResult<OrderType>> PostOrderType(OrderType orderType)
        {
            _context.OrderTypes.Add(orderType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderType", new { id = orderType.Id }, orderType);
        }

        // DELETE: api/OrderTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderType>> DeleteOrderType(Guid id)
        {
            var orderType = await _context.OrderTypes.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }

            _context.OrderTypes.Remove(orderType);
            await _context.SaveChangesAsync();

            return orderType;
        }

        private bool OrderTypeExists(Guid id)
        {
            return _context.OrderTypes.Any(e => e.Id == id);
        }
    }
}
