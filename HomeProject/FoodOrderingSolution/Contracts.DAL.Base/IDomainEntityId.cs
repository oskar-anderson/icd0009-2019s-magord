using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
        
    }
    
    public interface IDomainEntityId<TKey>
        where TKey: IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
    
}