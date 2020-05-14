﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Area : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Guid TownId { get; set; } = default!;
        public Town? Town { get; set; }
    }
}