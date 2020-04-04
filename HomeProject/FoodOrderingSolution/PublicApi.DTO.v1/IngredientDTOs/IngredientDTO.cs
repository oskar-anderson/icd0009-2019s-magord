﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.FoodDTOs;

namespace PublicApi.DTO.v1.IngredientDTOs
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public FoodDTO Food { get; set; } = default!;
    }
}