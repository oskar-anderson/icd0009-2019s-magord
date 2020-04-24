using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Order : Order<Guid>, IDomainEntityId
    {
    }
    
    public class Order<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public string TimeCreated { get; set; } = default!;

        public TKey FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public TKey IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        public TKey DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public TKey OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Bill>? Bills { get; set; }
    }
}