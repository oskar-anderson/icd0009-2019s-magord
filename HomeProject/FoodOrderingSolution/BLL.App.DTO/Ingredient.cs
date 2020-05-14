﻿using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Ingredient : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
}