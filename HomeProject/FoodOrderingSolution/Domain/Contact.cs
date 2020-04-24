using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Contact : Contact<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Contact<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public TKey ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }


    }
}