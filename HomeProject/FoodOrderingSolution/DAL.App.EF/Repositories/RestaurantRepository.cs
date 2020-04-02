using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.AreaDTOs;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace DAL.App.EF.Repositories
{
    public class RestaurantRepository : EFBaseRepository<Restaurant, AppDbContext>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public new async Task<IEnumerable<Restaurant>> AllAsync()
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .AsQueryable();
            
            return await query.ToListAsync();
        }

        
        public async Task<Restaurant> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .Where(r => r.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await FirstOrDefaultAsync(id);
            base.Remove(restaurant);
        }
        
        public async Task<IEnumerable<RestaurantDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .AsQueryable();
            
            return await query
                .Select(r => new RestaurantDTO()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    AreaId = r.AreaId,
                    OpenedFrom = r.OpenedFrom,
                    ClosedFrom = r.ClosedFrom,
                    Area = new AreaDTO()
                    {
                        Id = r.Area!.Id,
                        Name = r.Area.Name,
                        RestaurantCount = r.Area.Restaurants!.Count,
                        TownId = r.Area.TownId
                    }
                    
                })
                .ToListAsync();
        }

        public async Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .Where(r => r.Id == id).AsQueryable();

            var restaurantDTO = await query.Select(r => new RestaurantDTO()
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                AreaId = r.AreaId,
                OpenedFrom = r.OpenedFrom,
                ClosedFrom = r.ClosedFrom,
                Area = new AreaDTO()
                {
                    Id = r.Area!.Id,
                    Name = r.Area.Name,
                    RestaurantCount = r.Area.Restaurants!.Count,
                    TownId = r.Area.TownId
                }
            }).FirstOrDefaultAsync();

            return restaurantDTO;
        }
    }
}