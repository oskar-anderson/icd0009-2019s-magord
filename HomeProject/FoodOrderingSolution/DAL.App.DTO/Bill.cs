using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Bill : Bill<Guid>, IDomainEntityId
    {
    }
    
    public class Bill<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string TimeIssued { get; set; } = default!;
        
        public int Number { get; set; } = default!;

        public decimal Sum { get; set; } = default!;
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Payment>? Payments { get; set; }
    }
}