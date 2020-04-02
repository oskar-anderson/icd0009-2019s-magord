using System;
using PublicApi.DTO.v1.CampaignDTOs;
using PublicApi.DTO.v1.DrinkDTOs;
using PublicApi.DTO.v1.FoodDTOs;
using PublicApi.DTO.v1.IngredientDTOs;
using PublicApi.DTO.v1.OrderDTOs;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceDTO
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;

        public Guid IngredientId { get; set; } = default!;
        public IngredientDTO Ingredient { get; set; } = default!;
        
        public Guid FoodId { get; set; } = default!;
        public FoodDTO Food { get; set; } = default!;
        
        public Guid DrinkId { get; set; } = default!;
        public DrinkDTO Drink { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        public OrderDTO Order { get; set; } = default!;
        
        public Guid? CampaignId { get; set; }
        public CampaignDTO Campaign { get; set; } = default!;
    }
}