﻿using System;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OrderItem : DomainEntityIdMetadataUser<AppUser>
    {
        public int Quantity { get; set; } = default!;
        
        public Guid? FoodId { get; set; }
        public Food? Food { get; set; }

        public Guid? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public Guid? DrinkId { get; set; }
        public Drink? Drink { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
    }
}