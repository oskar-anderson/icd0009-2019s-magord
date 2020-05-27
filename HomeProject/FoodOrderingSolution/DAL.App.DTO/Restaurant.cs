﻿using System;
using Contracts.Domain.Base;


namespace DAL.App.DTO
{
    public class Restaurant : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public Guid AreaId { get; set; } = default!;
        public Area? Area { get; set; }
    }
}