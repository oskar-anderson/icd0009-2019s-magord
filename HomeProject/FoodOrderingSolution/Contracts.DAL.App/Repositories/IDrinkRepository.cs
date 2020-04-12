using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.DrinkDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkRepository : IBaseRepository<Drink>
    {
        Task<IEnumerable<Drink>> AllAsync(Guid? userId = null);
        Task<Drink> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<DrinkDTO>> DTOAllAsync(Guid? userId = null);
        Task<DrinkDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}