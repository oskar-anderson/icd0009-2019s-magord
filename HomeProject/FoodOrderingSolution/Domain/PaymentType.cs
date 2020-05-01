using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PaymentType : DomainEntityIdMetadata
    {
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}