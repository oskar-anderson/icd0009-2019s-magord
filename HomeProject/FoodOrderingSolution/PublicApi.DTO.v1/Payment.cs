using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Payment : PaymentEdit
    {
        public ICollection<Person> Persons { get; set; } = default!;
        public ICollection<Bill> Bills { get; set; } = default!;
        public ICollection<PaymentType> PaymentTypes { get; set; } = default!;
    }

    // for display only
    public class PaymentDetail : PaymentEdit
    {
        public Person Person { get; set; } = default!;
        public Bill Bill { get; set; } = default!;
        public PaymentType PaymentType { get; set; } = default!;
    }

    // from client to server
    public class PaymentEdit: PaymentCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class PaymentCreate
    {
        public int Amount { get; set; } = default!;
        public string TimeMade { get; set; } = default!;
        public Guid PersonId { get; set; }
        public Guid BillId { get; set; }
        public Guid PaymentTypeId { get; set; }
    }
}