using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Town : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<Area>? Areas { get; set; }
        public int AreaCount { get; set; }

        public Guid AppUserId { get; set; } 
        public AppUser? AppUser { get; set; }
        
    } 
}