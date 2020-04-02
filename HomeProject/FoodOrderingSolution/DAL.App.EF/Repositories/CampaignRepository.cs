using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.CampaignDTOs;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<Campaign, AppDbContext>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<Campaign> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await FirstOrDefaultAsync(id);
            base.Remove(area);
        }
        
        
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
    }
}