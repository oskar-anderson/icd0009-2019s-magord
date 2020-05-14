using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Drink : DomainEntityIdMetadata
    {
        public float Size { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;
        
        public ICollection<Order>? Orders { get; set; }

        
        
        // New
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
        
        /* OLD
        public ICollection<Price>? Prices { get; set; }
        */
    }
}