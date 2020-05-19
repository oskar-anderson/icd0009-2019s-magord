using System;

namespace DAL.App.DTO
{
    public class OrderItemView
    {
        public Guid Id { get; set; } = default!;
     
        public int Quantity { get; set; } = default!;
        
        public string? Food { get; set; }

        public string? Ingredient { get; set; }

        public string? Drink { get; set; }
        
        public int Order { get; set; } = default!;
    }
}