using System;

namespace PublicApi.DTO.v1
{
    public class BillEditDTO
    {
        public Guid Id { get; set; }
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
    }
}