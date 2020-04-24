using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Food : FoodEdit
    {
        public ICollection<FoodType> FoodTypes { get; set; } = default!;
    }

    // for display only
    public class FoodDetail : FoodEdit
    {
        public FoodType FoodType { get; set; } = default!;
    }

    // from client to server
    public class FoodEdit: FoodCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class FoodCreate
    {
        [MaxLength(1024)] public string? Description { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public float Size { get; set; } = default!;
        public Guid FoodTypeId { get; set; } = default!;
    }
}