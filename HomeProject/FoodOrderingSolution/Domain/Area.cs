using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Area
    {
        public int AreaId { get; set; }
        
        // If can be null then -> public string? Name { get; set; }
        // If cannot be null then -> public string Name { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Restaurant>? Restaurants { get; set; }

        public int TownId { get; set; }
        public Town? Town { get; set; }
    }
}