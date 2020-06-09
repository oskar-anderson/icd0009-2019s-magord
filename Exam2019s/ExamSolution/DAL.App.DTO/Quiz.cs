using System;
using DAL.App.DTO.Identity;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace DAL.App.DTO
{
    public class Quiz : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        
        public decimal? TotalPoints { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
    }
}