using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.CampaignDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        Task<Campaign> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<CampaignDTO>> DTOAllAsync();
        Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}