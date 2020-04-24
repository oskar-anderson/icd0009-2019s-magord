using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.Restaurant, DAL.App.DTO.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Restaurant, DAL.App.DTO.Restaurant>())
        {
        }
        
        public new async Task<IEnumerable<DAL.App.DTO.Restaurant>> AllAsync()
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        
        public async Task<DAL.App.DTO.Restaurant> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(r => r.Area)
                .Where(r => r.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await FirstOrDefaultAsync(id);
            base.Remove(restaurant);
        }
        
        /*
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
        */
    }
}