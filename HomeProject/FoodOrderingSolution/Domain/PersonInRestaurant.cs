using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PersonInRestaurant : DomainEntity
    {
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}