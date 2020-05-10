using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Area : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<Restaurant>? Restaurants { get; set; }
        public Guid TownId { get; set; } = default!;
        public Town? Town { get; set; }
    }
}