using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IPersonInRestaurantRepository : IPersonInRestaurantRepository<Guid, PersonInRestaurant>, IBaseRepository<PersonInRestaurant>
    {
        
    }
    public interface IPersonInRestaurantRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync();
        Task<TDALEntity> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        // DTO methods
        //Task<IEnumerable<PersonInRestaurantDTO>> DTOAllAsync();
        //Task<PersonInRestaurantDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}