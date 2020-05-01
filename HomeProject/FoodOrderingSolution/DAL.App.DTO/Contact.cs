﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Contact : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public Guid ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }
    }
}