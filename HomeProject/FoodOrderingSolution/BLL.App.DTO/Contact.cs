using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Contact : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public Guid ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }
        
        public Guid AppUserId { get; set; } 
        public AppUser? AppUser { get; set; }
    }
}