using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.FoodTypeDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodTypeRepository : IBaseRepository<FoodType>
    {
        Task<FoodType> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<FoodTypeDTO>> DTOAllAsync();
        Task<FoodTypeDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}