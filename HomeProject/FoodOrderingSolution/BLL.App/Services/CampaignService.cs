using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CampaignService : BaseEntityService<ICampaignRepository, IAppUnitOfWork, DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>,
        ICampaignService
    {
        public CampaignService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>(), unitOfWork.Campaigns)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Campaign>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Campaign> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<CampaignDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}