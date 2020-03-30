using System;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentCreateDTO
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;

        public DateTime TimeMade { get; set; } = default!;
    }
}