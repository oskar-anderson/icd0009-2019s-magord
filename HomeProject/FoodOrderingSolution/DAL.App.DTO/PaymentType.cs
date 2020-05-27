using System;
using Contracts.Domain.Base;


namespace DAL.App.DTO
{
    public class PaymentType : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}