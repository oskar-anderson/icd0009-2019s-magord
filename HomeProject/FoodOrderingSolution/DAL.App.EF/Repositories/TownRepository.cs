using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Town, DAL.App.DTO.Town>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Town, DAL.App.DTO.Town>())
        {
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
