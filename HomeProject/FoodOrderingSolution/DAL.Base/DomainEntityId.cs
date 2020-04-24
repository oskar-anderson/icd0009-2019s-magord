using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntityId : DomainBaseEntity<Guid>, IDomainEntityId
    {
        
    }
    
    public abstract class DomainBaseEntity<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }

}