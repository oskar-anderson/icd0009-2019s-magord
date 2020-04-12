using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.FoodTypeDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodTypeRepository : IBaseRepository<FoodType>
    {
        Task<IEnumerable<FoodType>> AllAsync(Guid? userId = null);
        Task<FoodType> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<FoodTypeDTO>> DTOAllAsync(Guid? userId = null);
        Task<FoodTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}