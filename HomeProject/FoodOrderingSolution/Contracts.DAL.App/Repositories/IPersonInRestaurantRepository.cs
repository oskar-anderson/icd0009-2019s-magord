using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.PersonInRestaurantDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonInRestaurantRepository : IBaseRepository<PersonInRestaurant>
    {
        Task<PersonInRestaurant> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<PersonInRestaurantDTO>> DTOAllAsync();
        Task<PersonInRestaurantDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}