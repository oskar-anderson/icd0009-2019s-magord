using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PersonInRestaurantDTOs
{
    public class PersonInRestaurantDTO
    {

        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        
        public Guid RestaurantId { get; set; } = default!;
        
    }
}