using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.IngredientDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        Task<Ingredient> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<IngredientDTO>> DTOAllAsync();
        Task<IngredientDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}