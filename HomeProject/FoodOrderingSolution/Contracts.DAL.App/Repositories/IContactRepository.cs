using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IContactRepository : IContactRepository<Guid, Contact>, IBaseRepository<Contact>
    {
        
    }
    public interface IContactRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync();
        Task<TDALEntity> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        // DTO methods
        //Task<IEnumerable<ContactDTO>> DTOAllAsync();
        //Task<ContactDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}