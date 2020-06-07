using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Question : DomainEntityIdMetadata
    {
        public int Number { get; set; }
        
        public string Description { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Points { get; set; } = default!;

        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        public ICollection<Choice>? Choices { get; set; }


    }
}