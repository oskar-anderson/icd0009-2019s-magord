using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Order : OrderEdit
    {
        public ICollection<Food> Foods { get; set; } = default!;
        public ICollection<Ingredient> Ingredients { get; set; } = default!;
        public ICollection<Drink> Drinks { get; set; } = default!;
        public ICollection<Restaurant> Restaurants { get; set; } = default!;
        public ICollection<OrderType> OrderTypes { get; set; } = default!;
        public ICollection<Person> Persons { get; set; } = default!;
        
    }

    // for display only
    public class OrderDetail : OrderEdit
    {
        public Food Food { get; set; } = default!;
        public Ingredient Ingredient { get; set; } = default!;
        public Drink Drink { get; set; } = default!;
        public Restaurant Restaurant { get; set; } = default!;
        public OrderType OrderType { get; set; } = default!;
        public Person Person { get; set; } = default!;
    }

    // from client to server
    public class OrderEdit: OrderCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class OrderCreate
    {
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;
        public int Number { get; set; } = default!;
        public string TimeCreated { get; set; } = default!;
        public Guid FoodId { get; set; }
        public Guid IngredientId { get; set; }
        public Guid DrinkId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid OrderTypeId { get; set; }
        public Guid PersonId { get; set; }
    }
}