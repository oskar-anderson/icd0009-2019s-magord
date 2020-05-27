using System;
using Contracts.Domain.Base;


namespace DAL.App.DTO
{
    public class Town : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    } 
}