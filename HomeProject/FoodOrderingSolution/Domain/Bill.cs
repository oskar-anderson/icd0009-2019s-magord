using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Bill : DomainEntityMetadata
    {
        public DateTime TimeIssued { get; set; } = default!;
        
        public int Number { get; set; } = default!;

        public decimal Sum { get; set; } = default!;
        
        [MaxLength(36)] public string OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        [MaxLength(36)] public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)] public string PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Payment>? Payments { get; set; }
    }
}