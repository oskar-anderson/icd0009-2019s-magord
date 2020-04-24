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
    public class PersonInRestaurantService : BaseEntityService<IPersonInRestaurantRepository, IAppUnitOfWork, DAL.App.DTO.PersonInRestaurant, BLL.App.DTO.PersonInRestaurant>,
        IPersonInRestaurantService
    {
        public PersonInRestaurantService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PersonInRestaurant, BLL.App.DTO.PersonInRestaurant>(), unitOfWork.PersonsInRestaurants)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.PersonInRestaurant>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.PersonInRestaurant> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<PersonInRestaurantDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<PersonInRestaurantDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}