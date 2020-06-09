using System;

namespace DAL.App.DTO
{
    public class QuestionView
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Description { get; set; } = default!;

        public decimal? Points { get; set; } = default!;

        public string Quiz { get; set; } = default!;
    }
}