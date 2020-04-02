using System;
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;

        public string TimeMade { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public PersonDTO Person { get; set; } = default!;
        
        public Guid BillId { get; set; } = default!;
        public BillDTO Bill { get; set; } = default!;
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentTypeDTO PaymentType { get; set; } = default!;
        
    }
}