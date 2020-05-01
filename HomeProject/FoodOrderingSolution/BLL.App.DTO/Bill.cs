using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Bill : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}