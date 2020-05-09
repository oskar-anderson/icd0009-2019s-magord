﻿using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Area : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public Guid TownId { get; set; }
        public Guid AppUserId { get; set; }
    }
}