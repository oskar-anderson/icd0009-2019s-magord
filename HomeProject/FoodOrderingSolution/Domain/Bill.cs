﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Bill : DomainEntityIdMetadataUser<AppUser>
    {
        public string TimeIssued { get; set; } = default!;
        
        public int Number { get; set; } = default!;

        public decimal Sum { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        
    }

}