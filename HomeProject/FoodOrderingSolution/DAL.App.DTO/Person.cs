using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public char Sex { get; set; } = default!;

        public string DateOfBirth { get; set; } = default!;

        //public ICollection<AppUser>? AppUsers { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
    }
    
}