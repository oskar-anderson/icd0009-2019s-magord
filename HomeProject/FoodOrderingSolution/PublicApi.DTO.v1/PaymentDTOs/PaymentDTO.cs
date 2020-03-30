using System;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;

        public string TimeMade { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        
        public Guid BillId { get; set; } = default!;
        
        public Guid PaymentTypeId { get; set; } = default!;
        
    }
}