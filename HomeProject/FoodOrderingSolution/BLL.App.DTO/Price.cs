using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Price : Price<Guid>, IDomainEntityId
    {
    }
    
    public class Price<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
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