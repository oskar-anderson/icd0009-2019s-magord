﻿using System;
using Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Drink : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public float Size { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
}