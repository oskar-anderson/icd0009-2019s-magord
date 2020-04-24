using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PaymentType : PaymentType<Guid>, IDomainEntityId
    {
    }
    
    public class PaymentType<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}