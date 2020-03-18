using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public DateTime TimeCreated { get; set; } = default!;

        [MaxLength(36)] public string FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        [MaxLength(36)] public string IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        [MaxLength(36)] public string DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        [MaxLength(36)] public string RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        [MaxLength(36)] public string OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }
        
        [MaxLength(36)] public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)] public string PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Bill>? Bills { get; set; }
        
    }
}