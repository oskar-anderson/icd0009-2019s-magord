using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class PersonInRestaurant : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public string Role { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public Guid AppUserId { get; set; } 
        public AppUser? AppUser { get; set; }
    }
    
}