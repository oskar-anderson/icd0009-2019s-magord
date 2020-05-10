using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Area : DomainEntityIdMetadata
    {
        // If can be null then -> public string? Name { get; set; }
        // If cannot be null then -> public string Name { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Restaurant>? Restaurants { get; set; }
        
        public Guid TownId { get; set; } = default!;
        public Town? Town { get; set; }
    }
    
}