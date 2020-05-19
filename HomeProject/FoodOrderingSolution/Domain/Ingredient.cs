using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Ingredient : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        
        // New
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
        
        /* OLD
        public ICollection<Price>? Prices { get; set; }
        */
    }
    
}