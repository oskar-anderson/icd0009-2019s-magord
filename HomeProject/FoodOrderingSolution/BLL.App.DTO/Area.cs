using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Area : Area<Guid>, IDomainEntityId
    {
    }
    
    public class Area<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public ICollection<Restaurant>? Restaurants { get; set; }
        
        public TKey TownId { get; set; } = default!;
        public Town? Town { get; set; }
    }
}