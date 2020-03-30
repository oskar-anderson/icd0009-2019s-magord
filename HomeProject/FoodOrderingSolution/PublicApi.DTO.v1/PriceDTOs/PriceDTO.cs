using System;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceDTO
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;

        public Guid IngredientId { get; set; } = default!;
        
        public Guid FoodId { get; set; } = default!;
        
        public Guid DrinkId { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        
        public Guid? CampaignId { get; set; }
    }
}