using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        [MaxLength(128)]
        [MinLength(1)]
        public string FirstName { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string LastName { get; set; } = default!;
        
        public ICollection<Order>? Orders { get; set; }
        
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}