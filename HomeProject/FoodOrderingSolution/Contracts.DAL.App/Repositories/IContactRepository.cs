using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<Contact> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<ContactDTO>> DTOAllAsync();
        Task<ContactDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}