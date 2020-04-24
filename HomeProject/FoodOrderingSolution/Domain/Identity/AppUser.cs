﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : AppUser<Guid>
    {
        
    }

    
    public class AppUser<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        //public Guid? PersonId { get; set; }
        //public Person? Person { get; set; }
        
        public ICollection<Person>? Persons { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Bill>? Bills { get; set; }
    }
}