using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<AppDbContext, Domain.Town, DAL.App.DTO.Town>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Town, DAL.App.DTO.Town>())
        {
        }
        
        public async Task<IEnumerable<DAL.App.DTO.Town>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        
        public async Task<DAL.App.DTO.Town> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(t => t.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var town = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(town.Id);

        }
        
        /*
        public async Task<IEnumerable<TownDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query
                .Select(t => new TownDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaCount = t.Areas!.Count
                })
                .ToListAsync();
        }

        public async Task<TownDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(t => t.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var townDTO = await query.Select(t => new TownDTO()
            {
                Id = t.Id,
                Name = t.Name,
                AreaCount = t.Areas!.Count
            }).FirstOrDefaultAsync();

            return townDTO;
        }
        */
    }
}