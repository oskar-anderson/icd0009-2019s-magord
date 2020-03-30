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
        
        
        public async Task<Town> FirstOrDefaultAsync(Guid id)
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
            var town = await FirstOrDefaultAsync(id);
            base.Remove(town);
        }
        
        
        public async Task<IEnumerable<TownDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(t => new TownDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaCount = t.Areas!.Count
                })
                .ToListAsync();
        }

        public async Task<TownDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(t => t.Id == id).AsQueryable();

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