using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Campaign, DAL.App.DTO.Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Campaign, DAL.App.DTO.Campaign>())
        {
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