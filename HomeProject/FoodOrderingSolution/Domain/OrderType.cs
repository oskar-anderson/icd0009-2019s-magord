using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OrderType : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(1024)] public string? Comment { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}