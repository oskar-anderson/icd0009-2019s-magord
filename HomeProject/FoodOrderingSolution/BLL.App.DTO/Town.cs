﻿using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Town : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        
        public Guid AppUserId { get; set; } 
        public AppUser? AppUser { get; set; }
        
    }
}