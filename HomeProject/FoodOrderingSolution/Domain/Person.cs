using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Person : DomainEntityMetadata
    {
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;
        
        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        public char Sex { get; set; } = default!;

        public DateTime DateOfBirth { get; set; } = default!;

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Payment>? Payments { get; set; }

        public ICollection<Bill>? Bills { get; set; }

        public ICollection<AppUser>? AppUsers { get; set; }

        public ICollection<Contact>? Contacts { get; set; }
    }
}