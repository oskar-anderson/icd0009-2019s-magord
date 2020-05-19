using System;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class BillView
    {
        public Guid Id { get; set; } = default!;
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
        
        public int Order { get; set; }

        public string Person { get; set; } = default!;
    }
}