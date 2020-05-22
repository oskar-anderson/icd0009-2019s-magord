using System;

namespace PublicApi.DTO.v1
{
    public class OrderItemView
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; } = default!;
        
        public string? Food { get; set; }

        public string? Ingredient { get; set; }

        public string? Drink { get; set; }
        
        public int Order { get; set; } = default!;
        
        public decimal FoodPrice { get; set; }
        public decimal DrinkPrice { get; set; }
        public decimal IngredientPrice { get; set; }

        public string OrderType { get; set; } = default!;
    }
}