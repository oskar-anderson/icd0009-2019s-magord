using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PaymentTypeDTOs
{
    public class PaymentTypeDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}