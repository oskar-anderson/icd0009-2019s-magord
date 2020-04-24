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
    public class FoodService : BaseEntityService<IFoodRepository, IAppUnitOfWork, DAL.App.DTO.Food, BLL.App.DTO.Food>,
        IFoodService
    {
        public FoodService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Food, BLL.App.DTO.Food>(), unitOfWork.Foods)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Food>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Food> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<FoodDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<FoodDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}