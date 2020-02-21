using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Town
    {
        public int TownId { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Area>? Areas { get; set; }
    }
}