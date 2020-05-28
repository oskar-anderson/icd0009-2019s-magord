using System;

namespace ee.itcollege.magord.healthyfood.Contracts.Domain.Base
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