using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Payment : DomainEntityIdMetadataUser<AppUser>
    {
        public int Amount { get; set; } = default!;

        public string TimeMade { get; set; } = default!;
        
        public Guid BillId { get; set; } = default!;
        public Bill? Bill { get; set; }
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
    }
    
}