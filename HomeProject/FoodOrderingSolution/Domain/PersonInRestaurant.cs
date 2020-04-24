using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PersonInRestaurant : PersonInRestaurant<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class PersonInRestaurant<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;

        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}