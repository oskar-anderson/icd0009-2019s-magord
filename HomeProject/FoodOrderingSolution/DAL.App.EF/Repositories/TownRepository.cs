using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<Town, AppDbContext>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Town>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }

        
        public async Task<Town> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(t => t.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
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
            var town = await FirstOrDefaultAsync(id, userId);
            base.Remove(town);
        }
        
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
    }
}