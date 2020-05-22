using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; }

        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
    }
}