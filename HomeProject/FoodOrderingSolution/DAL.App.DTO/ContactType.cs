using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class ContactType : ContactType<Guid>, IDomainEntityId
    {
    }
    
    public class ContactType<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }
    }
}