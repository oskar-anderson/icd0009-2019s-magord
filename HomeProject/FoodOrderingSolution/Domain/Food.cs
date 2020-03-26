using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Food : DomainEntity
    {
        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public int Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Price>? Prices { get; set; }

    }
}