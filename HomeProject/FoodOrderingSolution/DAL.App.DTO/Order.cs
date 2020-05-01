using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public string TimeCreated { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public Guid IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public Guid OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Bill>? Bills { get; set; }
    }
}