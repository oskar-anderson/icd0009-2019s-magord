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
using PublicApi.DTO.v1.DrinkDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DrinksController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public DrinksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> GetDrinks()
        {
            var drinkDTOs = await _uow.Drinks.DTOAllAsync(User.UserGuidId());
            
            return Ok(drinkDTOs);
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkDTO>> GetDrink(Guid id)
        {
            var drink = await _uow.Drinks.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(Guid id, DrinkEditDTO drinkEditDTO)
        {
            if (id != drinkEditDTO.Id)
            {
                return BadRequest();
            }

            var drink = await _uow.Drinks.FirstOrDefaultAsync(drinkEditDTO.Id, User.UserGuidId());
            if (drink == null)
            {
                return BadRequest();
            }

            drink.Size = drinkEditDTO.Size;
            drink.Name = drinkEditDTO.Name;
            drink.Amount = drinkEditDTO.Amount;

            _uow.Drinks.Update(drink);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Drinks.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Drinks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(DrinkCreateDTO drinkCreateDTO)
        {
            var drink = new Drink
            {
                Id = drinkCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                Size = drinkCreateDTO.Size,
                Amount = drinkCreateDTO.Amount,
                Name = drinkCreateDTO.Name,
            };
            
            _uow.Drinks.Add(drink);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drink>> DeleteDrink(Guid id)
        {
            var drink = await _uow.Drinks.FirstOrDefaultAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _uow.Drinks.Remove(drink);
            await _uow.SaveChangesAsync();

            return Ok(drink);
        }
    }
}
