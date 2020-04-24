using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Payment : Payment<Guid>, IDomainEntityId
    {
    }
    
    public class Payment<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
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