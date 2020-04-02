using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.DrinkDTOs
{
    public class DrinkEditDTO
    {
        public Guid Id { get; set; }
        
        public float Size { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;
    }
}