using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class FoodType : FoodTypeEdit
    {
        
    }

    // for display only
    public class FoodTypeDetail : FoodTypeEdit
    {
        
    }

    // from client to server
    public class FoodTypeEdit: FoodTypeCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class FoodTypeCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}