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
    }
}