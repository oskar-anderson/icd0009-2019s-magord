using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Payment : Payment<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class Payment<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public int Amount { get; set; } = default!;

        public string TimeMade { get; set; } = default!;

        public TKey PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        public TKey BillId { get; set; } = default!;
        public Bill? Bill { get; set; }
        
        public TKey PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
        
    }
}