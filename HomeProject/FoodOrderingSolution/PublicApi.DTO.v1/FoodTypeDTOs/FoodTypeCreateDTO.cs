using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.FoodTypeDTOs
{
    public class FoodTypeCreateDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}