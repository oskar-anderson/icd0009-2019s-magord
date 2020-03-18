using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Payment : DomainEntity
    {
        public int Amount { get; set; } = default!;

        public DateTime TimeMade { get; set; } = default!;

        [MaxLength(36)] public string PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        [MaxLength(36)] public string BillId { get; set; } = default!;
        public Bill? Bill { get; set; }
        
        [MaxLength(36)] public string PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
        
    }
}