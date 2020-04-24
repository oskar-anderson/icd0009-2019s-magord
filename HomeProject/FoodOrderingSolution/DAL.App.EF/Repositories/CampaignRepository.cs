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
    public class CampaignRepository : EFBaseRepository<AppDbContext, Domain.Campaign, DAL.App.DTO.Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Campaign, DAL.App.DTO.Campaign>())
        {
        }
        
        public new async Task<IEnumerable<DAL.App.DTO.Campaign>> AllAsync()
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public async Task<DAL.App.DTO.Campaign> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await FirstOrDefaultAsync(id);
            base.Remove(area);
        }
        
        /*
        public async Task<IEnumerable<CampaignDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new CampaignDTO()
                {
                    Id = c.Id,
                    From = c.From,
                    To = c.To,
                    Name = c.Name,
                    Comment = c.Comment
                })
                .ToListAsync();
        }

        public async Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var campaignDTO = await query.Select(c => new CampaignDTO()
            {
                Id = c.Id,
                From = c.From,
                To = c.To,
                Name = c.Name,
                Comment = c.Comment
            }).FirstOrDefaultAsync();

            return campaignDTO;
        }
        */
    }
}