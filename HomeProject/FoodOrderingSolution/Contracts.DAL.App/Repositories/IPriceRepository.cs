using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.PriceDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IPriceRepository : IBaseRepository<Price>
    {
        Task<Price> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<PriceDTO>> DTOAllAsync();
        Task<PriceDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}