using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Ingredient : IngredientEdit
    {
        public ICollection<Food> Foods { get; set; } = default!;
    }

    // for display only
    public class IngredientDetail : IngredientEdit
    {
        public Food Food { get; set; } = default!;
    }

    // from client to server
    public class IngredientEdit: IngredientCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class IngredientCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public Guid FoodId { get; set; } = default!;
    }
}