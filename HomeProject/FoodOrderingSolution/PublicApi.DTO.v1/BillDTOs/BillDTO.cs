using System;
using System.Collections.Generic;
using PublicApi.DTO.v1.OrderDTOs;

namespace PublicApi.DTO.v1
{
    public class BillDTO
    {
        public Guid Id { get; set; }
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Guid PersonId { get; set; } = default!;

        public OrderDTO Order { get; set; } = default!;

        public PersonDTO Person { get; set; } = default!;
    }
}