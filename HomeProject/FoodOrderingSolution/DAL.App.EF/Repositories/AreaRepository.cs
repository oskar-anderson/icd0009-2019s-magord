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

namespace DAL.App.EF.Repositories
{
    public class AreaRepository : EFBaseRepository<Area, AppDbContext>, IAreaRepository
    {
        public AreaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<IEnumerable<Area>>  AllAsync()
        {
            var query = RepoDbSet
                .Include(a => a.Town)
                .AsQueryable();

            return await query.ToListAsync();
        }


        public async Task<Area> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(a => a.Town)
                .Where(a => a.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }
        

        public async Task DeleteAsync(Guid id)
        {
            var area = await FirstOrDefaultAsync(id);
            base.Remove(area);
        }
        
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