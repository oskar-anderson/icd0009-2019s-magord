using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Bill : BillEdit
    {
        public ICollection<Order> Orders { get; set; } = default!;
        public ICollection<Person> Persons { get; set; } = default!;
    }

    // for display only
    public class BillDetail : BillEdit
    {
        public Order Order { get; set; } = default!;
        public Person Person { get; set; } = default!;
    }

    // from client to server
    public class BillEdit: BillCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class BillCreate
    {
        public string TimeIssued { get; set; } = default!;
        public int Number { get; set; } = default!;
        public decimal Sum { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Guid PersonId { get; set; } = default!;
    }
}