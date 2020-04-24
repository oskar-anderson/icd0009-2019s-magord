using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Ingredient : Ingredient<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Ingredient<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public TKey FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}