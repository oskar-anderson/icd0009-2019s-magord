using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PaymentType : PaymentType<Guid>, IDomainEntityBaseMetadata
    {
    }
    
    public class PaymentType<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}