using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Food : Food<Guid>, IDomainEntityId
    {
    }
    
    public class Food<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string? Description { get; set; }

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public float Size { get; set; } = default!;
        
        public TKey FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
}