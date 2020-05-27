﻿using System;
using Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class FoodType : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
    }
}