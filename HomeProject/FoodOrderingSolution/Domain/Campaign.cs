using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Campaign : Campaign<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Campaign<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)]
        public string? Comment { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
}