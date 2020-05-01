using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class AreaRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Area, DAL.App.DTO.Area>, IAreaRepository
    {
        public AreaRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.Area, DAL.App.DTO.Area>())
        {
        }

        public override async Task<IEnumerable<Area>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Town);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Area> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Town)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        /*
        public async Task<IEnumerable<AreaDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(a => a.Town)
                .AsQueryable();
            
            return await query
                .Select(a => new AreaDTO()
                {
                    Id = a.Id,
                    Name = a.Name,
                    TownId = a.TownId,
                    RestaurantCount = a.Restaurants!.Count,
                    Town = new TownDTO()
                    {
                        Id = a.Town!.Id,
                        AreaCount = a.Town.Areas!.Count,
                        Name = a.Town.Name
                    }
                })
                .ToListAsync();
        }

        public async Task<AreaDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();

            var areaDTO = await query.Select(a => new AreaDTO()
            {
                Id = a.Id,
                Name = a.Name,
                TownId = a.TownId,
                RestaurantCount = a.Restaurants!.Count,
                Town = new TownDTO()
                {
                    Id = a.Town!.Id,
                    AreaCount = a.Town.Areas!.Count,
                    Name = a.Town.Name
                }
            }).FirstOrDefaultAsync();

            return areaDTO;
        }



        /*public override async Task<IEnumerable<Area>> AllAsync()
        {
            return await RepoDbSet.Where(a => a.Name.StartsWith("a")).ToListAsync();
        }
        */
    }
}