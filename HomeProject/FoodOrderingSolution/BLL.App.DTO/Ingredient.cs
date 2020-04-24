using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Ingredient : Ingredient<Guid>, IDomainEntityId
    {
    }
    
    public class Ingredient<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public TKey FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}