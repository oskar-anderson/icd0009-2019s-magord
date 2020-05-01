using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Bill : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Guid PersonId { get; set; } = default!;
        public Guid AppUserId { get; set; }
    }

}