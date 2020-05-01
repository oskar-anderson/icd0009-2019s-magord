﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PaymentType : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}