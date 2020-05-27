using System;
using Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Town : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}