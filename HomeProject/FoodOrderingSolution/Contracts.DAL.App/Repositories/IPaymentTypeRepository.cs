using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentTypeRepository : IBaseRepository<PaymentType>
    {
        Task<PaymentType> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<PaymentTypeDTO>> DTOAllAsync();
        Task<PaymentTypeDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}