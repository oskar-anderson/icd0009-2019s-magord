using System;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Repositories;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
        
    }

    public interface IBaseEntityService<in TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        
    }
}