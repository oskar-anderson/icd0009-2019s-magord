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
        Task<IEnumerable<OrderType>> AllAsync(Guid? userId = null);
        Task<OrderType> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<OrderTypeDTO>> DTOAllAsync(Guid? userId = null);
        Task<OrderTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}