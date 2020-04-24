using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Drink : Drink<Guid>, IDomainEntityId
    {
    }
    
    public class Drink<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public float Size { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}