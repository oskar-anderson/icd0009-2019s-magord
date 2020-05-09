using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Contact : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public Guid ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }
    }
    
}