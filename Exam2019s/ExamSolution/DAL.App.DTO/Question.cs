using System;
using System.Collections.Generic;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace DAL.App.DTO
{
    public class Question : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Description { get; set; } = default!;

        public decimal? Points { get; set; } = default!;

        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
    }
}