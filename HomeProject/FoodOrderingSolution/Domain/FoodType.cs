using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class FoodType : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Food>? Foods { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}