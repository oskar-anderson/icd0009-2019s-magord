using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ITownRepository : IBaseRepository<Town>
    {
        Task<Town> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        Task<IEnumerable<TownDTO>> DTOAllAsync();
        Task<TownDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}