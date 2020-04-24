using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RestaurantsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
        {
            var restaurantDTOs = await _uow.Restaurants.DTOAllAsync();
            
            return Ok(restaurantDTOs);
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(Guid id)
        {
            var restaurant = await _uow.Restaurants.DTOFirstOrDefaultAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, RestaurantEditDTO restaurantEditDTO)
        {
            if (id != restaurantEditDTO.Id)
            {
                return BadRequest();
            }

            var restaurant = await _uow.Restaurants.FirstOrDefaultAsync(restaurantEditDTO.Id);
            if (restaurant == null)
            {
                return BadRequest();
            }

            restaurant.Name = restaurantEditDTO.Name;
            restaurant.Address = restaurantEditDTO.Address;
            restaurant.OpenedFrom = restaurantEditDTO.OpenedFrom;
            restaurant.ClosedFrom = restaurantEditDTO.ClosedFrom;
            
            _uow.Restaurants.Update(restaurant);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Restaurants.ExistsAsync(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantCreateDTO restaurantCreateDTO)
        {
            var restaurant = new Restaurant
            {
                Id = restaurantCreateDTO.Id,
                Name = restaurantCreateDTO.Name,
                Address = restaurantCreateDTO.Address,
                OpenedFrom = restaurantCreateDTO.OpenedFrom,
                ClosedFrom = restaurantCreateDTO.ClosedFrom
            };
            
            _uow.Restaurants.Add(restaurant);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _uow.Restaurants.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _uow.Restaurants.Remove(restaurant);
            await _uow.SaveChangesAsync();

            return Ok(restaurant);
        }
    }
}
