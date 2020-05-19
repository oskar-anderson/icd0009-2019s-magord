using System;

namespace PublicApi.DTO.v1
{
    public class PaymentView
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; } = default!;
        public string TimeMade { get; set; } = default!;
        public string Person { get; set; } = default!;
        public int Bill { get; set; }
        public string PaymentType { get; set; } = default!;
    }
}