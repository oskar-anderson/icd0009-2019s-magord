﻿using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Food : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string? Description { get; set; }

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public float Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
}