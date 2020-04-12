using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> AllAsync(Guid? userId = null);
        Task<Person> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        Task<IEnumerable<PersonDTO>> DTOAllAsync(Guid? userId = null);
        Task<PersonDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}