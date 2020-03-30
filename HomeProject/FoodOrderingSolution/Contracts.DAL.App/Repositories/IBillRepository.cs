using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        Task<IEnumerable<Bill>> AllAsync(Guid? userId = null);
        Task<Bill> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<BillDTO>> DTOAllAsync(Guid? userId = null);
        Task<BillDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);


    }
}