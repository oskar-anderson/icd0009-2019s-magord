using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Restaurant : Restaurant<Guid>, IDomainEntityId
    {
    }
    
    public class Restaurant<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public TKey AreaId { get; set; } = default!;
        public Area? Area { get; set; }

        public ICollection<PersonInRestaurant>? PersonInRestaurants { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}