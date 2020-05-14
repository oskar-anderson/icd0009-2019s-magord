﻿using System;
using System.Collections.Generic;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Price : DomainEntityIdMetadataUser<AppUser>
    {
        public decimal Value { get; set; } = default!;
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        
        

        // NEW
        public ICollection<Ingredient>? Ingredients { get; set; }
        public ICollection<Food>? Foods { get; set; }
        public ICollection<Drink>? Drinks { get; set; }
        
        
        /* OLD
        public Guid IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        */
    }
    
}