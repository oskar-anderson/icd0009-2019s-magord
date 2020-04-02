using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.AreaDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IAreaRepository : IBaseRepository<Area>
    {
        Task<Area> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        Task<IEnumerable<AreaDTO>> DTOAllAsync();
        Task<AreaDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}