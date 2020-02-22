using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Campaign : DomainEntityMetadata
    {
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)]
        public string? Comment { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
}