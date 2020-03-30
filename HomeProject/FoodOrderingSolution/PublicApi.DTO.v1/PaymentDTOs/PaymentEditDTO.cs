using System;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentEditDTO
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;
    }
}