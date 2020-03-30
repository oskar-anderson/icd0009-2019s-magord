using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.IngredientDTOs
{
    public class IngredientEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;
    }
}