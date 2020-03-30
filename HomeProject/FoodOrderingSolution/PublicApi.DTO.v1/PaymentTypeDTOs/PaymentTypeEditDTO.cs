using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PaymentTypeDTOs
{
    public class PaymentTypeEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}