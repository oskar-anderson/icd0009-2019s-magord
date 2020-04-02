using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.DrinkDTOs;
using PublicApi.DTO.v1.FoodDTOs;
using PublicApi.DTO.v1.IngredientDTOs;
using PublicApi.DTO.v1.OrderTypeDTOs;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace PublicApi.DTO.v1.OrderDTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public string TimeCreated { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public FoodDTO Food { get; set; } = default!;
        
        public Guid IngredientId { get; set; } = default!;
        public IngredientDTO Ingredient { get; set; } = default!;
        
        public Guid DrinkId { get; set; } = default!;
        public DrinkDTO Drink { get; set; } = default!;
        
        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO Restaurant { get; set; } = default!;
        
        public Guid OrderTypeId { get; set; } = default!;
        public OrderTypeDTO OrderType { get; set; } = default!;
        
        public Guid PersonId { get; set; } = default!;
        public PersonDTO Person { get; set; } = default!;

    }
}