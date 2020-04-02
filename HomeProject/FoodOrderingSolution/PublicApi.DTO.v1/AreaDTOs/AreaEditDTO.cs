﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.AreaDTOs
{
    public class AreaEditDTO
    {
        public Guid Id { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}