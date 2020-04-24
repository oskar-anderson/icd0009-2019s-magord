using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Drink : DrinkEdit
    {
        
    }

    // for display only
    public class DrinkDetail : DrinkEdit
    {
        
    }

    // from client to server
    public class DrinkEdit: DrinkCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class DrinkCreate
    {
        public float Size { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
    }
}