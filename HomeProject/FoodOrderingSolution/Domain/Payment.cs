using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Payment : DomainEntity
    {
        public int Amount { get; set; } = default!;

        public DateTime TimeMade { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public Guid BillId { get; set; } = default!;
        public Bill? Bill { get; set; }
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
        
    }
}