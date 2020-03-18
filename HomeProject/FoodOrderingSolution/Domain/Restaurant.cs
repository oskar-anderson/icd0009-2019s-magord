using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Restaurant : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;

        public DateTime OpenedFrom { get; set; } = default!;

        public DateTime ClosedFrom { get; set; } = default!;

        [MaxLength(36)] public string AreaId { get; set; } = default!;
        public Area? Area { get; set; }

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}