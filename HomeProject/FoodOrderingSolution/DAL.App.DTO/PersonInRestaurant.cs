using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class PersonInRestaurant : PersonInRestaurant<Guid>, IDomainEntityId
    {
    }
    
    public class PersonInRestaurant<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public string Role { get; set; } = default!;

        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}