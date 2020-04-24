using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Contact : Contact<Guid>, IDomainEntityId
    {
    }
    
    public class Contact<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public TKey ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }
    }
}