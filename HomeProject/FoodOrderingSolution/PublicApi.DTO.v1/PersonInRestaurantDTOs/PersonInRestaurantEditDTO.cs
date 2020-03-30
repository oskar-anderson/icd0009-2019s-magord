using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PersonInRestaurantDTOs
{
    public class PersonInRestaurantEditDTO
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;
    }
}