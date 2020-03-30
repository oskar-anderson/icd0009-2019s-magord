using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.FoodDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        Task<Food> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<FoodDTO>> DTOAllAsync();
        Task<FoodDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}