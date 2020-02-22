using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Drink : DomainEntityMetadata
    {
        public int Size { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public ICollection<Price>? Prices { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}