using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public char Sex { get; set; } = default!;

        public string DateOfBirth { get; set; } = default!;

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Payment>? Payments { get; set; }

        public ICollection<Bill>? Bills { get; set; }

        //public ICollection<AppUser>? AppUsers { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public ICollection<Contact>? Contacts { get; set; }
    }
    
}