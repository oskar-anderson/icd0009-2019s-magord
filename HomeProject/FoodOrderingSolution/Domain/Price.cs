using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Price : Price<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Price<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;

        public TKey IngredientId { get; set; } = default!;
        public Ingredient? Ingredient { get; set; }

        public TKey FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        public TKey DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public TKey CampaignId { get; set; } = default!;
        public Campaign? Campaign { get; set; }
        
    }
}