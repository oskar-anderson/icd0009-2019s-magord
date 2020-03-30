using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        Task<Restaurant> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<RestaurantDTO>> DTOAllAsync();
        Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}