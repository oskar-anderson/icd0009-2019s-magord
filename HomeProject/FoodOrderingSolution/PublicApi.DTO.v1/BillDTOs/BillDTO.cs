using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class BillDTO
    {
        public Guid Id { get; set; }
        
        public DateTime TimeIssued { get; set; } = default!;
        
        public int Number { get; set; } = default!;

        public decimal Sum { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;

        public Guid? AppUserId { get; set; }

        public Guid PersonId { get; set; } = default!;
    }
}