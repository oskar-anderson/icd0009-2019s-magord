using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace DAL.App.EF.Repositories
{
    public class RestaurantRepository : EFBaseRepository<Restaurant, AppDbContext>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Restaurant> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await FirstOrDefaultAsync(id);
            base.Remove(restaurant);
        }
        
        
        public async Task<IEnumerable<RestaurantDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new RestaurantDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    AreaId = c.AreaId,
                    OpenedFrom = c.OpenedFrom,
                    ClosedFrom = c.ClosedFrom
                })
                .ToListAsync();
        }

        public async Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var restaurantDTO = await query.Select(c => new RestaurantDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                AreaId = c.AreaId,
                OpenedFrom = c.OpenedFrom,
                ClosedFrom = c.ClosedFrom
            }).FirstOrDefaultAsync();

            return restaurantDTO;
        }
    }
}