using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class  Drink : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public float Size { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
    }
}