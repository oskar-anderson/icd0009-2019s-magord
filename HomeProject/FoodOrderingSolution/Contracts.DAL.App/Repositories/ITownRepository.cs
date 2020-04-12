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
        Task<IEnumerable<Town>> AllAsync(Guid? userId = null);
        Task<Town> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<TownDTO>> DTOAllAsync(Guid? userId = null);
        Task<TownDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);

    }
}