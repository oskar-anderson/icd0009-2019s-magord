using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Contact : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public Guid ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }
    }
    
}