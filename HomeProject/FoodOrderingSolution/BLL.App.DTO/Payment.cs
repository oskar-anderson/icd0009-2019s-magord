using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public int Amount { get; set; }

        public string TimeMade { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public Guid BillId { get; set; } = default!;
        public Bill? Bill { get; set; }
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
    }
    
}