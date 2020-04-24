using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityId : IDomainBaseEntity<Guid>
    {
    }
    
    public interface IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}