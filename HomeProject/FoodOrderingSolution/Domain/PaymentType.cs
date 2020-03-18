using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PaymentType : DomainEntity
    {
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}