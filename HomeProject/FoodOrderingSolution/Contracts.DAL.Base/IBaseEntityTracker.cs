using System;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }
    
    public interface IBaseEntityTracker<TKey>
        where TKey: IEquatable<TKey>
    {
        //Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> EntityTracker { get; }

        void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity);
    }
}