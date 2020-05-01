﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
 using DAL.App.DTO;
 using DAL.Base.EF.Repositories;
 using DAL.Base.Mappers;
 using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Restaurant, DAL.App.DTO.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Restaurant, DAL.App.DTO.Restaurant>())
        {
        }
        

        public override async Task<IEnumerable<DAL.App.DTO.Restaurant>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Area);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Restaurant> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Area)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
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
