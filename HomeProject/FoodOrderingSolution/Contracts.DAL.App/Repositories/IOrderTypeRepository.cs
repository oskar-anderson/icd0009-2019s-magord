using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.OrderTypeDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderTypeRepository : IBaseRepository<OrderType>
    {
        Task<OrderType> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<OrderTypeDTO>> DTOAllAsync();
        Task<OrderTypeDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}