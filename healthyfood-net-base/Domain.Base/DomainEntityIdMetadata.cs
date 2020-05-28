using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace ee.itcollege.magord.healthyfood.Domain.Base
{
    public abstract class DomainEntityIdMetadata : DomainEntityIdMetadata<Guid>, IDomainEntityId, IDomainEntityMetadata
    {
        
    }

    public abstract class DomainEntityIdMetadata<TKey> : DomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)]
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [MaxLength(256)]
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}