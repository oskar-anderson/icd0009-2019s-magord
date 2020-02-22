﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(36)] public override string Id { get; set; } = default!;

        [MaxLength(36)]
        public string? PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Bill>? Bills { get; set; }
    }
}