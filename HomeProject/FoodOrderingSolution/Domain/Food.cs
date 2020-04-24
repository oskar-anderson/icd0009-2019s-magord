using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Food : Food<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Food<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public float Size { get; set; } = default!;
        
        public TKey FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Price>? Prices { get; set; }

    }
}