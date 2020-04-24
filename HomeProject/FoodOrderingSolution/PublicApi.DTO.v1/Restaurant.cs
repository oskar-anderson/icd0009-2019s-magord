using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Restaurant : RestaurantEdit
    {
        public ICollection<Area> Areas { get; set; } = default!;
    }

    public class RestaurantDetail : RestaurantEdit
    {
        public Area Area { get; set; } = default!;
    }
    
    public class RestaurantCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;
        
        public Guid AreaId { get; set; } = default!;
    }
    
    public class RestaurantEdit : RestaurantCreate
    {
        public Guid Id { get; set; }
    }
}