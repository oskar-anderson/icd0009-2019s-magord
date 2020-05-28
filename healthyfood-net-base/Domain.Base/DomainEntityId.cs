using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace ee.itcollege.magord.healthyfood.Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
        
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}