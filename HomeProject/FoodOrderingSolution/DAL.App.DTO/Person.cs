using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Person : Person<Guid>, IDomainEntityId
    {
    }
    
    public class Person<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public char Sex { get; set; } = default!;

        public string DateOfBirth { get; set; } = default!;

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Payment>? Payments { get; set; }

        public ICollection<Bill>? Bills { get; set; }

        //public ICollection<AppUser>? AppUsers { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public ICollection<Contact>? Contacts { get; set; }
    }
}