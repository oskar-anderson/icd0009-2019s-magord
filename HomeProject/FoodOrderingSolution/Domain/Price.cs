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

        [MaxLength(36)] public string IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        [MaxLength(36)] public string FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        [MaxLength(36)] public string DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        [MaxLength(36)] public string OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        [MaxLength(36)] public string? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        
    }
}