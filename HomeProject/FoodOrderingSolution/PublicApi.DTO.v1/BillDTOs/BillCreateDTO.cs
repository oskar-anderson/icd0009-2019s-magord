using System;

namespace PublicApi.DTO.v1
{
    public class BillCreateDTO
    {
        public Guid Id { get; set; }
        public DateTime TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
    }
}