using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Restaurant : Town<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Town<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public TKey AreaId { get; set; } = default!;
        public Area? Area { get; set; }

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}