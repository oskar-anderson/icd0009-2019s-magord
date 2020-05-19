using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Food : DomainEntityIdMetadata
    {
        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public string Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        
        // New
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
        
        /* OLD
        public ICollection<Price>? Prices { get; set; }
        */
    }
}