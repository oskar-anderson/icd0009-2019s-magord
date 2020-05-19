using System;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PaymentView
    {
        public Guid Id { get; set; } = default!;
        
        public int Amount { get; set; }

        public string TimeMade { get; set; } = default!;
        public string Person { get; set; } = default!;
        
        public int Bill { get; set; }

        public string PaymentType { get; set; } = default!;
    }
}