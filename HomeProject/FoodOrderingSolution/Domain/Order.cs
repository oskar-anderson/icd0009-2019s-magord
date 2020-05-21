using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public string TimeCreated { get; set; } = default!;

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public Guid OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }
        
        public ICollection<Bill>? Bills { get; set; }
        
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}