using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.OrderDTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public DateTime TimeCreated { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        
        public Guid IngredientId { get; set; } = default!;
        
        public Guid DrinkId { get; set; } = default!;
        
        public Guid RestaurantId { get; set; } = default!;
        
        public Guid OrderTypeId { get; set; } = default!;
        
        public Guid? AppUserId { get; set; }
        public Guid PersonId { get; set; } = default!;

    }
}