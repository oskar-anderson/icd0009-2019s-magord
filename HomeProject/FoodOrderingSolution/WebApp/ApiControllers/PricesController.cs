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
using PublicApi.DTO.v1.PriceDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PricesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceDTO>>> GetPrices()
        {
            var priceDTOs = await _uow.Prices.DTOAllAsync();
            
            return Ok(priceDTOs);
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceDTO>> GetPrice(Guid id)
        {
            var price = await _uow.Prices.DTOFirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return Ok(price);
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(Guid id, PriceEditDTO priceEditDTO)
        {
            if (id != priceEditDTO.Id)
            {
                return BadRequest();
            }

            var price = await _uow.Prices.FirstOrDefaultAsync(priceEditDTO.Id);
            if (price == null)
            {
                return BadRequest();
            }

            price.From = priceEditDTO.From;
            price.To = priceEditDTO.To;
            price.Value = priceEditDTO.Value;
            
            _uow.Prices.Update(price);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _uow.Prices.ExistsAsync(id))
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

        // POST: api/Prices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(PriceCreateDTO priceCreateDTO)
        {
            var price = new Price
            {
                Id = priceCreateDTO.Id,
                From = priceCreateDTO.From,
                To = priceCreateDTO.To,
                Value = priceCreateDTO.Value,
            };
            
            _uow.Prices.Add(price);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Price>> DeletePrice(Guid id)
        {
            var price = await _uow.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _uow.Prices.Remove(price);
            await _uow.SaveChangesAsync();

            return Ok(price);
        }
    }
}
