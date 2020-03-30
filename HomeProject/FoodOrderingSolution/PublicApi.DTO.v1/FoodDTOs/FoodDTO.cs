using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.FoodDTOs
{
    public class FoodDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public int Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
    }
}