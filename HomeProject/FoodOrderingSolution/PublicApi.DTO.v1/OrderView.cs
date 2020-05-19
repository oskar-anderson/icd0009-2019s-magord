using System;


namespace PublicApi.DTO.v1
{
    public class OrderView
    {
        public Guid Id { get; set; }

        public string OrderStatus { get; set; } = default!;
        public int Number { get; set; } = default!;
        public string TimeCreated { get; set; } = default!;
        public string Restaurant { get; set; } = default!;
        public string OrderType { get; set; } = default!;
        public string Person { get; set; } = default!;

    }
}