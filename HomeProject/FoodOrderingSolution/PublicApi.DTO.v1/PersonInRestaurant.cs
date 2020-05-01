using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class PersonInRestaurant : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;
        
        public string To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;
        
        public Guid PersonId { get; set; }
        
        public Guid RestaurantId { get; set; }
    }
}