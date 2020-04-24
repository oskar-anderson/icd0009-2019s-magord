using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
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