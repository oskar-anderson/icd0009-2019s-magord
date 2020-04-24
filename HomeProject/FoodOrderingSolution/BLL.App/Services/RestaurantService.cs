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
    public class RestaurantService : BaseEntityService<IRestaurantRepository, IAppUnitOfWork, DAL.App.DTO.Restaurant, BLL.App.DTO.Restaurant>,
        IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Restaurant, BLL.App.DTO.Restaurant>(), unitOfWork.Restaurants)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Restaurant>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Restaurant> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<RestaurantDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}