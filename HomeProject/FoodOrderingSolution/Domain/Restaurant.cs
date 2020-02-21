using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;

        public DateTime OpenedFrom { get; set; }
        
        public DateTime ClosedFrom { get; set; }

        public int AreaId { get; set; }
        public Area? Area { get; set; }
        
        
    }
}