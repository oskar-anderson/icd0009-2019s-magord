using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;
        public string TimeMade { get; set; } = default!;
        public Guid PersonId { get; set; }
        public Guid BillId { get; set; }
        public Guid PaymentTypeId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}