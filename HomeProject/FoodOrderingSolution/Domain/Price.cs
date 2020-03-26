using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Price : DomainEntity
    {
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;

        public Guid IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        
    }
}