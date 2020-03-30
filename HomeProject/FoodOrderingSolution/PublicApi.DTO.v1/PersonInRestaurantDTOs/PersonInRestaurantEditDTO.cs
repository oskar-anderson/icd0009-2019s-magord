using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PersonInRestaurantDTOs
{
    public class PersonInRestaurantEditDTO
    {
        public Guid Id { get; set; }
        
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;
    }
}