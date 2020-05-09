using System;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;
        
        public Guid IngredientId { get; set; }
        public Guid FoodId { get; set; }
        public Guid DrinkId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CampaignId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}